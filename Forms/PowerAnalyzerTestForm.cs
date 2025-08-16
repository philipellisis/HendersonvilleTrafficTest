using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class PowerAnalyzerTestForm : Form
    {
        private readonly IPowerAnalyzer _powerAnalyzer;

        public PowerAnalyzerTestForm()
        {
            InitializeComponent();
            
            var factory = new EquipmentFactory();
            _powerAnalyzer = factory.CreatePowerAnalyzer();
            
            InitializePowerAnalyzer();
        }

        private async void InitializePowerAnalyzer()
        {
            try
            {
                await _powerAnalyzer.InitializeAsync();
                UpdateConnectionUI(_powerAnalyzer.IsConnected);
                
                if (_powerAnalyzer.IsConnected)
                {
                    await GetDeviceInfo();
                    await ReadMeasurements();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize power analyzer: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            grpMode.Enabled = connected;
            grpMeasurements.Enabled = connected;
            grpDeviceInfo.Enabled = connected;
            
            if (!connected)
            {
                chkAutoRead.Checked = false;
                ClearMeasurements();
                txtDeviceInfo.Text = "Device not connected";
            }
        }

        private void ClearMeasurements()
        {
            txtVoltage.Text = "-- V";
            txtCurrent.Text = "-- A";
            txtPower.Text = "-- W";
            txtFrequency.Text = "-- Hz";
            
            // Reset text colors
            txtVoltage.ForeColor = Color.Black;
            txtCurrent.ForeColor = Color.Black;
            txtPower.ForeColor = Color.Black;
            txtFrequency.ForeColor = Color.Black;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_powerAnalyzer.IsConnected)
                {
                    // For now, just show a message as the base class doesn't expose disconnect
                    MessageBox.Show("Disconnect functionality requires restarting the application", 
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                await _powerAnalyzer.InitializeAsync();
                UpdateConnectionUI(_powerAnalyzer.IsConnected);
                
                if (_powerAnalyzer.IsConnected)
                {
                    await GetDeviceInfo();
                    await ReadMeasurements();
                }
                else
                {
                    MessageBox.Show("Failed to connect to power analyzer", "Connection Error", 
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

        private async void btnSetMode_Click(object sender, EventArgs e)
        {
            if (!_powerAnalyzer.IsConnected)
            {
                MessageBox.Show("Power analyzer is not connected", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var mode = rbAC.Checked ? PowerMode.AC : PowerMode.DC;
                await _powerAnalyzer.SetModeAsync(mode);
                
                MessageBox.Show($"Power mode set to {mode}", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                // Clear measurements after mode change
                ClearMeasurements();
                
                // Wait a moment for mode to settle, then take a reading
                await Task.Delay(1000);
                await ReadMeasurements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to set power mode: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReadOnce_Click(object sender, EventArgs e)
        {
            await ReadMeasurements();
        }

        private async Task ReadMeasurements()
        {
            if (!_powerAnalyzer.IsConnected)
            {
                return;
            }

            try
            {
                // Read all measurements at once
                var measurements = await _powerAnalyzer.GetElectricalsAsync();

                // Update display
                UpdateMeasurementDisplay(measurements);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to read measurements: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Show error in measurements
                ShowMeasurementError();
            }
        }

        private void UpdateMeasurementDisplay(ElectricalMeasurement measurements)
        {
            txtVoltage.Text = $"{measurements.Voltage:F3} V";
            txtVoltage.ForeColor = Color.Black;
            
            txtCurrent.Text = $"{measurements.Current:F6} A";
            txtCurrent.ForeColor = Color.Black;
            
            txtPower.Text = $"{measurements.Power:F3} W";
            txtPower.ForeColor = Color.Black;
            
            txtFrequency.Text = $"{measurements.Frequency:F2} Hz";
            txtFrequency.ForeColor = Color.Black;
        }

        private void ShowMeasurementError()
        {
            txtVoltage.Text = "ERROR";
            txtVoltage.ForeColor = Color.Red;
            
            txtCurrent.Text = "ERROR";
            txtCurrent.ForeColor = Color.Red;
            
            txtPower.Text = "ERROR";
            txtPower.ForeColor = Color.Red;
            
            txtFrequency.Text = "ERROR";
            txtFrequency.ForeColor = Color.Red;
        }

        private async void btnGetDeviceInfo_Click(object sender, EventArgs e)
        {
            await GetDeviceInfo();
        }

        private async Task GetDeviceInfo()
        {
            if (!_powerAnalyzer.IsConnected)
            {
                txtDeviceInfo.Text = "Device not connected";
                return;
            }

            try
            {
                // Try to cast to our specific implementation to get device info
                if (_powerAnalyzer is Equipment.Hardware.Npa101PowerAnalyzer npa101)
                {
                    var deviceInfo = await npa101.GetDeviceInfoAsync();
                    txtDeviceInfo.Text = deviceInfo;
                    txtDeviceInfo.ForeColor = Color.Black;
                }
                else
                {
                    txtDeviceInfo.Text = "Device info not available for simulated device";
                    txtDeviceInfo.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                txtDeviceInfo.Text = $"Error getting device info: {ex.Message}";
                txtDeviceInfo.ForeColor = Color.Red;
            }
        }

        private async void btnCheckErrors_Click(object sender, EventArgs e)
        {
            if (!_powerAnalyzer.IsConnected)
            {
                MessageBox.Show("Power analyzer is not connected", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Try to cast to our specific implementation to check errors
                if (_powerAnalyzer is Equipment.Hardware.Npa101PowerAnalyzer npa101)
                {
                    var hasErrors = await npa101.CheckErrorsAsync();
                    
                    if (hasErrors)
                    {
                        MessageBox.Show("Device has reported errors. Check device status.", "Device Errors", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("No errors reported by device", "Device Status", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Error checking not available for simulated device", "Info", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to check device errors: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkAutoRead_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRead.Checked)
            {
                autoReadTimer.Start();
            }
            else
            {
                autoReadTimer.Stop();
            }
        }

        private async void autoReadTimer_Tick(object sender, EventArgs e)
        {
            if (_powerAnalyzer?.IsConnected == true)
            {
                try
                {
                    await ReadMeasurements();
                }
                catch
                {
                    // Silently ignore errors during auto-read to prevent spam
                    // User can use "Read Once" for detailed error messages
                }
            }
        }

    }
}