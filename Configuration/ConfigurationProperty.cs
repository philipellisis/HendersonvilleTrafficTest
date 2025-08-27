using System.ComponentModel;
using System.Reflection;

namespace HendersonvilleTrafficTest.Configuration
{
    public class ConfigurationProperty
    {
        public string Name { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Description { get; set; } = "";
        public Type PropertyType { get; set; } = typeof(object);
        public object? Value { get; set; }
        public PropertyInfo PropertyInfo { get; set; } = null!;
        public object ParentObject { get; set; } = null!;
        public string Category { get; set; } = "";

        public void SetValue(object? value)
        {
            PropertyInfo.SetValue(ParentObject, value);
            Value = value;
        }

        public T? GetValue<T>()
        {
            return (T?)PropertyInfo.GetValue(ParentObject);
        }
    }

    public static class ConfigurationPropertyExtractor
    {
        public static List<ConfigurationProperty> ExtractProperties(ApplicationConfiguration config)
        {
            var properties = new List<ConfigurationProperty>();

            ExtractPropertiesFromObject(config.Equipment, "Equipment", properties);
            ExtractPropertiesFromObject(config.Test, "Test Settings", properties);
            ExtractPropertiesFromObject(config.Database, "Database", properties);
            ExtractPropertiesFromObject(config.Safety, "Safety", properties);
            ExtractPropertiesFromObject(config.Calibration, "Calibration", properties);

            return properties;
        }

        private static void ExtractPropertiesFromObject(object obj, string category, List<ConfigurationProperty> properties)
        {
            var type = obj.GetType();
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var descriptionAttr = prop.GetCustomAttribute<DescriptionAttribute>();
                var description = descriptionAttr?.Description ?? prop.Name;

                var configProperty = new ConfigurationProperty
                {
                    Name = prop.Name,
                    DisplayName = FormatDisplayName(prop.Name),
                    Description = description,
                    PropertyType = prop.PropertyType,
                    Value = prop.GetValue(obj),
                    PropertyInfo = prop,
                    ParentObject = obj,
                    Category = category
                };

                properties.Add(configProperty);
            }
        }

        private static string FormatDisplayName(string propertyName)
        {
            var result = "";
            for (int i = 0; i < propertyName.Length; i++)
            {
                if (i > 0 && char.IsUpper(propertyName[i]))
                {
                    result += " ";
                }
                result += propertyName[i];
            }
            return result;
        }
    }
}