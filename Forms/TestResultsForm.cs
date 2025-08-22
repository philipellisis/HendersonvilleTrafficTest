using System;
using System.Drawing;
using System.Windows.Forms;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class TestResultsForm : Form
    {
        public TestResultsForm()
        {
            InitializeComponent();
            InitializeFormData();
        }

        private void InitializeFormData()
        {
            txtTimeDate.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        }

        public void SetPassFailStatus(bool passed)
        {
            txtPassFail.Text = passed ? "PASS" : "FAIL";
            txtPassFail.BackColor = passed ? Color.Green : Color.Red;
            txtPassFail.ForeColor = Color.White;
        }
    }
}