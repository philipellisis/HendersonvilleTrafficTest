using HendersonvilleTrafficTest.Forms;

namespace HendersonvilleTrafficTest
{
    public partial class Form1 : Form
    {
        public Form1()
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
    }
}