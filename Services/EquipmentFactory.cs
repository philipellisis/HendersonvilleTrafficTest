using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Equipment.Hardware;
using HendersonvilleTrafficTest.Equipment.Simulation;
using HendersonvilleTrafficTest.Configuration;

namespace HendersonvilleTrafficTest.Services
{
    public class EquipmentFactory
    {
        public IAcPowerSupply CreateAcPowerSupply()
        {
            return ConfigurationManager.Current.Equipment.AcPowerSupplyMode == EquipmentMode.Simulation
                ? new SimulatedAcPowerSupply() 
                : new ItechIT7321AcPowerSupply();
        }

        public IDcPowerSupply CreateDcPowerSupply()
        {
            return ConfigurationManager.Current.Equipment.DcPowerSupplyMode == EquipmentMode.Simulation
                ? new SimulatedDcPowerSupply() 
                : new ItechIT6922ADcPowerSupply();
        }

        public IPowerAnalyzer CreatePowerAnalyzer()
        {
            return ConfigurationManager.Current.Equipment.PowerAnalyzerMode == EquipmentMode.Simulation
                ? new SimulatedPowerAnalyzer() 
                : new Npa101PowerAnalyzer();
        }

        public ISpectrometer CreateSpectrometer()
        {
            return ConfigurationManager.Current.Equipment.SpectrometerType switch
            {
                Configuration.SpectrometerType.Simulation => new SimulatedSpectrometer(),
                Configuration.SpectrometerType.StellarNet => new StellarNetBlueWaveSpectrometer(),
                Configuration.SpectrometerType.OceanOpticsST => new OceanOpticsStSpectrometer(),
                _ => new SimulatedSpectrometer()
            };
        }

        public ITemperatureSensor CreateTemperatureSensor()
        {
            return ConfigurationManager.Current.Equipment.TemperatureSensorMode == EquipmentMode.Simulation
                ? new SimulatedTemperatureSensor() 
                : new UsbTemperatureSensor();
        }

        public IRelayController CreateRelayController()
        {
            return ConfigurationManager.Current.Equipment.RelayControllerMode == EquipmentMode.Simulation
                ? new SimulatedRelayController() 
                : new NcdRelayController();
        }
    }
}