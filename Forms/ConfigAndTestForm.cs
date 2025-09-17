using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HendersonvilleTrafficTest.Configuration;
using HendersonvilleTrafficTest.Services;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class ConfigAndTestForm : Form
    {
        public ConfigAndTestForm()
        {
            InitializeComponent();
        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            using var configForm = new ConfigurationForm();
            configForm.ShowDialog(this);
        }

        private void btnTestRelayController_Click(object sender, EventArgs e)
        {
            using var testForm = new RelayControllerTestForm();
            testForm.ShowDialog(this);
        }

        private void btnTestTemperatureSensor_Click(object sender, EventArgs e)
        {
            using var testForm = new TemperatureSensorTestForm();
            testForm.ShowDialog(this);
        }

        private void btnTestPowerAnalyzer_Click(object sender, EventArgs e)
        {
            using var testForm = new PowerAnalyzerTestForm();
            testForm.ShowDialog(this);
        }

        private void btnTestAcPowerSupply_Click(object sender, EventArgs e)
        {
            using var testForm = new AcPowerSupplyTestForm();
            testForm.ShowDialog(this);
        }

        private void btnTestDcPowerSupply_Click(object sender, EventArgs e)
        {
            using var testForm = new DcPowerSupplyTestForm();
            testForm.ShowDialog(this);
        }

        private void btnTestSpectrometer_Click(object sender, EventArgs e)
        {
            using var testForm = new SpectrometerTestForm();
            testForm.ShowDialog(this);
        }

        private void btnSpectrometerCalibration_Click(object sender, EventArgs e)
        {
            using var calibrationForm = new SpectrometerCalibrationForm();
            calibrationForm.ShowDialog(this);
        }

        private void btnColorCalibration_Click(object sender, EventArgs e)
        {
            using var colorCalibrationForm = new ColorCalibrationForm();
            colorCalibrationForm.ShowDialog(this);
        }

        private async void btnCalibrateDarkCurrent_Click(object sender, EventArgs e)
        {
            try
            {
                var factory = new EquipmentFactory();
                var spectrometer = factory.CreateSpectrometer();

                if (!spectrometer.IsConnected)
                {
                    await spectrometer.InitializeAsync();
                }

                if (!spectrometer.IsConnected)
                {
                    MessageBox.Show("Could not connect to spectrometer. Please check connection and try again.", 
                        "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create a simple progress dialog
                using var progressForm = new Form
                {
                    Text = "Dark Current Calibration",
                    Size = new System.Drawing.Size(500, 200),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var lblProgress = new Label
                {
                    Text = "Preparing calibration...",
                    Location = new System.Drawing.Point(20, 30),
                    Size = new System.Drawing.Size(450, 50),
                    Font = new System.Drawing.Font("Segoe UI", 10F)
                };

                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new System.Drawing.Point(200, 100),
                    Size = new System.Drawing.Size(100, 35),
                    DialogResult = DialogResult.Cancel
                };

                progressForm.Controls.Add(lblProgress);
                progressForm.Controls.Add(btnCancel);
                progressForm.CancelButton = btnCancel;

                var cts = new CancellationTokenSource();
                btnCancel.Click += (s, args) => cts.Cancel();

                var progress = new Progress<string>(message => lblProgress.Text = message);

                // Get configuration values
                var config = ConfigurationManager.Current.Equipment;
                int maxIntegrationTimeSeconds = (int)(config.SpectrometerMaxIntegrationTimeMs / 1000);
                double waitBeforeDarkSeconds = config.WaitForDarkCurrentSeconds;

                // Start calibration task
                var calibrationTask = spectrometer.CalibrateDarkCurrentAsync(progress, maxIntegrationTimeSeconds, waitBeforeDarkSeconds, cts.Token);

                // Show progress dialog
                progressForm.Show(this);

                try
                {
                    await calibrationTask;
                    progressForm.Close();
                    MessageBox.Show("Dark current calibration completed successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (OperationCanceledException)
                {
                    progressForm.Close();
                    MessageBox.Show("Dark current calibration was cancelled.", "Cancelled", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    progressForm.Close();
                    MessageBox.Show($"Error during dark current calibration: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start dark current calibration: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            using var userManagementForm = new UserManagementForm();
            userManagementForm.ShowDialog(this);
        }
    }
}