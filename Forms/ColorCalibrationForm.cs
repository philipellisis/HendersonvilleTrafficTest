using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HendersonvilleTrafficTest.Configuration;
using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;
using HendersonvilleTrafficTest.Shared;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class ColorCalibrationForm : Form
    {
        private  IAcPowerSupply _acPowerSupply;
        private  IDcPowerSupply _dcPowerSupply;
        private  ISpectrometer _spectrometer;
        private  ITemperatureSensor _temperatureSensor;
        private bool _testInProgress = false;

        public ColorCalibrationForm()
        {
            InitializeComponent();
            InitializeEquipment();
            cmbColorSample.SelectedIndex = 0;
        }

        private void InitializeEquipment()
        {
            var equipmentFactory = new EquipmentFactory();
            _acPowerSupply = equipmentFactory.CreateAcPowerSupply();
            _dcPowerSupply = equipmentFactory.CreateDcPowerSupply();
            _spectrometer = equipmentFactory.CreateSpectrometer();
            _temperatureSensor = equipmentFactory.CreateTemperatureSensor();
        }

        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            if (_testInProgress)
            {
                UpdateStatus("Test already in progress...");
                return;
            }

            if (cmbColorSample.SelectedItem == null)
            {
                UpdateStatus("Please select a color sample.");
                return;
            }

            await RunCalibrationTest();
        }

        private async Task RunCalibrationTest()
        {
            try
            {
                _testInProgress = true;
                btnStartTest.Enabled = false;
                cmbColorSample.Enabled = false;
                progressBar.Value = 0;
                ClearResults();

                var selectedColor = cmbColorSample.SelectedItem.ToString();
                var colorSample = GetColorSampleFromConfig(selectedColor);

                UpdateStatus($"Starting calibration test for {selectedColor} color sample...");
                UpdateProgress(10);

                // Step 1: Initialize equipment
                UpdateStatus("Initializing equipment...");
                await InitializeTestEquipment();
                UpdateProgress(20);

                // Step 2: Power on lamp
                UpdateStatus($"Powering on lamp at {colorSample.TestVoltage}V ({(colorSample.IsAC ? "AC" : "DC")})...");
                await PowerOnLamp(colorSample);
                UpdateProgress(30);

                // Step 3: Wait 3 seconds for stabilization
                UpdateStatus("Waiting for lamp stabilization (3 seconds)...");
                await Task.Delay(3000);
                UpdateProgress(40);

                // Step 4: Take spectrum measurement
                UpdateStatus("Taking spectrum measurement...");
                var integrationTime = await _spectrometer.AutoRangeAsync();
                var spectrumReading = await _spectrometer.GetSpectrumReadingAsync();
                UpdateProgress(50);

                // Step 5: Power off lamp
                UpdateStatus("Powering off lamp...");
                await PowerOffLamp(colorSample);
                UpdateProgress(60);

                // Step 6: Wait for dark current stabilization and take dark current measurement
                var waitTimeSeconds = ConfigurationManager.Current.Equipment.WaitForDarkCurrentSeconds;
                UpdateStatus($"Waiting for dark current stabilization ({waitTimeSeconds:F1} seconds)...");
                await Task.Delay((int)(waitTimeSeconds * 1000));
                UpdateProgress(70);

                UpdateStatus("Taking dark current measurement...");
                var darkCurrentReading = await _spectrometer.GetSpectrumReadingAsync();
                UpdateProgress(75);

                // Step 7: Take temperature measurement
                UpdateStatus("Taking temperature measurement...");
                var temperatureMeasured = await _temperatureSensor.GetTemperatureAsync();
                UpdateProgress(80);

                // Step 8: Process spectrum data
                UpdateStatus("Processing spectrum data...");
                var normalizedSpectrum = MathUtils.NormalizeSpectrumReading(spectrumReading, darkCurrentReading);
                var calibratedSpectrum = MathUtils.ApplyCalibrationFactors(normalizedSpectrum, _spectrometer.CurrentIntegrationTimeMicros);
                UpdateProgress(85);

                // Step 9: Calculate color values
                UpdateStatus("Calculating color values...");
                var colorData = CieColorCalculator.CalculateFromSpectrumData(calibratedSpectrum.Wavelengths, calibratedSpectrum.Intensities );
                var luxMeasured = colorData.Luminance; // LUX value since using cosine receptor
                UpdateProgress(90);

                // Step 10: Calculate ECL calibration value
                UpdateStatus("Calculating ECL calibration value...");
                var eclCalib = CalculateEclCalib(luxMeasured, temperatureMeasured, colorSample);
                UpdateProgress(95);

                // Step 11: Save results to configuration
                UpdateStatus("Saving calibration results...");
                await SaveCalibrationResults(selectedColor, luxMeasured, temperatureMeasured, eclCalib);
                UpdateProgress(100);

                // Display results
                DisplayResults(luxMeasured, temperatureMeasured, eclCalib);
                UpdateStatus($"Calibration test completed successfully for {selectedColor} color sample.");
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error during calibration test: {ex.Message}");
                MessageBox.Show($"Calibration test failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _testInProgress = false;
                btnStartTest.Enabled = true;
                cmbColorSample.Enabled = true;
            }
        }

        private ColorSample GetColorSampleFromConfig(string colorName)
        {
            var towerSettings = ConfigurationManager.Current.Tower;
            return colorName.ToLower() switch
            {
                "blue" => towerSettings.BlueColorSample,
                "green" => towerSettings.GreenColorSample,
                "yellow" => towerSettings.YellowColorSample,
                "orange" => towerSettings.OrangeColorSample,
                "red" => towerSettings.RedColorSample,
                "white" => towerSettings.WhiteColorSample,
                _ => throw new ArgumentException($"Unknown color sample: {colorName}")
            };
        }

        private async Task InitializeTestEquipment()
        {
            await _spectrometer.InitializeAsync();
            await _temperatureSensor.InitializeAsync();
            
            if (GetColorSampleFromConfig(cmbColorSample.SelectedItem.ToString()).IsAC)
            {
                await _acPowerSupply.InitializeAsync();
            }
            else
            {
                await _dcPowerSupply.InitializeAsync();
            }
        }

        private async Task PowerOnLamp(ColorSample colorSample)
        {
            if (colorSample.IsAC)
            {
                UpdateStatus("Configuring system for AC mode...");
                await EquipmentHelpers.SetAC();
                
                UpdateStatus($"Setting AC voltage to {colorSample.TestVoltage}V...");
                await _acPowerSupply.SetVoltsAsync(colorSample.TestVoltage);
                await _acPowerSupply.PowerOnAsync();
            }
            else
            {
                UpdateStatus("Configuring system for DC mode...");
                await EquipmentHelpers.SetDC();
                
                UpdateStatus($"Setting DC voltage to {colorSample.TestVoltage}V...");
                await _dcPowerSupply.SetVoltsAsync(colorSample.TestVoltage);
                await _dcPowerSupply.PowerOnAsync();
            }
        }

        private async Task PowerOffLamp(ColorSample colorSample)
        {
            if (colorSample.IsAC)
            {
                await _acPowerSupply.PowerOffAsync();
            }
            else
            {
                await _dcPowerSupply.PowerOffAsync();
            }
        }

        private double CalculateEclCalib(double luxMeasured, double temperatureMeasured, ColorSample colorSample)
        {
            // EclCalib = (LUX_Measured + [(Temperature_Measured - Temperature_Initial) * A_Color + B_Color]) / LUX_Initial
            var temperatureDifference = temperatureMeasured - colorSample.Temperature_Initial;
            var temperatureAdjustment = (temperatureDifference * colorSample.A_Color) + colorSample.B_Color;
            var adjustedLux = luxMeasured + temperatureAdjustment;
            return adjustedLux / colorSample.LUX_Initial;
        }

        private async Task SaveCalibrationResults(string colorName, double luxMeasured, double temperatureMeasured, double eclCalib)
        {
            var colorSample = GetColorSampleFromConfig(colorName);
            colorSample.LUX_Current = luxMeasured;
            colorSample.Temperature_Current = temperatureMeasured;
            colorSample.EclCalib = eclCalib;

            // TODO: Save configuration
            ConfigurationManager.SaveConfiguration();
        }

        private void DisplayResults(double luxMeasured, double temperatureMeasured, double eclCalib)
        {
            txtLuxMeasured.Text = luxMeasured.ToString("F2");
            txtTempMeasured.Text = temperatureMeasured.ToString("F1");
            txtEclCalib.Text = eclCalib.ToString("F4");
        }

        private void ClearResults()
        {
            txtLuxMeasured.Clear();
            txtTempMeasured.Clear();
            txtEclCalib.Clear();
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatus), message);
                return;
            }

            txtStatus.AppendText($"{DateTime.Now:HH:mm:ss} - {message}\r\n");
            txtStatus.ScrollToCaret();
            Application.DoEvents();
        }

        private void UpdateProgress(int percentage)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgress), percentage);
                return;
            }

            progressBar.Value = Math.Min(100, Math.Max(0, percentage));
            Application.DoEvents();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_testInProgress)
            {
                e.Cancel = true;
                MessageBox.Show("Cannot close form while test is in progress.", "Test In Progress", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            base.OnFormClosing(e);
        }
    }
}