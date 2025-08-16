using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Communication;
using HendersonvilleTrafficTest.Configuration;
using System.Globalization;
using System.IO.Ports;
using static System.Windows.Forms.DataFormats;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class Npa101PowerAnalyzer : EquipmentCommunicationBase, IPowerAnalyzer
    {
        public Npa101PowerAnalyzer() : base("NPA101 Power Analyzer", CreateSerialSettings())
        {
        }

        private static SerialPortSettings CreateSerialSettings()
        {
            var config = ConfigurationManager.Current.Equipment;
            return new SerialPortSettings
            {
                PortName = config.PowerAnalyzerComPort,
                BaudRate = config.PowerAnalyzerBaudRate,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeoutMs = 2000,
                WriteTimeoutMs = 2000,
                NewLine = "\n",
                DtrEnable = true,
                RtsEnable = false
            };
        }

        public new async Task InitializeAsync()
        {
            var success = await base.InitializeAsync();
            if (!success)
            {
                throw new InvalidOperationException("Failed to initialize NPA101 Power Analyzer");
            }
        }

        protected override async Task<bool> PerformEquipmentInitializationAsync()
        {
            try
            {
                // Send identification query to verify device
                var idResponse = await _communication.SendCommandAndReceiveAsync("*IDN?", 2000);
                if (string.IsNullOrEmpty(idResponse) || !idResponse.ToUpper().Contains("NPA"))
                {
                    LogError($"Invalid device response to *IDN?: {idResponse}");
                    return false;
                }

                // Reset device to known state
                //await _communication.SendCommandAsync("*RST");
                //await Task.Delay(1000); // Allow time for reset

                // Clear status
                await _communication.SendCommandAsync("*CLS");
                

                // Set device to remote mode for SCPI control
                await _communication.SendCommandAsync("SYST:REM");
                await SetModeAsync(PowerMode.DC);

                LogInfo("NPA101 Power Analyzer initialized successfully");
                return true;
            }
            catch (Exception ex)
            {
                LogError($"Equipment initialization failed: {ex.Message}");
                return false;
            }
        }

        public async Task SetModeAsync(PowerMode mode)
        {
            var modeCommand = mode switch
            {
                PowerMode.AC => "CHAN:ACQ:MODE AC",
                PowerMode.DC => "CHAN:ACQ:MODE DC",
                _ => throw new ArgumentException($"Unsupported power mode: {mode}")
            };

            var success = await _communication.SendCommandAsync(modeCommand);
            if (!success)
            {
                throw new InvalidOperationException($"Failed to set power mode to {mode}");
            }

            var success_volt_auto = await _communication.SendCommandAsync("CHAN:VOLT:RANG:AUTO 1");
            if (!success_volt_auto)
            {
                throw new InvalidOperationException($"Failed to set voltage range to auto");
            }

            var success_cur_auto = await _communication.SendCommandAsync("CHAN:CURR:RANG:AUTO 1");
            if (!success_cur_auto)
            {
                throw new InvalidOperationException($"Failed to set current range to auto");
            }

            // Give the device time to switch modes
            await Task.Delay(500);
        }

        public async Task<ElectricalMeasurement> GetElectricalsAsync()
        {
            try
            {
                
                var response = await _communication.SendCommandAndReceiveAsync("CHAN:MEAS:DATA?", 5000);
                if (string.IsNullOrEmpty(response))
                {
                    throw new InvalidOperationException("No response received for electrical measurements");
                }
                
                LogInfo($"Raw measurement response: {response}");

                // Parse the comma-separated response
                // Format: voltage,current,power,frequency,... (we only need first 4 values)
                var values = response.Trim().Split(',');
                if (values.Length < 4)
                {
                    throw new InvalidOperationException($"Invalid measurement response format: {response}");
                }

                var voltage = ParseMeasurementValue(values[0], "voltage");
                var current = ParseMeasurementValue(values[1], "current");
                var power = ParseMeasurementValue(values[2], "power");
                var frequency = ParseMeasurementValue(values[3], "frequency");

                return new ElectricalMeasurement(voltage, current, power, frequency);
            }
            catch (Exception ex)
            {
                LogError($"Failed to get electrical measurements: {ex.Message}");
                throw new InvalidOperationException($"Failed to read electrical measurements: {ex.Message}");
            }
        }

        private static double ParseMeasurementValue(string value, string measurementName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Empty {measurementName} value");
            }

            // Handle NAN values
            if (value.Trim().Equals("NAN", StringComparison.OrdinalIgnoreCase))
            {
                return 0.0; // Return 0 for NAN values
            }

            if (!double.TryParse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
            {
                throw new InvalidOperationException($"Invalid {measurementName} value: {value}");
            }

            return result;
        }

        public async Task<string> GetDeviceInfoAsync()
        {
            var response = await _communication.SendCommandAndReceiveAsync("*IDN?", 2000);
            return response ?? "No device information available";
        }

        public async Task<bool> CheckErrorsAsync()
        {
            var response = await _communication.SendCommandAndReceiveAsync("SYST:ERR?", 2000);
            return !string.IsNullOrEmpty(response) && !response.StartsWith("0,");
        }

    }
}