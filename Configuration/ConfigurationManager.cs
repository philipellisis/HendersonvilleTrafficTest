namespace HendersonvilleTrafficTest.Configuration
{
    public static class ConfigurationManager
    {
        private static ApplicationConfiguration? _configuration;
        private static XmlConfigurationService? _configService;
        private static readonly object _lock = new();

        public static ApplicationConfiguration Current
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_lock)
                    {
                        if (_configuration == null)
                        {
                            Initialize();
                        }
                    }
                }
                return _configuration!;
            }
        }

        public static void Initialize(string? configFilePath = null)
        {
            lock (_lock)
            {
                _configService = new XmlConfigurationService(configFilePath);
                _configuration = _configService.LoadConfiguration();
            }
        }

        public static void SaveConfiguration()
        {
            lock (_lock)
            {
                if (_configService == null || _configuration == null)
                {
                    throw new InvalidOperationException("Configuration manager not initialized. Call Initialize() first.");
                }

                _configService.SaveConfiguration(_configuration);
            }
        }

        public static void ReloadConfiguration()
        {
            lock (_lock)
            {
                if (_configService == null)
                {
                    throw new InvalidOperationException("Configuration manager not initialized. Call Initialize() first.");
                }

                _configuration = _configService.LoadConfiguration();
            }
        }

        public static string GetConfigurationPath()
        {
            return _configService?.GetConfigurationPath() ?? "config.xml";
        }

        public static bool IsInitialized => _configuration != null && _configService != null;
    }
}