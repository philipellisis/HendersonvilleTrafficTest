using HendersonvilleTrafficTest.Configuration;
using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;
using HendersonvilleTrafficTest.Shared;
using System.ComponentModel;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class SpectrometerCalibrationForm : Form
    {
        private readonly EquipmentFactory _equipmentFactory;
        private IDcPowerSupply? _powerSupply;
        private ISpectrometer? _spectrometer;
        private IPowerAnalyzer? _powerAnalyzer;
        private System.Windows.Forms.Timer? _calibrationTimer;
        private CancellationTokenSource? _cancellationTokenSource;
        
        private int _currentStep = 0;
        private int _elapsedSeconds = 0;
        private bool _isCalibrating = false;
        private SpectrumReading? _lightReading;
        private SpectrumReading? _darkReading;

        private enum CalibrationStep
        {
            Initializing = 0,
            RampingUp = 1,
            WarmingUp = 2,
            AdjustingCurrent = 3,
            TakingLightReading = 4,
            TurningOff = 5,
            WaitingForDark = 6,
            TakingDarkReading = 7,
            CalculatingFactors = 8,
            Complete = 9
        }

        public SpectrometerCalibrationForm()
        {
            InitializeComponent();
            _equipmentFactory = new EquipmentFactory();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _calibrationTimer = new System.Windows.Forms.Timer();
            _calibrationTimer.Interval = 1000; // 1 second
            _calibrationTimer.Tick += CalibrationTimer_Tick;
        }

        private async void btnStartCalibration_Click(object sender, EventArgs e)
        {
            if (_isCalibrating) return;

            try
            {
                await StartCalibrationProcess();
            }
            catch (Exception ex)
            {
                LogMessage($"Error during calibration: {ex.Message}");
                ResetCalibrationState();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_isCalibrating)
            {
                CancelCalibration();
            }
            else
            {
                this.Close();
            }
        }

        private async Task StartCalibrationProcess()
        {
            _isCalibrating = true;
            _currentStep = 0;
            _elapsedSeconds = 0;
            _cancellationTokenSource = new CancellationTokenSource();

            btnStartCalibration.Enabled = false;
            btnCancel.Enabled = true;
            btnCancel.Text = "Cancel";
            txtLog.Clear();

            try
            {
                LogMessage("Starting spectrometer calibration process...");
                
                // Initialize equipment
                await InitializeEquipment();
                
                // Start the calibration timer
                _calibrationTimer?.Start();
                
                // Run calibration steps
                await RunCalibrationSteps(_cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                LogMessage("Calibration cancelled by user.");
            }
            catch (Exception ex)
            {
                LogMessage($"Calibration failed: {ex.Message}");
            }
            finally
            {
                await CleanupEquipment();
                ResetCalibrationState();
            }
        }

        private async Task InitializeEquipment()
        {
            UpdateStatus("Initializing equipment...");
            
            _powerSupply = _equipmentFactory.CreateDcPowerSupply();
            _spectrometer = _equipmentFactory.CreateSpectrometer();
            _powerAnalyzer= _equipmentFactory.CreatePowerAnalyzer();
            
            await _powerSupply.InitializeAsync();
            await _spectrometer.InitializeAsync();
            await _powerAnalyzer.InitializeAsync();
            await _powerAnalyzer.SetModeAsync(PowerMode.DC);
            
            LogMessage("Equipment initialized successfully.");
        }

        private async Task RunCalibrationSteps(CancellationToken cancellationToken)
        {
            var config = ConfigurationManager.Current.Calibration;
            
            // Step 1: Ramp up voltage
            _currentStep = (int)CalibrationStep.RampingUp;
            UpdateStatus("Ramping up voltage...");
            await RampVoltage(0, config.CalibrationLampVoltage, config.CalibrationRampTimeSeconds, cancellationToken);
            
            // Step 2: Warm up period
            _currentStep = (int)CalibrationStep.WarmingUp;
            UpdateStatus("Warming up lamp...");
            await WaitWithTimer(config.CalibrationWarmupTimeSeconds, cancellationToken);
            
            // Step 3: Adjust current to target
            _currentStep = (int)CalibrationStep.AdjustingCurrent;
            UpdateStatus("Adjusting current to target...");
            await AdjustCurrentToTarget(cancellationToken);
            
            // Step 4: Take light reading
            _currentStep = (int)CalibrationStep.TakingLightReading;
            UpdateStatus("Taking spectrum reading...");
            _lightReading = await _spectrometer!.GetSpectrumReadingAsync();
            LogMessage("Light spectrum reading completed.");
            
            // Step 5: Turn off lamp
            _currentStep = (int)CalibrationStep.TurningOff;
            UpdateStatus("Turning off lamp...");
            await _powerSupply!.SetVoltsAsync(0);
            await _powerSupply.PowerOffAsync();
            
            // Step 6: Wait for lamp to cool down
            _currentStep = (int)CalibrationStep.WaitingForDark;
            var cooldownSeconds = ConfigurationManager.Current.Calibration.CalibrationLampCooldownSeconds;
            UpdateStatus($"Waiting for lamp to cool down ({cooldownSeconds} seconds)...");
            await WaitWithTimer(cooldownSeconds, cancellationToken);
            
            // Step 7: Take dark reading
            _currentStep = (int)CalibrationStep.TakingDarkReading;
            UpdateStatus("Taking dark current reading...");
            _darkReading = await _spectrometer!.GetSpectrumReadingAsync();
            LogMessage("Dark current reading completed.");
            
            // Step 8: Calculate calibration factors
            _currentStep = (int)CalibrationStep.CalculatingFactors;
            UpdateStatus("Calculating calibration factors...");
            CalculateCalibrationFactors();
            
            // Step 9: Complete
            _currentStep = (int)CalibrationStep.Complete;
            UpdateStatus("Calibration completed successfully!");
            LogMessage("Calibration factors have been saved to configuration.");
        }

        private async Task RampVoltage(double startVoltage, double endVoltage, int rampTimeSeconds, CancellationToken cancellationToken)
        {
            await _powerSupply!.SetVoltsAsync(startVoltage);
            await _powerSupply.PowerOnAsync();
            
            var steps = rampTimeSeconds * 10; // 10 steps per second
            var voltageStep = (endVoltage - startVoltage) / steps;
            
            for (int i = 0; i <= steps; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                var currentVoltage = startVoltage + (voltageStep * i);
                await _powerSupply.SetVoltsAsync(currentVoltage);
                
                UpdateVoltageDisplay(currentVoltage);
                await Task.Delay(100, cancellationToken); // 100ms delay
            }
        }

        private async Task AdjustCurrentToTarget(CancellationToken cancellationToken)
        {
            var config = ConfigurationManager.Current.Calibration;
            var targetCurrent = config.CalibrationLampCurrent;
            var tolerance = config.CalibrationCurrentTolerance;
            var maxAttempts = 20;
            var attempt = 0;

            while (attempt < maxAttempts)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                var measurement = await _powerAnalyzer!.GetElectricalsAsync();
                var currentDifference = targetCurrent - measurement.Current;
                
                UpdateCurrentDisplay(measurement.Current);
                
                if (Math.Abs(currentDifference) <= tolerance)
                {
                    LogMessage($"Current adjusted successfully: {measurement.Current:F3} A (target: {targetCurrent:F3} A)");
                    break;
                }
                
                // Adjust voltage based on current difference
                var voltageAdjustment = currentDifference * 0.1; // Simple proportional control
                var currentVoltage = measurement.Current;
                var newVoltage = Math.Max(0, currentVoltage + voltageAdjustment);
                
                await _powerSupply.SetVoltsAsync(newVoltage);
                UpdateVoltageDisplay(newVoltage);
                
                await Task.Delay(500, cancellationToken);
                attempt++;
            }
            
            if (attempt >= maxAttempts)
            {
                throw new InvalidOperationException($"Could not adjust current to target within tolerance after {maxAttempts} attempts.");
            }
        }

        private async Task WaitWithTimer(int seconds, CancellationToken cancellationToken)
        {
            for (int i = 0; i < seconds; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                UpdateTimerDisplay(seconds - i);
                await Task.Delay(1000, cancellationToken);
            }
        }

        private void CalculateCalibrationFactors()
        {
            if (_lightReading == null || _darkReading == null)
            {
                throw new InvalidOperationException("Light and dark readings are required for calibration.");
            }

            // Normalize readings
            var normalizedLight = MathUtils.NormalizeSpectrumReading(_lightReading, _darkReading);
            var standardSpectrum = ConfigurationManager.Current.Calibration.StandardLampSpectrum;
            
            // Calculate calibration factors: factor = standard / measured
            var calibrationFactors = new double[401];
            for (int i = 0; i < 401; i++)
            {
                if (normalizedLight.Intensities[i] > 0)
                {
                    calibrationFactors[i] = standardSpectrum[i] / normalizedLight.Intensities[i];
                }
                else
                {
                    calibrationFactors[i] = 1.0; // Default if no signal
                }
            }
            
            // Save to configuration
            ConfigurationManager.Current.Calibration.SpectrometerCalibrationFactors = calibrationFactors;
            ConfigurationManager.SaveConfiguration();
            
            LogMessage("Calibration factors calculated and saved successfully.");
        }

        private void CalibrationTimer_Tick(object sender, EventArgs e)
        {
            if (_isCalibrating)
            {
                _elapsedSeconds++;
                UpdateProgressBar();
            }
        }

        private void UpdateProgressBar()
        {
            var config = ConfigurationManager.Current.Calibration;
            var totalTime = config.CalibrationRampTimeSeconds + config.CalibrationWarmupTimeSeconds + 20; // Estimate
            
            var progress = Math.Min(100, (_elapsedSeconds * 100) / totalTime);
            progressBar.Value = progress;
        }

        private void UpdateStatus(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatus), status);
                return;
            }
            
            lblStatus.Text = status;
        }

        private void UpdateVoltageDisplay(double voltage)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<double>(UpdateVoltageDisplay), voltage);
                return;
            }
            
            lblVoltage.Text = $"Voltage: {voltage:F1} V";
        }

        private void UpdateCurrentDisplay(double current)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<double>(UpdateCurrentDisplay), current);
                return;
            }
            
            lblCurrent.Text = $"Current: {current:F3} A";
        }

        private void UpdateTimerDisplay(int seconds)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateTimerDisplay), seconds);
                return;
            }
            
            lblTimer.Text = $"Timer: {seconds} s";
        }

        private void LogMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LogMessage), message);
                return;
            }
            
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            txtLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
            txtLog.ScrollToCaret();
        }

        private void CancelCalibration()
        {
            _cancellationTokenSource?.Cancel();
            LogMessage("Cancellation requested...");
        }

        private async Task CleanupEquipment()
        {
            try
            {
                if (_powerSupply != null)
                {
                    await _powerSupply.SetVoltsAsync(0);
                    await _powerSupply.PowerOffAsync();
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error during cleanup: {ex.Message}");
            }
        }

        private void ResetCalibrationState()
        {
            _isCalibrating = false;
            _calibrationTimer?.Stop();
            
            btnStartCalibration.Enabled = true;
            btnCancel.Enabled = true;
            btnCancel.Text = "Close";
            
            progressBar.Value = 0;
            UpdateStatus("Ready");
            UpdateVoltageDisplay(0);
            UpdateCurrentDisplay(0);
            UpdateTimerDisplay(0);
            
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isCalibrating)
            {
                var result = MessageBox.Show("Calibration is in progress. Are you sure you want to cancel?", 
                    "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                
                CancelCalibration();
            }
            
            base.OnFormClosing(e);
        }
    }
}