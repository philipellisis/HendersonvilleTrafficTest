using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Configuration;

namespace HendersonvilleTrafficTest.Shared
{
    public static class MathUtils
    {
        public static double Interpolate(double y1, double y2, double x, double x1, double x2)
        {
            if (x1 == x2) return y1;
            return y1 + ((x - x1) / (x2 - x1)) * (y2 - y1);
        }

        public static SpectrumReading NormalizeSpectrumReading(SpectrumReading input, SpectrumReading? darkCurrent = null)
        {
            const int startWavelength = 380;
            const int finishWavelength = 780;
            const int spectrometerWavelengths = finishWavelength - startWavelength + 1;
            
            double[] wavelengths = input.Wavelengths;
            double[] intensities = input.Intensities;
            double[] normalizedIntensities = new double[spectrometerWavelengths];
            double[] normalizedWavelengths = new double[spectrometerWavelengths];
            
            // Create normalized wavelength array (380 to 780 in 1nm increments)
            for (int i = 0; i < spectrometerWavelengths; i++)
            {
                normalizedWavelengths[i] = startWavelength + i;
            }
            
            // Initialize accumulation arrays
            double[] accumulator = new double[spectrometerWavelengths];
            int[] counts = new int[spectrometerWavelengths];
            
            // Accumulate and count values for each integer wavelength
            for (int index = 0; index < wavelengths.Length; index++)
            {
                int roundedWavelength = (int)Math.Round(wavelengths[index], 0);
                
                // Check if wavelength is within our range
                if (roundedWavelength >= startWavelength && roundedWavelength <= finishWavelength)
                {
                    int arrayIndex = roundedWavelength - startWavelength;
                    
                    // Subtract dark current if provided
                    double correctedIntensity = intensities[index];
                    if (darkCurrent != null && index < darkCurrent.Intensities.Length)
                    {
                        correctedIntensity -= darkCurrent.Intensities[index];
                    }
                    
                    accumulator[arrayIndex] += correctedIntensity;
                    counts[arrayIndex]++;
                }
            }
            
            // Calculate averages for each wavelength
            for (int i = 0; i < spectrometerWavelengths; i++)
            {
                if (counts[i] > 0)
                {
                    normalizedIntensities[i] = accumulator[i] / counts[i];
                    
                    // Ensure non-negative values
                    if (normalizedIntensities[i] < 0)
                    {
                        normalizedIntensities[i] = 0;
                    }
                }
                else
                {
                    // No data for this wavelength, set to 0
                    normalizedIntensities[i] = 0;
                }
            }
            
            return new SpectrumReading
            {
                Wavelengths = normalizedWavelengths,
                Intensities = normalizedIntensities,
                Timestamp = input.Timestamp
            };
        }

        public static SpectrumReading ApplyCalibrationFactors(SpectrumReading normalizedReading, uint integrationTimeMicros)
        {
            var calibrationFactors = ConfigurationManager.Current.Calibration.SpectrometerCalibrationFactors;
            
            if (normalizedReading.Intensities.Length != calibrationFactors.Length)
            {
                throw new ArgumentException("Normalized reading intensities length must match calibration factors length (401 values)");
            }

            double[] calibratedIntensities = new double[normalizedReading.Intensities.Length];
            double integrationTimeSeconds = integrationTimeMicros / 1_000_000.0; // Convert microseconds to seconds

            for (int idx = 0; idx < normalizedReading.Intensities.Length; idx++)
            {
                calibratedIntensities[idx] = normalizedReading.Intensities[idx] * (calibrationFactors[idx] / integrationTimeSeconds);
            }

            return new SpectrumReading
            {
                Wavelengths = normalizedReading.Wavelengths,
                Intensities = calibratedIntensities,
                Timestamp = normalizedReading.Timestamp
            };
        }
    }
}