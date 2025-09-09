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
        
        // Test Results
        public bool VoltageTestPassed { get; set; }
        public bool CurrentTestPassed { get; set; }
        public bool PowerTestPassed { get; set; }
        public bool PowerFactorTestPassed { get; set; }
        public bool ThdTestPassed { get; set; }
        public bool FrequencyTestPassed { get; set; }
        public bool IntensityTestPassed { get; set; }
        public bool ColorTestPassed { get; set; }
        public bool OverallTestPassed { get; set; }
        
        // Test Configuration
        public bool IsAcTest { get; set; }
        public int RelayUsed { get; set; }
    }
}