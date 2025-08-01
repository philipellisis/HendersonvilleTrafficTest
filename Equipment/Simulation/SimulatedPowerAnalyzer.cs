using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedPowerAnalyzer : IPowerAnalyzer
    {
        private PowerMode _mode = PowerMode.AC;
        private readonly Random _random = new();

        public bool IsConnected { get; private set; } = false;

        public Task InitializeAsync()
        {
            IsConnected = true;
            return Task.CompletedTask;
        }

        public Task SetModeAsync(PowerMode mode)
        {
            _mode = mode;
            return Task.CompletedTask;
        }

        public Task<double> GetVoltsAsync()
        {
            var baseVoltage = _mode == PowerMode.AC ? 120.0 : 12.0;
            var noise = (_random.NextDouble() - 0.5) * 0.5;
            return Task.FromResult(baseVoltage + noise);
        }

        public Task<double> GetAmpsAsync()
        {
            var baseCurrent = _mode == PowerMode.AC ? 2.5 : 5.0;
            var noise = (_random.NextDouble() - 0.5) * 0.1;
            return Task.FromResult(baseCurrent + noise);
        }

        public Task<double> GetWattsAsync()
        {
            var baseWattage = _mode == PowerMode.AC ? 250.0 : 50.0;
            var noise = (_random.NextDouble() - 0.5) * 5.0;
            return Task.FromResult(baseWattage + noise);
        }

        public Task<double> GetPowerFactorAsync()
        {
            if (_mode == PowerMode.DC)
                return Task.FromResult(1.0);

            var basePF = 0.85;
            var noise = (_random.NextDouble() - 0.5) * 0.05;
            return Task.FromResult(Math.Max(0.1, Math.Min(1.0, basePF + noise)));
        }

        public Task<double> GetFrequencyAsync()
        {
            if (_mode == PowerMode.DC)
                return Task.FromResult(0.0);

            var baseFreq = 60.0;
            var noise = (_random.NextDouble() - 0.5) * 0.1;
            return Task.FromResult(baseFreq + noise);
        }
    }
}