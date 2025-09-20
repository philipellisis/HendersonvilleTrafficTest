using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedDcPowerSupply : IDcPowerSupply
    {
        private static bool _isInitialized = false;
        private static readonly object _initLock = new object();
        
        private double _volts = 0.0;
        private double _amps = 0.0;
        private bool _isPoweredOn = false;
        private readonly Random _random = new();

        public bool IsConnected { get; private set; } = false;

        public Task InitializeAsync()
        {
            lock (_initLock)
            {
                if (_isInitialized)
                {
                    return Task.CompletedTask; // Already initialized, skip
                }
                
                IsConnected = true;
                _isInitialized = true;
            }
            
            return Task.CompletedTask;
        }

        public Task<double> GetVoltsAsync()
        {
            var noise = (_random.NextDouble() - 0.5) * 0.05;
            return Task.FromResult(_volts + noise);
        }

        public Task SetVoltsAsync(double volts)
        {
            _volts = Math.Max(0, Math.Min(60, volts));
            return Task.CompletedTask;
        }

        public Task<double> GetAmpsAsync()
        {
            var noise = (_random.NextDouble() - 0.5) * 0.01;
            return Task.FromResult(_amps + noise);
        }

        public Task SetAmpsAsync(double amps)
        {
            _amps = Math.Max(0, Math.Min(20, amps));
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