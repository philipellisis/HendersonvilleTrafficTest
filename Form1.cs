using HendersonvilleTrafficTest.Configuration;
using HendersonvilleTrafficTest.Forms;
using HendersonvilleTrafficTest.Services;

namespace HendersonvilleTrafficTest
{
    public partial class Form1 : Form
    {
        private UserAccount? _currentUser;

        public Form1()
        {
            InitializeComponent();
            InitializeEquipmentAsync();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate credentials
            var userAccount = ValidateCredentials(username, password);
            if (userAccount != null)
            {
                _currentUser = userAccount;
                EnableUserInterface();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private UserAccount? ValidateCredentials(string username, string password)
        {
            var userAccounts = ConfigurationManager.Current.UserAccounts.GetUserAccounts();
            return userAccounts.FirstOrDefault(u => 
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
                u.Password == password && 
                u.IsActive);
        }

        private void EnableUserInterface()
        {
            if (_currentUser == null) return;

            // Enable buttons based on user permissions
            switch (_currentUser.UserType)
            {
                case UserType.TestOperator:
                    btnTest.Enabled = true;
                    btnConfigure.Enabled = false;
                    break;

                case UserType.ProductionSupervisor:
                    btnTest.Enabled = true;
                    btnConfigure.Enabled = true;
                    break;

                case UserType.CalibrationTechnician:
                    btnTest.Enabled = true;
                    btnConfigure.Enabled = true;
                    break;

                case UserType.Engineer:
                    btnTest.Enabled = true;
                    btnConfigure.Enabled = true;
                    break;

                default:
                    btnTest.Enabled = false;
                    btnConfigure.Enabled = false;
                    break;
            }

            // Update form title to show current user
            this.Text = $"Hendersonville Traffic Test - Logged in as: {_currentUser.FullName} ({_currentUser.UserType})";
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("Please log in first.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var testResultsForm = new TestResultsForm();
            testResultsForm.ShowDialog(this);
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("Please log in first.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if user has configuration permissions
            if (_currentUser.UserType == UserType.TestOperator)
            {
                MessageBox.Show("You do not have permission to access configuration.", "Access Denied", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var configAndTestForm = new ConfigAndTestForm();
            configAndTestForm.ShowDialog(this);
        }

        private async void InitializeEquipmentAsync()
        {
            try
            {
                var statusMessage = "Equipment Initialization Status:\n\n";
                var factory = new EquipmentFactory();
                var equipment = ConfigurationManager.Current.Equipment;

                // Initialize AC Power Supply
                try
                {
                    var acPowerSupply = factory.CreateAcPowerSupply();
                    await acPowerSupply.InitializeAsync();
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.AcPowerSupplyMode.ToString();
                    statusMessage += $"✓ AC Power Supply: Connected ({mode})\n";
                }
                catch (Exception ex)
                {
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.AcPowerSupplyMode.ToString();
                    statusMessage += $"✗ AC Power Supply: Failed ({mode}) - {ex.Message}\n";
                }

                // Initialize DC Power Supply
                try
                {
                    var dcPowerSupply = factory.CreateDcPowerSupply();
                    await dcPowerSupply.InitializeAsync();
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.DcPowerSupplyMode.ToString();
                    statusMessage += $"✓ DC Power Supply: Connected ({mode})\n";
                }
                catch (Exception ex)
                {
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.DcPowerSupplyMode.ToString();
                    statusMessage += $"✗ DC Power Supply: Failed ({mode}) - {ex.Message}\n";
                }

                // Initialize Power Analyzer
                try
                {
                    var powerAnalyzer = factory.CreatePowerAnalyzer();
                    await powerAnalyzer.InitializeAsync();
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.PowerAnalyzerMode.ToString();
                    statusMessage += $"✓ Power Analyzer: Connected ({mode})\n";
                }
                catch (Exception ex)
                {
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.PowerAnalyzerMode.ToString();
                    statusMessage += $"✗ Power Analyzer: Failed ({mode}) - {ex.Message}\n";
                }

                // Initialize Spectrometer
                try
                {
                    var spectrometer = factory.CreateSpectrometer();
                    await spectrometer.InitializeAsync();
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.SpectrometerType.ToString();
                    statusMessage += $"✓ Spectrometer: Connected ({mode})\n";
                }
                catch (Exception ex)
                {
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.SpectrometerType.ToString();
                    statusMessage += $"✗ Spectrometer: Failed ({mode}) - {ex.Message}\n";
                }

                // Initialize Temperature Sensor
                try
                {
                    var temperatureSensor = factory.CreateTemperatureSensor();
                    await temperatureSensor.InitializeAsync();
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.TemperatureSensorMode.ToString();
                    statusMessage += $"✓ Temperature Sensor: Connected ({mode})\n";
                }
                catch (Exception ex)
                {
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.TemperatureSensorMode.ToString();
                    statusMessage += $"✗ Temperature Sensor: Failed ({mode}) - {ex.Message}\n";
                }

                // Initialize Relay Controller
                try
                {
                    var relayController = factory.CreateRelayController();
                    await relayController.InitializeAsync();
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.RelayControllerMode.ToString();
                    statusMessage += $"✓ Relay Controller: Connected ({mode})\n";
                }
                catch (Exception ex)
                {
                    var mode = equipment.GlobalSimulationMode ? "Simulation" : equipment.RelayControllerMode.ToString();
                    statusMessage += $"✗ Relay Controller: Failed ({mode}) - {ex.Message}\n";
                }

                // Show overall status
                if (equipment.GlobalSimulationMode)
                {
                    statusMessage += "\n🔄 Global Simulation Mode: ENABLED\n";
                    statusMessage += "All equipment is running in simulation mode.";
                }
                else
                {
                    statusMessage += "\n🔧 Hardware Mode: Individual equipment modes apply";
                }

                MessageBox.Show(statusMessage, "Equipment Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Equipment initialization error: {ex.Message}", "Initialization Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}