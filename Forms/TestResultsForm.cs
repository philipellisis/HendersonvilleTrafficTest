using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HendersonvilleTrafficTest.Configuration;
using HendersonvilleTrafficTest.Controls;
using HendersonvilleTrafficTest.Equipment.Interfaces;
using HendersonvilleTrafficTest.Services;
using HendersonvilleTrafficTest.Services.Interfaces;
using HendersonvilleTrafficTest.Services.Models;
using HendersonvilleTrafficTest.Shared;

namespace HendersonvilleTrafficTest.Forms
{
    public partial class TestResultsForm : Form
    {
        private List<TestParametersControl> parameterControls = new List<TestParametersControl>();
        private  IDataAccessService _dataAccessService;
        private  IAcPowerSupply _acPowerSupply;
        private  IDcPowerSupply _dcPowerSupply;
        private  IPowerAnalyzer _powerAnalyzer;
        private  ISpectrometer _spectrometer;
        private  ITemperatureSensor _temperatureSensor;
        private  IRelayController _relayController;
        private bool _testInProgress = false;
        private List<TestMeasurementResult> _testResults = new List<TestMeasurementResult>();

        public TestResultsForm()
        {
            InitializeComponent();
            _dataAccessService = new DataAccessServiceFactory().CreateDataAccessService();
            InitializeEquipment();
            InitializeFormData();
        }

        private void InitializeEquipment()
        {
            var equipmentFactory = new EquipmentFactory();
            _acPowerSupply = equipmentFactory.CreateAcPowerSupply();
            _dcPowerSupply = equipmentFactory.CreateDcPowerSupply();
            _powerAnalyzer = equipmentFactory.CreatePowerAnalyzer();
            _spectrometer = equipmentFactory.CreateSpectrometer();
            _temperatureSensor = equipmentFactory.CreateTemperatureSensor();
            _relayController = equipmentFactory.CreateRelayController();
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

        private async void btnStartTest_Click(object sender, EventArgs e)
        {
            if (_testInProgress)
            {
                UpdateTestStatus("Test already in progress...");
                return;
            }

            if (txtTestCode.SelectedItem == null)
            {
                UpdateTestStatus("Please select a test sequence first.");
                return;
            }

            await RunTestSequence();
        }

        private async Task RunTestSequence()
        {
            try
            {
                _testInProgress = true;
                btnStartTest.Enabled = false;
                txtTestCode.Enabled = false;
                prgTestProgress.Value = 0;
                _testResults.Clear();

                var selectedSequenceId = txtTestCode.SelectedItem.ToString();
                UpdateTestStatus($"Starting test sequence: {selectedSequenceId}");

                // Get test sequence steps
                var testSteps = await _dataAccessService.GetTestSequenceAsync(selectedSequenceId);
                if (testSteps.Length == 0)
                {
                    UpdateTestStatus("No test steps found for selected sequence.");
                    return;
                }

                // Initialize equipment
                UpdateTestStatus("Initializing equipment...");
                await InitializeTestEquipment();
                UpdateProgress(5);

                int stepCount = testSteps.Length;
                for (int i = 0; i < stepCount; i++)
                {
                    var step = testSteps[i];
                    UpdateTestStatus($"Executing step {step.Step}: {step.StpNam}");

                    var result = await ExecuteTestStep(step);
                    _testResults.Add(result);

                    // Update progress
                    int progressPercentage = (int)((double)(i + 1) / stepCount * 95) + 5;
                    UpdateProgress(progressPercentage);

                    // Update the UI with the results
                    UpdateTestParameterDisplay(step, result);
                }

                // Calculate overall pass/fail
                bool overallPassed = _testResults.All(r => r.OverallTestPassed);
                SetPassFailStatus(overallPassed);

                UpdateTestStatus($"Test sequence completed. Overall result: {(overallPassed ? "PASS" : "FAIL")}");
                UpdateProgress(100);
            }
            catch (Exception ex)
            {
                UpdateTestStatus($"Error during test sequence: {ex.Message}");
                MessageBox.Show($"Test sequence failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _testInProgress = false;
                btnStartTest.Enabled = true;
                txtTestCode.Enabled = true;
            }
        }

        private async Task InitializeTestEquipment()
        {
            await _acPowerSupply.InitializeAsync();
            await _dcPowerSupply.InitializeAsync();
            await _powerAnalyzer.InitializeAsync();
            await _spectrometer.InitializeAsync();
            await _temperatureSensor.InitializeAsync();
            await _relayController.InitializeAsync();
        }

        private async Task<TestMeasurementResult> ExecuteTestStep(TestSequenceStep step)
        {
            var result = new TestMeasurementResult
            {
                TestSequenceId = step.Sequence,
                StepNumber = step.Step,
                StepName = step.StpNam,
                TestDateTime = DateTime.Now,
                RelayUsed = step.Relay
            };

            var towerConfig = ConfigurationManager.Current.Tower;

            // Step 1: Check E-Stop
            UpdateTestStatus("Checking E-Stop status...");
            if (await _relayController.ReadAnalogValueAsync(towerConfig.EStopInput))
            {
                throw new InvalidOperationException("E-Stop is activated. Test cannot continue.");
            }

            // Step 2: Check Light Current
            UpdateTestStatus("Checking light current status...");
            if (await _relayController.ReadAnalogValueAsync(towerConfig.LightCurrentInput))
            {
                throw new InvalidOperationException("Light current is detected. Test cannot continue for safety.");
            }

            // Step 2a: Handle CountdownShort relay based on step.Relay
            bool countdownShortActivated = false;
            if (step.Relay != 0)
            {
                UpdateTestStatus("Activating countdown short relay...");
                await _relayController.TurnOutputOnAsync(towerConfig.CountdownShort);
                countdownShortActivated = true;
            }

            try
            {
                // Step 3: Set voltage and turn on power
                await ConfigurePowerSupply(step, result);

                // Step 4: Wait 3 seconds and take electrical readings
                UpdateTestStatus("Waiting for stabilization (3 seconds)...");
                await Task.Delay(3000);

                await TakeElectricalMeasurements(step, result);

                // Step 5: Take spectrometer readings if applicable
                if (step.IntAct == 1 || step.ColorAct == 1)
                {
                    await TakeSpectrometerMeasurements(step, result);
                }

                // Power off
                UpdateTestStatus("Powering off lamp...");
                await PowerOffLamp(step);

                // Evaluate test results
                EvaluateTestResults(step, result);
            }
            finally
            {
                // Always deactivate CountdownShort if it was activated
                if (countdownShortActivated)
                {
                    UpdateTestStatus("Deactivating countdown short relay...");
                    await _relayController.TurnOutputOffAsync(towerConfig.CountdownShort);
                }
            }

            return result;
        }

        private async Task ConfigurePowerSupply(TestSequenceStep step, TestMeasurementResult result)
        {
            if (step.VacAct == 1)
            {
                // AC Power Supply
                UpdateTestStatus($"Setting AC voltage to {step.VacSet}V...");
                result.IsAcTest = true;
                result.VoltageSet = step.VacSet;
                
                await _acPowerSupply.SetVoltsAsync(step.VacSet);
                if (step.FrqAct == 1)
                {
                    await _acPowerSupply.SetFrequencyAsync(step.FrqSet);
                }
                await _acPowerSupply.PowerOnAsync();
            }
            else if (step.VdcAct == 1)
            {
                // DC Power Supply
                UpdateTestStatus($"Setting DC voltage to {step.VdcSet}V...");
                result.IsAcTest = false;
                result.VoltageSet = step.VdcSet;
                
                await _dcPowerSupply.SetVoltsAsync(step.VdcSet);
                await _dcPowerSupply.PowerOnAsync();
            }
        }

        private async Task TakeElectricalMeasurements(TestSequenceStep step, TestMeasurementResult result)
        {
            UpdateTestStatus("Taking electrical measurements...");

            var measurement = await _powerAnalyzer.GetElectricalsAsync();
            
            result.VoltageMeasured = measurement.Voltage;
            result.CurrentMeasured = measurement.Current;
            result.PowerMeasured = measurement.Power;
            //result.PowerFactorMeasured = measurement.PowerFactor;
            //result.ThdMeasured = measurement.Thd;
            result.FrequencyMeasured = measurement.Frequency;

            // Take temperature measurement
            result.TemperatureMeasured = await _temperatureSensor.GetTemperatureAsync();
        }

        private async Task TakeSpectrometerMeasurements(TestSequenceStep step, TestMeasurementResult result)
        {
            UpdateTestStatus("Taking spectrum measurement...");
            var integrationTime = await _spectrometer.AutoRangeAsync();
            var spectrumReading = await _spectrometer.GetSpectrumReadingAsync();

            // Power off for dark current measurement
            await PowerOffLamp(step);
            
            UpdateTestStatus("Waiting for dark current stabilization (3 seconds)...");
            await Task.Delay(3000);

            UpdateTestStatus("Taking dark current measurement...");
            var darkCurrentReading = await _spectrometer.GetSpectrumReadingAsync();

            // Process spectrum data
            UpdateTestStatus("Processing spectrum data...");
            var normalizedSpectrum = MathUtils.NormalizeSpectrumReading(spectrumReading, darkCurrentReading);
            var calibratedSpectrum = MathUtils.ApplyCalibrationFactors(normalizedSpectrum, integrationTime);

            // Calculate color values
            UpdateTestStatus("Calculating color values...");
            var colorData = CieColorCalculator.CalculateFromSpectrumData(calibratedSpectrum.Wavelengths, calibratedSpectrum.Intensities);

            result.IntensityMeasured = colorData.Luminance;
            //result.DominantWavelength = colorData.;
            result.Cct = colorData.CCT;
            result.CcxMeasured = colorData.CcX;
            result.CcyMeasured = colorData.CcY;
            result.Luminance = colorData.Luminance;
        }

        private async Task PowerOffLamp(TestSequenceStep step)
        {
            if (step.VacAct == 1)
            {
                await _acPowerSupply.PowerOffAsync();
            }
            else if (step.VdcAct == 1)
            {
                await _dcPowerSupply.PowerOffAsync();
            }
        }

        private void EvaluateTestResults(TestSequenceStep step, TestMeasurementResult result)
        {
            // Voltage test
            if (step.VacAct == 1)
            {
                result.VoltageTestPassed = result.VoltageMeasured >= step.VacLcl && result.VoltageMeasured <= step.VacUcl;
            }
            else if (step.VdcAct == 1)
            {
                result.VoltageTestPassed = result.VoltageMeasured >= step.VdcLcl && result.VoltageMeasured <= step.VdcUcl;
            }

            // Current test
            if (step.IAct == 1)
            {
                result.CurrentTestPassed = result.CurrentMeasured >= step.ILcl && result.CurrentMeasured <= step.IUcl;
            }

            // Power test
            if (step.PAct == 1)
            {
                result.PowerTestPassed = result.PowerMeasured >= step.PLcl && result.PowerMeasured <= step.PUcl;
            }

            // Power Factor test
            if (step.PFAct == 1)
            {
                result.PowerFactorTestPassed = result.PowerFactorMeasured >= step.PFLcl && result.PowerFactorMeasured <= step.PFUcl;
            }

            // THD test
            if (step.THDAct == 1)
            {
                result.ThdTestPassed = result.ThdMeasured >= step.THDLIC && result.ThdMeasured <= step.THDLSC;
            }

            // Frequency test
            if (step.FrqAct == 1)
            {
                result.FrequencyTestPassed = result.FrequencyMeasured >= step.FrqLcl && result.FrequencyMeasured <= step.FrqUcl;
            }

            // Intensity test
            if (step.IntAct == 1)
            {
                result.IntensityTestPassed = result.IntensityMeasured >= step.IntLIC && result.IntensityMeasured <= step.IntLSC;
            }

            // Color test (simplified - checking if within first color boundary)
            if (step.ColorAct == 1)
            {
                result.ColorTestPassed = result.CcxMeasured >= step.X1 && result.CcxMeasured <= step.X2 &&
                                        result.CcyMeasured >= step.Y1 && result.CcyMeasured <= step.Y2;
            }

            // Overall test result
            result.OverallTestPassed = result.VoltageTestPassed && result.CurrentTestPassed && 
                                     result.PowerTestPassed && result.PowerFactorTestPassed &&
                                     result.ThdTestPassed && result.FrequencyTestPassed &&
                                     result.IntensityTestPassed && result.ColorTestPassed;
        }

        private void UpdateTestParameterDisplay(TestSequenceStep step, TestMeasurementResult result)
        {
            var sectionTitle = $"Step {step.Step}: {step.StpNam}";
            var control = parameterControls.FirstOrDefault(c => c.SectionTitle == sectionTitle);
            
            if (control != null)
            {
                // Update the measurement values and pass/fail status
                if (step.VacAct == 1)
                {
                    control.SetParameterValues("VAC", step.VacLcl.ToString("F1"), 
                        result.VoltageMeasured.ToString("F1"), step.VacUcl.ToString("F1"));
                    control.SetParameterStatus("VAC", "MEAS", result.VoltageTestPassed);
                }
                
                if (step.IAct == 1)
                {
                    control.SetParameterValues("mA", step.ILcl.ToString("F1"), 
                        result.CurrentMeasured.ToString("F1"), step.IUcl.ToString("F1"));
                    control.SetParameterStatus("mA", "MEAS", result.CurrentTestPassed);
                }

                if (step.PAct == 1)
                {
                    control.SetParameterValues("W", step.PLcl.ToString("F1"), 
                        result.PowerMeasured.ToString("F1"), step.PUcl.ToString("F1"));
                    control.SetParameterStatus("W", "MEAS", result.PowerTestPassed);
                }

                if (step.PFAct == 1)
                {
                    control.SetParameterValues("PF", step.PFLcl.ToString("F2"), 
                        result.PowerFactorMeasured.ToString("F2"), step.PFUcl.ToString("F2"));
                    control.SetParameterStatus("PF", "MEAS", result.PowerFactorTestPassed);
                }

                if (step.THDAct == 1)
                {
                    control.SetParameterValues("THD(%)", step.THDLIC.ToString("F1"), 
                        result.ThdMeasured.ToString("F1"), step.THDLSC.ToString("F1"));
                    control.SetParameterStatus("THD(%)", "MEAS", result.ThdTestPassed);
                }

                if (step.IntAct == 1)
                {
                    control.SetParameterValues("INTEN", step.IntLIC.ToString("F0"), 
                        result.IntensityMeasured.ToString("F0"), step.IntLSC.ToString("F0"));
                    control.SetParameterStatus("INTEN", "MEAS", result.IntensityTestPassed);
                }

                if (step.ColorAct == 1)
                {
                    control.SetParameterValues("CCX", step.X1.ToString("F3"), 
                        result.CcxMeasured.ToString("F3"), step.X2.ToString("F3"));
                    control.SetParameterStatus("CCX", "MEAS", result.ColorTestPassed);

                    control.SetParameterValues("CCY", step.Y1.ToString("F3"), 
                        result.CcyMeasured.ToString("F3"), step.Y2.ToString("F3"));
                    control.SetParameterStatus("CCY", "MEAS", result.ColorTestPassed);
                }

                control.SetParameterValues("T (C)", "20", 
                    result.TemperatureMeasured.ToString("F1"), "30");
            }
        }

        private void UpdateTestStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateTestStatus), message);
                return;
            }

            txtTestStatus.AppendText($"{DateTime.Now:HH:mm:ss} - {message}\r\n");
            txtTestStatus.ScrollToCaret();
            Application.DoEvents();
        }

        private void UpdateProgress(int percentage)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgress), percentage);
                return;
            }

            prgTestProgress.Value = Math.Min(100, Math.Max(0, percentage));
            Application.DoEvents();
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