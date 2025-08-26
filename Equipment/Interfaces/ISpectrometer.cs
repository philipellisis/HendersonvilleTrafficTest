namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public class SpectrumReading
    {
        public double[] Wavelengths { get; set; } = Array.Empty<double>();
        public double[] Intensities { get; set; } = Array.Empty<double>();
        public DateTime Timestamp { get; set; }
    }

    public interface ISpectrometer
    {
        Task InitializeAsync();
        Task<SpectrumReading> GetSpectrumReadingAsync();
        Task AutoRangeAsync();
        Task SetIntegrationTimeAsync(uint integrationTimeMicros);
        bool IsConnected { get; }
    }
}