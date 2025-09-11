using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Equipment.Hardware;
using HendersonvilleTrafficTest.Equipment.Simulation;
using HendersonvilleTrafficTest.Configuration;

namespace HendersonvilleTrafficTest.Services
{
    public class EquipmentFactory
    {
        // Singleton instances for each equipment type
        private static IAcPowerSupply? _acPowerSupplyInstance;
        private static IDcPowerSupply? _dcPowerSupplyInstance;
        private static IPowerAnalyzer? _powerAnalyzerInstance;
        private static ISpectrometer? _spectrometerInstance;
        private static ITemperatureSensor? _temperatureSensorInstance;
        private static IRelayController? _relayControllerInstance;
        private static readonly object _lock = new object();

        // Keep track of current configuration to detect changes
        private static EquipmentMode? _lastAcPowerSupplyMode;
        private static EquipmentMode? _lastDcPowerSupplyMode;
        private static EquipmentMode? _lastPowerAnalyzerMode;
        private static SpectrometerType? _lastSpectrometerType;
        private static EquipmentMode? _lastTemperatureSensorMode;
        private static EquipmentMode? _lastRelayControllerMode;
        public IAcPowerSupply CreateAcPowerSupply()
        {
            lock (_lock)
            {
                var currentMode = ConfigurationManager.Current.Equipment.AcPowerSupplyMode;
                
                // If mode changed or no instance exists, create new one
                if (_acPowerSupplyInstance == null || _lastAcPowerSupplyMode != currentMode)
                {
                    // Dispose old instance if it exists
                    if (_acPowerSupplyInstance is IDisposable disposable)
                        disposable.Dispose();
                    
                    _acPowerSupplyInstance = currentMode == EquipmentMode.Simulation
                        ? new SimulatedAcPowerSupply() 
                        : new ItechIT7321AcPowerSupply();
                    
                    _lastAcPowerSupplyMode = currentMode;
                }
                
                return _acPowerSupplyInstance;
            }
        }

        public IDcPowerSupply CreateDcPowerSupply()
        {
            lock (_lock)
            {
                var currentMode = ConfigurationManager.Current.Equipment.DcPowerSupplyMode;
                
                // If mode changed or no instance exists, create new one
                if (_dcPowerSupplyInstance == null || _lastDcPowerSupplyMode != currentMode)
                {
                    // Dispose old instance if it exists
                    if (_dcPowerSupplyInstance is IDisposable disposable)
                        disposable.Dispose();
                    
                    _dcPowerSupplyInstance = currentMode == EquipmentMode.Simulation
                        ? new SimulatedDcPowerSupply() 
                        : new ItechIT6922ADcPowerSupply();
                    
                    _lastDcPowerSupplyMode = currentMode;
                }
                
                return _dcPowerSupplyInstance;
            }
        }

        public IPowerAnalyzer CreatePowerAnalyzer()
        {
            lock (_lock)
            {
                var currentMode = ConfigurationManager.Current.Equipment.PowerAnalyzerMode;
                
                // If mode changed or no instance exists, create new one
                if (_powerAnalyzerInstance == null || _lastPowerAnalyzerMode != currentMode)
                {
                    // Dispose old instance if it exists
                    if (_powerAnalyzerInstance is IDisposable disposable)
                        disposable.Dispose();
                    
                    _powerAnalyzerInstance = currentMode == EquipmentMode.Simulation
                        ? new SimulatedPowerAnalyzer() 
                        : new Npa101PowerAnalyzer();
                    
                    _lastPowerAnalyzerMode = currentMode;
                }
                
                return _powerAnalyzerInstance;
            }
        }

        public ISpectrometer CreateSpectrometer()
        {
            lock (_lock)
            {
                var currentType = ConfigurationManager.Current.Equipment.SpectrometerType;
                
                // If type changed or no instance exists, create new one
                if (_spectrometerInstance == null || _lastSpectrometerType != currentType)
                {
                    // Dispose old instance if it exists
                    if (_spectrometerInstance is IDisposable disposable)
                        disposable.Dispose();
                    
                    _spectrometerInstance = currentType switch
                    {
                        Configuration.SpectrometerType.Simulation => new SimulatedSpectrometer(),
                        Configuration.SpectrometerType.StellarNet => new StellarNetBlueWaveSpectrometer(),
                        Configuration.SpectrometerType.OceanOpticsST => new OceanOpticsStSpectrometer(),
                        _ => new SimulatedSpectrometer()
                    };
                    
                    _lastSpectrometerType = currentType;
                }
                
                return _spectrometerInstance;
            }
        }

        public ITemperatureSensor CreateTemperatureSensor()
        {
            lock (_lock)
            {
                var currentMode = ConfigurationManager.Current.Equipment.TemperatureSensorMode;
                
                // If mode changed or no instance exists, create new one
                if (_temperatureSensorInstance == null || _lastTemperatureSensorMode != currentMode)
                {
                    // Dispose old instance if it exists
                    if (_temperatureSensorInstance is IDisposable disposable)
                        disposable.Dispose();
                    
                    _temperatureSensorInstance = currentMode == EquipmentMode.Simulation
                        ? new SimulatedTemperatureSensor() 
                        : new UsbTemperatureSensor();
                    
                    _lastTemperatureSensorMode = currentMode;
                }
                
                return _temperatureSensorInstance;
            }
        }

        public IRelayController CreateRelayController()
        {
            lock (_lock)
            {
                var currentMode = ConfigurationManager.Current.Equipment.RelayControllerMode;
                
                // If mode changed or no instance exists, create new one
                if (_relayControllerInstance == null || _lastRelayControllerMode != currentMode)
                {
                    // Dispose old instance if it exists
                    if (_relayControllerInstance is IDisposable disposable)
                        disposable.Dispose();
                    
                    _relayControllerInstance = currentMode == EquipmentMode.Simulation
                        ? new SimulatedRelayController() 
                        : new NcdRelayController();
                    
                    _lastRelayControllerMode = currentMode;
                }
                
                return _relayControllerInstance;
            }
        }

        /// <summary>
        /// Dispose all singleton instances (useful for application shutdown or configuration reset)
        /// </summary>
        public static void DisposeAllInstances()
        {
            lock (_lock)
            {
                if (_acPowerSupplyInstance is IDisposable acDisposable)
                    acDisposable.Dispose();
                if (_dcPowerSupplyInstance is IDisposable dcDisposable)
                    dcDisposable.Dispose();
                if (_powerAnalyzerInstance is IDisposable paDisposable)
                    paDisposable.Dispose();
                if (_spectrometerInstance is IDisposable specDisposable)
                    specDisposable.Dispose();
                if (_temperatureSensorInstance is IDisposable tempDisposable)
                    tempDisposable.Dispose();
                if (_relayControllerInstance is IDisposable relayDisposable)
                    relayDisposable.Dispose();

                // Clear all instances
                _acPowerSupplyInstance = null;
                _dcPowerSupplyInstance = null;
                _powerAnalyzerInstance = null;
                _spectrometerInstance = null;
                _temperatureSensorInstance = null;
                _relayControllerInstance = null;

                // Reset configuration tracking
                _lastAcPowerSupplyMode = null;
                _lastDcPowerSupplyMode = null;
                _lastPowerAnalyzerMode = null;
                _lastSpectrometerType = null;
                _lastTemperatureSensorMode = null;
                _lastRelayControllerMode = null;
            }
        }
    }
}