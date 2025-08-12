using System.IO.Ports;

namespace HendersonvilleTrafficTest.Communication
{
    public partial class RS232TestForm : Form
    {
        private RS232Communication? _communication;
        private readonly SerialPortSettings _settings;

        public RS232TestForm()
        {
            InitializeComponent();
            _settings = new SerialPortSettings();
            LoadPortSettings();
            RefreshPortList();
        }

        private void LoadPortSettings()
        {
            // Load common baud rates
            cmbBaudRate.Items.AddRange(new object[] { 9600, 19200, 38400, 57600, 115200 });
            cmbBaudRate.SelectedItem = 9600;

            // Load parity options
            cmbParity.Items.AddRange(Enum.GetValues<Parity>().Cast<object>().ToArray());
            cmbParity.SelectedItem = Parity.None;

            // Load data bits
            cmbDataBits.Items.AddRange(new object[] { 7, 8 });
            cmbDataBits.SelectedItem = 8;

            // Load stop bits
            cmbStopBits.Items.AddRange(Enum.GetValues<StopBits>().Cast<object>().ToArray());
            cmbStopBits.SelectedItem = StopBits.One;
        }

        private void RefreshPortList()
        {
            cmbPortName.Items.Clear();
            var ports = RS232Communication.GetAvailablePorts();
            cmbPortName.Items.AddRange(ports);
            
            if (ports.Length > 0)
            {
                cmbPortName.SelectedIndex = 0;
            }

            lblPortCount.Text = $"Available Ports: {ports.Length}";
        }

        private void btnRefreshPorts_Click(object sender, EventArgs e)
        {
            RefreshPortList();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_communication?.IsConnected == true)
                {
                    await _communication.DisconnectAsync();
                    return;
                }

                UpdateSettingsFromUI();
                
                _communication?.Dispose();
                _communication = new RS232Communication(_settings);
                
                // Subscribe to events
                _communication.DataReceived += OnDataReceived;
                _communication.ByteDataReceived += OnByteDataReceived;
                _communication.ErrorOccurred += OnErrorOccurred;
                _communication.ConnectionStateChanged += OnConnectionStateChanged;

                var connected = await _communication.ConnectAsync();
                
                if (!connected)
                {
                    MessageBox.Show("Failed to connect to serial port", "Connection Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSettingsFromUI()
        {
            _settings.PortName = cmbPortName.Text;
            _settings.BaudRate = (int)cmbBaudRate.SelectedItem;
            _settings.Parity = (Parity)cmbParity.SelectedItem;
            _settings.DataBits = (int)cmbDataBits.SelectedItem;
            _settings.StopBits = (StopBits)cmbStopBits.SelectedItem;
            _settings.ReadTimeoutMs = (int)numReadTimeout.Value;
            _settings.WriteTimeoutMs = (int)numWriteTimeout.Value;
        }

        private async void btnSendCommand_Click(object sender, EventArgs e)
        {
            if (_communication?.IsConnected != true)
            {
                MessageBox.Show("Not connected to serial port", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var command = txtCommand.Text.Trim();
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            try
            {
                bool isHexCommand = IsHexString(command);
                
                if (isHexCommand)
                {
                    // Handle hex command
                    var commandBytes = ByteUtilities.HexStringToBytes(command);
                    
                    if (chkExpectResponse.Checked)
                    {
                        AppendToLog($"-> HEX: {ByteUtilities.BytesToHexString(commandBytes)} [Expecting Response]");
                        await _communication.SendBytesAsync(commandBytes);
                    }
                    else
                    {
                        var success = await _communication.SendBytesAsync(commandBytes);
                        AppendToLog($"-> HEX: {ByteUtilities.BytesToHexString(commandBytes)} [{(success ? "Sent" : "Failed")}]");
                    }
                }
                else
                {
                    // Handle text command
                    if (chkExpectResponse.Checked)
                    {
                        var timeout = (int)numResponseTimeout.Value;
                        var response = await _communication.SendCommandAndReceiveAsync(command, timeout);
                        
                        if (response != null)
                        {
                            AppendToLog($"-> TEXT: {command}");
                            AppendToLog($"<- TEXT: {response}");
                        }
                        else
                        {
                            AppendToLog($"-> TEXT: {command}");
                            AppendToLog("<- [No Response]");
                        }
                    }
                    else
                    {
                        var success = await _communication.SendCommandAsync(command);
                        AppendToLog($"-> TEXT: {command} [{(success ? "Sent" : "Failed")}]");
                    }
                }

                // Add command to history
                if (!cmbCommandHistory.Items.Contains(command))
                {
                    cmbCommandHistory.Items.Insert(0, command);
                    if (cmbCommandHistory.Items.Count > 10)
                    {
                        cmbCommandHistory.Items.RemoveAt(10);
                    }
                }
            }
            catch (Exception ex)
            {
                AppendToLog($"Error sending command: {ex.Message}");
            }
        }

        private bool IsHexString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
                
            // Remove spaces and check if all characters are hex digits
            var cleanInput = input.Replace(" ", "").Replace("-", "");
            return cleanInput.Length > 0 && 
                   cleanInput.Length % 2 == 0 && 
                   cleanInput.All(c => "0123456789ABCDEFabcdef".Contains(c));
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void cmbCommandHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommandHistory.SelectedItem != null)
            {
                txtCommand.Text = cmbCommandHistory.SelectedItem.ToString();
            }
        }

        private void OnDataReceived(object? sender, string data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object?, string>(OnDataReceived), sender, data);
                return;
            }

            AppendToLog($"Received TEXT: {data}");
        }

        private void OnByteDataReceived(object? sender, byte[] data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object?, byte[]>(OnByteDataReceived), sender, data);
                return;
            }

            AppendToLog($"Received HEX: {ByteUtilities.BytesToHexString(data)} ({data.Length} bytes)");
            AppendToLog($"Received ASCII: {ByteUtilities.AsciiBytesToString(data)}");
        }

        private void OnErrorOccurred(object? sender, string error)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object?, string>(OnErrorOccurred), sender, error);
                return;
            }

            AppendToLog($"Error: {error}");
        }

        private void OnConnectionStateChanged(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object?, EventArgs>(OnConnectionStateChanged), sender, e);
                return;
            }

            UpdateConnectionUI();
        }

        private void UpdateConnectionUI()
        {
            var isConnected = _communication?.IsConnected == true;
            
            btnConnect.Text = isConnected ? "Disconnect" : "Connect";
            btnConnect.BackColor = isConnected ? Color.LightCoral : Color.LightGreen;
            
            lblConnectionStatus.Text = isConnected ? "Connected" : "Disconnected";
            lblConnectionStatus.ForeColor = isConnected ? Color.Green : Color.Red;
            
            // Enable/disable controls
            grpPortSettings.Enabled = !isConnected;
            grpCommands.Enabled = isConnected;
        }

        private void AppendToLog(string message)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            txtLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
            txtLog.ScrollToCaret();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _communication?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}