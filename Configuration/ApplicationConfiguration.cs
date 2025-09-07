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

        [XmlElement("CalibrationSettings")]
        public CalibrationSettings Calibration { get; set; } = new();

        [XmlElement("DataAccessSettings")]
        public DataAccessSettings DataAccess { get; set; } = new();

        [XmlElement("TowerSettings")]
        public TowerSettings Tower { get; set; } = new();
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

        [XmlElement("SpectrometerType")]
        [Description("Spectrometer Type (Simulation, StellarNet, or OceanOpticsST)")]
        public SpectrometerType SpectrometerType { get; set; } = SpectrometerType.Simulation;

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

        [XmlElement("TemperatureSensorType")]
        [Description("Temperature sensor type (OHT20, OT60, OT150)")]
        public string TemperatureSensorType { get; set; } = "OT150";

        [XmlElement("PowerAnalyzerComPort")]
        [Description("COM port for NPA101 Power Analyzer")]
        public string PowerAnalyzerComPort { get; set; } = "COM3";

        [XmlElement("PowerAnalyzerBaudRate")]
        [Description("Baud rate for NPA101 Power Analyzer")]
        public int PowerAnalyzerBaudRate { get; set; } = 9600;

        [XmlElement("AcPowerSupplyComPort")]
        [Description("COM port for Itech IT7321 AC Power Supply")]
        public string AcPowerSupplyComPort { get; set; } = "COM4";

        [XmlElement("AcPowerSupplyBaudRate")]
        [Description("Baud rate for Itech IT7321 AC Power Supply")]
        public int AcPowerSupplyBaudRate { get; set; } = 9600;

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

    [Serializable]
    public class CalibrationSettings
    {
        [XmlArray("SpectrometerCalibrationFactors")]
        [XmlArrayItem("Factor")]
        [Description("Calibration factors from 380nm to 780nm in 1nm increments (401 values)")]
        public double[] SpectrometerCalibrationFactors { get; set; } = new double[401];

        [XmlArray("StandardLampSpectrum")]
        [XmlArrayItem("Intensity")]
        [Description("Standard lamp spectrum values from 380nm to 780nm in 1nm increments (401 values)")]
        public double[] StandardLampSpectrum { get; set; } = new double[401];

        [XmlElement("CalibrationLampVoltage")]
        [Description("Calibration lamp voltage setting")]
        public double CalibrationLampVoltage { get; set; } = 12.0;

        [XmlElement("CalibrationLampCurrent")]
        [Description("Target calibration lamp current in amps")]
        public double CalibrationLampCurrent { get; set; } = 1.0;

        [XmlElement("CalibrationCurrentTolerance")]
        [Description("Acceptable current tolerance in amps")]
        public double CalibrationCurrentTolerance { get; set; } = 0.05;

        [XmlElement("CalibrationWarmupTimeSeconds")]
        [Description("Calibration lamp warmup time in seconds")]
        public int CalibrationWarmupTimeSeconds { get; set; } = 30;

        [XmlElement("CalibrationRampTimeSeconds")]
        [Description("Time to ramp voltage up/down in seconds")]
        public int CalibrationRampTimeSeconds { get; set; } = 10;

        public CalibrationSettings()
        {
            // Initialize all calibration factors to 1.0 (no calibration applied)
            for (int i = 0; i < SpectrometerCalibrationFactors.Length; i++)
            {
                SpectrometerCalibrationFactors[i] = 1.0;
            }

            // Initialize standard lamp spectrum to 1.0 (flat spectrum)
            for (int i = 0; i < StandardLampSpectrum.Length; i++)
            {
                StandardLampSpectrum[i] = 1.0;
            }
        }
    }

    [Serializable]
    public class DataAccessSettings
    {
        [XmlElement("DataAccessMode")]
        [Description("Data access mode (Local or Database)")]
        public DataAccessMode DataAccessMode { get; set; } = DataAccessMode.Local;
    }

    [Serializable]
    public class TowerSettings
    {
        [XmlElement("TowerId")]
        [Description("Unique identifier for this measurement tower")]
        public string TowerId { get; set; } = "Tower001";

        [XmlElement("BlueColorSample")]
        public ColorSample BlueColorSample { get; set; } = new();

        [XmlElement("GreenColorSample")]
        public ColorSample GreenColorSample { get; set; } = new();

        [XmlElement("YellowColorSample")]
        public ColorSample YellowColorSample { get; set; } = new();

        [XmlElement("OrangeColorSample")]
        public ColorSample OrangeColorSample { get; set; } = new();

        [XmlElement("RedColorSample")]
        public ColorSample RedColorSample { get; set; } = new();

        [XmlElement("WhiteColorSample")]
        public ColorSample WhiteColorSample { get; set; } = new();
    }

    [Serializable]
    public class ColorSample
    {
        [XmlElement("A_Color")]
        [Description("A coefficient for color calibration")]
        public double A_Color { get; set; } = 1.0;

        [XmlElement("B_Color")]
        [Description("B coefficient for color calibration")]
        public double B_Color { get; set; } = 0.0;

        [XmlElement("LUX_Initial")]
        [Description("Initial LUX measurement for calibration")]
        public double LUX_Initial { get; set; } = 1000.0;

        [XmlElement("Temperature_Initial")]
        [Description("Initial temperature measurement for calibration (°C)")]
        public double Temperature_Initial { get; set; } = 25.0;

        [XmlElement("TestVoltage")]
        [Description("Test voltage for this color sample")]
        public double TestVoltage { get; set; } = 120.0;

        [XmlElement("IsAC")]
        [Description("True for AC test, False for DC test")]
        public bool IsAC { get; set; } = true;

        [XmlElement("LUX_Current")]
        [Description("Current LUX measurement for calibration")]
        public double LUX_Current { get; set; } = 1000.0;

        [XmlElement("Temperature_Current")]
        [Description("Current temperature measurement for calibration (°C)")]
        public double Temperature_Current { get; set; } = 25.0;

        [XmlElement("EclCalib")]
        [Description("ECL calibration value")]
        public double EclCalib { get; set; } = 1.0;
    }
}