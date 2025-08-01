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
        Task<double> GetVoltsAsync();
        Task<double> GetAmpsAsync();
        Task<double> GetWattsAsync();
        Task<double> GetPowerFactorAsync();
        Task<double> GetFrequencyAsync();
        bool IsConnected { get; }
    }
}