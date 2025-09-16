namespace HendersonvilleTrafficTest.Forms
{
    partial class PowerAnalyzerTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.rbDC = new System.Windows.Forms.RadioButton();
            this.rbAC = new System.Windows.Forms.RadioButton();
            this.btnSetMode = new System.Windows.Forms.Button();
            this.grpMeasurements = new System.Windows.Forms.GroupBox();
            this.lblVoltage = new System.Windows.Forms.Label();
            this.txtVoltage = new System.Windows.Forms.TextBox();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.lblPower = new System.Windows.Forms.Label();
            this.txtPower = new System.Windows.Forms.TextBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.lblTHD = new System.Windows.Forms.Label();
            this.txtTHD = new System.Windows.Forms.TextBox();
            this.lblPowerFactor = new System.Windows.Forms.Label();
            this.txtPowerFactor = new System.Windows.Forms.TextBox();
            this.btnReadOnce = new System.Windows.Forms.Button();
            this.chkAutoRead = new System.Windows.Forms.CheckBox();
            this.grpDeviceInfo = new System.Windows.Forms.GroupBox();
            this.txtDeviceInfo = new System.Windows.Forms.TextBox();
            this.btnGetDeviceInfo = new System.Windows.Forms.Button();
            this.btnCheckErrors = new System.Windows.Forms.Button();
            this.autoReadTimer = new System.Windows.Forms.Timer(this.components);
            this.grpConnection.SuspendLayout();
            this.grpMode.SuspendLayout();
            this.grpMeasurements.SuspendLayout();
            this.grpDeviceInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.lblConnectionStatus);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpConnection.Location = new System.Drawing.Point(12, 12);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(250, 80);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Location = new System.Drawing.Point(15, 25);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(83, 15);
            this.lblConnectionStatus.TabIndex = 0;
            this.lblConnectionStatus.Text = "Disconnected";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnConnect.Location = new System.Drawing.Point(15, 45);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 25);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.rbDC);
            this.grpMode.Controls.Add(this.rbAC);
            this.grpMode.Controls.Add(this.btnSetMode);
            this.grpMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMode.Location = new System.Drawing.Point(280, 12);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(200, 80);
            this.grpMode.TabIndex = 1;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "Power Mode";
            // 
            // rbDC
            // 
            this.rbDC.AutoSize = true;
            this.rbDC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbDC.Location = new System.Drawing.Point(70, 25);
            this.rbDC.Name = "rbDC";
            this.rbDC.Size = new System.Drawing.Size(41, 19);
            this.rbDC.TabIndex = 1;
            this.rbDC.Text = "DC";
            this.rbDC.UseVisualStyleBackColor = true;
            // 
            // rbAC
            // 
            this.rbAC.AutoSize = true;
            this.rbAC.Checked = true;
            this.rbAC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rbAC.Location = new System.Drawing.Point(15, 25);
            this.rbAC.Name = "rbAC";
            this.rbAC.Size = new System.Drawing.Size(41, 19);
            this.rbAC.TabIndex = 0;
            this.rbAC.TabStop = true;
            this.rbAC.Text = "AC";
            this.rbAC.UseVisualStyleBackColor = true;
            // 
            // btnSetMode
            // 
            this.btnSetMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSetMode.Location = new System.Drawing.Point(15, 45);
            this.btnSetMode.Name = "btnSetMode";
            this.btnSetMode.Size = new System.Drawing.Size(80, 25);
            this.btnSetMode.TabIndex = 2;
            this.btnSetMode.Text = "Set Mode";
            this.btnSetMode.UseVisualStyleBackColor = true;
            this.btnSetMode.Click += new System.EventHandler(this.btnSetMode_Click);
            // 
            // grpMeasurements
            // 
            this.grpMeasurements.Controls.Add(this.lblVoltage);
            this.grpMeasurements.Controls.Add(this.txtVoltage);
            this.grpMeasurements.Controls.Add(this.lblCurrent);
            this.grpMeasurements.Controls.Add(this.txtCurrent);
            this.grpMeasurements.Controls.Add(this.lblPower);
            this.grpMeasurements.Controls.Add(this.txtPower);
            this.grpMeasurements.Controls.Add(this.lblFrequency);
            this.grpMeasurements.Controls.Add(this.txtFrequency);
            this.grpMeasurements.Controls.Add(this.lblTHD);
            this.grpMeasurements.Controls.Add(this.txtTHD);
            this.grpMeasurements.Controls.Add(this.lblPowerFactor);
            this.grpMeasurements.Controls.Add(this.txtPowerFactor);
            this.grpMeasurements.Controls.Add(this.btnReadOnce);
            this.grpMeasurements.Controls.Add(this.chkAutoRead);
            this.grpMeasurements.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpMeasurements.Location = new System.Drawing.Point(12, 110);
            this.grpMeasurements.Name = "grpMeasurements";
            this.grpMeasurements.Size = new System.Drawing.Size(470, 180);
            this.grpMeasurements.TabIndex = 2;
            this.grpMeasurements.TabStop = false;
            this.grpMeasurements.Text = "Measurements";
            // 
            // lblVoltage
            // 
            this.lblVoltage.AutoSize = true;
            this.lblVoltage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVoltage.Location = new System.Drawing.Point(15, 30);
            this.lblVoltage.Name = "lblVoltage";
            this.lblVoltage.Size = new System.Drawing.Size(67, 15);
            this.lblVoltage.TabIndex = 0;
            this.lblVoltage.Text = "Voltage (V):";
            // 
            // txtVoltage
            // 
            this.txtVoltage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtVoltage.Location = new System.Drawing.Point(100, 27);
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.ReadOnly = true;
            this.txtVoltage.Size = new System.Drawing.Size(100, 23);
            this.txtVoltage.TabIndex = 1;
            this.txtVoltage.Text = "-- V";
            this.txtVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCurrent.Location = new System.Drawing.Point(15, 61);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(69, 15);
            this.lblCurrent.TabIndex = 2;
            this.lblCurrent.Text = "Current (A):";
            // 
            // txtCurrent
            // 
            this.txtCurrent.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCurrent.Location = new System.Drawing.Point(100, 56);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.ReadOnly = true;
            this.txtCurrent.Size = new System.Drawing.Size(100, 23);
            this.txtCurrent.TabIndex = 3;
            this.txtCurrent.Text = "-- A";
            this.txtCurrent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPower.Location = new System.Drawing.Point(15, 90);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(65, 15);
            this.lblPower.TabIndex = 4;
            this.lblPower.Text = "Power (W):";
            // 
            // txtPower
            // 
            this.txtPower.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPower.Location = new System.Drawing.Point(100, 87);
            this.txtPower.Name = "txtPower";
            this.txtPower.ReadOnly = true;
            this.txtPower.Size = new System.Drawing.Size(100, 23);
            this.txtPower.TabIndex = 5;
            this.txtPower.Text = "-- W";
            this.txtPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFrequency.Location = new System.Drawing.Point(240, 30);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(90, 15);
            this.lblFrequency.TabIndex = 6;
            this.lblFrequency.Text = "Frequency (Hz):";
            // 
            // txtFrequency
            // 
            this.txtFrequency.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFrequency.Location = new System.Drawing.Point(330, 27);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.ReadOnly = true;
            this.txtFrequency.Size = new System.Drawing.Size(100, 23);
            this.txtFrequency.TabIndex = 7;
            this.txtFrequency.Text = "-- Hz";
            this.txtFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTHD
            // 
            this.lblTHD.AutoSize = true;
            this.lblTHD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTHD.Location = new System.Drawing.Point(289, 90);
            this.lblTHD.Name = "lblTHD";
            this.lblTHD.Size = new System.Drawing.Size(34, 15);
            this.lblTHD.TabIndex = 8;
            this.lblTHD.Text = "THD:";
            // 
            // txtTHD
            // 
            this.txtTHD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTHD.Location = new System.Drawing.Point(330, 90);
            this.txtTHD.Name = "txtTHD";
            this.txtTHD.ReadOnly = true;
            this.txtTHD.Size = new System.Drawing.Size(100, 23);
            this.txtTHD.TabIndex = 9;
            this.txtTHD.Text = "-- %";
            this.txtTHD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPowerFactor
            // 
            this.lblPowerFactor.AutoSize = true;
            this.lblPowerFactor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPowerFactor.Location = new System.Drawing.Point(300, 60);
            this.lblPowerFactor.Name = "lblPowerFactor";
            this.lblPowerFactor.Size = new System.Drawing.Size(23, 15);
            this.lblPowerFactor.TabIndex = 10;
            this.lblPowerFactor.Text = "PF:";
            // 
            // txtPowerFactor
            // 
            this.txtPowerFactor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPowerFactor.Location = new System.Drawing.Point(330, 57);
            this.txtPowerFactor.Name = "txtPowerFactor";
            this.txtPowerFactor.ReadOnly = true;
            this.txtPowerFactor.Size = new System.Drawing.Size(100, 23);
            this.txtPowerFactor.TabIndex = 11;
            this.txtPowerFactor.Text = "-- PF";
            this.txtPowerFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReadOnce
            // 
            this.btnReadOnce.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReadOnce.Location = new System.Drawing.Point(15, 130);
            this.btnReadOnce.Name = "btnReadOnce";
            this.btnReadOnce.Size = new System.Drawing.Size(100, 30);
            this.btnReadOnce.TabIndex = 8;
            this.btnReadOnce.Text = "Read Once";
            this.btnReadOnce.UseVisualStyleBackColor = true;
            this.btnReadOnce.Click += new System.EventHandler(this.btnReadOnce_Click);
            // 
            // chkAutoRead
            // 
            this.chkAutoRead.AutoSize = true;
            this.chkAutoRead.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAutoRead.Location = new System.Drawing.Point(130, 137);
            this.chkAutoRead.Name = "chkAutoRead";
            this.chkAutoRead.Size = new System.Drawing.Size(118, 19);
            this.chkAutoRead.TabIndex = 9;
            this.chkAutoRead.Text = "Auto Read (1 sec)";
            this.chkAutoRead.UseVisualStyleBackColor = true;
            this.chkAutoRead.CheckedChanged += new System.EventHandler(this.chkAutoRead_CheckedChanged);
            // 
            // grpDeviceInfo
            // 
            this.grpDeviceInfo.Controls.Add(this.txtDeviceInfo);
            this.grpDeviceInfo.Controls.Add(this.btnGetDeviceInfo);
            this.grpDeviceInfo.Controls.Add(this.btnCheckErrors);
            this.grpDeviceInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpDeviceInfo.Location = new System.Drawing.Point(12, 280);
            this.grpDeviceInfo.Name = "grpDeviceInfo";
            this.grpDeviceInfo.Size = new System.Drawing.Size(470, 100);
            this.grpDeviceInfo.TabIndex = 3;
            this.grpDeviceInfo.TabStop = false;
            this.grpDeviceInfo.Text = "Device Information";
            // 
            // txtDeviceInfo
            // 
            this.txtDeviceInfo.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDeviceInfo.Location = new System.Drawing.Point(15, 25);
            this.txtDeviceInfo.Multiline = true;
            this.txtDeviceInfo.Name = "txtDeviceInfo";
            this.txtDeviceInfo.ReadOnly = true;
            this.txtDeviceInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDeviceInfo.Size = new System.Drawing.Size(320, 60);
            this.txtDeviceInfo.TabIndex = 0;
            this.txtDeviceInfo.Text = "Device not connected";
            // 
            // btnGetDeviceInfo
            // 
            this.btnGetDeviceInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGetDeviceInfo.Location = new System.Drawing.Point(350, 25);
            this.btnGetDeviceInfo.Name = "btnGetDeviceInfo";
            this.btnGetDeviceInfo.Size = new System.Drawing.Size(100, 25);
            this.btnGetDeviceInfo.TabIndex = 1;
            this.btnGetDeviceInfo.Text = "Get Device Info";
            this.btnGetDeviceInfo.UseVisualStyleBackColor = true;
            this.btnGetDeviceInfo.Click += new System.EventHandler(this.btnGetDeviceInfo_Click);
            // 
            // btnCheckErrors
            // 
            this.btnCheckErrors.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCheckErrors.Location = new System.Drawing.Point(350, 60);
            this.btnCheckErrors.Name = "btnCheckErrors";
            this.btnCheckErrors.Size = new System.Drawing.Size(100, 25);
            this.btnCheckErrors.TabIndex = 2;
            this.btnCheckErrors.Text = "Check Errors";
            this.btnCheckErrors.UseVisualStyleBackColor = true;
            this.btnCheckErrors.Click += new System.EventHandler(this.btnCheckErrors_Click);
            // 
            // autoReadTimer
            // 
            this.autoReadTimer.Interval = 1000;
            this.autoReadTimer.Tick += new System.EventHandler(this.autoReadTimer_Tick);
            // 
            // PowerAnalyzerTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.grpDeviceInfo);
            this.Controls.Add(this.grpMeasurements);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.grpConnection);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PowerAnalyzerTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NPA101 Power Analyzer Test";
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.grpMeasurements.ResumeLayout(false);
            this.grpMeasurements.PerformLayout();
            this.grpDeviceInfo.ResumeLayout(false);
            this.grpDeviceInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grpConnection;
        private Label lblConnectionStatus;
        private Button btnConnect;
        private GroupBox grpMode;
        private RadioButton rbAC;
        private RadioButton rbDC;
        private Button btnSetMode;
        private GroupBox grpMeasurements;
        private Label lblVoltage;
        private TextBox txtVoltage;
        private Label lblCurrent;
        private TextBox txtCurrent;
        private Label lblPower;
        private TextBox txtPower;
        private Label lblFrequency;
        private TextBox txtFrequency;
        private Label lblTHD;
        private TextBox txtTHD;
        private Label lblPowerFactor;
        private TextBox txtPowerFactor;
        private Button btnReadOnce;
        private CheckBox chkAutoRead;
        private GroupBox grpDeviceInfo;
        private TextBox txtDeviceInfo;
        private Button btnGetDeviceInfo;
        private Button btnCheckErrors;
        private System.Windows.Forms.Timer autoReadTimer;
    }
}