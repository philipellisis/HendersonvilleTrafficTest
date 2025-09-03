namespace HendersonvilleTrafficTest.Forms
{
    partial class SpectrometerCalibrationForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnStartCalibration;
        private Button btnCancel;
        private Label lblStatus;
        private ProgressBar progressBar;
        private TextBox txtLog;
        private GroupBox grpStatus;
        private Label lblVoltage;
        private Label lblCurrent;
        private Label lblTimer;

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
            this.btnStartCalibration = new Button();
            this.btnCancel = new Button();
            this.lblStatus = new Label();
            this.progressBar = new ProgressBar();
            this.txtLog = new TextBox();
            this.grpStatus = new GroupBox();
            this.lblVoltage = new Label();
            this.lblCurrent = new Label();
            this.lblTimer = new Label();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();

            // 
            // btnStartCalibration
            // 
            this.btnStartCalibration.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnStartCalibration.Location = new Point(20, 20);
            this.btnStartCalibration.Name = "btnStartCalibration";
            this.btnStartCalibration.Size = new Size(200, 50);
            this.btnStartCalibration.TabIndex = 0;
            this.btnStartCalibration.Text = "Start Calibration";
            this.btnStartCalibration.UseVisualStyleBackColor = true;
            this.btnStartCalibration.Click += new EventHandler(this.btnStartCalibration_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new Font("Segoe UI", 10F);
            this.btnCancel.Location = new Point(240, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 50);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblStatus.Location = new Point(20, 90);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(49, 19);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Ready";

            // 
            // progressBar
            // 
            this.progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.progressBar.Location = new Point(20, 120);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new Size(540, 25);
            this.progressBar.TabIndex = 3;

            // 
            // grpStatus
            // 
            this.grpStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.grpStatus.Controls.Add(this.lblTimer);
            this.grpStatus.Controls.Add(this.lblCurrent);
            this.grpStatus.Controls.Add(this.lblVoltage);
            this.grpStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpStatus.Location = new Point(20, 160);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new Size(540, 100);
            this.grpStatus.TabIndex = 4;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Current Status";

            // 
            // lblVoltage
            // 
            this.lblVoltage.AutoSize = true;
            this.lblVoltage.Font = new Font("Segoe UI", 9F);
            this.lblVoltage.Location = new Point(15, 25);
            this.lblVoltage.Name = "lblVoltage";
            this.lblVoltage.Size = new Size(83, 15);
            this.lblVoltage.TabIndex = 0;
            this.lblVoltage.Text = "Voltage: 0.0 V";

            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new Font("Segoe UI", 9F);
            this.lblCurrent.Location = new Point(15, 45);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new Size(81, 15);
            this.lblCurrent.TabIndex = 1;
            this.lblCurrent.Text = "Current: 0.0 A";

            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new Font("Segoe UI", 9F);
            this.lblTimer.Location = new Point(15, 65);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new Size(66, 15);
            this.lblTimer.TabIndex = 2;
            this.lblTimer.Text = "Timer: 0 s";

            // 
            // txtLog
            // 
            this.txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtLog.Font = new Font("Consolas", 9F);
            this.txtLog.Location = new Point(20, 280);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Both;
            this.txtLog.Size = new Size(540, 200);
            this.txtLog.TabIndex = 5;

            // 
            // SpectrometerCalibrationForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(584, 500);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStartCalibration);
            this.Font = new Font("Segoe UI", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpectrometerCalibrationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Spectrometer Calibration";
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}