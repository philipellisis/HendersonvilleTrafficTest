namespace HendersonvilleTrafficTest.Equipment.Interfaces
{
    public interface IRelayController
    {
        Task<bool> InitializeAsync();
        Task TurnOutputOnAsync(int outputNumber);
        Task TurnOutputOffAsync(int outputNumber);
        Task<byte> ReadAnalogValueAsync(int inputNumber);
        Task<bool> GetOutputStateAsync(int outputNumber);
        int MaxOutputChannels { get; }
        int MaxInputChannels { get; }
        bool IsConnected { get; }
    }
}