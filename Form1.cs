using HendersonvilleTrafficTest.Configuration;
using HendersonvilleTrafficTest.Forms;

namespace HendersonvilleTrafficTest
{
    public partial class Form1 : Form
    {
        private UserAccount? _currentUser;

        public Form1()
        {
            InitializeComponent();
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
    }
}