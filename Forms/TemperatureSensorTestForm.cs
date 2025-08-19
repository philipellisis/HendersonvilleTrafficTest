using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class TemperatureSensorTestForm : Form
    {
        private readonly ITemperatureSensor _temperatureSensor;

        public TemperatureSensorTestForm()
        {
            InitializeComponent();
            
            var factory = new EquipmentFactory();
            _temperatureSensor = factory.CreateTemperatureSensor();
            
            InitializeSensor();
            UpdateSensorTypeDisplay();
        }

        private void UpdateSensorTypeDisplay()
        {
            // Update the title to show the current sensor type from configuration
            var sensorTypeText = _temperatureSensor.SensorType switch
            {
                TemperatureSensorType.OHT20 => "OHT20 (Temperature + Humidity)",
                TemperatureSensorType.OT60 => "OT60 (Temperature -10 to +60°C)",
                TemperatureSensorType.OT150 => "OT150 (Temperature -50 to +150°C)",
                _ => "Unknown"
            };
            
            this.Text = $"Temperature Sensor Test - {sensorTypeText}";
            
            // Update humidity field visibility based on sensor type
            bool supportsHumidity = _temperatureSensor.SensorType == TemperatureSensorType.OHT20;
            lblHumidity.Enabled = supportsHumidity;
            txtHumidity.Enabled = supportsHumidity;
            chkHumValid.Enabled = supportsHumidity;
            
            if (!supportsHumidity)
            {
                txtHumidity.Text = "N/A";
                txtHumidity.ForeColor = Color.Gray;
                chkHumValid.Checked = false;
            }
        }

        private async void InitializeSensor()
        {
            try
            {
                var connected = await _temperatureSensor.InitializeAsync();
                lblConnectionStatus.Text = $"Connected: {connected}";
                lblConnectionStatus.ForeColor = connected ? Color.Green : Color.Red;
                
                if (connected)
                {
                    UpdateConnectionUI(true);
                    await ReadSensorOnce();
                }
                else
                {
                    UpdateConnectionUI(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize temperature sensor: {ex.Message}", "Error", 
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
            btnReadOnce.Enabled = connected;
            chkAutoRead.Enabled = connected;
            
            if (!connected)
            {
                chkAutoRead.Checked = false;
                ClearReadings();
            }
        }

        private void ClearReadings()
        {
            txtTemperature.Text = "-- °C";
            txtHumidity.Text = "-- %";
            txtErrorCount.Text = "0";
            chkTempValid.Checked = false;
            chkHumValid.Checked = false;
            chkHeating.Checked = false;
            chkErrorOverflow.Checked = false;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_temperatureSensor.IsConnected)
                {
                    // Disconnect logic would go here if the interface supported it
                    MessageBox.Show("Disconnect functionality not implemented in current interface", 
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var connected = await _temperatureSensor.InitializeAsync();
                UpdateConnectionUI(connected);
                
                if (connected)
                {
                    await ReadSensorOnce();
                }
                else
                {
                    MessageBox.Show("Failed to connect to temperature sensor", "Connection Error", 
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

        private async void btnReadOnce_Click(object sender, EventArgs e)
        {
            await ReadSensorOnce();
        }

        private async Task ReadSensorOnce()
        {
            if (!_temperatureSensor.IsConnected)
            {
                return;
            }

            try
            {
                var reading = await _temperatureSensor.GetFullReadingAsync();
                UpdateReadingDisplay(reading);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to read sensor: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Show error in readings
                txtTemperature.Text = "ERROR";
                txtTemperature.ForeColor = Color.Red;
                
                if (_temperatureSensor.SensorType == TemperatureSensorType.OHT20)
                {
                    txtHumidity.Text = "ERROR";
                    txtHumidity.ForeColor = Color.Red;
                }
            }
        }

        private void UpdateReadingDisplay(TemperatureReading reading)
        {
            // Update temperature
            txtTemperature.Text = $"{reading.Temperature:F1} °C";
            txtTemperature.ForeColor = reading.IsTemperatureValid ? Color.Black : Color.Red;
            
            // Update humidity (only for OHT20)
            if (_temperatureSensor.SensorType == TemperatureSensorType.OHT20)
            {
                if (reading.Humidity.HasValue)
                {
                    txtHumidity.Text = $"{reading.Humidity.Value:F1} %";
                    txtHumidity.ForeColor = reading.IsHumidityValid ? Color.Black : Color.Red;
                }
                else
                {
                    txtHumidity.Text = "N/A";
                    txtHumidity.ForeColor = Color.Gray;
                }
            }
            else
            {
                txtHumidity.Text = "N/A";
                txtHumidity.ForeColor = Color.Gray;
            }
            
            // Update error count
            txtErrorCount.Text = reading.ErrorCount.ToString();
            txtErrorCount.ForeColor = reading.ErrorCount > 0 ? Color.Red : Color.Black;
            
            // Update status checkboxes
            chkTempValid.Checked = reading.IsTemperatureValid;
            chkHumValid.Checked = reading.IsHumidityValid;
            chkHeating.Checked = reading.IsHeatingOn;
            chkErrorOverflow.Checked = reading.HasErrorOverflow;
            
            // Color code checkboxes
            chkTempValid.ForeColor = reading.IsTemperatureValid ? Color.Green : Color.Red;
            chkHumValid.ForeColor = reading.IsHumidityValid ? Color.Green : Color.Red;
            chkHeating.ForeColor = reading.IsHeatingOn ? Color.Orange : Color.Gray;
            chkErrorOverflow.ForeColor = reading.HasErrorOverflow ? Color.Red : Color.Gray;
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
            if (_temperatureSensor?.IsConnected == true)
            {
                await ReadSensorOnce();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                autoReadTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}