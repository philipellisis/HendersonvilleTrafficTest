using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedTemperatureSensor : ITemperatureSensor
    {
        private readonly Random _random = new();
        private double _baseTemperature = 22.0;

        public bool IsConnected { get; private set; } = false;

        public Task InitializeAsync()
        {
            IsConnected = true;
            return Task.CompletedTask;
        }

        public Task<double> GetTemperatureAsync()
        {
            var drift = Math.Sin(DateTime.Now.TimeOfDay.TotalHours * Math.PI / 12) * 2.0;
            var noise = (_random.NextDouble() - 0.5) * 0.5;
            var temperature = _baseTemperature + drift + noise;
            
            return Task.FromResult(temperature);
        }
    }
}