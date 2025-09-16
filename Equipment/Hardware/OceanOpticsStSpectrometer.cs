using HendersonvilleTrafficTest.Equipment.Interfaces;
using NetOceanDirect;
using System;
using System.Threading.Tasks;
using HendersonvilleTrafficTest.Shared;
using HendersonvilleTrafficTest.Configuration;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class OceanOpticsStSpectrometer : ISpectrometer
    {
        private const uint integrationTime = 1000;
        private OceanDirect? _ocean;
        private int _deviceId = -1;
        private double[] _wavelengths = Array.Empty<double>();
        private bool _disposed = false;
        private uint _currentIntegrationTimeMicros = integrationTime;

        public bool IsConnected { get; private set; } = false;

        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    _ocean = OceanDirect.getInstance();
                    Devices[] devices = _ocean.findDevices();

                    if (devices.Length == 0)
                    {
                        throw new InvalidOperationException("No Ocean Optics spectrometers found");
                    }

                    _deviceId = devices[0].Id;
                    int errorCode = 0;
                    
                    _ocean.openDevice(_deviceId, ref errorCode);
                    
                    if (errorCode > 0 && errorCode < 10001)
                    {
                        throw new InvalidOperationException($"Failed to open Ocean Optics spectrometer. Error code: {errorCode}");
                    }

                    int numberOfPixels = _ocean.getNumberOfPixels(_deviceId, ref errorCode);
                    if (errorCode > 0 && errorCode < 10001)
                    {
                        throw new InvalidOperationException($"Could not read number of pixels. Error code: {errorCode}");
                    }

                    _wavelengths = _ocean.getWavelengths(_deviceId, ref errorCode);
                    if (errorCode > 0 && errorCode < 10001)
                    {
                        throw new InvalidOperationException($"Could not read wavelengths. Error code: {errorCode}");
                    }


                    uint integrationTime = 200000;
                    _ocean.setIntegrationTimeMicros(_deviceId, ref errorCode, integrationTime);
                    if (errorCode > 0 && errorCode < 10001)
                    {
                        throw new InvalidOperationException($"Could not set integration time. Error code: {errorCode}");
                    }
                    _currentIntegrationTimeMicros = integrationTime;

                    IsConnected = true;
                }
                catch (Exception)
                {
                    IsConnected = false;
                    throw;
                }
            });
        }

        public async Task<SpectrumReading> GetSpectrumReadingAsync(double? maxReadTimeSeconds = null)
        {
            if (!IsConnected || _ocean == null || _deviceId == -1)
            {
                throw new InvalidOperationException("Spectrometer not initialized or connected");
            }

            // Use provided maxReadTimeSeconds or fall back to configuration
            double actualMaxReadTime = maxReadTimeSeconds ?? ConfigurationManager.Current.Equipment.SpectrometerMaxReadTimeSeconds;
            
            // Calculate number of readings to average based on integration time and max read time
            double integrationTimeSeconds = _currentIntegrationTimeMicros / 1_000_000.0; // Convert microseconds to seconds
            int numberOfReadings = Math.Max(1, (int)(actualMaxReadTime / integrationTimeSeconds));

            return await Task.Run(() =>
            {
                try
                {
                    // Initialize arrays for averaging
                    double[]? averagedSpectrum = null;
                    
                    for (int i = 0; i < numberOfReadings; i++)
                    {
                        int errorCode = 0;
                        double[] spectrum = _ocean.getSpectrum(_deviceId, ref errorCode);
                        
                        if (errorCode != 0)
                        {
                            throw new InvalidOperationException($"Could not read spectrum. Error code: {errorCode}");
                        }

                        // Initialize averaged spectrum on first reading
                        if (averagedSpectrum == null)
                        {
                            averagedSpectrum = new double[spectrum.Length];
                            Array.Copy(spectrum, averagedSpectrum, spectrum.Length);
                        }
                        else
                        {
                            // Add current spectrum to running average
                            for (int j = 0; j < spectrum.Length && j < averagedSpectrum.Length; j++)
                            {
                                averagedSpectrum[j] += spectrum[j];
                            }
                        }
                    }

                    // Calculate final average
                    if (averagedSpectrum != null && numberOfReadings > 1)
                    {
                        for (int j = 0; j < averagedSpectrum.Length; j++)
                        {
                            averagedSpectrum[j] /= numberOfReadings;
                        }
                    }

                    SpectrumReading reading = new SpectrumReading
                    {
                        Wavelengths = _wavelengths,
                        Intensities = averagedSpectrum ?? Array.Empty<double>(),
                        Timestamp = DateTime.Now
                    };
                    return MathUtils.NormalizeSpectrumReading(reading);
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }

        private async Task<double[]> GetRawSpectrumAsync()
        {
            if (!IsConnected || _ocean == null || _deviceId == -1)
            {
                throw new InvalidOperationException("Spectrometer not initialized or connected");
            }

            return await Task.Run(() =>
            {
                try
                {
                    int errorCode = 0;
                    double[] spectrum = _ocean.getSpectrum(_deviceId, ref errorCode);
                    
                    if (errorCode != 0)
                    {
                        throw new InvalidOperationException($"Could not read spectrum. Error code: {errorCode}");
                    }

                    return spectrum;
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }

        public async Task<uint> AutoRangeAsync()
        {
            if (!IsConnected || _ocean == null || _deviceId == -1)
            {
                throw new InvalidOperationException("Spectrometer not initialized or connected");
            }

            var config = ConfigurationManager.Current.Equipment;
            uint minIntegrationTime = config.SpectrometerMinIntegrationTimeMs * 1000; // Convert ms to microseconds
            uint maxIntegrationTime = config.SpectrometerMaxIntegrationTimeMs * 1000; // Convert ms to microseconds
            
            // Start with minimum integration time
            uint currentIntegrationTime = minIntegrationTime;
            
            // Get maximum counts value for the spectrometer (typically 65535 for 16-bit or 4095 for 12-bit)
            const double maxCounts = 65535.0;
            const double targetMinPercent = 0.50; // 50% of max counts
            const double targetMaxPercent = 0.95; // 95% of max counts
            
            const int maxIterations = 20; // Prevent infinite loops
            int iteration = 0;
            
            while (iteration < maxIterations)
            {
                // Set the current integration time
                await SetIntegrationTimeAsync(currentIntegrationTime);
                
                // Take a measurement
                var spectrum = await GetRawSpectrumAsync();
                
                // Find the maximum intensity in the spectrum
                double maxIntensity = 0;
                foreach (double intensity in spectrum)
                {
                    if (intensity > maxIntensity)
                        maxIntensity = intensity;
                }
                
                // Calculate percentage of maximum counts
                double intensityPercent = maxIntensity / maxCounts;
                
                // Check if we're in the target range (50%-95%)
                if (intensityPercent >= targetMinPercent && intensityPercent <= targetMaxPercent)
                {
                    // We found a good integration time
                    return currentIntegrationTime;
                }
                else if (intensityPercent < targetMinPercent)
                {
                    // Signal too weak, increase integration time
                    uint newIntegrationTime = (uint)(currentIntegrationTime * 1.5);
                    if (newIntegrationTime > maxIntegrationTime)
                    {
                        // We've reached maximum, use it
                        currentIntegrationTime = maxIntegrationTime;
                        await SetIntegrationTimeAsync(currentIntegrationTime);
                        return currentIntegrationTime;
                    }
                    currentIntegrationTime = newIntegrationTime;
                }
                else if (intensityPercent > targetMaxPercent)
                {
                    // Signal too strong, decrease integration time
                    uint newIntegrationTime = (uint)(currentIntegrationTime * 0.7);
                    if (newIntegrationTime < minIntegrationTime)
                    {
                        // We've reached minimum, use it
                        currentIntegrationTime = minIntegrationTime;
                        await SetIntegrationTimeAsync(currentIntegrationTime);
                        return currentIntegrationTime;
                    }
                    currentIntegrationTime = newIntegrationTime;
                }
                
                iteration++;
            }
            
            // If we couldn't find optimal range after max iterations, return current value
            return currentIntegrationTime;
        }

        public async Task SetIntegrationTimeAsync(uint integrationTimeMicros)
        {
            if (!IsConnected || _ocean == null || _deviceId == -1)
            {
                throw new InvalidOperationException("Spectrometer not initialized or connected");
            }

            await Task.Run(() =>
            {
                int errorCode = 0;
                
                _ocean.setIntegrationTimeMicros(_deviceId, ref errorCode, integrationTimeMicros);
                
                if (errorCode > 0 && errorCode < 10001)
                {
                    throw new InvalidOperationException($"Could not set integration time. Error code: {errorCode}");
                }
                
                _currentIntegrationTimeMicros = integrationTimeMicros;
            });
        }

        public void Dispose()
        {
            if (!_disposed && _ocean != null && _deviceId != -1)
            {
                try
                {
                    int errorCode = 0;
                    _ocean.closeDevice(_deviceId, ref errorCode);
                    _ocean.shutDown();
                }
                catch
                {
                }
                finally
                {
                    _disposed = true;
                    IsConnected = false;
                }
            }
        }

        ~OceanOpticsStSpectrometer()
        {
            Dispose();
        }
    }
}