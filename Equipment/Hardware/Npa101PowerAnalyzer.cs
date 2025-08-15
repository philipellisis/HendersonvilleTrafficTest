using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Communication;
using HendersonvilleTrafficTest.Configuration;
using System.Globalization;
using System.IO.Ports;

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
                DtrEnable = false,
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
                PowerMode.AC => "CONF:POW:AC",
                PowerMode.DC => "CONF:POW:DC",
                _ => throw new ArgumentException($"Unsupported power mode: {mode}")
            };

            var success = await _communication.SendCommandAsync(modeCommand);
            if (!success)
            {
                throw new InvalidOperationException($"Failed to set power mode to {mode}");
            }

            // Give the device time to switch modes
            await Task.Delay(500);
        }

        public async Task<double> GetVoltsAsync()
        {
            var response = await _communication.SendCommandAndReceiveAsync("MEAS:VOLT?", 2000);
            if (string.IsNullOrEmpty(response))
            {
                throw new InvalidOperationException("No response received for voltage measurement");
            }

            if (!double.TryParse(response.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double voltage))
            {
                throw new InvalidOperationException($"Invalid voltage response: {response}");
            }

            return voltage;
        }

        public async Task<double> GetAmpsAsync()
        {
            var response = await _communication.SendCommandAndReceiveAsync("MEAS:CURR?", 2000);
            if (string.IsNullOrEmpty(response))
            {
                throw new InvalidOperationException("No response received for current measurement");
            }

            if (!double.TryParse(response.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double current))
            {
                throw new InvalidOperationException($"Invalid current response: {response}");
            }

            return current;
        }

        public async Task<double> GetWattsAsync()
        {
            var response = await _communication.SendCommandAndReceiveAsync("MEAS:POW?", 2000);
            if (string.IsNullOrEmpty(response))
            {
                throw new InvalidOperationException("No response received for power measurement");
            }

            if (!double.TryParse(response.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double power))
            {
                throw new InvalidOperationException($"Invalid power response: {response}");
            }

            return power;
        }

        public async Task<double> GetPowerFactorAsync()
        {
            var response = await _communication.SendCommandAndReceiveAsync("MEAS:POW:PFAC?", 2000);
            if (string.IsNullOrEmpty(response))
            {
                throw new InvalidOperationException("No response received for power factor measurement");
            }

            if (!double.TryParse(response.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double powerFactor))
            {
                throw new InvalidOperationException($"Invalid power factor response: {response}");
            }

            return powerFactor;
        }

        public async Task<double> GetFrequencyAsync()
        {
            var response = await _communication.SendCommandAndReceiveAsync("MEAS:FREQ?", 2000);
            if (string.IsNullOrEmpty(response))
            {
                throw new InvalidOperationException("No response received for frequency measurement");
            }

            if (!double.TryParse(response.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out double frequency))
            {
                throw new InvalidOperationException($"Invalid frequency response: {response}");
            }

            return frequency;
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