namespace HendersonvilleTrafficTest.Controls
{
    partial class TestParametersControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvParameters = new System.Windows.Forms.DataGridView();
            this.lblSectionTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameters)).BeginInit();
            this.SuspendLayout();
            
            // 
            // lblSectionTitle
            // 
            this.lblSectionTitle.AutoSize = false;
            this.lblSectionTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSectionTitle.Location = new System.Drawing.Point(0, 5);
            this.lblSectionTitle.Name = "lblSectionTitle";
            this.lblSectionTitle.Size = new System.Drawing.Size(350, 55);
            this.lblSectionTitle.TabIndex = 0;
            this.lblSectionTitle.Text = "Test Section";
            this.lblSectionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSectionTitle.BackColor = System.Drawing.Color.LightBlue;
            this.lblSectionTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            
            // 
            // dgvParameters
            // 
            this.dgvParameters.AllowUserToAddRows = false;
            this.dgvParameters.AllowUserToDeleteRows = false;
            this.dgvParameters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvParameters.Location = new System.Drawing.Point(0, 65);
            this.dgvParameters.Name = "dgvParameters";
            this.dgvParameters.ReadOnly = true;
            this.dgvParameters.RowHeadersVisible = false;
            this.dgvParameters.Size = new System.Drawing.Size(350, 450);
            this.dgvParameters.TabIndex = 1;
            this.dgvParameters.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.dgvParameters.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.dgvParameters.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            this.dgvParameters.ColumnHeadersHeight = 50;
            this.dgvParameters.RowTemplate.Height = 40;
            this.dgvParameters.BackgroundColor = System.Drawing.Color.White;
            this.dgvParameters.GridColor = System.Drawing.Color.Black;
            this.dgvParameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvParameters.ScrollBars = System.Windows.Forms.ScrollBars.None;

            // Create columns
            var paramColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            paramColumn.HeaderText = "PARAM";
            paramColumn.Name = "PARAM";
            paramColumn.ReadOnly = true;
            paramColumn.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            paramColumn.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);

            var lclColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lclColumn.HeaderText = "LCL";
            lclColumn.Name = "LCL";
            lclColumn.ReadOnly = true;

            var measColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            measColumn.HeaderText = "MEAS";
            measColumn.Name = "MEAS";
            measColumn.ReadOnly = true;

            var uclColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            uclColumn.HeaderText = "UCL";
            uclColumn.Name = "UCL";
            uclColumn.ReadOnly = true;

            this.dgvParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                paramColumn, lclColumn, measColumn, uclColumn
            });

            // 
            // TestParametersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MinimumSize = new System.Drawing.Size(350, 550);
            this.MaximumSize = new System.Drawing.Size(350, 550);
            this.Controls.Add(this.dgvParameters);
            this.Controls.Add(this.lblSectionTitle);
            this.Name = "TestParametersControl";
            this.Size = new System.Drawing.Size(350, 550);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameters)).EndInit();
            this.ResumeLayout(false);
        }
    }
}