using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HendersonvilleTrafficTest.Controls;
using HendersonvilleTrafficTest.Services;
using HendersonvilleTrafficTest.Services.Interfaces;
using HendersonvilleTrafficTest.Services.Models;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class TestResultsForm : Form
    {
        private List<TestParametersControl> parameterControls = new List<TestParametersControl>();
        private readonly IDataAccessService _dataAccessService;

        public TestResultsForm()
        {
            InitializeComponent();
            _dataAccessService = new DataAccessServiceFactory().CreateDataAccessService();
            InitializeFormData();
        }

        private async void InitializeFormData()
        {
            txtTimeDate.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            
            // Load test sequences into dropdown
            await LoadTestSequences();
            
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

        private async Task LoadTestSequences()
        {
            try
            {
                var testSequences = await _dataAccessService.GetAllTestSequencesAsync();
                txtTestCode.Items.Clear();
                foreach (var sequence in testSequences)
                {
                    txtTestCode.Items.Add(sequence);
                }
                
                if (txtTestCode.Items.Count > 0)
                {
                    txtTestCode.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading test sequences: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private async void txtTestCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtTestCode.SelectedItem == null)
                return;

            var selectedSequenceId = txtTestCode.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selectedSequenceId))
                return;

            try
            {
                var testSequenceSteps = await _dataAccessService.GetTestSequenceAsync(selectedSequenceId);
                
                // Clear existing test sections
                ClearTestSections();
                
                // Create test sections from the loaded data
                CreateTestSectionsFromSequence(testSequenceSteps);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading test sequence '{selectedSequenceId}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateTestSectionsFromSequence(TestSequenceStep[] steps)
        {
            // Group steps by their step number or create individual sections
            foreach (var step in steps)
            {
                var parameterData = ConvertStepToParameterData(step);
                AddTestSection($"Step {step.Step}: {step.StpNam}", parameterData);
            }
        }

        private TestParameterData[] ConvertStepToParameterData(TestSequenceStep step)
        {
            var parameterList = new List<TestParameterData>();

            // Add VAC parameters if active
            if (step.VacAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "VAC",
                    LCL = step.VacLcl.ToString("F1"),
                    MEAS = step.VacSet.ToString("F1"),
                    UCL = step.VacUcl.ToString("F1"),
                    MEASPassed = true
                });
            }

            // Add VDC parameters if active
            if (step.VdcAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "VDC",
                    LCL = step.VdcLcl.ToString("F1"),
                    MEAS = step.VdcSet.ToString("F1"),
                    UCL = step.VdcUcl.ToString("F1"),
                    MEASPassed = true
                });
            }

            // Add frequency parameters if active
            if (step.FrqAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "FREQ (Hz)",
                    LCL = step.FrqLcl.ToString("F1"),
                    MEAS = step.FrqSet.ToString("F1"),
                    UCL = step.FrqUcl.ToString("F1"),
                    MEASPassed = true
                });
            }

            // Add current parameters if active
            if (step.IAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "mA",
                    LCL = step.ILcl.ToString("F1"),
                    MEAS = "0",
                    UCL = step.IUcl.ToString("F1"),
                    MEASPassed = true
                });
            }

            // Add power parameters if active
            if (step.PAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "W",
                    LCL = step.PLcl.ToString("F1"),
                    MEAS = "0",
                    UCL = step.PUcl.ToString("F1"),
                    MEASPassed = true
                });
            }

            // Add power factor parameters if active
            if (step.PFAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "PF",
                    LCL = step.PFLcl.ToString("F2"),
                    MEAS = "0",
                    UCL = step.PFUcl.ToString("F2"),
                    MEASPassed = true
                });
            }

            // Add THD parameters if active
            if (step.THDAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "THD(%)",
                    LCL = step.THDLIC.ToString("F1"),
                    MEAS = "0",
                    UCL = step.THDLSC.ToString("F1"),
                    MEASPassed = true
                });
            }

            // Add intensity parameters if active
            if (step.IntAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "INTEN",
                    LCL = step.IntLIC.ToString("F0"),
                    MEAS = "0",
                    UCL = step.IntLSC.ToString("F0"),
                    MEASPassed = true
                });
            }

            // Add color parameters if active
            if (step.ColorAct == 1)
            {
                parameterList.Add(new TestParameterData
                {
                    Parameter = "CCX",
                    LCL = step.X1.ToString("F3"),
                    MEAS = "0",
                    UCL = step.X2.ToString("F3"),
                    MEASPassed = true
                });
                
                parameterList.Add(new TestParameterData
                {
                    Parameter = "CCY",
                    LCL = step.Y1.ToString("F3"),
                    MEAS = "0",
                    UCL = step.Y2.ToString("F3"),
                    MEASPassed = true
                });
            }

            return parameterList.ToArray();
        }

        private void btnColorCalibration_Click(object sender, EventArgs e)
        {
            var colorCalibrationForm = new ColorCalibrationForm();
            colorCalibrationForm.ShowDialog(this);
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