using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Configuration;
using HendersonvilleTrafficTest.Services;

namespace HendersonvilleTrafficTest.Shared
{
    public static class EquipmentHelpers
    {
        /// <summary>
        /// Sets equipment to AC mode: turns off both power supplies, sets power analyzer to AC mode,
        /// and turns OFF the AC/DC select relays (based on configuration)
        /// </summary>
        public static async Task SetAC()
        {
            var factory = new EquipmentFactory();
            
            // Get equipment instances
            var acPowerSupply = factory.CreateAcPowerSupply();
            var dcPowerSupply = factory.CreateDcPowerSupply();
            var powerAnalyzer = factory.CreatePowerAnalyzer();
            var relayController = factory.CreateRelayController();
            
            // Get relay configuration
            var config = ConfigurationManager.Current;
            int acDcSelectPositive = config.Tower.ACDCSelectPositive;
            int acDcSelectNegative = config.Tower.ACDCSelectNegative;
            
            // Turn off both power supplies first for safety
            await acPowerSupply.PowerOffAsync();
            await dcPowerSupply.PowerOffAsync();
            
            // Set power analyzer to AC mode
            await powerAnalyzer.SetModeAsync(PowerMode.AC);
            
            // Turn OFF both AC/DC select relays for AC mode
            await relayController.TurnOutputOffAsync(acDcSelectPositive);
            await relayController.TurnOutputOffAsync(acDcSelectNegative);
        }
        
        /// <summary>
        /// Sets equipment to DC mode: turns off both power supplies, sets power analyzer to DC mode,
        /// and turns ON the AC/DC select relays (based on configuration)
        /// </summary>
        public static async Task SetDC()
        {
            var factory = new EquipmentFactory();
            
            // Get equipment instances
            var acPowerSupply = factory.CreateAcPowerSupply();
            var dcPowerSupply = factory.CreateDcPowerSupply();
            var powerAnalyzer = factory.CreatePowerAnalyzer();
            var relayController = factory.CreateRelayController();
            
            // Get relay configuration
            var config = ConfigurationManager.Current;
            int acDcSelectPositive = config.Tower.ACDCSelectPositive;
            int acDcSelectNegative = config.Tower.ACDCSelectNegative;
            
            // Turn off both power supplies first for safety
            await acPowerSupply.PowerOffAsync();
            await dcPowerSupply.PowerOffAsync();
            
            // Set power analyzer to DC mode
            await powerAnalyzer.SetModeAsync(PowerMode.DC);
            
            // Turn ON both AC/DC select relays for DC mode
            await relayController.TurnOutputOnAsync(acDcSelectPositive);
            await relayController.TurnOutputOnAsync(acDcSelectNegative);
        }
    }
}