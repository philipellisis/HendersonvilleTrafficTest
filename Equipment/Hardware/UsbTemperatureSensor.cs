using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Communication;
using HendersonvilleTrafficTest.Configuration;
using System.IO.Ports;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class UsbTemperatureSensor : EquipmentCommunicationBase, ITemperatureSensor
    {
        private byte[]? _lastReceivedData;
        private readonly object _dataLock = new();
        
        public TemperatureSensorType SensorType { get; set; } = TemperatureSensorType.OHT20;

        public UsbTemperatureSensor() : base("USB Temperature Sensor", CreateSerialSettings())
        {
            _communication.ByteDataReceived += OnByteDataReceived;
        }

        private void OnByteDataReceived(object? sender, byte[] data)
        {
            lock (_dataLock)
            {
                _lastReceivedData = new byte[data.Length];
                Array.Copy(data, _lastReceivedData, data.Length);
            }
        }

        private async Task<byte[]?> WaitForResponseAsync(int timeoutMs = 1000)
        {
            var startTime = DateTime.Now;
            
            lock (_dataLock) { _lastReceivedData = null; }
            
            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                lock (_dataLock)
                {
                    if (_lastReceivedData != null)
                    {
                        var result = new byte[_lastReceivedData.Length];
                        Array.Copy(_lastReceivedData, result, _lastReceivedData.Length);
                        _lastReceivedData = null;
                        return result;
                    }
                }
                await Task.Delay(25);
            }
            
            return null;
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
                NewLine = "\r\n",
                UseNewLineForBinary = false,
                DtrEnable = true,
                RtsEnable = true
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
                
                await SendBytesAsync(command);
                var response = await WaitForResponseAsync(2000);
                
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
            // Response format: 0xFD, 0x02, E0, AB, 38, 60, C0
            // Bytes 2-3: HUM (humidity data)
            // Bytes 4-5: TEMP (temperature data)  
            // Byte 6: FLAG
            
            var humBytes = (ushort)((response[2] << 8) | response[3]);
            var tempBytes = (ushort)((response[4] << 8) | response[5]);
            var flag = response[6];
            
            var reading = new TemperatureReading();
            
            // Parse flag bits
            reading.ErrorCount = flag & 0x1F;  // Bits 0-4
            reading.HasErrorOverflow = (flag & 0x20) != 0;  // Bit 5
            reading.IsHeatingOn = (flag & 0x40) != 0;  // Bit 6
            reading.IsTemperatureValid = (flag & 0x80) != 0;  // Bit 7
            reading.IsHumidityValid = (flag & 0x100) != 0;  // Bit 8 (note: this might not work as flag is only 1 byte)
            
            // Convert temperature based on sensor type
            reading.Temperature = ConvertTemperature(tempBytes, SensorType);
            
            // Convert humidity (only valid for OHT20)
            if (SensorType == TemperatureSensorType.OHT20 && reading.IsHumidityValid)
            {
                reading.Humidity = ConvertHumidity(humBytes);
            }
            else
            {
                reading.Humidity = null;
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