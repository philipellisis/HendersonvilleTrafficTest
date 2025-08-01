using System.Text.Json;

namespace HendersonvilleTrafficTest.Services
{
    public class ConfigurationService
    {
        private readonly string _configPath;
        private readonly Dictionary<string, object> _config;

        public ConfigurationService(string configPath = "appsettings.json")
        {
            _configPath = configPath;
            _config = LoadConfiguration();
        }

        public bool UseSimulation => GetValue<bool>("Equipment:UseSimulation", true);

        private Dictionary<string, object> LoadConfiguration()
        {
            try
            {
                if (!File.Exists(_configPath))
                {
                    return new Dictionary<string, object>();
                }

                var json = File.ReadAllText(_configPath);
                var config = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                return config ?? new Dictionary<string, object>();
            }
            catch (Exception)
            {
                return new Dictionary<string, object>();
            }
        }

        private T GetValue<T>(string key, T defaultValue)
        {
            try
            {
                var keys = key.Split(':');
                object current = _config;

                foreach (var k in keys)
                {
                    if (current is Dictionary<string, object> dict && dict.ContainsKey(k))
                    {
                        current = dict[k];
                    }
                    else if (current is JsonElement element)
                    {
                        if (element.TryGetProperty(k, out var property))
                        {
                            current = property;
                        }
                        else
                        {
                            return defaultValue;
                        }
                    }
                    else
                    {
                        return defaultValue;
                    }
                }

                if (current is JsonElement jsonElement)
                {
                    return jsonElement.ValueKind switch
                    {
                        JsonValueKind.True => (T)(object)true,
                        JsonValueKind.False => (T)(object)false,
                        JsonValueKind.Number => (T)(object)jsonElement.GetDouble(),
                        JsonValueKind.String => (T)(object)jsonElement.GetString()!,
                        _ => defaultValue
                    };
                }

                return (T)current;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}