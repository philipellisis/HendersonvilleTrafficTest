using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Shared
{
    /// <summary>
    /// CIE color calculation results
    /// </summary>
    public class CieColorResult
    {
        /// <summary>CIE 1931 chromaticity coordinate x</summary>
        public double CcX { get; set; }
        
        /// <summary>CIE 1931 chromaticity coordinate y</summary>
        public double CcY { get; set; }
        
        /// <summary>CIE 1976 UCS chromaticity coordinate u'</summary>
        public double UPrime { get; set; }
        
        /// <summary>CIE 1976 UCS chromaticity coordinate v'</summary>
        public double VPrime { get; set; }
        
        /// <summary>CIE XYZ tristimulus value X</summary>
        public double X { get; set; }
        
        /// <summary>CIE XYZ tristimulus value Y</summary>
        public double Y { get; set; }
        
        /// <summary>CIE XYZ tristimulus value Z</summary>
        public double Z { get; set; }
        
        /// <summary>Luminance in cd/m² (Y * 683)</summary>
        public double Luminance { get; set; }
        
        /// <summary>Total watts per steradian</summary>
        public double WattsPerSteradian { get; set; }

        public double CCT { get; set; }
    }

    /// <summary>
    /// Static class for calculating CIE color coordinates from spectrum data
    /// </summary>
    public static class CieColorCalculator
    {
        /// <summary>
        /// Standard wavelength range start (380nm) matching CIE2DegreeFunctions array
        /// </summary>
        public const double StartWavelength = 380.0;
        
        /// <summary>
        /// Standard wavelength range end (780nm) matching CIE2DegreeFunctions array
        /// </summary>
        public const double EndWavelength = 780.0;
        
        /// <summary>
        /// Wavelength step (1nm) matching CIE2DegreeFunctions array
        /// </summary>
        public const double WavelengthStep = 1.0;
        
        /// <summary>
        /// Photometric constant (683 lm/W) for converting Y to luminance
        /// </summary>
        public const double PhotometricConstant = 683.0;

        /// <summary>
        /// Calculate CIE color coordinates from a spectrum reading
        /// </summary>
        /// <param name="spectrum">Spectrum data with wavelengths and intensities</param>
        /// <param name="wavelengthStart">Start wavelength for calculation (default: use spectrum range)</param>
        /// <param name="wavelengthEnd">End wavelength for calculation (default: use spectrum range)</param>
        /// <returns>CIE color coordinates and derived values</returns>
        public static CieColorResult CalculateFromSpectrum(SpectrumReading spectrum, 
            double? wavelengthStart = null, double? wavelengthEnd = null)
        {
            if (spectrum == null)
                throw new ArgumentNullException(nameof(spectrum));
            
            if (spectrum.Wavelengths == null || spectrum.Intensities == null)
                throw new ArgumentException("Spectrum must contain wavelength and intensity data");
                
            if (spectrum.Wavelengths.Length != spectrum.Intensities.Length)
                throw new ArgumentException("Wavelength and intensity arrays must have the same length");

            // Determine wavelength range
            var startWave = wavelengthStart ?? Math.Max(spectrum.Wavelengths.Min(), StartWavelength);
            var endWave = wavelengthEnd ?? Math.Min(spectrum.Wavelengths.Max(), EndWavelength);

            return CalculateFromSpectrumData(spectrum.Wavelengths, spectrum.Intensities, startWave, endWave);
        }

        /// <summary>
        /// Calculate CIE color coordinates from wavelength and intensity arrays
        /// </summary>
        /// <param name="wavelengths">Array of wavelengths in nm</param>
        /// <param name="intensities">Array of corresponding intensities</param>
        /// <param name="wavelengthStart">Start wavelength for calculation</param>
        /// <param name="wavelengthEnd">End wavelength for calculation</param>
        /// <returns>CIE color coordinates and derived values</returns>
        public static CieColorResult CalculateFromSpectrumData(double[] wavelengths, double[] intensities,
            double wavelengthStart = 380, double wavelengthEnd = 780)
        {
            if (wavelengths == null) throw new ArgumentNullException(nameof(wavelengths));
            if (intensities == null) throw new ArgumentNullException(nameof(intensities));
            if (wavelengths.Length != intensities.Length)
                throw new ArgumentException("Wavelength and intensity arrays must have the same length");

            double bigX = 0, bigY = 0, bigZ = 0, wsr = 0;

            // Iterate through spectrum data
            for (int cnt = 0; cnt < wavelengths.Length; cnt++)
            {
                var wavelength = wavelengths[cnt];
                var intensity = intensities[cnt];

                // Check if wavelength is in our calculation range
                if (wavelength >= wavelengthStart && wavelength <= wavelengthEnd)
                {
                    // Calculate CIE function index (assumes 1nm steps starting at 380nm)
                    var cieIndex = (int)Math.Round(wavelength - StartWavelength);
                    
                    // Ensure index is within CIE function array bounds
                    if (cieIndex >= 0 && cieIndex < CIE2DegreeFunctions.xbar.Length)
                    {
                        // Calculate XYZ contributions
                        bigX += CIE2DegreeFunctions.xbar[cieIndex] * intensity;
                        bigY += CIE2DegreeFunctions.ybar[cieIndex] * intensity;
                        bigZ += CIE2DegreeFunctions.zbar[cieIndex] * intensity;
                    }
                }
                
                // Calculate total watts per steradian (include all wavelengths)
                wsr += intensity;
            }

            // Create result object
            var result = new CieColorResult
            {
                X = bigX,
                Y = bigY,
                Z = bigZ,
                WattsPerSteradian = wsr,
                Luminance = bigY * PhotometricConstant
            };

            // Calculate chromaticity coordinates (avoid division by zero)
            var sum = bigX + bigY + bigZ;
            if (sum > 0)
            {
                result.CcX = bigX / sum;
                result.CcY = bigY / sum;
                result.CCT = parseCCT(result.CcX, result.CcY);

                // Calculate CIE 1976 UCS coordinates (u', v')
                var denominator = -2 * result.CcX + 12 * result.CcY + 3;
                if (Math.Abs(denominator) > 1e-10) // Avoid division by very small numbers
                {
                    result.UPrime = 4 * result.CcX / denominator;
                    result.VPrime = 6 * result.CcY / denominator;
                }
                else
                {
                    // Handle edge case where denominator is near zero
                    result.UPrime = 0;
                    result.VPrime = 0;
                }
            }
            else
            {
                // No valid color data
                result.CcX = 0;
                result.CcY = 0;
                result.UPrime = 0;
                result.VPrime = 0;
                result.CCT = 0;
            }

            return result;
        }

        /// <summary>
        /// Calculate CIE color coordinates for a uniform spectrum (for testing)
        /// </summary>
        /// <param name="startWavelength">Start wavelength in nm</param>
        /// <param name="endWavelength">End wavelength in nm</param>
        /// <param name="intensity">Uniform intensity value</param>
        /// <param name="stepSize">Wavelength step size in nm (default: 1.0)</param>
        /// <returns>CIE color coordinates</returns>
        public static CieColorResult CalculateUniformSpectrum(double startWavelength, double endWavelength, 
            double intensity, double stepSize = 1.0)
        {
            var steps = (int)Math.Ceiling((endWavelength - startWavelength) / stepSize) + 1;
            var wavelengths = new double[steps];
            var intensities = new double[steps];

            for (int i = 0; i < steps; i++)
            {
                wavelengths[i] = startWavelength + i * stepSize;
                intensities[i] = intensity;
            }

            return CalculateFromSpectrumData(wavelengths, intensities, startWavelength, endWavelength);
        }

        /// <summary>
        /// Convert CIE 1931 chromaticity coordinates to CIE 1976 UCS coordinates
        /// </summary>
        /// <param name="ccx">CIE 1931 x chromaticity coordinate</param>
        /// <param name="ccy">CIE 1931 y chromaticity coordinate</param>
        /// <returns>Tuple of (u', v') coordinates</returns>
        public static (double uPrime, double vPrime) ConvertToUCS(double ccx, double ccy)
        {
            var denominator = -2 * ccx + 12 * ccy + 3;
            if (Math.Abs(denominator) > 1e-10)
            {
                var uPrime = 4 * ccx / denominator;
                var vPrime = 6 * ccy / denominator;
                return (uPrime, vPrime);
            }
            return (0, 0);
        }

        /// <summary>
        /// Convert CIE 1976 UCS coordinates to CIE 1931 chromaticity coordinates
        /// </summary>
        /// <param name="uPrime">CIE 1976 u' coordinate</param>
        /// <param name="vPrime">CIE 1976 v' coordinate</param>
        /// <returns>Tuple of (x, y) chromaticity coordinates</returns>
        public static (double x, double y) ConvertFromUCS(double uPrime, double vPrime)
        {
            var denominator = 6 * uPrime - 16 * vPrime + 12;
            if (Math.Abs(denominator) > 1e-10)
            {
                var x = 9 * uPrime / denominator;
                var y = 4 * vPrime / denominator;
                return (x, y);
            }
            return (0, 0);
        }


        public static double parseCCT(double x, double y)
        {
            int closestuv;
            double closestduv = 100;
            double a;
            double b;
            double c;
            double bigA;
            double bigB;
            double bigC;
            double tm;
            double tmPlus;
            double tmMinus;
            double dm;
            double dmPlus;
            double dmMinus;
            // Dim x As Decimal
            // Dim y As Decimal
            double u;
            double v;
            double CCT = 0;



            closestduv = 100;
            closestuv = 0;
            u = 4 * x / (-2 * x + 12 * y + 3);
            v = 6 * y / (-2 * x + 12 * y + 3);
            // mTP(i, j).uPrime = 4 * mTP(i, j).x / (-2 * mTP(i, j).x + 12 * mTP(i, j).y + 3)
            // mTP(i, j).vPrime = 6 * mTP(i, j).y / (-2 * mTP(i, j).x + 12 * mTP(i, j).y + 3) * 1.5
            for (int k = 0; k <= 302; k += 1)
            {
                if (closestduv > Math.Pow((u - CIE2DegreeFunctions.bbu[k]), 2) + Math.Pow((v - CIE2DegreeFunctions.bbv[k]), 2))
                {
                    closestduv = ((Math.Pow((u - CIE2DegreeFunctions.bbu[k]), 2) + Math.Pow((v - CIE2DegreeFunctions.bbv[k]), 2)));
                    closestuv = k;
                }
            }
            if (closestuv > 0 & closestuv < 302)
            {
                tm = CIE2DegreeFunctions.cct[closestuv];
                tmMinus = CIE2DegreeFunctions.cct[closestuv - 1];
                tmPlus = CIE2DegreeFunctions.cct[closestuv + 1];
                dm = closestduv;
                dmPlus = (Math.Pow((u - CIE2DegreeFunctions.bbu[closestuv + 1]), 2) + Math.Pow((v - CIE2DegreeFunctions.bbv[closestuv + 1]), 2));
                dmMinus = (Math.Pow((u - CIE2DegreeFunctions.bbu[closestuv - 1]), 2) + Math.Pow((v - CIE2DegreeFunctions.bbv[closestuv - 1]), 2));

                a = dmMinus / (tmMinus - tm) / (tmMinus - tmPlus);
                b = dm / (tm - tmMinus) / (tm - tmPlus);
                c = dmPlus / (tmPlus - tmMinus) / (tmPlus - tm);
                bigA = a + b + c;
                bigB = -1 * (a * (tmPlus + tm) + b * (tmMinus + tmPlus) + c * (tm + tmMinus));
                bigC = a * tm * tmPlus + b * tmMinus * tmPlus + c * tm * tmMinus;
                CCT = -1 * bigB / (2 * bigA);
            }
            else
                CCT = 0;
            return CCT;
        }

    }
}