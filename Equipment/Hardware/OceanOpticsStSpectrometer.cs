using HendersonvilleTrafficTest.Equipment.Interfaces;
using NetOceanDirect;
using System;
using System.Threading.Tasks;
using HendersonvilleTrafficTest.Shared;
using HendersonvilleTrafficTest.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class OceanOpticsStSpectrometer : ISpectrometer
    {
        private static bool _isInitialized = false;
        private static readonly object _initLock = new object();
        
        private const uint integrationTime = 1000;
        private OceanDirect? _ocean;
        private int _deviceId = -1;
        private double[] _wavelengths = Array.Empty<double>();
        private bool _disposed = false;
        private uint _currentIntegrationTimeMicros = integrationTime;

        public bool IsConnected { get; private set; } = false;
        public uint CurrentIntegrationTimeMicros => _currentIntegrationTimeMicros;

        public async Task InitializeAsync()
        {
            lock (_initLock)
            {
                if (_isInitialized)
                {
                    return; // Already initialized, skip
                }
            }

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
                    
                    lock (_initLock)
                    {
                        _isInitialized = true;
                    }
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
                    
                    // Apply dark current correction if configured
                    var correctedReading = MathUtils.ApplyDarkCurrentCorrection(reading, _currentIntegrationTimeMicros);
                    return MathUtils.NormalizeSpectrumReading(correctedReading);
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

            // Round to nearest millisecond (1000 microseconds) to avoid precision errors
            uint roundedIntegrationTime = (uint)(Math.Round(integrationTimeMicros / 1000.0) * 1000);

            await Task.Run(() =>
            {
                int errorCode = 0;
                
                _ocean.setIntegrationTimeMicros(_deviceId, ref errorCode, roundedIntegrationTime);
                
                if (errorCode > 0 && errorCode < 10001)
                {
                    throw new InvalidOperationException($"Could not set integration time. Error code: {errorCode}");
                }
                
                _currentIntegrationTimeMicros = roundedIntegrationTime;
            });
        }

        public async Task CalibrateDarkCurrentAsync(IProgress<string> progress, int maxIntegrationTimeSeconds, double waitBeforeDarkSeconds, CancellationToken cancellationToken)
        {
            if (!IsConnected || _ocean == null || _deviceId == -1)
            {
                throw new InvalidOperationException("Spectrometer not initialized or connected");
            }

            var darkScans = new List<DarkScan>();
            
            // Wait for warmup period
            progress?.Report($"Waiting for warmup, {waitBeforeDarkSeconds} seconds remaining");
            var startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalSeconds < waitBeforeDarkSeconds)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var remaining = waitBeforeDarkSeconds - (DateTime.Now - startTime).TotalSeconds;
                progress?.Report($"Waiting for warmup, {remaining:F1} seconds remaining");
                await Task.Delay(100, cancellationToken);
            }

            // Define integration times (in microseconds) similar to VB implementation
            var integrationTimes = new List<uint>
            {
                1000, 2000, 4000, 8000, 16000, 32000, 64000, 96000, 128000, 256000, 384000, 512000, 640000, 768000, 896000, 1024000, 1152000, 1280000, 1408000, 1536000, 1664000, 1792000, 1920000, 2048000, 2176000, 2304000, 2432000, 2560000, 2688000, 2816000, 2944000, 3072000, 3328000, 3584000, 3840000, 4096000, 4352000, 4608000, 4864000, 5120000, 5376000, 5632000, 5888000, 6144000, 6400000, 6656000, 7168000, 7680000, 8192000, 8704000, 9216000, 9728000, 10000000
            };

            // Filter to only include times up to max integration time
            var filteredTimes = integrationTimes.Where(t => t <= maxIntegrationTimeSeconds * 1000000u).ToList();
            
            // Add additional 1-second increments up to max time
            for (int i = 11; i <= maxIntegrationTimeSeconds; i++)
            {
                filteredTimes.Add((uint)(i * 1000000));
            }

            int scanCount = 0;
            foreach (var integrationTimeMicros in filteredTimes)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                progress?.Report($"Calibrating Dark Current, Scan {scanCount + 1} of {filteredTimes.Count}");
                
                // Set integration time
                await SetIntegrationTimeAsync(integrationTimeMicros);
                
                // Calculate number of scans to average (minimum 10, but ensure at least 1 second total time)
                int numberScans = Math.Max(10, (int)(1000000 / integrationTimeMicros));
                // But don't exceed 30 seconds total measurement time
                numberScans = Math.Min(numberScans, (int)(30000000 / integrationTimeMicros));
                numberScans = Math.Max(1, numberScans); // Ensure at least 1 scan

                // Take multiple dark readings and average them
                var darkSpectrum = new double[_wavelengths.Length];
                for (int i = 0; i < numberScans; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var spectrum = await GetRawSpectrumAsync();
                    for (int j = 0; j < spectrum.Length && j < darkSpectrum.Length; j++)
                    {
                        darkSpectrum[j] += spectrum[j];
                    }
                }

                // Average the readings
                for (int j = 0; j < darkSpectrum.Length; j++)
                {
                    darkSpectrum[j] /= numberScans;
                }

                // Apply smoothing (average over +/- 10 points)
                var smoothedSpectrum = new double[darkSpectrum.Length];
                for (int j = 0; j < darkSpectrum.Length; j++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int k = Math.Max(0, j - 10); k <= Math.Min(darkSpectrum.Length - 1, j + 10); k++)
                    {
                        sum += darkSpectrum[k];
                        count++;
                    }
                    smoothedSpectrum[j] = sum / count;
                }

                darkScans.Add(new DarkScan(smoothedSpectrum, integrationTimeMicros));
                scanCount++;
            }

            // Convert dark scans to string format and save to configuration
            var darkCurrentData = ConvertDarkScansToString(darkScans);
            ConfigurationManager.Current.Equipment.DarkCurrentCalibrationData = darkCurrentData;
            ConfigurationManager.SaveConfiguration();

            progress?.Report("Dark current calibration completed and saved");
        }

        private string ConvertDarkScansToString(List<DarkScan> darkScans)
        {
            var sb = new StringBuilder();
            
            foreach (var darkScan in darkScans)
            {
                if (sb.Length > 0)
                    sb.AppendLine();
                    
                sb.Append(darkScan.IntegrationTimeMicros.ToString());
                
                foreach (var intensity in darkScan.Intensity)
                {
                    sb.Append($" {intensity:E}");
                }
            }
            
            return sb.ToString();
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