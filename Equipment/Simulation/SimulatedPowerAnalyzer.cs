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

            var measurement = new ElectricalMeasurement(voltage, current, power, frequency);
            return Task.FromResult(measurement);
        }
    }
}