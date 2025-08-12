using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class RelayControllerTestForm : Form
    {
        private readonly IRelayController _relayController;
        private readonly Button[] _onButtons;
        private readonly Button[] _offButtons;
        private readonly Label[] _stateLabels;
        private readonly TextBox[] _adcTextBoxes;
        private readonly Button[] _readAdcButtons;
        private readonly System.Windows.Forms.Timer _autoReadTimer;

        public RelayControllerTestForm()
        {
            InitializeComponent();
            
            var factory = new EquipmentFactory();
            _relayController = factory.CreateRelayController();
            
            _onButtons = new Button[8];
            _offButtons = new Button[8];
            _stateLabels = new Label[8];
            _adcTextBoxes = new TextBox[8];
            _readAdcButtons = new Button[8];
            
            _autoReadTimer = new System.Windows.Forms.Timer();
            _autoReadTimer.Interval = 1000; // 1 second
            _autoReadTimer.Tick += AutoReadTimer_Tick;
            
            CreateControls();
            InitializeController();
        }

        private async void InitializeController()
        {
            try
            {
                await _relayController.InitializeAsync();
                lblConnectionStatus.Text = $"Connected: {_relayController.IsConnected}";
                lblConnectionStatus.ForeColor = _relayController.IsConnected ? Color.Green : Color.Red;
                
                if (_relayController.IsConnected)
                {
                    await RefreshAllStates();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize relay controller: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblConnectionStatus.Text = "Connection Failed";
                lblConnectionStatus.ForeColor = Color.Red;
            }
        }

        private void CreateControls()
        {
            for (int i = 0; i < 8; i++)
            {
                int channel = i + 1;
                int yPos = 60 + (i * 35);
                
                // Relay controls
                var channelLabel = new Label
                {
                    Text = $"Relay {channel}:",
                    Location = new Point(12, yPos + 5),
                    Size = new Size(60, 23),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                
                _onButtons[i] = new Button
                {
                    Text = "ON",
                    Location = new Point(80, yPos),
                    Size = new Size(50, 25),
                    BackColor = Color.LightGreen,
                    Tag = channel
                };
                _onButtons[i].Click += OnButton_Click;
                
                _offButtons[i] = new Button
                {
                    Text = "OFF",
                    Location = new Point(135, yPos),
                    Size = new Size(50, 25),
                    BackColor = Color.LightCoral,
                    Tag = channel
                };
                _offButtons[i].Click += OffButton_Click;
                
                _stateLabels[i] = new Label
                {
                    Text = "OFF",
                    Location = new Point(195, yPos + 5),
                    Size = new Size(40, 23),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.Red
                };
                
                // ADC controls
                var adcLabel = new Label
                {
                    Text = $"ADC {channel}:",
                    Location = new Point(270, yPos + 5),
                    Size = new Size(50, 23),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                
                _adcTextBoxes[i] = new TextBox
                {
                    Location = new Point(325, yPos),
                    Size = new Size(60, 25),
                    ReadOnly = true,
                    TextAlign = HorizontalAlignment.Center,
                    Font = new Font("Segoe UI", 9F)
                };
                
                _readAdcButtons[i] = new Button
                {
                    Text = "Read",
                    Location = new Point(395, yPos),
                    Size = new Size(50, 25),
                    Tag = channel
                };
                _readAdcButtons[i].Click += ReadAdcButton_Click;
                
                // Add controls to form
                pnlControls.Controls.AddRange(new Control[] {
                    channelLabel, _onButtons[i], _offButtons[i], _stateLabels[i],
                    adcLabel, _adcTextBoxes[i], _readAdcButtons[i]
                });
            }
        }

        private async void OnButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is int channel)
            {
                try
                {
                    await _relayController.TurnOutputOnAsync(channel);
                    await RefreshRelayState(channel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to turn on relay {channel}: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void OffButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is int channel)
            {
                try
                {
                    await _relayController.TurnOutputOffAsync(channel);
                    await RefreshRelayState(channel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to turn off relay {channel}: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void ReadAdcButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is int channel)
            {
                try
                {
                    var value = await _relayController.ReadAnalogValueAsync(channel);
                    _adcTextBoxes[channel - 1].Text = value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to read ADC {channel}: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _adcTextBoxes[channel - 1].Text = "Error";
                }
            }
        }

        private async Task RefreshRelayState(int channel)
        {
            try
            {
                var state = await _relayController.GetOutputStateAsync(channel);
                var label = _stateLabels[channel - 1];
                label.Text = state ? "ON" : "OFF";
                label.ForeColor = state ? Color.Green : Color.Red;
            }
            catch (Exception ex)
            {
                _stateLabels[channel - 1].Text = "Error";
                _stateLabels[channel - 1].ForeColor = Color.Orange;
            }
        }

        private async Task RefreshAllStates()
        {
            for (int i = 1; i <= 8; i++)
            {
                await RefreshRelayState(i);
            }
        }

        private async void AutoReadTimer_Tick(object sender, EventArgs e)
        {
            if (!_relayController.IsConnected) return;
            
            for (int i = 1; i <= 8; i++)
            {
                try
                {
                    var value = await _relayController.ReadAnalogValueAsync(i);
                    _adcTextBoxes[i - 1].Text = value.ToString();
                }
                catch
                {
                    _adcTextBoxes[i - 1].Text = "Error";
                }
            }
        }

        private void chkAutoRead_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoRead.Checked)
            {
                _autoReadTimer.Start();
            }
            else
            {
                _autoReadTimer.Stop();
            }
        }

        private async void btnRefreshAll_Click(object sender, EventArgs e)
        {
            await RefreshAllStates();
            
            for (int i = 1; i <= 8; i++)
            {
                try
                {
                    var value = await _relayController.ReadAnalogValueAsync(i);
                    _adcTextBoxes[i - 1].Text = value.ToString();
                }
                catch
                {
                    _adcTextBoxes[i - 1].Text = "Error";
                }
            }
        }

        private async void btnAllOn_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 8; i++)
            {
                try
                {
                    await _relayController.TurnOutputOnAsync(i);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to turn on relay {i}: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            await RefreshAllStates();
        }

        private async void btnAllOff_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 8; i++)
            {
                try
                {
                    await _relayController.TurnOutputOffAsync(i);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to turn off relay {i}: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            await RefreshAllStates();
        }

        private async void btnTestAll_Click(object sender, EventArgs e)
        {
            // Disable the button to prevent multiple simultaneous tests
            btnTestAll.Enabled = false;
            btnTestAll.Text = "Testing...";
            
            try
            {
                // Test each relay individually
                for (int i = 1; i <= 8; i++)
                {
                    try
                    {
                        // Turn relay ON
                        await _relayController.TurnOutputOnAsync(i);
                        await RefreshRelayState(i);
                        
                        // Wait 500ms so user can see it
                        await Task.Delay(500);
                        
                        // Verify it's actually ON
                        var actualState = await _relayController.GetOutputStateAsync(i);
                        if (actualState)
                        {
                            // Turn relay OFF
                            await _relayController.TurnOutputOffAsync(i);
                            await RefreshRelayState(i);
                            
                            // Wait 500ms so user can see it
                            await Task.Delay(500);
                            
                            // Verify it's actually OFF
                            var offState = await _relayController.GetOutputStateAsync(i);
                            if (!offState)
                            {
                                // Test passed
                                continue;
                            }
                            else
                            {
                                MessageBox.Show($"Relay {i} test FAILED: Could not turn OFF", "Test Failed", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Relay {i} test FAILED: Could not turn ON", "Test Failed", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Relay {i} test ERROR: {ex.Message}", "Test Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
                // Final refresh to show all states
                await RefreshAllStates();
                
                MessageBox.Show("All relay tests completed!", "Test Complete", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                // Re-enable the button
                btnTestAll.Enabled = true;
                btnTestAll.Text = "Test All";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _autoReadTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}