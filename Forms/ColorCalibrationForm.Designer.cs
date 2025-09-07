namespace HendersonvilleTrafficTest.Forms
{
    partial class ColorCalibrationForm
    {
        private System.ComponentModel.IContainer components = null;
        
        private ComboBox cmbColorSample;
        private Label lblColorSample;
        private Button btnStartTest;
        private TextBox txtStatus;
        private Label lblStatus;
        private ProgressBar progressBar;
        private Label lblProgress;
        private GroupBox grpResults;
        private Label lblLuxMeasured;
        private TextBox txtLuxMeasured;
        private Label lblTempMeasured;
        private TextBox txtTempMeasured;
        private Label lblEclCalib;
        private TextBox txtEclCalib;

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
            this.cmbColorSample = new System.Windows.Forms.ComboBox();
            this.lblColorSample = new System.Windows.Forms.Label();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.lblLuxMeasured = new System.Windows.Forms.Label();
            this.txtLuxMeasured = new System.Windows.Forms.TextBox();
            this.lblTempMeasured = new System.Windows.Forms.Label();
            this.txtTempMeasured = new System.Windows.Forms.TextBox();
            this.lblEclCalib = new System.Windows.Forms.Label();
            this.txtEclCalib = new System.Windows.Forms.TextBox();
            this.grpResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblColorSample
            // 
            this.lblColorSample.AutoSize = true;
            this.lblColorSample.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblColorSample.Location = new System.Drawing.Point(30, 30);
            this.lblColorSample.Name = "lblColorSample";
            this.lblColorSample.Size = new System.Drawing.Size(108, 21);
            this.lblColorSample.TabIndex = 0;
            this.lblColorSample.Text = "Color Sample:";
            // 
            // cmbColorSample
            // 
            this.cmbColorSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorSample.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbColorSample.FormattingEnabled = true;
            this.cmbColorSample.Items.AddRange(new object[] {
            "Blue",
            "Green",
            "Yellow",
            "Orange",
            "Red",
            "White"});
            this.cmbColorSample.Location = new System.Drawing.Point(150, 27);
            this.cmbColorSample.Name = "cmbColorSample";
            this.cmbColorSample.Size = new System.Drawing.Size(150, 29);
            this.cmbColorSample.TabIndex = 1;
            // 
            // btnStartTest
            // 
            this.btnStartTest.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartTest.Location = new System.Drawing.Point(350, 20);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(150, 45);
            this.btnStartTest.TabIndex = 2;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(30, 90);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 21);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtStatus.Location = new System.Drawing.Point(30, 120);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(500, 120);
            this.txtStatus.TabIndex = 4;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProgress.Location = new System.Drawing.Point(30, 260);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(74, 21);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "Progress:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(30, 290);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(500, 25);
            this.progressBar.TabIndex = 6;
            // 
            // grpResults
            // 
            this.grpResults.Controls.Add(this.txtEclCalib);
            this.grpResults.Controls.Add(this.lblEclCalib);
            this.grpResults.Controls.Add(this.txtTempMeasured);
            this.grpResults.Controls.Add(this.lblTempMeasured);
            this.grpResults.Controls.Add(this.txtLuxMeasured);
            this.grpResults.Controls.Add(this.lblLuxMeasured);
            this.grpResults.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpResults.Location = new System.Drawing.Point(30, 340);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(500, 150);
            this.grpResults.TabIndex = 7;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "Measurement Results";
            // 
            // lblLuxMeasured
            // 
            this.lblLuxMeasured.AutoSize = true;
            this.lblLuxMeasured.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLuxMeasured.Location = new System.Drawing.Point(20, 35);
            this.lblLuxMeasured.Name = "lblLuxMeasured";
            this.lblLuxMeasured.Size = new System.Drawing.Size(103, 20);
            this.lblLuxMeasured.TabIndex = 0;
            this.lblLuxMeasured.Text = "LUX Measured:";
            // 
            // txtLuxMeasured
            // 
            this.txtLuxMeasured.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtLuxMeasured.Location = new System.Drawing.Point(150, 32);
            this.txtLuxMeasured.Name = "txtLuxMeasured";
            this.txtLuxMeasured.ReadOnly = true;
            this.txtLuxMeasured.Size = new System.Drawing.Size(120, 27);
            this.txtLuxMeasured.TabIndex = 1;
            // 
            // lblTempMeasured
            // 
            this.lblTempMeasured.AutoSize = true;
            this.lblTempMeasured.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTempMeasured.Location = new System.Drawing.Point(20, 72);
            this.lblTempMeasured.Name = "lblTempMeasured";
            this.lblTempMeasured.Size = new System.Drawing.Size(119, 20);
            this.lblTempMeasured.TabIndex = 2;
            this.lblTempMeasured.Text = "Temp Measured:";
            // 
            // txtTempMeasured
            // 
            this.txtTempMeasured.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTempMeasured.Location = new System.Drawing.Point(150, 69);
            this.txtTempMeasured.Name = "txtTempMeasured";
            this.txtTempMeasured.ReadOnly = true;
            this.txtTempMeasured.Size = new System.Drawing.Size(120, 27);
            this.txtTempMeasured.TabIndex = 3;
            // 
            // lblEclCalib
            // 
            this.lblEclCalib.AutoSize = true;
            this.lblEclCalib.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEclCalib.Location = new System.Drawing.Point(20, 109);
            this.lblEclCalib.Name = "lblEclCalib";
            this.lblEclCalib.Size = new System.Drawing.Size(75, 20);
            this.lblEclCalib.TabIndex = 4;
            this.lblEclCalib.Text = "ECL Calib:";
            // 
            // txtEclCalib
            // 
            this.txtEclCalib.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtEclCalib.Location = new System.Drawing.Point(150, 106);
            this.txtEclCalib.Name = "txtEclCalib";
            this.txtEclCalib.ReadOnly = true;
            this.txtEclCalib.Size = new System.Drawing.Size(120, 27);
            this.txtEclCalib.TabIndex = 5;
            // 
            // ColorCalibrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 520);
            this.Controls.Add(this.grpResults);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStartTest);
            this.Controls.Add(this.cmbColorSample);
            this.Controls.Add(this.lblColorSample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ColorCalibrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Color Calibration Test";
            this.grpResults.ResumeLayout(false);
            this.grpResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}