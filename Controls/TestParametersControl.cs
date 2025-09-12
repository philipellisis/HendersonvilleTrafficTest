using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HendersonvilleTrafficTest.Controls
{
    public partial class TestParametersControl : UserControl
    {
        private DataGridView dgvParameters;
        private Label lblSectionTitle;

        public TestParametersControl()
        {
            InitializeComponent();
            InitializeParameters();
        }

        public string SectionTitle
        {
            get => lblSectionTitle.Text;
            set => lblSectionTitle.Text = value;
        }

        private void InitializeParameters()
        {
            string[] parameters = {
                "VAC", "mA", "W", "PF", "THD(%)", "INTEN", 
                "DOM WAVE", "CCX", "CCY", "T (C)"
            };

            foreach (string param in parameters)
            {
                int rowIndex = dgvParameters.Rows.Add();
                dgvParameters.Rows[rowIndex].Cells["PARAM"].Value = param;
            }
        }

        public void SetParameterValues(string parameter, string lcl, string meas, string ucl)
        {
            foreach (DataGridViewRow row in dgvParameters.Rows)
            {
                if (row.Cells["PARAM"].Value?.ToString() == parameter)
                {
                    row.Cells["LCL"].Value = lcl;
                    row.Cells["MEAS"].Value = meas;
                    row.Cells["UCL"].Value = ucl;
                    break;
                }
            }
        }

        public void SetParameterStatus(string parameter, string column, bool passed)
        {
            foreach (DataGridViewRow row in dgvParameters.Rows)
            {
                if (row.Cells["PARAM"].Value?.ToString() == parameter)
                {
                    var cell = row.Cells[column];
                    cell.Style.BackColor = passed ? Color.LightGreen : Color.LightPink;
                    cell.Style.ForeColor = passed ? Color.DarkGreen : Color.DarkRed;
                    break;
                }
            }
        }

        public void SetParameterFieldActive(string parameter, string column, bool active)
        {
            foreach (DataGridViewRow row in dgvParameters.Rows)
            {
                if (row.Cells["PARAM"].Value?.ToString() == parameter)
                {
                    var cell = row.Cells[column];
                    if (!active)
                    {
                        // Gray out inactive fields
                        cell.Style.BackColor = Color.LightGray;
                        cell.Style.ForeColor = Color.DarkGray;
                        cell.Value = "--"; // Replace empty values with dashes
                    }
                    else
                    {
                        // Reset to default colors for active fields
                        cell.Style.BackColor = Color.White;
                        cell.Style.ForeColor = Color.Black;
                    }
                    break;
                }
            }
        }

        public void SetAllParameterData(TestParameterData[] data)
        {
            // First, gray out all parameters that are not provided in the data
            GrayOutUnusedParameters(data);
            
            foreach (var paramData in data)
            {
                SetParameterValues(paramData.Parameter, paramData.LCL, paramData.MEAS, paramData.UCL);
                
                // Auto-detect inactive fields based on empty or meaningless values
                bool lclActive = paramData.LCLActive && !IsFieldEmpty(paramData.LCL);
                bool uclActive = paramData.UCLActive && !IsFieldEmpty(paramData.UCL);
                bool measActive = paramData.MEASActive && !IsFieldEmpty(paramData.MEAS);
                
                // Apply graying out logic for inactive fields
                SetParameterFieldActive(paramData.Parameter, "LCL", lclActive);
                SetParameterFieldActive(paramData.Parameter, "UCL", uclActive);
                SetParameterFieldActive(paramData.Parameter, "MEAS", measActive);
                
                if (paramData.LCLPassed.HasValue)
                    SetParameterStatus(paramData.Parameter, "LCL", paramData.LCLPassed.Value);
                
                if (paramData.MEASPassed.HasValue)
                    SetParameterStatus(paramData.Parameter, "MEAS", paramData.MEASPassed.Value);
                
                if (paramData.UCLPassed.HasValue)
                    SetParameterStatus(paramData.Parameter, "UCL", paramData.UCLPassed.Value);
            }
        }

        private void GrayOutUnusedParameters(TestParameterData[] data)
        {
            var providedParameters = data.Select(d => d.Parameter).ToHashSet();
            
            foreach (DataGridViewRow row in dgvParameters.Rows)
            {
                string parameter = row.Cells["PARAM"].Value?.ToString();
                if (!string.IsNullOrEmpty(parameter) && !providedParameters.Contains(parameter))
                {
                    // Gray out entire row for unused parameters
                    SetParameterFieldActive(parameter, "LCL", false);
                    SetParameterFieldActive(parameter, "UCL", false);
                    SetParameterFieldActive(parameter, "MEAS", false);
                }
            }
        }

        private bool IsFieldEmpty(string value)
        {
            // Consider field empty if it's null, whitespace, or explicitly marked as unused
            return string.IsNullOrWhiteSpace(value) || value == "--" || value == "N/A";
        }
    }

    public class TestParameterData
    {
        public string Parameter { get; set; }
        public string LCL { get; set; }
        public string MEAS { get; set; }
        public string UCL { get; set; }
        public bool? LCLPassed { get; set; }
        public bool? MEASPassed { get; set; }
        public bool? UCLPassed { get; set; }
        
        // Properties to track which fields should be active/grayed out
        public bool LCLActive { get; set; } = true;
        public bool UCLActive { get; set; } = true;
        public bool MEASActive { get; set; } = true;
    }
}