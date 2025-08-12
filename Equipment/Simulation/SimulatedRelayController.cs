using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedRelayController : IRelayController
    {
        private readonly bool[] _outputStates;
        private readonly Random _random = new();
        private readonly byte[] _analogBaseline;

        public int MaxOutputChannels { get; } = 8;
        public int MaxInputChannels { get; } = 8;
        public bool IsConnected { get; private set; } = false;

        public SimulatedRelayController()
        {
            _outputStates = new bool[MaxOutputChannels];
            _analogBaseline = new byte[MaxInputChannels];
            
            // Initialize random baseline values for analog inputs
            for (int i = 0; i < MaxInputChannels; i++)
            {
                _analogBaseline[i] = (byte)_random.Next(100, 180);
            }
        }

        public Task<bool> InitializeAsync()
        {
            IsConnected = true;
            
            // Reset all outputs to off
            for (int i = 0; i < MaxOutputChannels; i++)
            {
                _outputStates[i] = false;
            }
            
            return Task.FromResult(IsConnected);
        }

        public Task TurnOutputOnAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            _outputStates[outputNumber - 1] = true;
            return Task.CompletedTask;
        }

        public Task TurnOutputOffAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
            _outputStates[outputNumber - 1] = false;
            return Task.CompletedTask;
        }

        public Task<byte> ReadAnalogValueAsync(int inputNumber)
        {
            ValidateInputNumber(inputNumber);
            
            // Simulate analog reading with baseline + noise
            var baseline = _analogBaseline[inputNumber - 1];
            var noise = (_random.NextDouble() - 0.5) * 10; // ±5 units of noise
            var value = (int)(baseline + noise);
            
            // Clamp to valid range (0-255)
            value = Math.Max(0, Math.Min(255, value));
            
            return Task.FromResult((byte)value);
        }

        public Task<bool> GetOutputStateAsync(int outputNumber)
        {
            ValidateOutputNumber(outputNumber);
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