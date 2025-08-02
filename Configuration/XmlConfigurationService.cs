using System.Xml;
using System.Xml.Serialization;

namespace HendersonvilleTrafficTest.Configuration
{
    public class XmlConfigurationService
    {
        private readonly string _configFilePath;

        public XmlConfigurationService(string? configFilePath = null)
        {
            _configFilePath = configFilePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
        }

        public ApplicationConfiguration LoadConfiguration()
        {
            try
            {
                if (File.Exists(_configFilePath))
                {
                    var serializer = new XmlSerializer(typeof(ApplicationConfiguration));
                    using var fileStream = new FileStream(_configFilePath, FileMode.Open, FileAccess.Read);
                    var config = serializer.Deserialize(fileStream) as ApplicationConfiguration;
                    return config ?? CreateDefaultConfiguration();
                }
                else
                {
                    var defaultConfig = CreateDefaultConfiguration();
                    SaveConfiguration(defaultConfig);
                    return defaultConfig;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load configuration from {_configFilePath}: {ex.Message}", ex);
            }
        }

        public void SaveConfiguration(ApplicationConfiguration configuration)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ApplicationConfiguration));
                
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = Environment.NewLine
                };

                using var fileStream = new FileStream(_configFilePath, FileMode.Create, FileAccess.Write);
                using var xmlWriter = XmlWriter.Create(fileStream, settings);
                
                serializer.Serialize(xmlWriter, configuration);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save configuration to {_configFilePath}: {ex.Message}", ex);
            }
        }

        public bool ConfigurationExists()
        {
            return File.Exists(_configFilePath);
        }

        public string GetConfigurationPath()
        {
            return _configFilePath;
        }

        private ApplicationConfiguration CreateDefaultConfiguration()
        {
            return new ApplicationConfiguration();
        }
    }
}