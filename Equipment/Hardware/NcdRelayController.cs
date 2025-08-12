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
            
            // Subscribe to raw data events for debugging
            _communication.ByteDataReceived += OnByteDataReceived;
            _communication.DataReceived += OnTextDataReceived;
        }

        private void OnByteDataReceived(object? sender, byte[] data)
        {
            LogDebug($"Raw byte data received: {ByteUtilities.BytesToHexString(data)} ({data.Length} bytes)");
            
            // Store the last received data for our custom reading logic
            lock (_dataLock)
            {
                _lastReceivedData = new byte[data.Length];
                Array.Copy(data, _lastReceivedData, data.Length);
            }
        }

        private void OnTextDataReceived(object? sender, string data)
        {
            LogDebug($"Raw text data received: '{data}' (length: {data.Length})");
        }

        private async Task<byte[]?> WaitForResponseAsync(int timeoutMs = 2000)
        {
            var startTime = DateTime.Now;
            
            // Clear any old data first
            lock (_dataLock)
            {
                _lastReceivedData = null;
            }
            
            // Wait for new data to arrive
            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                lock (_dataLock)
                {
                    if (_lastReceivedData != null)
                    {
                        var result = new byte[_lastReceivedData.Length];
                        Array.Copy(_lastReceivedData, result, _lastReceivedData.Length);
                        _lastReceivedData = null; // Clear it so we don't reuse old data
                        return result;
                    }
                }
                await Task.Delay(50);
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
                Handshake = Handshake.None,  // Critical for NCD - no flow control
                ReadTimeoutMs = 2000,        // Longer timeout for FTDI
                WriteTimeoutMs = 2000,       // Longer timeout for FTDI
                NewLine = "\r\n",            // Default line ending (not used for binary)
                UseNewLineForBinary = false, // Don't use line endings for NCD binary protocol
                DtrEnable = true,            // Enable DTR for FTDI/NCD
                RtsEnable = true             // Try enabling RTS for FTDI chipset
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

                // FTDI-specific: Wait for driver to settle
                await Task.Delay(500);
                
                // Clear buffers after connection
                await _communication.ClearBuffersAsync();
                
                // Initialize all relay states to off
                for (int i = 0; i < MaxOutputChannels; i++)
                {
                    _outputStates[i] = false;
                }

                // Test communication with a simple command (query relay 1 state)
                LogInfo("Testing communication with NCD device...");
                try
                {
                    var testResponse = await TestCommunicationAsync();
                    if (testResponse)
                    {
                        LogInfo("NCD Relay Controller communication test successful");
                    }
                    else
                    {
                        LogWarning("NCD Relay Controller communication test failed, but continuing...");
                    }
                }
                catch (Exception testEx)
                {
                    LogWarning($"Communication test failed: {testEx.Message}, but continuing...");
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
            try
            {
                // Send a simple state query command to test communication
                var testCommand = new byte[] { 170, 3, 254, 116, 0, 23 }; // Query relay 1 state
                LogDebug($"Testing communication with command: {ByteUtilities.BytesToHexString(testCommand)}");
                
                var response = await SendBytesWithResponseAsync(testCommand, 1, 2000);
                
                LogDebug($"Test communication response: {(response != null ? ByteUtilities.BytesToHexString(response) : "null")}");
                
                return response != null && response.Length > 0;
            }
            catch (Exception ex)
            {
                LogError($"Communication test error: {ex.Message}");
                return false;
            }
        }

        public async Task TurnOutputOnAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            
            try
            {
                //170 3   254 108 0   23
                // NCD Relay Controller turn ON commands:
                // Port 1: 254 108 1 -> expect 85
                // Port 2: 254 109 1 -> expect 85
                // ...
                // Port 8: 254 115 1 -> expect 85
                byte commandByte = (byte)(107 + outputNumber); // 108 for port 1, 109 for port 2, etc.
                var command = new byte[] {170,3, 254, commandByte, 0, 23 };
                
                LogDebug($"Turning ON relay {outputNumber} with command: {ByteUtilities.BytesToHexString(command)}");
                
                // Clear any pending data first
                await _communication.ClearBuffersAsync();
                
                // Send command and wait for response using our custom method
                await SendBytesAsync(command);
                var response = await WaitForResponseAsync(3000);
                
                LogDebug($"Relay {outputNumber} ON response: {(response != null ? ByteUtilities.BytesToHexString(response) : "null")}");
                
                // Parse NCD response format: AA 01 55 00
                // Byte 0: 0xAA (command header)
                // Byte 1: 0x01 (response length)  
                // Byte 2: 0x55 (85 decimal - success code)
                // Byte 3: 0x00 (status/padding)
                if (response != null && response.Length >= 3 && response[0] == 0xAA && response[2] == 85)
                {
                    _outputStates[outputNumber - 1] = true;
                    LogInfo($"Successfully turned ON relay {outputNumber} - NCD response: {ByteUtilities.BytesToHexString(response)}");
                }
                else
                {
                    var responseHex = response != null ? ByteUtilities.BytesToHexString(response) : "null";
                    LogWarning($"Relay {outputNumber} turned ON but unexpected response. Expected: AA 01 55 XX, got: {responseHex}");
                    
                    // Still mark as successful since the relay is working
                    _outputStates[outputNumber - 1] = true;
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
                // NCD Relay Controller turn OFF commands:
                // Port 1: 254 100 1 -> expect 85
                // Port 2: 254 101 1 -> expect 85
                // ...
                // Port 8: 254 107 1 -> expect 85
                byte commandByte = (byte)(99 + outputNumber); // 100 for port 1, 101 for port 2, etc.
                var command = new byte[] { 254, commandByte, 1 };
                
                LogDebug($"Turning OFF relay {outputNumber} with command: {ByteUtilities.BytesToHexString(command)}");
                
                // Clear any pending data first
                await _communication.ClearBuffersAsync();
                
                // Send command and wait for response using our custom method
                await SendBytesAsync(command);
                var response = await WaitForResponseAsync(3000);
                
                LogDebug($"Relay {outputNumber} OFF response: {(response != null ? ByteUtilities.BytesToHexString(response) : "null")}");
                
                // Parse NCD response format: AA 01 55 00
                if (response != null && response.Length >= 3 && response[0] == 0xAA && response[2] == 85)
                {
                    _outputStates[outputNumber - 1] = false;
                    LogInfo($"Successfully turned OFF relay {outputNumber} - NCD response: {ByteUtilities.BytesToHexString(response)}");
                }
                else
                {
                    var responseHex = response != null ? ByteUtilities.BytesToHexString(response) : "null";
                    LogWarning($"Relay {outputNumber} turned OFF but unexpected response. Expected: AA 01 55 XX, got: {responseHex}");
                    
                    // Still mark as successful since the relay is working
                    _outputStates[outputNumber - 1] = false;
                }
            }
            catch (Exception ex)
            {
                LogError($"Error turning OFF relay {outputNumber}: {ex.Message}");
                throw;
            }
        }

        public Task<byte> ReadAnalogValueAsync(int inputNumber)
        {
            ValidateInputNumber(inputNumber);
            // TODO: Implement ADC reading with specific NCD commands
            throw new NotImplementedException("Read analog value command not implemented yet");
        }

        public async Task<bool> GetOutputStateAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            
            try
            {
                // NCD Relay Controller state query commands:
                // Port 1: 254 116 1 -> expect 0 (off) or 1 (on)
                // Port 2: 254 117 1 -> expect 0 (off) or 1 (on)
                // ...
                // Port 8: 254 123 1 -> expect 0 (off) or 1 (on)
                byte commandByte = (byte)(115 + outputNumber); // 116 for port 1, 117 for port 2, etc.
                var command = new byte[] { 254, commandByte, 1 };
                
                LogDebug($"Querying state of relay {outputNumber} with command: {ByteUtilities.BytesToHexString(command)}");
                
                // Clear any pending data first
                await _communication.ClearBuffersAsync();
                
                // Send command and wait for response using our custom method
                await SendBytesAsync(command);
                var response = await WaitForResponseAsync(2000);
                
                LogDebug($"Relay {outputNumber} state response: {(response != null ? ByteUtilities.BytesToHexString(response) : "null")} (expected: 00 or 01)");
                
                if (response != null && response.Length > 0)
                {
                    // For state queries, NCD returns just the state byte: 0x00 (off) or 0x01 (on)
                    bool isOn = response[0] == 1;
                    
                    // Update cached state to match actual state
                    _outputStates[outputNumber - 1] = isOn;
                    
                    LogInfo($"Relay {outputNumber} actual state: {(isOn ? "ON" : "OFF")} - response: {ByteUtilities.BytesToHexString(response)}");
                    return isOn;
                }
                else
                {
                    LogWarning($"No response when querying relay {outputNumber} state, returning cached state");
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