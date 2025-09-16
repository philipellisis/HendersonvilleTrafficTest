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

                LogInfo($"Device identified: {idResponse}");

                // Clear status
                await _communication.SendCommandAsync("*CLS");
                await Task.Delay(200);

                // Set device to remote mode for SCPI control
                await _communication.SendCommandAsync("SYST:REM");
                await Task.Delay(200);

                // Configure measurement format to binary for faster data transfer
                await _communication.SendCommandAsync("CHANnel:MEASurement:FORMat ASCii");
                await Task.Delay(200);

                // Configure display functions - Page 1 can display all 6 parameters
                await _communication.SendCommandAsync("VIEW:NUMeric:PAGE1:CELL1:FUNCtion URMS");
                await Task.Delay(100);
                await _communication.SendCommandAsync("VIEW:NUMeric:PAGE1:CELL2:FUNCtion IRMS");
                await Task.Delay(100);
                await _communication.SendCommandAsync("VIEW:NUMeric:PAGE1:CELL3:FUNCtion P");
                await Task.Delay(100);
                await _communication.SendCommandAsync("VIEW:NUMeric:PAGE1:CELL4:FUNCtion FU");
                await Task.Delay(100);
                await _communication.SendCommandAsync("VIEW:NUMeric:PAGE1:CELL5:FUNCtion UTHD");
                await Task.Delay(100);
                await _communication.SendCommandAsync("VIEW:NUMeric:PAGE1:CELL6:FUNCtion LAMBDA");
                await Task.Delay(100);

                // Configure channel measurement functions to match display
                await _communication.SendCommandAsync("CHAN:MEAS:FUNCtions URMS,IRMS,P,FU,UTHD,LAMBDA");
                await Task.Delay(200);

                // Set default mode
                await SetModeAsync(PowerMode.DC);

                LogInfo("NPA101 Power Analyzer initialized successfully with extended measurements");
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
                // Format: URMS,IRMS,P,FU,UTHD,LAMBDA (all 6 configured values)
                var values = response.Trim().Split(',');
                if (values.Length < 6)
                {
                    throw new InvalidOperationException($"Invalid measurement response format: expected 6 values, got {values.Length}: {response}");
                }

                var voltage = ParseMeasurementValue(values[0], "URMS");     // RMS Voltage
                var current = ParseMeasurementValue(values[1], "IRMS");     // RMS Current
                var power = ParseMeasurementValue(values[2], "P");          // Real Power
                var frequency = ParseMeasurementValue(values[3], "FU");     // Frequency
                var thd = ParseMeasurementValue(values[4], "UTHD");         // Voltage THD
                var powerFactor = ParseMeasurementValue(values[5], "LAMBDA"); // Power Factor

                return new ElectricalMeasurement(voltage, current, power, frequency, thd, powerFactor);
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