using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedTemperatureSensor : ITemperatureSensor
    {
        private readonly Random _random = new();
        private double _baseTemperature = 22.0;
        private double _baseHumidity = 45.0;

        public bool IsConnected { get; private set; } = false;
        public TemperatureSensorType SensorType { get; set; } = TemperatureSensorType.OHT20;

        public Task<bool> InitializeAsync()
        {
            IsConnected = true;
            return Task.FromResult(true);
        }

        public Task<double> GetTemperatureAsync()
        {
            var drift = Math.Sin(DateTime.Now.TimeOfDay.TotalHours * Math.PI / 12) * 2.0;
            var noise = (_random.NextDouble() - 0.5) * 0.5;
            var temperature = _baseTemperature + drift + noise;
            
            return Task.FromResult(temperature);
        }

        public async Task<TemperatureReading> GetFullReadingAsync()
        {
            var temperature = await GetTemperatureAsync();
            
            var reading = new TemperatureReading
            {
                Temperature = temperature,
                IsTemperatureValid = true,
                ErrorCount = 0,
                HasErrorOverflow = false,
                IsHeatingOn = false
            };

            // Add humidity for OHT20 sensor type
            if (SensorType == TemperatureSensorType.OHT20)
            {
                var humidityDrift = Math.Sin(DateTime.Now.TimeOfDay.TotalHours * Math.PI / 8) * 10.0;
                var humidityNoise = (_random.NextDouble() - 0.5) * 2.0;
                reading.Humidity = Math.Max(0, Math.Min(100, _baseHumidity + humidityDrift + humidityNoise));
                reading.IsHumidityValid = true;
            }
            else
            {
                reading.Humidity = null;
                reading.IsHumidityValid = false;
            }

            return reading;
        }
    }
}