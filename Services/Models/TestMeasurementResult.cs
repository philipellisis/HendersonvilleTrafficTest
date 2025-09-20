using System;

namespace HendersonvilleTrafficTest.Services.Models
{
    public class TestMeasurementResult
    {
        public string TestSequenceId { get; set; } = "";
        public int StepNumber { get; set; }
        public string StepName { get; set; } = "";
        public DateTime TestDateTime { get; set; } = DateTime.Now;
        
        // Electrical Measurements
        public double VoltageSet { get; set; }
        public double VoltageMeasured { get; set; }
        public double CurrentMeasured { get; set; }
        public double PowerMeasured { get; set; }
        public double PowerFactorMeasured { get; set; }
        public double ThdMeasured { get; set; }
        public double FrequencyMeasured { get; set; }
        
        // Environmental Measurements
        public double TemperatureMeasured { get; set; }
        
        // Optical Measurements
        public double IntensityMeasured { get; set; }
        public double DominantWavelength { get; set; }
        public double Cct { get; set; }
        public double CcxMeasured { get; set; }
        public double CcyMeasured { get; set; }
        public double Luminance { get; set; }
        
        // Test Limits (UCL/LCL values)
        public double? VoltageUcl { get; set; }
        public double? VoltageLcl { get; set; }
        public double? CurrentUcl { get; set; }
        public double? CurrentLcl { get; set; }
        public double? PowerUcl { get; set; }
        public double? PowerLcl { get; set; }
        public double? PowerFactorUcl { get; set; }
        public double? PowerFactorLcl { get; set; }
        public double? ThdUcl { get; set; }
        public double? ThdLcl { get; set; }
        public double? FrequencyUcl { get; set; }
        public double? FrequencyLcl { get; set; }
        public double? IntensityUcl { get; set; }
        public double? IntensityLcl { get; set; }
        
        // Color test limits (stored as polygon vertices)
        public double[] ColorPolygonX { get; set; } = Array.Empty<double>();
        public double[] ColorPolygonY { get; set; } = Array.Empty<double>();
        
        // Test Results (calculated dynamically)
        public bool VoltageTestPassed 
        { 
            get 
            { 
                return (!VoltageLcl.HasValue || VoltageMeasured >= VoltageLcl.Value) && 
                       (!VoltageUcl.HasValue || VoltageMeasured <= VoltageUcl.Value); 
            } 
        }
        
        public bool CurrentTestPassed 
        { 
            get 
            { 
                return (!CurrentLcl.HasValue || CurrentMeasured >= CurrentLcl.Value) && 
                       (!CurrentUcl.HasValue || CurrentMeasured <= CurrentUcl.Value); 
            } 
        }
        
        public bool PowerTestPassed 
        { 
            get 
            { 
                return (!PowerLcl.HasValue || PowerMeasured >= PowerLcl.Value) && 
                       (!PowerUcl.HasValue || PowerMeasured <= PowerUcl.Value); 
            } 
        }
        
        public bool PowerFactorTestPassed 
        { 
            get 
            { 
                return (!PowerFactorLcl.HasValue || PowerFactorMeasured >= PowerFactorLcl.Value) && 
                       (!PowerFactorUcl.HasValue || PowerFactorMeasured <= PowerFactorUcl.Value); 
            } 
        }
        
        public bool ThdTestPassed 
        { 
            get 
            { 
                return (!ThdLcl.HasValue || ThdMeasured >= ThdLcl.Value) && 
                       (!ThdUcl.HasValue || ThdMeasured <= ThdUcl.Value); 
            } 
        }
        
        public bool FrequencyTestPassed 
        { 
            get 
            { 
                return (!FrequencyLcl.HasValue || FrequencyMeasured >= FrequencyLcl.Value) && 
                       (!FrequencyUcl.HasValue || FrequencyMeasured <= FrequencyUcl.Value); 
            } 
        }
        
        public bool IntensityTestPassed 
        { 
            get 
            { 
                return (!IntensityLcl.HasValue || Ecl >= IntensityLcl.Value) && 
                       (!IntensityUcl.HasValue || Ecl <= IntensityUcl.Value); 
            } 
        }
        
        public bool ColorTestPassed { get; set; } // Still settable since it uses complex polygon logic
        
        public bool OverallTestPassed 
        { 
            get 
            { 
                return VoltageTestPassed && CurrentTestPassed && PowerTestPassed && 
                       PowerFactorTestPassed && ThdTestPassed && FrequencyTestPassed &&
                       IntensityTestPassed && ColorTestPassed; 
            } 
        }
        
        // Test Configuration
        public bool IsAcTest { get; set; }
        public int RelayUsed { get; set; }
        
        // Calibration Parameters (from TestSequenceStep)
        public double EclCalib { get; set; }
        public double At { get; set; }
        public double Bt { get; set; }
        public double Ax { get; set; }
        public double Bx { get; set; }
        public double Ay { get; set; }
        public double By { get; set; }
        
        // Calculated ECL and Adjusted Color Values (getters)
        public double Ecl
        {
            get
            {
                return IntensityMeasured * EclCalib * (TemperatureMeasured * At + Bt);
            }
        }
        
        public double CcxAdjusted
        {
            get
            {
                return CcxMeasured - (TemperatureMeasured * Ax + Bx);
            }
        }
        
        public double CcyAdjusted
        {
            get
            {
                return CcyMeasured - (TemperatureMeasured * Ay + By);
            }
        }
    }
}