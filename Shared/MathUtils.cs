using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Shared
{
    public static class MathUtils
    {
        public static double Interpolate(double y1, double y2, double x, double x1, double x2)
        {
            if (x1 == x2) return y1;
            return y1 + ((x - x1) / (x2 - x1)) * (y2 - y1);
        }

        public static SpectrumReading NormalizeSpectrumReading(SpectrumReading input)
        {
            const int startWavelength = 380;
            const int finishWavelength = 780;
            const int spectrometerWavelengths = finishWavelength - startWavelength + 1;
            
            double[] wavelengths = input.Wavelengths;
            double[] spd = input.Intensities;
            double[] spds = new double[spectrometerWavelengths];
            double[] normalizedWavelengths = new double[spectrometerWavelengths];
            
            // Create normalized wavelength array (380 to 780 in 1nm increments)
            for (int i = 0; i < spectrometerWavelengths; i++)
            {
                normalizedWavelengths[i] = startWavelength + i;
            }
            
            int cnt = 0;
            int actualWavelength = startWavelength;
            bool done = false;
            
            while (!done)
            {
                // Handle case where start wavelength is larger than the starting wavelength provided
                if (cnt == 0 && wavelengths[cnt] > actualWavelength)
                {
                    while (!((wavelengths[cnt] <= actualWavelength && wavelengths[cnt + 1] > actualWavelength) || cnt + 1 >= wavelengths.Length - 1))
                    {
                        spds[actualWavelength - startWavelength] = 0;
                        actualWavelength++;
                    }
                }
                
                // Find the appropriate wavelength range for interpolation
                while (!((wavelengths[cnt] <= actualWavelength && wavelengths[cnt + 1] > actualWavelength) || cnt + 1 >= wavelengths.Length - 1))
                {
                    cnt++;
                }
                
                // Perform interpolation if we're within the wavelength range
                if (wavelengths[cnt] <= actualWavelength && wavelengths[cnt + 1] > actualWavelength)
                {
                    spds[actualWavelength - startWavelength] = Interpolate(spd[cnt], spd[cnt + 1], actualWavelength, wavelengths[cnt], wavelengths[cnt + 1]);
                    actualWavelength++;
                }
                
                // Handle end of wavelength data
                if (cnt + 1 >= wavelengths.Length - 1)
                {
                    if (actualWavelength == finishWavelength && cnt + 1 == wavelengths.Length - 1)
                    {
                        spds[actualWavelength - startWavelength] = spd[wavelengths.Length - 1];
                    }
                    
                    // Fill remaining wavelengths with 0
                    while (actualWavelength <= finishWavelength)
                    {
                        if (actualWavelength - startWavelength < spds.Length)
                        {
                            spds[actualWavelength - startWavelength] = 0;
                        }
                        actualWavelength++;
                    }
                }
                
                if (actualWavelength > finishWavelength)
                {
                    done = true;
                }
            }
            
            return new SpectrumReading
            {
                Wavelengths = normalizedWavelengths,
                Intensities = spds,
                Timestamp = input.Timestamp
            };
        }
    }
}