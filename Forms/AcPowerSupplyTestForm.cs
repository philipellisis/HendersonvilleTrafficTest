using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class AcPowerSupplyTestForm : Form
    {
        private readonly IAcPowerSupply _acPowerSupply;

        public AcPowerSupplyTestForm()
        {
            InitializeComponent();
            
            var factory = new EquipmentFactory();
            _acPowerSupply = factory.CreateAcPowerSupply();
            
            InitializePowerSupply();
        }

        private async void InitializePowerSupply()
        {
            try
            {
                await _acPowerSupply.InitializeAsync();
                UpdateConnectionUI(_acPowerSupply.IsConnected);
                
                if (_acPowerSupply.IsConnected)
                {
                    await GetDeviceInfo();
                    await ReadAllMeasurements();
                    await UpdateOutputStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize AC power supply: {ex.Message}", "Error", 
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
            grpOutput.Enabled = connected;
            grpSettings.Enabled = connected;
            grpMeasurements.Enabled = connected;
            grpDeviceInfo.Enabled = connected;
            
            if (!connected)
            {
                chkAutoRead.Checked = false;
                ClearMeasurements();
                txtDeviceInfo.Text = "Device not connected";
                lblOutputStatus.Text = "UNKNOWN";
                lblOutputStatus.ForeColor = Color.Gray;
            }
        }

        private void ClearMeasurements()
        {
            txtMeasVoltage.Text = "-- V";
            txtMeasCurrent.Text = "-- A";
            txtMeasPower.Text = "-- W";
            txtMeasPowerFactor.Text = "-- PF";
            txtMeasFrequency.Text = "-- Hz";
            
            // Reset text colors
            txtMeasVoltage.ForeColor = Color.Black;
            txtMeasCurrent.ForeColor = Color.Black;
            txtMeasPower.ForeColor = Color.Black;
            txtMeasPowerFactor.ForeColor = Color.Black;
            txtMeasFrequency.ForeColor = Color.Black;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_acPowerSupply.IsConnected)
                {
                    MessageBox.Show("Disconnect functionality requires restarting the application", 
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                await _acPowerSupply.InitializeAsync();
                UpdateConnectionUI(_acPowerSupply.IsConnected);
                
                if (_acPowerSupply.IsConnected)
                {
                    await GetDeviceInfo();
                    await ReadAllMeasurements();
                    await UpdateOutputStatus();
                }
                else
                {
                    MessageBox.Show("Failed to connect to AC power supply", "Connection Error", 
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

        private async void btnOutputOn_Click(object sender, EventArgs e)
        {
            if (!_acPowerSupply.IsConnected)
            {
                MessageBox.Show("Power supply is not connected", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _acPowerSupply.PowerOnAsync();
                await UpdateOutputStatus();
                
                MessageBox.Show("Output turned ON", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to turn output on: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnOutputOff_Click(object sender, EventArgs e)
        {
            if (!_acPowerSupply.IsConnected)
            {
                MessageBox.Show("Power supply is not connected", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _acPowerSupply.PowerOffAsync();
                await UpdateOutputStatus();
                
                MessageBox.Show("Output turned OFF", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to turn output off: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateOutputStatus()
        {
            try
            {
                // Read current voltage to determine if output is likely on
                var voltage = await _acPowerSupply.GetVoltsAsync();
                var isOn = voltage > 1.0; // Assume output is on if voltage > 1V
                
                lblOutputStatus.Text = isOn ? "ON" : "OFF";
                lblOutputStatus.ForeColor = isOn ? Color.Green : Color.Red;
            }
            catch
            {
                lblOutputStatus.Text = "UNKNOWN";
                lblOutputStatus.ForeColor = Color.Orange;
            }
        }

        private async void btnSetVoltage_Click(object sender, EventArgs e)
        {
            if (!_acPowerSupply.IsConnected)
            {
                MessageBox.Show("Power supply is not connected", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var voltage = (double)numVoltage.Value;
                await _acPowerSupply.SetVoltsAsync(voltage);
                
                MessageBox.Show($"Voltage set to {voltage:F1}V", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                // Update measurements after setting voltage
                await Task.Delay(500);
                await ReadAllMeasurements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to set voltage: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSetFrequency_Click(object sender, EventArgs e)
        {
            if (!_acPowerSupply.IsConnected)
            {
                MessageBox.Show("Power supply is not connected", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var frequency = (double)numFrequency.Value;
                await _acPowerSupply.SetFrequencyAsync(frequency);
                
                MessageBox.Show($"Frequency set to {frequency:F1}Hz", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                // Update measurements after setting frequency
                await Task.Delay(500);
                await ReadAllMeasurements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to set frequency: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReadMeasurements_Click(object sender, EventArgs e)
        {
            await ReadAllMeasurements();
        }

        private async Task ReadAllMeasurements()
        {
            if (!_acPowerSupply.IsConnected)
            {
                return;
            }

            try
            {
                // Read all measurements
                var voltage = await _acPowerSupply.GetVoltsAsync();
                var current = await _acPowerSupply.GetCurrentAsync();
                var power = await _acPowerSupply.GetPowerAsync();
                var powerFactor = await _acPowerSupply.GetPowerFactorAsync();
                var frequency = await _acPowerSupply.GetFrequencyAsync();

                // Update display
                UpdateMeasurementDisplay(voltage, current, power, powerFactor, frequency);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to read measurements: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Show error in measurements
                ShowMeasurementError();
            }
        }

        private void UpdateMeasurementDisplay(double voltage, double current, double power, double powerFactor, double frequency)
        {
            txtMeasVoltage.Text = $"{voltage:F3} V";
            txtMeasVoltage.ForeColor = Color.Black;
            
            txtMeasCurrent.Text = $"{current:F6} A";
            txtMeasCurrent.ForeColor = Color.Black;
            
            txtMeasPower.Text = $"{power:F3} W";
            txtMeasPower.ForeColor = Color.Black;
            
            txtMeasPowerFactor.Text = $"{powerFactor:F3}";
            txtMeasPowerFactor.ForeColor = Color.Black;
            
            txtMeasFrequency.Text = $"{frequency:F2} Hz";
            txtMeasFrequency.ForeColor = Color.Black;
        }

        private void ShowMeasurementError()
        {
            txtMeasVoltage.Text = "ERROR";
            txtMeasVoltage.ForeColor = Color.Red;
            
            txtMeasCurrent.Text = "ERROR";
            txtMeasCurrent.ForeColor = Color.Red;
            
            txtMeasPower.Text = "ERROR";
            txtMeasPower.ForeColor = Color.Red;
            
            txtMeasPowerFactor.Text = "ERROR";
            txtMeasPowerFactor.ForeColor = Color.Red;
            
            txtMeasFrequency.Text = "ERROR";
            txtMeasFrequency.ForeColor = Color.Red;
        }

        private async void btnGetDeviceInfo_Click(object sender, EventArgs e)
        {
            await GetDeviceInfo();
        }

        private async Task GetDeviceInfo()
        {
            if (!_acPowerSupply.IsConnected)
            {
                txtDeviceInfo.Text = "Device not connected";
                return;
            }

            try
            {
                // Try to cast to our specific implementation to get device info
                if (_acPowerSupply is Equipment.Hardware.ItechIT7321AcPowerSupply itech)
                {
                    var deviceInfo = await itech.GetDeviceInfoAsync();
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
            if (!_acPowerSupply.IsConnected)
            {
                MessageBox.Show("Power supply is not connected", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Try to cast to our specific implementation to check errors
                if (_acPowerSupply is Equipment.Hardware.ItechIT7321AcPowerSupply itech)
                {
                    var hasErrors = await itech.CheckErrorsAsync();
                    
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
            if (_acPowerSupply?.IsConnected == true)
            {
                try
                {
                    await ReadAllMeasurements();
                    await UpdateOutputStatus();
                }
                catch
                {
                    // Silently ignore errors during auto-read to prevent spam
                    // User can use "Read Measurements" for detailed error messages
                }
            }
        }
    }
}