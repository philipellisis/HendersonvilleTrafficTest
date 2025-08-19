using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Communication;
using HendersonvilleTrafficTest.Configuration;
using System.IO.Ports;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class UsbTemperatureSensor : EquipmentCommunicationBase, ITemperatureSensor
    {
        
        public TemperatureSensorType SensorType { get; set; } = TemperatureSensorType.OT150;

        public UsbTemperatureSensor() : base("USB Temperature Sensor", CreateSerialSettings())
        {
            // No longer need event subscriptions - using synchronous communication
            
            // Set sensor type from configuration
            var config = ConfigurationManager.Current.Equipment;
            if (Enum.TryParse<TemperatureSensorType>(config.TemperatureSensorType, out var sensorType))
            {
                SensorType = sensorType;
            }
        }

        private static SerialPortSettings CreateSerialSettings()
        {
            var config = ConfigurationManager.Current.Equipment;
            return new SerialPortSettings
            {
                PortName = config.TemperatureSensorComPort,
                BaudRate = config.TemperatureSensorBaudRate,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeoutMs = 1000,
                WriteTimeoutMs = 1000,
                NewLine = "\r",
                UseNewLineForBinary = false,
                DtrEnable = false,
                RtsEnable = false
            };
        }

        public override async Task<bool> InitializeAsync()
        {
            try
            {
                LogInfo($"Initializing USB Temperature Sensor on {_communication.Settings.PortName} at {_communication.Settings.BaudRate} baud");
                
                var connected = await base.InitializeAsync();
                if (!connected)
                {
                    return false;
                }

                await Task.Delay(200);
                
                try
                {
                    var testReading = await GetFullReadingAsync();
                    LogInfo($"Temperature sensor communication verified - Temp: {testReading.Temperature:F1}°C");
                }
                catch (Exception testEx)
                {
                    LogWarning($"Communication test failed: {testEx.Message}");
                }

                LogInfo("USB Temperature Sensor initialized successfully");
                return true;
            }
            catch (Exception ex)
            {
                LogError($"Failed to initialize USB Temperature Sensor: {ex.Message}");
                return false;
            }
        }

        public async Task<double> GetTemperatureAsync()
        {
            var reading = await GetFullReadingAsync();
            return reading.Temperature;
        }

        public async Task<TemperatureReading> GetFullReadingAsync()
        {
            try
            {
                var command = new byte[] { 0x02, 0xFD };
                
                // Use the new synchronous send-and-receive method
                var response = await _communication.SendBytesAndReceiveAsync(command, 2000, 7);
                
                if (response == null || response.Length < 7)
                {
                    throw new InvalidOperationException($"Invalid response length: expected 7 bytes, got {response?.Length ?? 0}");
                }
                
                // Validate response header: should be 0xFD, 0x02
                if (response[0] != 0xFD || response[1] != 0x02)
                {
                    throw new InvalidOperationException($"Invalid response header: expected FD 02, got {response[0]:X2} {response[1]:X2}");
                }
                
                return ParseSensorResponse(response);
            }
            catch (Exception ex)
            {
                LogError($"Error reading temperature sensor: {ex.Message}");
                throw;
            }
        }

        private TemperatureReading ParseSensorResponse(byte[] response)
        {
            // Response format: 0xFD, 0x02, HUM_LSB, HUM_MSB, TEMP_LSB, TEMP_MSB, FLAG
            // Bytes 0-1: Header (0xFD, 0x02)
            // Bytes 2-3: HUM (LSB, MSB) - little endian
            // Bytes 4-5: TEMP (LSB, MSB) - little endian  
            // Byte 6: FLAG
            
            // Parse little endian 16-bit values
            var humBytes = (ushort)(response[2] | (response[3] << 8));  // LSB first, then MSB
            var tempBytes = (ushort)(response[4] | (response[5] << 8)); // LSB first, then MSB
            var flag = response[6];
            
            var reading = new TemperatureReading();
            
            // Parse flag bits
            reading.ErrorCount = flag & 0x1F;  // Bits 0-4
            reading.HasErrorOverflow = (flag & 0x20) != 0;  // Bit 5
            reading.IsHeatingOn = (flag & 0x40) != 0;  // Bit 6
            reading.IsTemperatureValid = (flag & 0x80) != 0;  // Bit 7
            // Note: For humidity validity, we rely on sensor type since flag is only 1 byte
            reading.IsHumidityValid = (SensorType == TemperatureSensorType.OHT20) && reading.IsTemperatureValid;
            
            // Convert temperature based on sensor type
            reading.Temperature = ConvertTemperature(tempBytes, SensorType);
            
            // Convert humidity (only valid for OHT20)
            if (SensorType == TemperatureSensorType.OHT20)
            {
                reading.Humidity = ConvertHumidity(humBytes);
                // For non-OHT20 sensors, humidity should be zero according to documentation
            }
            else
            {
                reading.Humidity = null;
                reading.IsHumidityValid = false;
            }
            
            LogDebug($"Sensor reading - Temp: {reading.Temperature:F1}°C, Humidity: {reading.Humidity?.ToString("F1") ?? "N/A"}%, Errors: {reading.ErrorCount}");
            
            return reading;
        }

        private static double ConvertTemperature(ushort tempValue, TemperatureSensorType sensorType)
        {
            return sensorType switch
            {
                TemperatureSensorType.OHT20 => (tempValue * 175.0 / 65535.0) - 45.0,
                TemperatureSensorType.OT60 => (tempValue * 70.0 / 2048.0) - 10.0,
                TemperatureSensorType.OT150 => (tempValue * 200.0 / 2048.0) - 50.0,
                _ => throw new ArgumentException($"Unknown sensor type: {sensorType}")
            };
        }

        private static double ConvertHumidity(ushort humValue)
        {
            return humValue * 100.0 / 65535.0;
        }
    }
}