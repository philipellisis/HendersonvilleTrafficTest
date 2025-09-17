using System;

namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public class DarkScan
    {
        public double[] Intensity { get; set; }
        public uint IntegrationTimeMicros { get; set; }

        public DarkScan(double[] intensity, uint integrationTimeMicros)
        {
            Intensity = intensity;
            IntegrationTimeMicros = integrationTimeMicros;
        }
    }
}