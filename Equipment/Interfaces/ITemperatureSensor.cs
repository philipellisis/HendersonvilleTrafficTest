namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public interface ITemperatureSensor
    {
        Task InitializeAsync();
        Task<double> GetTemperatureAsync();
        bool IsConnected { get; }
    }
}