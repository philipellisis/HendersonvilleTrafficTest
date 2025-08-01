using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class ItechIT7321AcPowerSupply : IAcPowerSupply
    {
        public bool IsConnected { get; private set; } = false;

        public Task InitializeAsync()
        {
            // TODO: Initialize USB connection to ITECH IT 7321
            throw new NotImplementedException("USB interface initialization not implemented");
        }

        public Task<double> GetVoltsAsync()
        {
            // TODO: Send USB command to get current voltage setting
            throw new NotImplementedException("Get voltage command not implemented");
        }

        public Task SetVoltsAsync(double volts)
        {
            // TODO: Send USB command to set voltage
            throw new NotImplementedException("Set voltage command not implemented");
        }

        public Task<double> GetFrequencyAsync()
        {
            // TODO: Send USB command to get current frequency setting
            throw new NotImplementedException("Get frequency command not implemented");
        }

        public Task SetFrequencyAsync(double frequency)
        {
            // TODO: Send USB command to set frequency
            throw new NotImplementedException("Set frequency command not implemented");
        }

        public Task PowerOnAsync()
        {
            // TODO: Send USB command to turn power on
            throw new NotImplementedException("Power on command not implemented");
        }

        public Task PowerOffAsync()
        {
            // TODO: Send USB command to turn power off
            throw new NotImplementedException("Power off command not implemented");
        }
    }
}