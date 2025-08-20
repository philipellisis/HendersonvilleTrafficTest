using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;
using HendersonvilleTrafficTest.Shared;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class SpectrometerTestForm : Form
    {
        private readonly ISpectrometer _spectrometer;
        private SpectrumReading? _lastReading;

        public SpectrometerTestForm()
        {
            InitializeComponent();
            
            var factory = new EquipmentFactory();
            _spectrometer = factory.CreateSpectrometer();
            
            InitializeSpectrometer();
        }

        private async void InitializeSpectrometer()
        {
            try
            {
                await _spectrometer.InitializeAsync();
                lblConnectionStatus.Text = $"Connected: {_spectrometer.IsConnected}";
                lblConnectionStatus.ForeColor = _spectrometer.IsConnected ? Color.Green : Color.Red;
                
                if (_spectrometer.IsConnected)
                {
                    UpdateConnectionUI(true);
                    lblWavelengthRange.Text = $"Range: {_spectrometer.MinWavelength:F0} - {_spectrometer.MaxWavelength:F0} nm";
                    await CaptureSpectrum();
                }
                else
                {
                    UpdateConnectionUI(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize spectrometer: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblConnectionStatus.Text = "Connection Failed";
                lblConnectionStatus.ForeColor = Color.Red;
                UpdateConnectionUI(false);
            }
        }

        private void UpdateConnectionUI(bool connected)
        {
            btnConnect.Text = connected ? "Disconnect" : "Connect";
            btnConnect.BackColor = connected ? Color.LightCoral : Color.LightGreen;
            
            lblConnectionStatus.Text = connected ? "Connected" : "Disconnected";
            lblConnectionStatus.ForeColor = connected ? Color.Green : Color.Red;
            
            // Enable/disable controls
            grpSpectrumControls.Enabled = connected;
            btnCapture.Enabled = connected;
            btnAutoRange.Enabled = connected;
            chkAutoCapture.Enabled = connected;
            btnSaveSpectrum.Enabled = connected && _lastReading != null;
            
            if (!connected)
            {
                chkAutoCapture.Checked = false;
                ClearSpectrum();
            }
        }

        private void ClearSpectrum()
        {
            _lastReading = null;
            picSpectrum.Invalidate(); // Trigger repaint
            lblPeakWavelength.Text = "Peak: -- nm";
            lblPeakIntensity.Text = "Peak Intensity: --";
            lblTotalIntensity.Text = "Total: --";
            lblCieCoordinates.Text = "CIE: x=-- y=-- u'=-- v'=--";
            txtSpectrumData.Clear();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_spectrometer.IsConnected)
                {
                    // Note: The current interface doesn't have a disconnect method
                    MessageBox.Show("Disconnect functionality not implemented in current interface", 
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                await _spectrometer.InitializeAsync();
                UpdateConnectionUI(_spectrometer.IsConnected);
                
                if (_spectrometer.IsConnected)
                {
                    lblWavelengthRange.Text = $"Range: {_spectrometer.MinWavelength:F0} - {_spectrometer.MaxWavelength:F0} nm";
                    await CaptureSpectrum();
                }
                else
                {
                    MessageBox.Show("Failed to connect to spectrometer", "Connection Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateConnectionUI(false);
            }
        }

        private async void btnCapture_Click(object sender, EventArgs e)
        {
            await CaptureSpectrum();
        }

        private async void btnAutoRange_Click(object sender, EventArgs e)
        {
            try
            {
                await _spectrometer.AutoRangeAsync();
                lblWavelengthRange.Text = $"Range: {_spectrometer.MinWavelength:F0} - {_spectrometer.MaxWavelength:F0} nm";
                MessageBox.Show("Auto-range completed", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Auto-range error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CaptureSpectrum()
        {
            if (!_spectrometer.IsConnected)
            {
                return;
            }

            try
            {
                btnCapture.Enabled = false;
                btnCapture.Text = "Capturing...";
                
                _lastReading = await _spectrometer.GetSpectrumReadingAsync();
                UpdateSpectrumDisplay(_lastReading);
                
                // Enable save button now that we have data
                btnSaveSpectrum.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to capture spectrum: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                ClearSpectrum();
            }
            finally
            {
                btnCapture.Enabled = true;
                btnCapture.Text = "Capture Spectrum";
            }
        }

        private void UpdateSpectrumDisplay(SpectrumReading reading)
        {
            // Trigger a repaint of the spectrum
            picSpectrum.Invalidate();

            // Find peak wavelength and intensity
            if (reading.Intensities.Length > 0)
            {
                int peakIndex = Array.IndexOf(reading.Intensities, reading.Intensities.Max());
                double peakWavelength = reading.Wavelengths[peakIndex];
                double peakIntensity = reading.Intensities[peakIndex];
                double totalIntensity = reading.Intensities.Sum();

                lblPeakWavelength.Text = $"Peak: {peakWavelength:F1} nm";
                lblPeakIntensity.Text = $"Peak Intensity: {peakIntensity:F0}";
                lblTotalIntensity.Text = $"Total: {totalIntensity:F0}";

                // Calculate CIE color coordinates
                try
                {
                    var cieResult = CieColorCalculator.CalculateFromSpectrum(reading);
                    lblCieCoordinates.Text = $"CIE: x={cieResult.CcX:F4} y={cieResult.CcY:F4} u'={cieResult.UPrime:F4} v'={cieResult.VPrime:F4}";
                    
                    // Update spectrum data text box with key statistics
                    txtSpectrumData.Text = $"Spectrum captured at {reading.Timestamp:HH:mm:ss}\r\n" +
                                          $"Data points: {reading.Wavelengths.Length}\r\n" +
                                          $"Range: {reading.Wavelengths.Min():F1} - {reading.Wavelengths.Max():F1} nm\r\n" +
                                          $"Peak wavelength: {peakWavelength:F1} nm\r\n" +
                                          $"Peak intensity: {peakIntensity:F0}\r\n" +
                                          $"Total intensity: {totalIntensity:F0}\r\n" +
                                          $"Average intensity: {reading.Intensities.Average():F0}\r\n" +
                                          $"\r\nCIE Color Analysis:\r\n" +
                                          $"X = {cieResult.X:F2}, Y = {cieResult.Y:F2}, Z = {cieResult.Z:F2}\r\n" +
                                          $"x = {cieResult.CcX:F4}, y = {cieResult.CcY:F4}\r\n" +
                                          $"u' = {cieResult.UPrime:F4}, v' = {cieResult.VPrime:F4}\r\n" +
                                          $"Luminance: {cieResult.Luminance:F1} cd/m²\r\n" +
                                          $"Watts/Sr: {cieResult.WattsPerSteradian:F2}";
                }
                catch (Exception ex)
                {
                    lblCieCoordinates.Text = "CIE: Error calculating coordinates";
                    
                    // Update spectrum data text box with basic statistics only
                    txtSpectrumData.Text = $"Spectrum captured at {reading.Timestamp:HH:mm:ss}\r\n" +
                                          $"Data points: {reading.Wavelengths.Length}\r\n" +
                                          $"Range: {reading.Wavelengths.Min():F1} - {reading.Wavelengths.Max():F1} nm\r\n" +
                                          $"Peak wavelength: {peakWavelength:F1} nm\r\n" +
                                          $"Peak intensity: {peakIntensity:F0}\r\n" +
                                          $"Total intensity: {totalIntensity:F0}\r\n" +
                                          $"Average intensity: {reading.Intensities.Average():F0}\r\n" +
                                          $"\r\nCIE calculation error: {ex.Message}";
                }
            }
        }

        private void picSpectrum_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = picSpectrum.ClientRectangle;
            
            // Clear background
            g.Clear(Color.White);
            
            if (_lastReading == null || _lastReading.Wavelengths.Length == 0)
            {
                // Draw "No Data" message
                using var font = new Font("Segoe UI", 12, FontStyle.Bold);
                using var brush = new SolidBrush(Color.Gray);
                var text = "No spectrum data available";
                var textSize = g.MeasureString(text, font);
                var x = (rect.Width - textSize.Width) / 2;
                var y = (rect.Height - textSize.Height) / 2;
                g.DrawString(text, font, brush, x, y);
                return;
            }

            var data = _lastReading;
            var margin = 40;
            var plotRect = new Rectangle(margin, margin, rect.Width - 2 * margin, rect.Height - 2 * margin);
            
            // Draw border
            using var borderPen = new Pen(Color.Black, 2);
            g.DrawRectangle(borderPen, plotRect);
            
            // Calculate scaling
            var minWavelength = data.Wavelengths.Min();
            var maxWavelength = data.Wavelengths.Max();
            var maxIntensity = data.Intensities.Max();
            
            if (maxIntensity == 0) maxIntensity = 1; // Avoid division by zero
            
            // Draw grid lines and labels
            DrawGrid(g, plotRect, minWavelength, maxWavelength, maxIntensity);
            
            // Draw spectrum line
            using var spectrumPen = new Pen(Color.Blue, 2);
            var points = new PointF[data.Wavelengths.Length];
            
            for (int i = 0; i < data.Wavelengths.Length; i++)
            {
                var x = plotRect.Left + (data.Wavelengths[i] - minWavelength) / (maxWavelength - minWavelength) * plotRect.Width;
                var y = plotRect.Bottom - (data.Intensities[i] / maxIntensity) * plotRect.Height;
                points[i] = new PointF((float)x, (float)y);
            }
            
            if (points.Length > 1)
            {
                g.DrawLines(spectrumPen, points);
            }
            
            // Draw peak marker
            if (data.Intensities.Length > 0)
            {
                int peakIndex = Array.IndexOf(data.Intensities, data.Intensities.Max());
                var peakX = plotRect.Left + (data.Wavelengths[peakIndex] - minWavelength) / (maxWavelength - minWavelength) * plotRect.Width;
                var peakY = plotRect.Bottom - (data.Intensities[peakIndex] / maxIntensity) * plotRect.Height;
                
                using var peakBrush = new SolidBrush(Color.Red);
                g.FillEllipse(peakBrush, (float)peakX - 3, (float)peakY - 3, 6, 6);
                
                // Peak label
                using var font = new Font("Segoe UI", 8, FontStyle.Bold);
                using var textBrush = new SolidBrush(Color.Red);
                var peakText = $"{data.Wavelengths[peakIndex]:F0}nm";
                g.DrawString(peakText, font, textBrush, (float)peakX - 15, (float)peakY - 20);
            }
        }

        private void DrawGrid(Graphics g, Rectangle plotRect, double minWave, double maxWave, double maxIntensity)
        {
            using var gridPen = new Pen(Color.LightGray, 1);
            using var labelFont = new Font("Segoe UI", 8);
            using var labelBrush = new SolidBrush(Color.Black);
            using var axisBrush = new SolidBrush(Color.DarkBlue);
            
            // Vertical grid lines (wavelength)
            var waveStep = 50.0; // 50nm intervals
            var waveStart = Math.Ceiling(minWave / waveStep) * waveStep;
            
            for (double wave = waveStart; wave <= maxWave; wave += waveStep)
            {
                var x = plotRect.Left + (wave - minWave) / (maxWave - minWave) * plotRect.Width;
                g.DrawLine(gridPen, (float)x, plotRect.Top, (float)x, plotRect.Bottom);
                
                // Wavelength labels
                var label = $"{wave:F0}";
                var labelSize = g.MeasureString(label, labelFont);
                g.DrawString(label, labelFont, labelBrush, (float)x - labelSize.Width / 2, plotRect.Bottom + 5);
            }
            
            // Horizontal grid lines (intensity)
            var intensitySteps = 5;
            for (int i = 0; i <= intensitySteps; i++)
            {
                var intensity = maxIntensity * i / intensitySteps;
                var y = plotRect.Bottom - (intensity / maxIntensity) * plotRect.Height;
                g.DrawLine(gridPen, plotRect.Left, (float)y, plotRect.Right, (float)y);
                
                // Intensity labels
                var label = $"{intensity:F0}";
                var labelSize = g.MeasureString(label, labelFont);
                g.DrawString(label, labelFont, labelBrush, plotRect.Left - labelSize.Width - 5, (float)y - labelSize.Height / 2);
            }
            
            // Axis labels
            using var axisFont = new Font("Segoe UI", 10, FontStyle.Bold);
            
            // X-axis label
            var xLabel = "Wavelength (nm)";
            var xLabelSize = g.MeasureString(xLabel, axisFont);
            var xLabelX = plotRect.Left + (plotRect.Width - xLabelSize.Width) / 2;
            var xLabelY = plotRect.Bottom + 25;
            g.DrawString(xLabel, axisFont, axisBrush, xLabelX, xLabelY);
            
            // Y-axis label (rotated)
            var yLabel = "Intensity";
            var yLabelSize = g.MeasureString(yLabel, axisFont);
            var yLabelX = 5;
            var yLabelY = plotRect.Top + (plotRect.Height + yLabelSize.Width) / 2;
            
            // Rotate and draw Y-axis label
            g.TranslateTransform(yLabelX, yLabelY);
            g.RotateTransform(-90);
            g.DrawString(yLabel, axisFont, axisBrush, 0, 0);
            g.ResetTransform();
        }

        private void chkAutoCapture_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoCapture.Checked)
            {
                autoTimer.Start();
            }
            else
            {
                autoTimer.Stop();
            }
        }

        private async void autoTimer_Tick(object sender, EventArgs e)
        {
            if (_spectrometer?.IsConnected == true && !btnCapture.Text.Contains("Capturing"))
            {
                await CaptureSpectrum();
            }
        }

        private void btnSaveSpectrum_Click(object sender, EventArgs e)
        {
            if (_lastReading == null)
            {
                MessageBox.Show("No spectrum data to save", "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            saveDialog.DefaultExt = "csv";
            saveDialog.FileName = $"spectrum_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveSpectrumToCsv(_lastReading, saveDialog.FileName);
                    MessageBox.Show("Spectrum data saved successfully", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save spectrum: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveSpectrumToCsv(SpectrumReading reading, string fileName)
        {
            using var writer = new StreamWriter(fileName);
            writer.WriteLine("Wavelength (nm),Intensity");
            writer.WriteLine($"# Captured at {reading.Timestamp}");
            writer.WriteLine($"# Data points: {reading.Wavelengths.Length}");
            
            for (int i = 0; i < reading.Wavelengths.Length; i++)
            {
                writer.WriteLine($"{reading.Wavelengths[i]:F2},{reading.Intensities[i]:F2}");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                autoTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}