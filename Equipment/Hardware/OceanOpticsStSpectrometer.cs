using HendersonvilleTrafficTest.Equipment.Interfaces;
using NetOceanDirect;
using System;
using System.Threading.Tasks;
using HendersonvilleTrafficTest.Shared;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class OceanOpticsStSpectrometer : ISpectrometer
    {
        private const uint integrationTime = 1000;
        private OceanDirect? _ocean;
        private int _deviceId = -1;
        private double[] _wavelengths = Array.Empty<double>();
        private bool _disposed = false;

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

                    IsConnected = true;
                }
                catch (Exception)
                {
                    IsConnected = false;
                    throw;
                }
            });
        }

        public async Task<SpectrumReading> GetSpectrumReadingAsync()
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

                    SpectrumReading reading = new SpectrumReading
                    {
                        Wavelengths = _wavelengths,
                        Intensities = spectrum,
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

        public Task<uint> AutoRangeAsync()
        {
            return Task.FromResult(integrationTime);
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