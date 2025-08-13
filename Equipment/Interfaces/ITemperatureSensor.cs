namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public enum TemperatureSensorType
    {
        OHT20,  // Temperature and humidity sensor
        OT60,   // Temperature only, range -10 to +60°C
        OT150   // Temperature only, range -50 to +150°C
    }

    public class TemperatureReading
    {
        public double Temperature { get; set; }
        public double? Humidity { get; set; }
        public bool IsTemperatureValid { get; set; }
        public bool IsHumidityValid { get; set; }
        public int ErrorCount { get; set; }
        public bool HasErrorOverflow { get; set; }
        public bool IsHeatingOn { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public interface ITemperatureSensor
    {
        Task<bool> InitializeAsync();
        Task<double> GetTemperatureAsync();
        Task<TemperatureReading> GetFullReadingAsync();
        TemperatureSensorType SensorType { get; set; }
        bool IsConnected { get; }
    }
}