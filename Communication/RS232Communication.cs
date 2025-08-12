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

        public event EventHandler<string>? DataReceived;
        public event EventHandler<byte[]>? ByteDataReceived;
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

                        _serialPort.DataReceived += OnDataReceived;
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
                        
                        // Wait for response
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

        public async Task<byte[]?> SendBytesAndReceiveAsync(byte[] command, int expectedResponseLength, int timeoutMs = 1000)
        {
            return await SendBytesAndReceiveAsync(command, expectedResponseLength, timeoutMs, false);
        }

        public async Task<byte[]?> SendBytesAndReceiveAsync(byte[] command, int expectedResponseLength, int timeoutMs, bool readUntilTimeout)
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
                        
                        // Send command
                        _serialPort.Write(command, 0, command.Length);
                        
                        // Wait for response
                        var originalTimeout = _serialPort.ReadTimeout;
                        _serialPort.ReadTimeout = timeoutMs;
                        
                        try
                        {
                            if (readUntilTimeout)
                            {
                                // Read all available data until timeout
                                var allData = new List<byte>();
                                var startTime = DateTime.Now;
                                
                                
                                while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
                                {
                                    if (_serialPort.BytesToRead > 0)
                                    {
                                        var buffer = new byte[_serialPort.BytesToRead];
                                        var bytesRead = _serialPort.Read(buffer, 0, buffer.Length);
                                        for (int i = 0; i < bytesRead; i++)
                                        {
                                            allData.Add(buffer[i]);
                                        }
                                        
                                        // Reset timeout if we received data
                                        startTime = DateTime.Now;
                                    }
                                    else
                                    {
                                        Thread.Sleep(10); // Small delay to prevent busy waiting
                                    }
                                }
                                
                                return allData.ToArray();
                            }
                            else
                            {
                                // Read exact number of bytes
                                var buffer = new byte[expectedResponseLength];
                                var totalBytesRead = 0;
                                var startTime = DateTime.Now;
                                
                                while (totalBytesRead < expectedResponseLength && 
                                       (DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
                                {
                                    if (_serialPort.BytesToRead > 0)
                                    {
                                        var bytesToRead = Math.Min(expectedResponseLength - totalBytesRead, _serialPort.BytesToRead);
                                        var bytesRead = _serialPort.Read(buffer, totalBytesRead, bytesToRead);
                                        totalBytesRead += bytesRead;
                                    }
                                    else
                                    {
                                        Thread.Sleep(10);
                                    }
                                }
                                
                                if (totalBytesRead < expectedResponseLength)
                                {
                                    var result = new byte[totalBytesRead];
                                    Array.Copy(buffer, result, totalBytesRead);
                                    return result;
                                }
                                
                                return buffer;
                            }
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

        public async Task<byte[]?> ReadBytesAsync(int count, int timeoutMs = 1000)
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
                            var buffer = new byte[count];
                            var bytesRead = _serialPort.Read(buffer, 0, count);
                            
                            if (bytesRead < count)
                            {
                                var result = new byte[bytesRead];
                                Array.Copy(buffer, result, bytesRead);
                                return result;
                            }
                            
                            return buffer;
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

        public async Task<byte[]?> ReadAvailableBytesAsync()
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    try
                    {
                        if (_serialPort?.IsOpen != true || _serialPort.BytesToRead == 0)
                        {
                            return null;
                        }

                        var buffer = new byte[_serialPort.BytesToRead];
                        var bytesRead = _serialPort.Read(buffer, 0, buffer.Length);
                        
                        if (bytesRead < buffer.Length)
                        {
                            var result = new byte[bytesRead];
                            Array.Copy(buffer, result, bytesRead);
                            return result;
                        }
                        
                        return buffer;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred($"Failed to read available bytes: {ex.Message}");
                        return null;
                    }
                }
            });
        }

        public int BytesAvailable
        {
            get
            {
                lock (_lock)
                {
                    return _serialPort?.IsOpen == true ? _serialPort.BytesToRead : 0;
                }
            }
        }

        public static string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_serialPort?.IsOpen == true && _serialPort.BytesToRead > 0)
                {
                    // Read raw bytes first
                    var buffer = new byte[_serialPort.BytesToRead];
                    var bytesRead = _serialPort.Read(buffer, 0, buffer.Length);
                    
                    if (bytesRead > 0)
                    {
                        var byteData = new byte[bytesRead];
                        Array.Copy(buffer, byteData, bytesRead);
                        
                        // Fire byte data event
                        ByteDataReceived?.Invoke(this, byteData);
                        
                        // Also convert to string and fire text event (for compatibility)
                        var textData = System.Text.Encoding.ASCII.GetString(byteData);
                        if (!string.IsNullOrEmpty(textData))
                        {
                            DataReceived?.Invoke(this, textData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred($"Error reading data: {ex.Message}");
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