using System;
using System.Windows.Forms;

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
    }
}