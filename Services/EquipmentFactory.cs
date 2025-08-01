using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Equipment.Hardware;
using HendersonvilleTrafficTest.Equipment.Simulation;

namespace HendersonvilleTrafficTest.Services
{
    public class EquipmentFactory
    {
        private readonly bool _useSimulation;

        public EquipmentFactory(ConfigurationService? configService = null)
        {
            _useSimulation = configService?.UseSimulation ?? true;
        }

        public EquipmentFactory(bool useSimulation)
        {
            _useSimulation = useSimulation;
        }

        public IAcPowerSupply CreateAcPowerSupply()
        {
            return _useSimulation 
                ? new SimulatedAcPowerSupply() 
                : new ItechIT7321AcPowerSupply();
        }

        public IDcPowerSupply CreateDcPowerSupply()
        {
            return _useSimulation 
                ? new SimulatedDcPowerSupply() 
                : new ItechIT6922ADcPowerSupply();
        }

        public IPowerAnalyzer CreatePowerAnalyzer()
        {
            return _useSimulation 
                ? new SimulatedPowerAnalyzer() 
                : new Npa101PowerAnalyzer();
        }

        public ISpectrometer CreateSpectrometer()
        {
            return _useSimulation 
                ? new SimulatedSpectrometer() 
                : new StellarNetBlueWaveSpectrometer();
        }

        public ITemperatureSensor CreateTemperatureSensor()
        {
            return _useSimulation 
                ? new SimulatedTemperatureSensor() 
                : new UsbTemperatureSensor();
        }
    }
}