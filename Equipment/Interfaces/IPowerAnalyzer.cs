namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public enum PowerMode
    {
        AC,
        DC
    }

    public interface IPowerAnalyzer
    {
        Task InitializeAsync();
        Task SetModeAsync(PowerMode mode);
        Task<ElectricalMeasurement> GetElectricalsAsync();
        bool IsConnected { get; }
    }
}