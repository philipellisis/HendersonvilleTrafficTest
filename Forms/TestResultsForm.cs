using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HendersonvilleTrafficTest.Controls;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class TestResultsForm : Form
    {
        private List<TestParametersControl> parameterControls = new List<TestParametersControl>();

        public TestResultsForm()
        {
            InitializeComponent();
            InitializeFormData();
        }

        private void InitializeFormData()
        {
            txtTimeDate.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            
            // Demo: Add sample test sections
            AddSampleTestSections();
        }

        private void AddSampleTestSections()
        {
            // Sample data for demonstration
            var section1Data = new TestParameterData[]
            {
                new TestParameterData { Parameter = "VAC", LCL = "110", MEAS = "120", UCL = "130", MEASPassed = true },
                new TestParameterData { Parameter = "mA", LCL = "50", MEAS = "45", UCL = "60", MEASPassed = false },
                new TestParameterData { Parameter = "W", LCL = "5", MEAS = "6.2", UCL = "8", MEASPassed = true },
                new TestParameterData { Parameter = "PF", LCL = "0.8", MEAS = "0.85", UCL = "1.0", MEASPassed = true },
                new TestParameterData { Parameter = "THD(%)", LCL = "0", MEAS = "2.5", UCL = "5", MEASPassed = true },
                new TestParameterData { Parameter = "INTEN", LCL = "800", MEAS = "850", UCL = "1000", MEASPassed = true },
                new TestParameterData { Parameter = "DOM WAVE", LCL = "580", MEAS = "590", UCL = "620", MEASPassed = true },
                new TestParameterData { Parameter = "CCT (K)", LCL = "2700", MEAS = "2800", UCL = "3000", MEASPassed = true },
                new TestParameterData { Parameter = "CCX", LCL = "0.4", MEAS = "0.42", UCL = "0.5", MEASPassed = true },
                new TestParameterData { Parameter = "CCY", LCL = "0.35", MEAS = "0.38", UCL = "0.45", MEASPassed = true },
                new TestParameterData { Parameter = "T (C)", LCL = "20", MEAS = "25", UCL = "30", MEASPassed = true }
            };

            var section2Data = new TestParameterData[]
            {
                new TestParameterData { Parameter = "VAC", LCL = "220", MEAS = "225", UCL = "240", MEASPassed = true },
                new TestParameterData { Parameter = "mA", LCL = "100", MEAS = "105", UCL = "120", MEASPassed = true },
                new TestParameterData { Parameter = "W", LCL = "10", MEAS = "12.4", UCL = "15", MEASPassed = true },
                new TestParameterData { Parameter = "PF", LCL = "0.85", MEAS = "0.90", UCL = "1.0", MEASPassed = true },
                new TestParameterData { Parameter = "THD(%)", LCL = "0", MEAS = "1.8", UCL = "5", MEASPassed = true },
                new TestParameterData { Parameter = "INTEN", LCL = "900", MEAS = "950", UCL = "1100", MEASPassed = true },
                new TestParameterData { Parameter = "DOM WAVE", LCL = "585", MEAS = "595", UCL = "625", MEASPassed = true },
                new TestParameterData { Parameter = "CCT (K)", LCL = "2800", MEAS = "2900", UCL = "3100", MEASPassed = true },
                new TestParameterData { Parameter = "CCX", LCL = "0.42", MEAS = "0.44", UCL = "0.52", MEASPassed = true },
                new TestParameterData { Parameter = "CCY", LCL = "0.37", MEAS = "0.40", UCL = "0.47", MEASPassed = true },
                new TestParameterData { Parameter = "T (C)", LCL = "22", MEAS = "27", UCL = "32", MEASPassed = true }
            };

            AddTestSection("120V Test Results", section1Data);
            AddTestSection("240V Test Results", section2Data);
            AddTestSection("Warm Light Test", section1Data);
            AddTestSection("Cool Light Test", section2Data);
            AddTestSection("High Power Test", section1Data);
            AddTestSection("Low Power Test", section2Data);
        }

        public void SetPassFailStatus(bool passed)
        {
            lblPassFail.Text = passed ? "PASS" : "FAIL";
            lblPassFail.BackColor = passed ? Color.Green : Color.Red;
            lblPassFail.ForeColor = Color.White;
        }

        public void AddTestSection(string sectionTitle, TestParameterData[] parameterData)
        {
            var paramControl = new TestParametersControl();
            paramControl.SectionTitle = sectionTitle;
            paramControl.SetAllParameterData(parameterData);
            paramControl.Margin = new Padding(5, 5, 5, 15); // Add margin for proper spacing
            
            // FlowLayoutPanel handles positioning automatically
            pnlTestParameters.Controls.Add(paramControl);
            parameterControls.Add(paramControl);
        }

        public void AddMultipleTestSections(Dictionary<string, TestParameterData[]> sections)
        {
            foreach (var section in sections)
            {
                AddTestSection(section.Key, section.Value);
            }
        }

        public void ClearTestSections()
        {
            foreach (var control in parameterControls)
            {
                pnlTestParameters.Controls.Remove(control);
                control.Dispose();
            }
            parameterControls.Clear();
        }

        private void UpdateScrollableArea()
        {
            // FlowLayoutPanel handles scrolling automatically, no manual calculation needed
        }

        public void UpdateParameterValues(string sectionTitle, string parameter, string lcl, string meas, string ucl)
        {
            var control = parameterControls.FirstOrDefault(c => c.SectionTitle == sectionTitle);
            control?.SetParameterValues(parameter, lcl, meas, ucl);
        }

        public void UpdateParameterStatus(string sectionTitle, string parameter, string column, bool passed)
        {
            var control = parameterControls.FirstOrDefault(c => c.SectionTitle == sectionTitle);
            control?.SetParameterStatus(parameter, column, passed);
        }

        public void RefreshLayout()
        {
            // Reposition all controls when panel size changes
            for (int i = 0; i < parameterControls.Count; i++)
            {
                int controlWidth = 620;
                int controlsPerRow = Math.Max(1, pnlTestParameters.Width / controlWidth);
                
                int xPosition = (i % controlsPerRow) * controlWidth + 10;
                int yPosition = (i / controlsPerRow) * 395 + 10;
                
                parameterControls[i].Location = new Point(xPosition, yPosition);
            }
            UpdateScrollableArea();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (parameterControls.Count > 0)
            {
                RefreshLayout();
            }
        }
    }
}