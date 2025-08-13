using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Communication;
using HendersonvilleTrafficTest.Configuration;
using System.IO.Ports;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class NcdRelayController : EquipmentCommunicationBase, IRelayController
    {
        private readonly bool[] _outputStates;
        private byte[]? _lastReceivedData;
        private readonly object _dataLock = new();
        
        public int MaxOutputChannels { get; } = 8;
        public int MaxInputChannels { get; } = 8;

        public NcdRelayController() : base("NCD Relay Controller", CreateSerialSettings())
        {
            _outputStates = new bool[MaxOutputChannels];
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
                PortName = config.RelayControllerComPort,
                BaudRate = config.RelayControllerBaudRate,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeoutMs = 2000,
                WriteTimeoutMs = 2000,
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
                LogInfo($"Initializing NCD Relay Controller on {_communication.Settings.PortName} at {_communication.Settings.BaudRate} baud");
                
                var connected = await base.InitializeAsync();
                if (!connected)
                {
                    return false;
                }

                await Task.Delay(200);
                
                for (int i = 0; i < MaxOutputChannels; i++)
                {
                    _outputStates[i] = false;
                }

                try
                {
                    await TestCommunicationAsync();
                    LogInfo("NCD Relay Controller communication verified");
                }
                catch (Exception testEx)
                {
                    LogWarning($"Communication test failed: {testEx.Message}");
                }

                LogInfo("NCD Relay Controller initialized successfully");
                return true;
            }
            catch (Exception ex)
            {
                LogError($"Failed to initialize NCD Relay Controller: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TestCommunicationAsync()
        {
            var testCommand = new byte[] { 254, 116, 1 };
            await SendBytesAsync(testCommand);
            var response = await WaitForResponseAsync(1000);
            return response != null && response.Length > 0;
        }

        public async Task TurnOutputOnAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            
            try
            {
                byte commandByte = (byte)(107 + outputNumber);
                byte checksumByte = (byte)(22 + outputNumber);
                var command = new byte[] { 170, 3, 254, commandByte, 0, checksumByte };
                
                await SendBytesAsync(command);
                var response = await WaitForResponseAsync(2000);
                
                if (response != null && response.Length >= 3 && response[0] == 0xAA && response[2] == 85)
                {
                    _outputStates[outputNumber - 1] = true;
                    LogInfo($"Relay {outputNumber} turned ON");
                }
                else
                {
                    _outputStates[outputNumber - 1] = true;
                    LogWarning($"Relay {outputNumber} ON - unexpected response format");
                }
            }
            catch (Exception ex)
            {
                LogError($"Error turning ON relay {outputNumber}: {ex.Message}");
                throw;
            }
        }

        public async Task TurnOutputOffAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            
            try
            {
                byte commandByte = (byte)(99 + outputNumber);
                byte checksumByte = (byte)(14 + outputNumber);
                var command = new byte[] { 170, 3, 254, commandByte, 0, checksumByte };

                await SendBytesAsync(command);
                var response = await WaitForResponseAsync(2000);

                if (response != null && response.Length >= 3 && response[0] == 0xAA && response[2] == 85)
                {
                    _outputStates[outputNumber - 1] = false;
                    LogInfo($"Relay {outputNumber} turned OFF");
                }
                else
                {
                    _outputStates[outputNumber - 1] = false;
                    LogWarning($"Relay {outputNumber} OFF - unexpected response format");
                }
            }
            catch (Exception ex)
            {
                LogError($"Error turning OFF relay {outputNumber}: {ex.Message}");
                throw;
            }
        }

        public async Task<byte> ReadAnalogValueAsync(int inputNumber)
        {
            ValidateOutputNumber(inputNumber);

            try
            {
                byte commandByte = (byte)(149 + inputNumber);
                byte checksumByte = (byte)(63 + inputNumber);
                var command = new byte[] { 170, 2, 254, commandByte, checksumByte };

                await SendBytesAsync(command);
                var response = await WaitForResponseAsync(2000);

                if (response != null && response.Length > 2)
                {
                    return response[3];
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                LogError($"Error querying ADC {inputNumber} value: {ex.Message}");
                // Return cached state on error
                return 0;
            }
        }

        public async Task<bool> GetOutputStateAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            
            try
            {
                byte commandByte = (byte)(115 + outputNumber);
                var command = new byte[] { 254, commandByte, 1 };
                
                await SendBytesAsync(command);
                var response = await WaitForResponseAsync(1000);
                
                if (response != null && response.Length > 0)
                {
                    bool isOn = response[0] == 1;
                    _outputStates[outputNumber - 1] = isOn;
                    return isOn;
                }
                else
                {
                    return _outputStates[outputNumber - 1];
                }
            }
            catch (Exception ex)
            {
                LogError($"Error querying relay {outputNumber} state: {ex.Message}");
                // Return cached state on error
                return _outputStates[outputNumber - 1];
            }
        }

        private void ValidateOutputNumber(int outputNumber)
        {
            if (outputNumber < 1 || outputNumber > MaxOutputChannels)
            {
                throw new ArgumentOutOfRangeException(nameof(outputNumber), 
                    $"Output number must be between 1 and {MaxOutputChannels}");
            }
        }

        private void ValidateInputNumber(int inputNumber)
        {
            if (inputNumber < 1 || inputNumber > MaxInputChannels)
            {
                throw new ArgumentOutOfRangeException(nameof(inputNumber), 
                    $"Input number must be between 1 and {MaxInputChannels}");
            }
        }
    }
}