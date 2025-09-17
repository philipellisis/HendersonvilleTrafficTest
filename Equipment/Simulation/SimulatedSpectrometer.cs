using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Simulation
{
    public class SimulatedSpectrometer : ISpectrometer
    {
        private readonly Random _random = new();
        private const uint integrationTime = 1000;
        private uint _currentIntegrationTimeMicros = integrationTime;
        private const double MinWavelength = 380.0;
        private const double MaxWavelength = 780.0;
        
        public bool IsConnected { get; private set; } = false;
        public uint CurrentIntegrationTimeMicros => _currentIntegrationTimeMicros;

        public Task InitializeAsync()
        {
            IsConnected = true;
            return Task.CompletedTask;
        }

        public Task<SpectrumReading> GetSpectrumReadingAsync(double? maxReadTimeSeconds = null)
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

        public Task<uint> AutoRangeAsync()
        {
            return Task.FromResult(integrationTime);
        }

        public Task SetIntegrationTimeAsync(uint integrationTimeMicros)
        {
            _currentIntegrationTimeMicros = integrationTimeMicros;
            return Task.CompletedTask;
        }

        public async Task CalibrateDarkCurrentAsync(IProgress<string> progress, int maxIntegrationTimeSeconds, double waitBeforeDarkSeconds, CancellationToken cancellationToken)
        {
            // Simulate dark current calibration
            progress?.Report($"Simulating warmup for {waitBeforeDarkSeconds} seconds");
            await Task.Delay(TimeSpan.FromSeconds(Math.Min(waitBeforeDarkSeconds, 2)), cancellationToken);
            
            // Simulate calibration process
            for (int i = 1; i <= 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                progress?.Report($"Simulating calibration scan {i} of 10");
                await Task.Delay(200, cancellationToken);
            }
            
            progress?.Report("Simulated dark current calibration completed");
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