using HendersonvilleTrafficTest.Equipment.Interfaces;

namespace HendersonvilleTrafficTest.Equipment.Hardware
{
    public class ItechIT6922ADcPowerSupply : IDcPowerSupply
    {
        public bool IsConnected { get; private set; } = false;

        public Task InitializeAsync()
        {
            // TODO: Initialize USB connection to ITECH IT6922A
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

        public Task<double> GetAmpsAsync()
        {
            // TODO: Send USB command to get current amperage setting
            throw new NotImplementedException("Get amperage command not implemented");
        }

        public Task SetAmpsAsync(double amps)
        {
            // TODO: Send USB command to set amperage
            throw new NotImplementedException("Set amperage command not implemented");
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