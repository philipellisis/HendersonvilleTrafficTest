using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Communication;
using HendersonvilleTrafficTest.Configuration;
using System.Globalization;
using System.IO.Ports;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class ItechIT7321AcPowerSupply : EquipmentCommunicationBase, IAcPowerSupply
    {
        public ItechIT7321AcPowerSupply() : base("ITECH IT7321 AC Power Supply", CreateSerialSettings())
        {
        }

        private static SerialPortSettings CreateSerialSettings()
        {
            var config = ConfigurationManager.Current.Equipment;
            return new SerialPortSettings
            {
                PortName = config.AcPowerSupplyComPort,
                BaudRate = config.AcPowerSupplyBaudRate,
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
                throw new InvalidOperationException("Failed to initialize ITECH IT7321 AC Power Supply");
            }
        }

        protected override async Task<bool> PerformEquipmentInitializationAsync()
        {
            try
            {

                // Clear buffers first
                await _communication.ClearBuffersAsync();
                await Task.Delay(500);

                // Send identification query to verify device
                var idResponse = await _communication.SendCommandAndReceiveAsync("*IDN?", 3000);
                if (string.IsNullOrEmpty(idResponse) || !idResponse.ToUpper().Contains("IT7321"))
                {
                    LogError($"Invalid device response to *IDN?: {idResponse}");
                    return false;
                }

                LogInfo($"Device identified: {idResponse}");

                // Give the device time to be ready for next commands
                await Task.Delay(200);

                // Set device to remote mode for SCPI control
                await _communication.SendCommandAsync("SYST:REM");
                await Task.Delay(200);

                //Clear status and errors
                await _communication.SendCommandAsync("*CLS");
                await Task.Delay(200);

                // Initialize power supply to safe state
                await PowerOffAsync(); // Ensure output is off
                await Task.Delay(200);

                await SetVoltsAsync(0.0); // Set voltage to 0
                await Task.Delay(200);

                await SetFrequencyAsync(60.0); // Set frequency to 60Hz
                await Task.Delay(200);

                LogInfo("ITECH IT7321 AC Power Supply initialized successfully");
                return true;
            }
            catch (Exception ex)
            {
                LogError($"Equipment initialization failed: {ex.Message}");
                return false;
            }
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

        public async Task SetVoltsAsync(double volts)
        {
            var success = await _communication.SendCommandAsync($"VOLT {volts:F1}");
            if (!success)
            {
                throw new InvalidOperationException($"Failed to set voltage to {volts}V");
            }

            // Give device time to process the command
            await Task.Delay(100);
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

        public async Task SetFrequencyAsync(double frequency)
        {
            var success = await _communication.SendCommandAsync($"FREQ {frequency:F1}");
            if (!success)
            {
                throw new InvalidOperationException($"Failed to set frequency to {frequency}Hz");
            }

            // Give device time to process the command
            await Task.Delay(100);
        }

        public async Task PowerOnAsync()
        {
            var success = await _communication.SendCommandAsync("OUTP 1");
            if (!success)
            {
                throw new InvalidOperationException("Failed to turn power on");
            }

            // Give device time to turn on
            await Task.Delay(500);
            LogInfo("AC Power Supply output turned ON");
        }

        public async Task PowerOffAsync()
        {
            var success = await _communication.SendCommandAsync("OUTP 0");
            if (!success)
            {
                throw new InvalidOperationException("Failed to turn power off");
            }

            // Give device time to turn off
            await Task.Delay(500);
            LogInfo("AC Power Supply output turned OFF");
        }

        public async Task<double> GetCurrentAsync()
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

        public async Task<double> GetPowerAsync()
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