using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedAcPowerSupply : IAcPowerSupply
    {
        private double _volts = 0.0;
        private double _frequency = 60.0;
        private bool _isPoweredOn = false;
        private readonly Random _random = new();

        public bool IsConnected { get; private set; } = false;

        public Task InitializeAsync()
        {
            IsConnected = true;
            return Task.CompletedTask;
        }

        public Task<double> GetVoltsAsync()
        {
            var noise = (_random.NextDouble() - 0.5) * 0.1;
            return Task.FromResult(_volts + noise);
        }

        public Task SetVoltsAsync(double volts)
        {
            _volts = Math.Max(0, Math.Min(300, volts));
            return Task.CompletedTask;
        }

        public Task<double> GetFrequencyAsync()
        {
            var noise = (_random.NextDouble() - 0.5) * 0.1;
            return Task.FromResult(_frequency + noise);
        }

        public Task SetFrequencyAsync(double frequency)
        {
            _frequency = Math.Max(40, Math.Min(400, frequency));
            return Task.CompletedTask;
        }

        public Task PowerOnAsync()
        {
            _isPoweredOn = true;
            return Task.CompletedTask;
        }

        public Task PowerOffAsync()
        {
            _isPoweredOn = false;
            return Task.CompletedTask;
        }
    }
}