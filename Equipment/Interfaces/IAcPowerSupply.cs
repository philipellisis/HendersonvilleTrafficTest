namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public interface IAcPowerSupply
    {
        Task InitializeAsync();
        Task<double> GetVoltsAsync();
        Task SetVoltsAsync(double volts);
        Task<double> GetFrequencyAsync();
        Task SetFrequencyAsync(double frequency);
        Task PowerOnAsync();
        Task PowerOffAsync();
        bool IsConnected { get; }
    }
}