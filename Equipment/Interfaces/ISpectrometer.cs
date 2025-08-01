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
        Task SetRangeAsync(double minWavelength, double maxWavelength);
        double MinWavelength { get; }
        double MaxWavelength { get; }
        bool IsConnected { get; }
    }
}