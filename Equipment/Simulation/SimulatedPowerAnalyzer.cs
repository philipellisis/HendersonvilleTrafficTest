using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedPowerAnalyzer : IPowerAnalyzer
    {
        private static bool _isInitialized = false;
        private static readonly object _initLock = new object();
        
        private PowerMode _mode = PowerMode.AC;
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

        public Task SetModeAsync(PowerMode mode)
        {
            _mode = mode;
            return Task.CompletedTask;
        }

        public Task<ElectricalMeasurement> GetElectricalsAsync()
        {
            var baseVoltage = _mode == PowerMode.AC ? 120.0 : 12.0;
            var voltageNoise = (_random.NextDouble() - 0.5) * 0.5;
            var voltage = baseVoltage + voltageNoise;

            var baseCurrent = _mode == PowerMode.AC ? 2.5 : 5.0;
            var currentNoise = (_random.NextDouble() - 0.5) * 0.1;
            var current = baseCurrent + currentNoise;

            var baseWattage = _mode == PowerMode.AC ? 250.0 : 50.0;
            var wattageNoise = (_random.NextDouble() - 0.5) * 5.0;
            var power = baseWattage + wattageNoise;

            var frequency = _mode == PowerMode.DC ? 0.0 : 60.0 + (_random.NextDouble() - 0.5) * 0.1;

            // Simulate THD (Total Harmonic Distortion) - typically 1-5% for AC, 0% for DC
            var baseTHD = _mode == PowerMode.AC ? 2.5 : 0.0;
            var thdNoise = _mode == PowerMode.AC ? (_random.NextDouble() - 0.5) * 1.0 : 0.0;
            var thd = Math.Max(0.0, baseTHD + thdNoise);

            // Simulate Power Factor - typically 0.8-1.0 for AC, 1.0 for DC
            var basePowerFactor = _mode == PowerMode.AC ? 0.9 : 1.0;
            var pfNoise = _mode == PowerMode.AC ? (_random.NextDouble() - 0.5) * 0.1 : 0.0;
            var powerFactor = Math.Max(0.0, Math.Min(1.0, basePowerFactor + pfNoise));

            var measurement = new ElectricalMeasurement(voltage, current, power, frequency, thd, powerFactor);
            return Task.FromResult(measurement);
        }
    }
}