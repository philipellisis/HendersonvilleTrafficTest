using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class UsbTemperatureSensor : ITemperatureSensor
    {
        public bool IsConnected { get; private set; } = false;

        public Task InitializeAsync()
        {
            // TODO: Initialize USB connection to temperature sensor
            throw new NotImplementedException("USB interface initialization not implemented");
        }

        public Task<double> GetTemperatureAsync()
        {
            // TODO: Send USB command to read temperature
            throw new NotImplementedException("Get temperature reading not implemented");
        }
    }
}