namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public class ElectricalMeasurement
    {
        public double Voltage { get; set; }     // Maps to URMS
        public double Current { get; set; }     // Maps to IRMS
        public double Power { get; set; }       // Maps to P
        public double Frequency { get; set; }   // Maps to FU
        public double THD { get; set; }         // Maps to UTHD
        public double PowerFactor { get; set; } // Maps to LAMBDA

        public ElectricalMeasurement()
        {
        }

        public ElectricalMeasurement(double voltage, double current, double power, double frequency)
        {
            Voltage = voltage;
            Current = current;
            Power = power;
            Frequency = frequency;
        }

        public ElectricalMeasurement(double voltage, double current, double power, double frequency, double thd, double powerFactor)
        {
            Voltage = voltage;
            Current = current;
            Power = power;
            Frequency = frequency;
            THD = thd;
            PowerFactor = powerFactor;
        }

        public override string ToString()
        {
            return $"V: {Voltage:F3}V, I: {Current:F6}A, P: {Power:F3}W, F: {Frequency:F2}Hz, THD: {THD:F2}%, PF: {PowerFactor:F3}";
        }
    }
}