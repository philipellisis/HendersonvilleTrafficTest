namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public class ElectricalMeasurement
    {
        public double Voltage { get; set; }
        public double Current { get; set; }
        public double Power { get; set; }
        public double Frequency { get; set; }

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

        public override string ToString()
        {
            return $"V: {Voltage:F3}V, I: {Current:F6}A, P: {Power:F3}W, F: {Frequency:F2}Hz";
        }
    }
}