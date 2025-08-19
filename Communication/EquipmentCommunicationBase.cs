namespace HendersonvilleTrafficTest.Communication
{
    public abstract class EquipmentCommunicationBase : IDisposable
    {
        protected readonly RS232Communication _communication;
        protected readonly string _equipmentName;
        private bool _disposed = false;

        protected EquipmentCommunicationBase(string equipmentName, SerialPortSettings settings)
        {
            _equipmentName = equipmentName ?? throw new ArgumentNullException(nameof(equipmentName));
            _communication = new RS232Communication(settings);
            
            // Subscribe to communication events
            _communication.ErrorOccurred += OnCommunicationError;
            _communication.ConnectionStateChanged += OnConnectionStateChanged;
        }

        public bool IsConnected => _communication.IsConnected;
        public string EquipmentName => _equipmentName;

        public virtual async Task<bool> InitializeAsync()
        {
            try
            {
                var connected = await _communication.ConnectAsync();
                if (connected)
                {
                    LogInfo($"{_equipmentName} connected successfully on {_communication.Settings.PortName}");
                    
                    // Perform equipment-specific initialization
                    return await PerformEquipmentInitializationAsync();
                }
                else
                {
                    LogError($"{_equipmentName} failed to connect on {_communication.Settings.PortName}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogError($"{_equipmentName} initialization failed: {ex.Message}");
                return false;
            }
        }

        public virtual async Task DisconnectAsync()
        {
            try
            {
                await _communication.DisconnectAsync();
                LogInfo($"{_equipmentName} disconnected");
            }
            catch (Exception ex)
            {
                LogError($"{_equipmentName} disconnect error: {ex.Message}");
            }
        }

        protected virtual async Task<bool> PerformEquipmentInitializationAsync()
        {
            // Override in derived classes for equipment-specific initialization
            return true;
        }

        protected async Task<string?> SendCommandWithResponseAsync(string command, int timeoutMs = 2000)
        {
            try
            {
                LogDebug($"{_equipmentName} -> {command}");
                var response = await _communication.SendCommandAndReceiveAsync(command, timeoutMs);
                
                if (response != null)
                {
                    LogDebug($"{_equipmentName} <- {response}");
                }
                else
                {
                    LogWarning($"{_equipmentName} no response to command: {command}");
                }
                
                return response;
            }
            catch (Exception ex)
            {
                LogError($"{_equipmentName} command error '{command}': {ex.Message}");
                return null;
            }
        }

        protected async Task<bool> SendCommandAsync(string command)
        {
            try
            {
                LogDebug($"{_equipmentName} -> {command}");
                return await _communication.SendCommandAsync(command);
            }
            catch (Exception ex)
            {
                LogError($"{_equipmentName} send command error '{command}': {ex.Message}");
                return false;
            }
        }



        protected async Task<bool> SendBytesAsync(byte[] command)
        {
            try
            {
                LogDebug($"{_equipmentName} -> {ByteUtilities.BytesToHexString(command)}");
                return await _communication.SendBytesAsync(command);
            }
            catch (Exception ex)
            {
                LogError($"{_equipmentName} send bytes error: {ex.Message}");
                return false;
            }
        }

        protected async Task<byte[]?> SendBytesWithResponseAsync(byte[] command, int timeoutMs = 2000, int expectedBytes = 0)
        {
            try
            {
                LogDebug($"{_equipmentName} -> {ByteUtilities.BytesToHexString(command)}");
                var response = await _communication.SendBytesAndReceiveAsync(command, timeoutMs, expectedBytes);
                
                if (response != null)
                {
                    LogDebug($"{_equipmentName} <- {ByteUtilities.BytesToHexString(response)}");
                }
                else
                {
                    LogWarning($"{_equipmentName} no response to byte command");
                }
                
                return response;
            }
            catch (Exception ex)
            {
                LogError($"{_equipmentName} send bytes with response error: {ex.Message}");
                return null;
            }
        }





        protected async Task<bool> SendHexStringAsync(string hexCommand)
        {
            try
            {
                var commandBytes = ByteUtilities.HexStringToBytes(hexCommand);
                return await SendBytesAsync(commandBytes);
            }
            catch (Exception ex)
            {
                LogError($"{_equipmentName} send hex string error '{hexCommand}': {ex.Message}");
                return false;
            }
        }

        protected virtual void OnCommunicationError(object? sender, string error)
        {
            LogError($"{_equipmentName} communication error: {error}");
        }

        protected virtual void OnConnectionStateChanged(object? sender, EventArgs e)
        {
            var state = IsConnected ? "Connected" : "Disconnected";
            LogInfo($"{_equipmentName} connection state changed: {state}");
        }

        protected virtual void LogInfo(string message)
        {
            // Override in derived classes or integrate with your logging system
            System.Diagnostics.Debug.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss.fff} {message}");
        }

        protected virtual void LogWarning(string message)
        {
            // Override in derived classes or integrate with your logging system
            System.Diagnostics.Debug.WriteLine($"[WARN] {DateTime.Now:HH:mm:ss.fff} {message}");
        }

        protected virtual void LogError(string message)
        {
            // Override in derived classes or integrate with your logging system
            System.Diagnostics.Debug.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss.fff} {message}");
        }

        protected virtual void LogDebug(string message)
        {
            // Override in derived classes or integrate with your logging system
            System.Diagnostics.Debug.WriteLine($"[DEBUG] {DateTime.Now:HH:mm:ss.fff} {message}");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _communication?.Dispose();
                _disposed = true;
            }
        }

        ~EquipmentCommunicationBase()
        {
            Dispose(false);
        }
    }
}