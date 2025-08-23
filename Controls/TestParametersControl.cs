using System;
using System.Drawing;
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
                "DOM WAVE", "CCT (K)", "CCX", "CCY", "T (C)"
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

        public void SetAllParameterData(TestParameterData[] data)
        {
            foreach (var paramData in data)
            {
                SetParameterValues(paramData.Parameter, paramData.LCL, paramData.MEAS, paramData.UCL);
                
                if (paramData.LCLPassed.HasValue)
                    SetParameterStatus(paramData.Parameter, "LCL", paramData.LCLPassed.Value);
                
                if (paramData.MEASPassed.HasValue)
                    SetParameterStatus(paramData.Parameter, "MEAS", paramData.MEASPassed.Value);
                
                if (paramData.UCLPassed.HasValue)
                    SetParameterStatus(paramData.Parameter, "UCL", paramData.UCLPassed.Value);
            }
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
    }
}