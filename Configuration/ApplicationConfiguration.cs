using System.ComponentModel;
using System.Xml.Serialization;

namespace HendersonvilleTrafficTest.Configuration
{
    [Serializable]
    [XmlRoot("ApplicationConfiguration")]
    public class ApplicationConfiguration
    {
        [XmlElement("EquipmentSettings")]
        public EquipmentSettings Equipment { get; set; } = new();

        [XmlElement("TestSettings")]
        public TestSettings Test { get; set; } = new();

        [XmlElement("DatabaseSettings")]
        public DatabaseSettings Database { get; set; } = new();

        [XmlElement("SafetySettings")]
        public SafetySettings Safety { get; set; } = new();
    }

    [Serializable]
    public class EquipmentSettings
    {
        [XmlElement("AcPowerSupplyMode")]
        [Description("AC Power Supply Mode (Simulation or Hardware)")]
        public EquipmentMode AcPowerSupplyMode { get; set; } = EquipmentMode.Simulation;

        [XmlElement("DcPowerSupplyMode")]
        [Description("DC Power Supply Mode (Simulation or Hardware)")]
        public EquipmentMode DcPowerSupplyMode { get; set; } = EquipmentMode.Simulation;

        [XmlElement("PowerAnalyzerMode")]
        [Description("Power Analyzer Mode (Simulation or Hardware)")]
        public EquipmentMode PowerAnalyzerMode { get; set; } = EquipmentMode.Simulation;

        [XmlElement("SpectrometerMode")]
        [Description("Spectrometer Mode (Simulation or Hardware)")]
        public EquipmentMode SpectrometerMode { get; set; } = EquipmentMode.Simulation;

        [XmlElement("TemperatureSensorMode")]
        [Description("Temperature Sensor Mode (Simulation or Hardware)")]
        public EquipmentMode TemperatureSensorMode { get; set; } = EquipmentMode.Simulation;

        [XmlElement("RelayControllerMode")]
        [Description("Relay Controller Mode (Simulation or Hardware)")]
        public EquipmentMode RelayControllerMode { get; set; } = EquipmentMode.Simulation;

        [XmlElement("RelayControllerComPort")]
        [Description("COM port for NCD Relay Controller")]
        public string RelayControllerComPort { get; set; } = "COM1";

        [XmlElement("RelayControllerBaudRate")]
        [Description("Baud rate for NCD Relay Controller")]
        public int RelayControllerBaudRate { get; set; } = 115200;

        [XmlElement("TemperatureSensorComPort")]
        [Description("COM port for USB Temperature Sensor")]
        public string TemperatureSensorComPort { get; set; } = "COM2";

        [XmlElement("TemperatureSensorBaudRate")]
        [Description("Baud rate for USB Temperature Sensor")]
        public int TemperatureSensorBaudRate { get; set; } = 9600;

        [XmlElement("PowerAnalyzerComPort")]
        [Description("COM port for NPA101 Power Analyzer")]
        public string PowerAnalyzerComPort { get; set; } = "COM3";

        [XmlElement("PowerAnalyzerBaudRate")]
        [Description("Baud rate for NPA101 Power Analyzer")]
        public int PowerAnalyzerBaudRate { get; set; } = 9600;

        [XmlElement("ConnectionTimeoutMs")]
        [Description("Equipment connection timeout in milliseconds")]
        public int ConnectionTimeoutMs { get; set; } = 5000;
    }

    [Serializable]
    public class TestSettings
    {
        [XmlElement("DefaultVoltage")]
        [Description("Default test voltage")]
        public double DefaultVoltage { get; set; } = 120.0;

        [XmlElement("DefaultFrequency")]
        [Description("Default test frequency (Hz)")]
        public double DefaultFrequency { get; set; } = 60.0;

        [XmlElement("VoltageRampTimeMs")]
        [Description("Time to ramp voltage up/down in milliseconds")]
        public int VoltageRampTimeMs { get; set; } = 3000;

        [XmlElement("SettleTimeMs")]
        [Description("Time to wait for readings to settle in milliseconds")]
        public int SettleTimeMs { get; set; } = 1000;

        [XmlElement("MaxTestTimeMs")]
        [Description("Maximum test duration in milliseconds")]
        public int MaxTestTimeMs { get; set; } = 30000;

        [XmlElement("AutoSaveResults")]
        [Description("Automatically save test results")]
        public bool AutoSaveResults { get; set; } = true;
    }

    [Serializable]
    public class DatabaseSettings
    {
        [XmlElement("ServerName")]
        [Description("SQL Server name or IP address")]
        public string ServerName { get; set; } = "localhost";

        [XmlElement("DatabaseName")]
        [Description("Database name")]
        public string DatabaseName { get; set; } = "TrafficTestDB";

        [XmlElement("UseIntegratedSecurity")]
        [Description("Use Windows integrated security")]
        public bool UseIntegratedSecurity { get; set; } = true;

        [XmlElement("Username")]
        [Description("Database username (if not using integrated security)")]
        public string Username { get; set; } = "";

        [XmlElement("Password")]
        [Description("Database password (if not using integrated security)")]
        public string Password { get; set; } = "";

        [XmlElement("ConnectionTimeoutSeconds")]
        [Description("Database connection timeout in seconds")]
        public int ConnectionTimeoutSeconds { get; set; } = 30;
    }

    [Serializable]
    public class SafetySettings
    {
        [XmlElement("MaxVoltage")]
        [Description("Maximum allowed voltage")]
        public double MaxVoltage { get; set; } = 300.0;

        [XmlElement("MaxCurrent")]
        [Description("Maximum allowed current (Amps)")]
        public double MaxCurrent { get; set; } = 20.0;

        [XmlElement("EmergencyStopEnabled")]
        [Description("Enable emergency stop functionality")]
        public bool EmergencyStopEnabled { get; set; } = true;

        [XmlElement("LightCurtainRequired")]
        [Description("Require light curtain for operation")]
        public bool LightCurtainRequired { get; set; } = true;
    }
}