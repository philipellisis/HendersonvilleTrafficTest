using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class DcPowerSupplyTestForm : Form
    {
        private readonly IDcPowerSupply _dcPowerSupply;

        public DcPowerSupplyTestForm()
        {
            InitializeComponent();
            
            var factory = new EquipmentFactory();
            _dcPowerSupply = factory.CreateDcPowerSupply();
            
            InitializePowerSupply();
        }

        private async 
        Task
        InitializePowerSupply()
        {
            try
            {
                await _dcPowerSupply.InitializeAsync();
                UpdateConnectionUI(_dcPowerSupply.IsConnected);
                
                if (_dcPowerSupply.IsConnected)
                {
                    await GetDeviceInfo();
                    await ReadAllMeasurements();
                    await UpdateOutputStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize DC power supply: {ex.Message}", "Error", 
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
            
            // Reset text colors
            txtMeasVoltage.ForeColor = Color.Black;
            txtMeasCurrent.ForeColor = Color.Black;
            txtMeasPower.ForeColor = Color.Black;
        }

        private async Task ReadAllMeasurements()
        {
            if (!_dcPowerSupply.IsConnected)
                return;

            try
            {
                // Read voltage
                var voltage = await _dcPowerSupply.GetVoltsAsync();
                txtMeasVoltage.Text = $"{voltage:F2} V";
                txtMeasVoltage.ForeColor = Color.Blue;

                // Read current
                var current = await _dcPowerSupply.GetAmpsAsync();
                txtMeasCurrent.Text = $"{current:F3} A";
                txtMeasCurrent.ForeColor = Color.Blue;

                // Read power if available
                if (_dcPowerSupply is HendersonvilleTrafficTest.Equipment.Hardware.ItechIT6922ADcPowerSupply dcPs)
                {
                    var power = await dcPs.GetPowerAsync();
                    txtMeasPower.Text = $"{power:F2} W";
                    txtMeasPower.ForeColor = Color.Blue;
                }
                else
                {
                    // Calculate power for other implementations
                    var power = voltage * current;
                    txtMeasPower.Text = $"{power:F2} W";
                    txtMeasPower.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading measurements: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task GetDeviceInfo()
        {
            try
            {
                if (_dcPowerSupply is HendersonvilleTrafficTest.Equipment.Hardware.ItechIT6922ADcPowerSupply dcPs)
                {
                    var deviceInfo = await dcPs.GetDeviceInfoAsync();
                    txtDeviceInfo.Text = deviceInfo;
                }
                else
                {
                    txtDeviceInfo.Text = "Device information not available for this power supply type";
                }
            }
            catch (Exception ex)
            {
                txtDeviceInfo.Text = $"Error getting device info: {ex.Message}";
            }
        }

        private async Task UpdateOutputStatus()
        {
            // For now, we'll assume output status based on successful measurements
            // Real implementation would need output status query command
            try
            {
                var voltage = await _dcPowerSupply.GetVoltsAsync();
                var current = await _dcPowerSupply.GetAmpsAsync();
                
                bool outputOn = voltage > 0.1 || current > 0.001; // Threshold for "on" status
                lblOutputStatus.Text = outputOn ? "ON" : "OFF";
                lblOutputStatus.ForeColor = outputOn ? Color.Red : Color.Green;
            }
            catch
            {
                lblOutputStatus.Text = "UNKNOWN";
                lblOutputStatus.ForeColor = Color.Gray;
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (_dcPowerSupply.IsConnected)
            {
                // Disconnect logic (depends on base class implementation)
                UpdateConnectionUI(false);
            }
            else
            {
                await InitializePowerSupply();
            }
        }

        private async void btnOutputOn_Click(object sender, EventArgs e)
        {
            try
            {
                await _dcPowerSupply.PowerOnAsync();
                await Task.Delay(500);
                await UpdateOutputStatus();
                await ReadAllMeasurements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error turning output on: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnOutputOff_Click(object sender, EventArgs e)
        {
            try
            {
                await _dcPowerSupply.PowerOffAsync();
                await Task.Delay(500);
                await UpdateOutputStatus();
                await ReadAllMeasurements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error turning output off: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSetVoltage_Click(object sender, EventArgs e)
        {
            try
            {
                double voltage = (double)numVoltage.Value;
                await _dcPowerSupply.SetVoltsAsync(voltage);
                await Task.Delay(200);
                await ReadAllMeasurements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting voltage: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSetCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                double current = (double)numCurrent.Value;
                await _dcPowerSupply.SetAmpsAsync(current);
                await Task.Delay(200);
                await ReadAllMeasurements();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting current: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReadMeasurements_Click(object sender, EventArgs e)
        {
            await ReadAllMeasurements();
        }

        private async void btnGetDeviceInfo_Click(object sender, EventArgs e)
        {
            await GetDeviceInfo();
        }

        private async void btnCheckErrors_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dcPowerSupply is HendersonvilleTrafficTest.Equipment.Hardware.ItechIT6922ADcPowerSupply dcPs)
                {
                    bool hasErrors = await dcPs.CheckErrorsAsync();
                    string message = hasErrors ? "Device has errors!" : "No errors detected";
                    MessageBoxIcon icon = hasErrors ? MessageBoxIcon.Warning : MessageBoxIcon.Information;
                    MessageBox.Show(message, "Error Check", MessageBoxButtons.OK, icon);
                }
                else
                {
                    MessageBox.Show("Error checking not available for this power supply type", "Information", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking for errors: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkAutoRead_CheckedChanged(object sender, EventArgs e)
        {
            autoReadTimer.Enabled = chkAutoRead.Checked;
        }

        private async void autoReadTimer_Tick(object sender, EventArgs e)
        {
            if (_dcPowerSupply.IsConnected && !btnConnect.Focused && 
                !btnSetVoltage.Focused && !btnSetCurrent.Focused)
            {
                await ReadAllMeasurements();
                await UpdateOutputStatus();
            }
        }

        private void DcPowerSupplyTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            chkAutoRead.Checked = false;
            autoReadTimer.Enabled = false;
        }
    }
}