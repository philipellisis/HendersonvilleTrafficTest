namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public interface IDcPowerSupply
    {
        Task InitializeAsync();
        Task<double> GetVoltsAsync();
        Task SetVoltsAsync(double volts);
        Task<double> GetAmpsAsync();
        Task SetAmpsAsync(double amps);
        Task PowerOnAsync();
        Task PowerOffAsync();
        bool IsConnected { get; }
    }
}