using System.IO.Ports;
using System.Text;

namespace HendersonvilleTrafficTest.Communication
{
    public class RS232Communication : IDisposable
    {
        private SerialPort? _serialPort;
        private readonly SerialPortSettings _settings;
        private readonly object _lock = new();
        private bool _disposed = false;

        public event EventHandler<string>? ErrorOccurred;
        public event EventHandler? ConnectionStateChanged;

        public bool IsConnected => _serialPort?.IsOpen ?? false;
        public SerialPortSettings Settings => _settings.Clone();

        public RS232Communication(SerialPortSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<bool> ConnectAsync()
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen == true)
                        {
                            return true;
                        }

                        _serialPort?.Close();
                        _serialPort?.Dispose();

                        _serialPort = new SerialPort
                        {
                            PortName = _settings.PortName,
                            BaudRate = _settings.BaudRate,
                            Parity = _settings.Parity,
                            DataBits = _settings.DataBits,
                            StopBits = _settings.StopBits,
                            Handshake = _settings.Handshake,
                            ReadTimeout = _settings.ReadTimeoutMs,
                            WriteTimeout = _settings.WriteTimeoutMs,
                            DtrEnable = _settings.DtrEnable,
                            RtsEnable = _settings.RtsEnable
                        };

                        // Only set NewLine if it's being used for binary protocols
                        if (_settings.UseNewLineForBinary && !string.IsNullOrEmpty(_settings.NewLine))
                        {
                            _serialPort.NewLine = _settings.NewLine;
                        }

                        // Only subscribe to error events - let hardware implementations handle data reception
                        _serialPort.ErrorReceived += OnErrorReceived;

                        _serialPort.Open();
                        
                        OnConnectionStateChanged();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to connect to {_settings.PortName}: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public async Task DisconnectAsync()
        {
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen == true)
                        {
                            _serialPort.Close();
                            OnConnectionStateChanged();
                        }
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Error during disconnect: {ex.Message}");
                    }
                }
            });
        }

        public async Task<bool> SendCommandAsync(string command)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen != true)
                        {
                            OnErrorOccurred("Cannot send command: Port is not open");
                            return false;
                        }

                        _serialPort.WriteLine(command);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to send command '{command}': {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public async Task<bool> SendBytesAsync(byte[] data)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen != true)
                        {
                            OnErrorOccurred("Cannot send bytes: Port is not open");
                            return false;
                        }

                        _serialPort.Write(data, 0, data.Length);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to send bytes: {ex.Message}");
                        return false;
                    }
                }
            });
        }

        public async Task<string?> SendCommandAndReceiveAsync(string command, int timeoutMs = 1000)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen != true)
                        {
                            OnErrorOccurred("Cannot send command: Port is not open");
                            return null;
                        }

                        // Clear any existing data in the buffer
                        _serialPort.DiscardInBuffer();
                        
                        // Send command
                        _serialPort.WriteLine(command);
                        
                        // Wait for response with proper timeout management
                        var originalTimeout = _serialPort.ReadTimeout;
                        _serialPort.ReadTimeout = timeoutMs;
                        
                        try
                        {
                            var response = _serialPort.ReadLine();
                            return response;
                        }
                        finally
                        {
                            _serialPort.ReadTimeout = originalTimeout;
                        }
                    }
                    catch (TimeoutException)
                    {
                        OnErrorOccurred($"Timeout waiting for response to command '{command}'");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to send command and receive response '{command}': {ex.Message}");
                        return null;
                    }
                }
            });
        }


        public async Task<string?> ReadLineAsync(int timeoutMs = 1000)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen != true)
                        {
                            return null;
                        }

                        var originalTimeout = _serialPort.ReadTimeout;
                        _serialPort.ReadTimeout = timeoutMs;
                        
                        try
                        {
                            return _serialPort.ReadLine();
                        }
                        finally
                        {
                            _serialPort.ReadTimeout = originalTimeout;
                        }
                    }
                    catch (TimeoutException)
                    {
                        return null;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to read line: {ex.Message}");
                        return null;
                    }
                }
            });
        }

        public async Task<byte[]?> ReadBytesAsync(int timeoutMs = 1000, int expectedBytes = 0)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen != true)
                        {
                            return null;
                        }

                        var originalTimeout = _serialPort.ReadTimeout;
                        _serialPort.ReadTimeout = timeoutMs;
                        
                        try
                        {
                            // Wait for data to arrive
                            var startTime = DateTime.Now;
                            while (_serialPort.BytesToRead == 0 && (DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
                            {
                                Thread.Sleep(10);
                            }

                            if (_serialPort.BytesToRead == 0)
                            {
                                return null; // Timeout waiting for data
                            }

                            // If we're expecting a specific number of bytes, wait for them
                            if (expectedBytes > 0)
                            {
                                while (_serialPort.BytesToRead < expectedBytes && (DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
                                {
                                    Thread.Sleep(10);
                                }
                            }

                            // Read all available bytes
                            var bytesToRead = _serialPort.BytesToRead;
                            var buffer = new byte[bytesToRead];
                            var bytesRead = _serialPort.Read(buffer, 0, bytesToRead);
                            
                            if (bytesRead > 0)
                            {
                                var result = new byte[bytesRead];
                                Array.Copy(buffer, result, bytesRead);
                                return result;
                            }
                            
                            return null;
                        }
                        finally
                        {
                            _serialPort.ReadTimeout = originalTimeout;
                        }
                    }
                    catch (TimeoutException)
                    {
                        return null;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to read bytes: {ex.Message}");
                        return null;
                    }
                }
            });
        }

        public async Task<byte[]?> SendBytesAndReceiveAsync(byte[] data, int timeoutMs = 1000, int expectedBytes = 0)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen != true)
                        {
                            OnErrorOccurred("Cannot send bytes: Port is not open");
                            return null;
                        }

                        // Clear any existing data in the buffer
                        _serialPort.DiscardInBuffer();
                        
                        // Send bytes
                        _serialPort.Write(data, 0, data.Length);
                        
                        // Wait for response with proper timeout management
                        var originalTimeout = _serialPort.ReadTimeout;
                        _serialPort.ReadTimeout = timeoutMs;
                        
                        try
                        {
                            // Wait for data to arrive
                            var startTime = DateTime.Now;
                            while (_serialPort.BytesToRead == 0 && (DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
                            {
                                Thread.Sleep(10);
                            }

                            if (_serialPort.BytesToRead == 0)
                            {
                                return null; // Timeout waiting for data
                            }

                            // If we're expecting a specific number of bytes, wait for them
                            if (expectedBytes > 0)
                            {
                                while (_serialPort.BytesToRead < expectedBytes && (DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
                                {
                                    Thread.Sleep(10);
                                }
                            }

                            // Read all available bytes
                            var bytesToRead = _serialPort.BytesToRead;
                            var buffer = new byte[bytesToRead];
                            var bytesRead = _serialPort.Read(buffer, 0, bytesToRead);
                            
                            if (bytesRead > 0)
                            {
                                var result = new byte[bytesRead];
                                Array.Copy(buffer, result, bytesRead);
                                return result;
                            }
                            
                            return null;
                        }
                        finally
                        {
                            _serialPort.ReadTimeout = originalTimeout;
                        }
                    }
                    catch (TimeoutException)
                    {
                        OnErrorOccurred($"Timeout waiting for response to byte command");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to send bytes and receive response: {ex.Message}");
                        return null;
                    }
                }
            });
        }

        public async Task ClearBuffersAsync()
        {
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen == true)
                        {
                            _serialPort.DiscardInBuffer();
                            _serialPort.DiscardOutBuffer();
                        }
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to clear buffers: {ex.Message}");
                    }
                }
            });
        }




        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        public void SubscribeToDataReceived(SerialDataReceivedEventHandler handler)
        {
            if (_serialPort != null)
            {
                _serialPort.DataReceived += handler;
            }
        }

        public void UnsubscribeFromDataReceived(SerialDataReceivedEventHandler handler)
        {
            if (_serialPort != null)
            {
                _serialPort.DataReceived -= handler;
            }
        }


        private void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            OnErrorOccurred($"Serial port error: {e.EventType}");
        }

        private void OnErrorOccurred(string error)
        {
            ErrorOccurred?.Invoke(this, error);
        }

        private void OnConnectionStateChanged()
        {
            ConnectionStateChanged?.Invoke(this, EventArgs.Empty);
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
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen == true)
                        {
                            _serialPort.Close();
                        }
                        _serialPort?.Dispose();
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Error during disposal: {ex.Message}");
                    }
                }
                _disposed = true;
            }
        }

        ~RS232Communication()
        {
            Dispose(false);
        }
    }
}