using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Communication;
using HendersonvilleTrafficTest.Configuration;
using System.IO.Ports;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class NcdRelayController : EquipmentCommunicationBase, IRelayController
    {
        private readonly bool[] _outputStates;
        
        public int MaxOutputChannels { get; } = 8;
        public int MaxInputChannels { get; } = 8;

        public NcdRelayController() : base("NCD Relay Controller", CreateSerialSettings())
        {
            _outputStates = new bool[MaxOutputChannels];
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
                ReadTimeoutMs = 1000,
                WriteTimeoutMs = 1000
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

                // Initialize all relay states to off
                for (int i = 0; i < MaxOutputChannels; i++)
                {
                    _outputStates[i] = false;
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

        public async Task TurnOutputOnAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            
            try
            {
                // NCD Relay Controller turn ON commands:
                // Port 1: 254 108 1 -> expect 85
                // Port 2: 254 109 1 -> expect 85
                // ...
                // Port 8: 254 115 1 -> expect 85
                byte commandByte = (byte)(107 + outputNumber); // 108 for port 1, 109 for port 2, etc.
                var command = new byte[] { 254, commandByte, 1 };
                
                LogDebug($"Turning ON relay {outputNumber} with command: {ByteUtilities.BytesToHexString(command)}");
                
                var response = await SendBytesWithResponseAsync(command, 1, 2000);
                
                if (response != null && response.Length > 0 && response[0] == 85)
                {
                    _outputStates[outputNumber - 1] = true;
                    LogInfo($"Successfully turned ON relay {outputNumber}");
                }
                else
                {
                    var responseHex = response != null ? ByteUtilities.BytesToHexString(response) : "null";
                    LogError($"Failed to turn ON relay {outputNumber}. Expected response: 55 (85 decimal), got: {responseHex}");
                    throw new InvalidOperationException($"Invalid response from relay controller: expected 85, got {responseHex}");
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
                
                var response = await SendBytesWithResponseAsync(command, 1, 2000);
                
                if (response != null && response.Length > 0 && response[0] == 85)
                {
                    _outputStates[outputNumber - 1] = false;
                    LogInfo($"Successfully turned OFF relay {outputNumber}");
                }
                else
                {
                    var responseHex = response != null ? ByteUtilities.BytesToHexString(response) : "null";
                    LogError($"Failed to turn OFF relay {outputNumber}. Expected response: 55 (85 decimal), got: {responseHex}");
                    throw new InvalidOperationException($"Invalid response from relay controller: expected 85, got {responseHex}");
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

        public Task<bool> GetOutputStateAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            
            // Return cached state for now
            // TODO: Could implement actual state query command if available
            return Task.FromResult(_outputStates[outputNumber - 1]);
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