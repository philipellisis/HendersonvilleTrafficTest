using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedSpectrometer : ISpectrometer
    {
        private readonly Random _random = new();
        private const double MinWavelength = 380.0;
        private const double MaxWavelength = 780.0;
        
        public bool IsConnected { get; private set; } = false;

        public Task InitializeAsync()
        {
            IsConnected = true;
            return Task.CompletedTask;
        }

        public Task<SpectrumReading> GetSpectrumReadingAsync()
        {
            const int dataPoints = 401;
            var wavelengths = new double[dataPoints];
            var intensities = new double[dataPoints];

            for (int i = 0; i < dataPoints; i++)
            {
                wavelengths[i] = MinWavelength + (MaxWavelength - MinWavelength) * i / (dataPoints - 1);
                
                var baseIntensity = GenerateRealisticSpectrum(wavelengths[i]);
                var noise = (_random.NextDouble() - 0.5) * 0.1 * baseIntensity;
                intensities[i] = Math.Max(0, baseIntensity + noise);
            }

            var reading = new SpectrumReading
            {
                Wavelengths = wavelengths,
                Intensities = intensities,
                Timestamp = DateTime.Now
            };

            return Task.FromResult(reading);
        }

        public Task AutoRangeAsync()
        {
            return Task.CompletedTask;
        }

        public Task SetIntegrationTimeAsync(uint integrationTimeMicros)
        {
            return Task.CompletedTask;
        }

        private double GenerateRealisticSpectrum(double wavelength)
        {
            var red = Math.Exp(-Math.Pow((wavelength - 650) / 50, 2)) * 0.8;
            var green = Math.Exp(-Math.Pow((wavelength - 550) / 40, 2)) * 0.6;
            var blue = Math.Exp(-Math.Pow((wavelength - 450) / 35, 2)) * 0.4;
            var warmWhite = Math.Exp(-Math.Pow((wavelength - 580) / 80, 2)) * 0.5;
            
            return (red + green + blue + warmWhite) * 1000;
        }
    }
}