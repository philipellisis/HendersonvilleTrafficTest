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
            this.grpConnection = new GroupBox();
            this.lblConnectionStatus = new Label();
            this.btnConnect = new Button();
            this.grpMode = new GroupBox();
            this.rbDC = new RadioButton();
            this.rbAC = new RadioButton();
            this.btnSetMode = new Button();
            this.grpMeasurements = new GroupBox();
            this.lblVoltage = new Label();
            this.txtVoltage = new TextBox();
            this.lblCurrent = new Label();
            this.txtCurrent = new TextBox();
            this.lblPower = new Label();
            this.txtPower = new TextBox();
            this.lblPowerFactor = new Label();
            this.txtPowerFactor = new TextBox();
            this.lblFrequency = new Label();
            this.txtFrequency = new TextBox();
            this.btnReadOnce = new Button();
            this.chkAutoRead = new CheckBox();
            this.grpDeviceInfo = new GroupBox();
            this.txtDeviceInfo = new TextBox();
            this.btnGetDeviceInfo = new Button();
            this.btnCheckErrors = new Button();
            this.autoReadTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.lblConnectionStatus);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpConnection.Location = new Point(12, 12);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new Size(250, 80);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";
            
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblConnectionStatus.ForeColor = Color.Red;
            this.lblConnectionStatus.Location = new Point(15, 25);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new Size(86, 15);
            this.lblConnectionStatus.TabIndex = 0;
            this.lblConnectionStatus.Text = "Disconnected";
            
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = Color.LightGreen;
            this.btnConnect.Font = new Font("Segoe UI", 9F);
            this.btnConnect.Location = new Point(15, 45);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new Size(80, 25);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new EventHandler(this.btnConnect_Click);
            
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.rbDC);
            this.grpMode.Controls.Add(this.rbAC);
            this.grpMode.Controls.Add(this.btnSetMode);
            this.grpMode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpMode.Location = new Point(280, 12);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new Size(200, 80);
            this.grpMode.TabIndex = 1;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "Power Mode";
            
            // 
            // rbAC
            // 
            this.rbAC.AutoSize = true;
            this.rbAC.Checked = true;
            this.rbAC.Font = new Font("Segoe UI", 9F);
            this.rbAC.Location = new Point(15, 25);
            this.rbAC.Name = "rbAC";
            this.rbAC.Size = new Size(39, 19);
            this.rbAC.TabIndex = 0;
            this.rbAC.TabStop = true;
            this.rbAC.Text = "AC";
            this.rbAC.UseVisualStyleBackColor = true;
            
            // 
            // rbDC
            // 
            this.rbDC.AutoSize = true;
            this.rbDC.Font = new Font("Segoe UI", 9F);
            this.rbDC.Location = new Point(70, 25);
            this.rbDC.Name = "rbDC";
            this.rbDC.Size = new Size(40, 19);
            this.rbDC.TabIndex = 1;
            this.rbDC.Text = "DC";
            this.rbDC.UseVisualStyleBackColor = true;
            
            // 
            // btnSetMode
            // 
            this.btnSetMode.Font = new Font("Segoe UI", 9F);
            this.btnSetMode.Location = new Point(15, 45);
            this.btnSetMode.Name = "btnSetMode";
            this.btnSetMode.Size = new Size(80, 25);
            this.btnSetMode.TabIndex = 2;
            this.btnSetMode.Text = "Set Mode";
            this.btnSetMode.UseVisualStyleBackColor = true;
            this.btnSetMode.Click += new EventHandler(this.btnSetMode_Click);
            
            // 
            // grpMeasurements
            // 
            this.grpMeasurements.Controls.Add(this.lblVoltage);
            this.grpMeasurements.Controls.Add(this.txtVoltage);
            this.grpMeasurements.Controls.Add(this.lblCurrent);
            this.grpMeasurements.Controls.Add(this.txtCurrent);
            this.grpMeasurements.Controls.Add(this.lblPower);
            this.grpMeasurements.Controls.Add(this.txtPower);
            this.grpMeasurements.Controls.Add(this.lblPowerFactor);
            this.grpMeasurements.Controls.Add(this.txtPowerFactor);
            this.grpMeasurements.Controls.Add(this.lblFrequency);
            this.grpMeasurements.Controls.Add(this.txtFrequency);
            this.grpMeasurements.Controls.Add(this.btnReadOnce);
            this.grpMeasurements.Controls.Add(this.chkAutoRead);
            this.grpMeasurements.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpMeasurements.Location = new Point(12, 110);
            this.grpMeasurements.Name = "grpMeasurements";
            this.grpMeasurements.Size = new Size(470, 200);
            this.grpMeasurements.TabIndex = 2;
            this.grpMeasurements.TabStop = false;
            this.grpMeasurements.Text = "Measurements";
            
            // 
            // lblVoltage
            // 
            this.lblVoltage.AutoSize = true;
            this.lblVoltage.Font = new Font("Segoe UI", 9F);
            this.lblVoltage.Location = new Point(15, 30);
            this.lblVoltage.Name = "lblVoltage";
            this.lblVoltage.Size = new Size(67, 15);
            this.lblVoltage.TabIndex = 0;
            this.lblVoltage.Text = "Voltage (V):";
            
            // 
            // txtVoltage
            // 
            this.txtVoltage.Font = new Font("Segoe UI", 9F);
            this.txtVoltage.Location = new Point(100, 27);
            this.txtVoltage.Name = "txtVoltage";
            this.txtVoltage.ReadOnly = true;
            this.txtVoltage.Size = new Size(100, 23);
            this.txtVoltage.TabIndex = 1;
            this.txtVoltage.Text = "-- V";
            this.txtVoltage.TextAlign = HorizontalAlignment.Center;
            
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new Font("Segoe UI", 9F);
            this.lblCurrent.Location = new Point(15, 60);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new Size(67, 15);
            this.lblCurrent.TabIndex = 2;
            this.lblCurrent.Text = "Current (A):";
            
            // 
            // txtCurrent
            // 
            this.txtCurrent.Font = new Font("Segoe UI", 9F);
            this.txtCurrent.Location = new Point(100, 57);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.ReadOnly = true;
            this.txtCurrent.Size = new Size(100, 23);
            this.txtCurrent.TabIndex = 3;
            this.txtCurrent.Text = "-- A";
            this.txtCurrent.TextAlign = HorizontalAlignment.Center;
            
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Font = new Font("Segoe UI", 9F);
            this.lblPower.Location = new Point(15, 90);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new Size(60, 15);
            this.lblPower.TabIndex = 4;
            this.lblPower.Text = "Power (W):";
            
            // 
            // txtPower
            // 
            this.txtPower.Font = new Font("Segoe UI", 9F);
            this.txtPower.Location = new Point(100, 87);
            this.txtPower.Name = "txtPower";
            this.txtPower.ReadOnly = true;
            this.txtPower.Size = new Size(100, 23);
            this.txtPower.TabIndex = 5;
            this.txtPower.Text = "-- W";
            this.txtPower.TextAlign = HorizontalAlignment.Center;
            
            // 
            // lblPowerFactor
            // 
            this.lblPowerFactor.AutoSize = true;
            this.lblPowerFactor.Font = new Font("Segoe UI", 9F);
            this.lblPowerFactor.Location = new Point(240, 30);
            this.lblPowerFactor.Name = "lblPowerFactor";
            this.lblPowerFactor.Size = new Size(76, 15);
            this.lblPowerFactor.TabIndex = 6;
            this.lblPowerFactor.Text = "Power Factor:";
            
            // 
            // txtPowerFactor
            // 
            this.txtPowerFactor.Font = new Font("Segoe UI", 9F);
            this.txtPowerFactor.Location = new Point(330, 27);
            this.txtPowerFactor.Name = "txtPowerFactor";
            this.txtPowerFactor.ReadOnly = true;
            this.txtPowerFactor.Size = new Size(100, 23);
            this.txtPowerFactor.TabIndex = 7;
            this.txtPowerFactor.Text = "-- PF";
            this.txtPowerFactor.TextAlign = HorizontalAlignment.Center;
            
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Font = new Font("Segoe UI", 9F);
            this.lblFrequency.Location = new Point(240, 60);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new Size(84, 15);
            this.lblFrequency.TabIndex = 8;
            this.lblFrequency.Text = "Frequency (Hz):";
            
            // 
            // txtFrequency
            // 
            this.txtFrequency.Font = new Font("Segoe UI", 9F);
            this.txtFrequency.Location = new Point(330, 57);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.ReadOnly = true;
            this.txtFrequency.Size = new Size(100, 23);
            this.txtFrequency.TabIndex = 9;
            this.txtFrequency.Text = "-- Hz";
            this.txtFrequency.TextAlign = HorizontalAlignment.Center;
            
            // 
            // btnReadOnce
            // 
            this.btnReadOnce.Font = new Font("Segoe UI", 9F);
            this.btnReadOnce.Location = new Point(15, 130);
            this.btnReadOnce.Name = "btnReadOnce";
            this.btnReadOnce.Size = new Size(100, 30);
            this.btnReadOnce.TabIndex = 10;
            this.btnReadOnce.Text = "Read Once";
            this.btnReadOnce.UseVisualStyleBackColor = true;
            this.btnReadOnce.Click += new EventHandler(this.btnReadOnce_Click);
            
            // 
            // chkAutoRead
            // 
            this.chkAutoRead.AutoSize = true;
            this.chkAutoRead.Font = new Font("Segoe UI", 9F);
            this.chkAutoRead.Location = new Point(130, 137);
            this.chkAutoRead.Name = "chkAutoRead";
            this.chkAutoRead.Size = new Size(125, 19);
            this.chkAutoRead.TabIndex = 11;
            this.chkAutoRead.Text = "Auto Read (1 sec)";
            this.chkAutoRead.UseVisualStyleBackColor = true;
            this.chkAutoRead.CheckedChanged += new EventHandler(this.chkAutoRead_CheckedChanged);
            
            // 
            // grpDeviceInfo
            // 
            this.grpDeviceInfo.Controls.Add(this.txtDeviceInfo);
            this.grpDeviceInfo.Controls.Add(this.btnGetDeviceInfo);
            this.grpDeviceInfo.Controls.Add(this.btnCheckErrors);
            this.grpDeviceInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpDeviceInfo.Location = new Point(12, 330);
            this.grpDeviceInfo.Name = "grpDeviceInfo";
            this.grpDeviceInfo.Size = new Size(470, 100);
            this.grpDeviceInfo.TabIndex = 3;
            this.grpDeviceInfo.TabStop = false;
            this.grpDeviceInfo.Text = "Device Information";
            
            // 
            // txtDeviceInfo
            // 
            this.txtDeviceInfo.Font = new Font("Consolas", 8F);
            this.txtDeviceInfo.Location = new Point(15, 25);
            this.txtDeviceInfo.Multiline = true;
            this.txtDeviceInfo.Name = "txtDeviceInfo";
            this.txtDeviceInfo.ReadOnly = true;
            this.txtDeviceInfo.ScrollBars = ScrollBars.Vertical;
            this.txtDeviceInfo.Size = new Size(320, 60);
            this.txtDeviceInfo.TabIndex = 0;
            this.txtDeviceInfo.Text = "Device not connected";
            
            // 
            // btnGetDeviceInfo
            // 
            this.btnGetDeviceInfo.Font = new Font("Segoe UI", 9F);
            this.btnGetDeviceInfo.Location = new Point(350, 25);
            this.btnGetDeviceInfo.Name = "btnGetDeviceInfo";
            this.btnGetDeviceInfo.Size = new Size(100, 25);
            this.btnGetDeviceInfo.TabIndex = 1;
            this.btnGetDeviceInfo.Text = "Get Device Info";
            this.btnGetDeviceInfo.UseVisualStyleBackColor = true;
            this.btnGetDeviceInfo.Click += new EventHandler(this.btnGetDeviceInfo_Click);
            
            // 
            // btnCheckErrors
            // 
            this.btnCheckErrors.Font = new Font("Segoe UI", 9F);
            this.btnCheckErrors.Location = new Point(350, 60);
            this.btnCheckErrors.Name = "btnCheckErrors";
            this.btnCheckErrors.Size = new Size(100, 25);
            this.btnCheckErrors.TabIndex = 2;
            this.btnCheckErrors.Text = "Check Errors";
            this.btnCheckErrors.UseVisualStyleBackColor = true;
            this.btnCheckErrors.Click += new EventHandler(this.btnCheckErrors_Click);
            
            // 
            // autoReadTimer
            // 
            this.autoReadTimer.Interval = 1000;
            this.autoReadTimer.Tick += new EventHandler(this.autoReadTimer_Tick);
            
            // 
            // PowerAnalyzerTestForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 450);
            this.Controls.Add(this.grpDeviceInfo);
            this.Controls.Add(this.grpMeasurements);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.grpConnection);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PowerAnalyzerTestForm";
            this.StartPosition = FormStartPosition.CenterParent;
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
        private Label lblPowerFactor;
        private TextBox txtPowerFactor;
        private Label lblFrequency;
        private TextBox txtFrequency;
        private Button btnReadOnce;
        private CheckBox chkAutoRead;
        private GroupBox grpDeviceInfo;
        private TextBox txtDeviceInfo;
        private Button btnGetDeviceInfo;
        private Button btnCheckErrors;
        private System.Windows.Forms.Timer autoReadTimer;
    }
}