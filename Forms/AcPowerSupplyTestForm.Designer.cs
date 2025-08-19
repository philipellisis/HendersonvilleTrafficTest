namespace HendersonvilleTrafficTest.Forms
{
    partial class AcPowerSupplyTestForm
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
            this.grpOutput = new GroupBox();
            this.lblOutputStatus = new Label();
            this.btnOutputOn = new Button();
            this.btnOutputOff = new Button();
            this.grpSettings = new GroupBox();
            this.lblVoltage = new Label();
            this.numVoltage = new NumericUpDown();
            this.btnSetVoltage = new Button();
            this.lblFrequency = new Label();
            this.numFrequency = new NumericUpDown();
            this.btnSetFrequency = new Button();
            this.grpMeasurements = new GroupBox();
            this.lblMeasVoltage = new Label();
            this.txtMeasVoltage = new TextBox();
            this.lblMeasCurrent = new Label();
            this.txtMeasCurrent = new TextBox();
            this.lblMeasPower = new Label();
            this.txtMeasPower = new TextBox();
            this.lblMeasPowerFactor = new Label();
            this.txtMeasPowerFactor = new TextBox();
            this.lblMeasFrequency = new Label();
            this.txtMeasFrequency = new TextBox();
            this.btnReadMeasurements = new Button();
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
            this.grpConnection.Size = new Size(200, 80);
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
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.lblOutputStatus);
            this.grpOutput.Controls.Add(this.btnOutputOn);
            this.grpOutput.Controls.Add(this.btnOutputOff);
            this.grpOutput.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpOutput.Location = new Point(230, 12);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new Size(200, 80);
            this.grpOutput.TabIndex = 1;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output Control";
            
            // 
            // lblOutputStatus
            // 
            this.lblOutputStatus.AutoSize = true;
            this.lblOutputStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblOutputStatus.ForeColor = Color.Red;
            this.lblOutputStatus.Location = new Point(15, 25);
            this.lblOutputStatus.Name = "lblOutputStatus";
            this.lblOutputStatus.Size = new Size(28, 15);
            this.lblOutputStatus.TabIndex = 0;
            this.lblOutputStatus.Text = "OFF";
            
            // 
            // btnOutputOn
            // 
            this.btnOutputOn.BackColor = Color.LightGreen;
            this.btnOutputOn.Font = new Font("Segoe UI", 9F);
            this.btnOutputOn.Location = new Point(15, 45);
            this.btnOutputOn.Name = "btnOutputOn";
            this.btnOutputOn.Size = new Size(50, 25);
            this.btnOutputOn.TabIndex = 1;
            this.btnOutputOn.Text = "ON";
            this.btnOutputOn.UseVisualStyleBackColor = false;
            this.btnOutputOn.Click += new EventHandler(this.btnOutputOn_Click);
            
            // 
            // btnOutputOff
            // 
            this.btnOutputOff.BackColor = Color.LightCoral;
            this.btnOutputOff.Font = new Font("Segoe UI", 9F);
            this.btnOutputOff.Location = new Point(75, 45);
            this.btnOutputOff.Name = "btnOutputOff";
            this.btnOutputOff.Size = new Size(50, 25);
            this.btnOutputOff.TabIndex = 2;
            this.btnOutputOff.Text = "OFF";
            this.btnOutputOff.UseVisualStyleBackColor = false;
            this.btnOutputOff.Click += new EventHandler(this.btnOutputOff_Click);
            
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.lblVoltage);
            this.grpSettings.Controls.Add(this.numVoltage);
            this.grpSettings.Controls.Add(this.btnSetVoltage);
            this.grpSettings.Controls.Add(this.lblFrequency);
            this.grpSettings.Controls.Add(this.numFrequency);
            this.grpSettings.Controls.Add(this.btnSetFrequency);
            this.grpSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpSettings.Location = new Point(12, 110);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new Size(420, 100);
            this.grpSettings.TabIndex = 2;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Output Settings";
            
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
            // numVoltage
            // 
            this.numVoltage.DecimalPlaces = 1;
            this.numVoltage.Font = new Font("Segoe UI", 9F);
            this.numVoltage.Location = new Point(90, 28);
            this.numVoltage.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            this.numVoltage.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numVoltage.Name = "numVoltage";
            this.numVoltage.Size = new Size(80, 23);
            this.numVoltage.TabIndex = 1;
            this.numVoltage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            
            // 
            // btnSetVoltage
            // 
            this.btnSetVoltage.Font = new Font("Segoe UI", 9F);
            this.btnSetVoltage.Location = new Point(180, 27);
            this.btnSetVoltage.Name = "btnSetVoltage";
            this.btnSetVoltage.Size = new Size(75, 25);
            this.btnSetVoltage.TabIndex = 2;
            this.btnSetVoltage.Text = "Set";
            this.btnSetVoltage.UseVisualStyleBackColor = true;
            this.btnSetVoltage.Click += new EventHandler(this.btnSetVoltage_Click);
            
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Font = new Font("Segoe UI", 9F);
            this.lblFrequency.Location = new Point(15, 65);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new Size(84, 15);
            this.lblFrequency.TabIndex = 3;
            this.lblFrequency.Text = "Frequency (Hz):";
            
            // 
            // numFrequency
            // 
            this.numFrequency.DecimalPlaces = 1;
            this.numFrequency.Font = new Font("Segoe UI", 9F);
            this.numFrequency.Location = new Point(105, 63);
            this.numFrequency.Maximum = new decimal(new int[] { 400, 0, 0, 0 });
            this.numFrequency.Minimum = new decimal(new int[] { 40, 0, 0, 0 });
            this.numFrequency.Name = "numFrequency";
            this.numFrequency.Size = new Size(80, 23);
            this.numFrequency.TabIndex = 4;
            this.numFrequency.Value = new decimal(new int[] { 60, 0, 0, 0 });
            
            // 
            // btnSetFrequency
            // 
            this.btnSetFrequency.Font = new Font("Segoe UI", 9F);
            this.btnSetFrequency.Location = new Point(195, 62);
            this.btnSetFrequency.Name = "btnSetFrequency";
            this.btnSetFrequency.Size = new Size(75, 25);
            this.btnSetFrequency.TabIndex = 5;
            this.btnSetFrequency.Text = "Set";
            this.btnSetFrequency.UseVisualStyleBackColor = true;
            this.btnSetFrequency.Click += new EventHandler(this.btnSetFrequency_Click);
            
            // 
            // grpMeasurements
            // 
            this.grpMeasurements.Controls.Add(this.lblMeasVoltage);
            this.grpMeasurements.Controls.Add(this.txtMeasVoltage);
            this.grpMeasurements.Controls.Add(this.lblMeasCurrent);
            this.grpMeasurements.Controls.Add(this.txtMeasCurrent);
            this.grpMeasurements.Controls.Add(this.lblMeasPower);
            this.grpMeasurements.Controls.Add(this.txtMeasPower);
            this.grpMeasurements.Controls.Add(this.lblMeasPowerFactor);
            this.grpMeasurements.Controls.Add(this.txtMeasPowerFactor);
            this.grpMeasurements.Controls.Add(this.lblMeasFrequency);
            this.grpMeasurements.Controls.Add(this.txtMeasFrequency);
            this.grpMeasurements.Controls.Add(this.btnReadMeasurements);
            this.grpMeasurements.Controls.Add(this.chkAutoRead);
            this.grpMeasurements.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpMeasurements.Location = new Point(12, 230);
            this.grpMeasurements.Name = "grpMeasurements";
            this.grpMeasurements.Size = new Size(520, 180);
            this.grpMeasurements.TabIndex = 3;
            this.grpMeasurements.TabStop = false;
            this.grpMeasurements.Text = "Measurements";
            
            // 
            // lblMeasVoltage
            // 
            this.lblMeasVoltage.AutoSize = true;
            this.lblMeasVoltage.Font = new Font("Segoe UI", 9F);
            this.lblMeasVoltage.Location = new Point(15, 30);
            this.lblMeasVoltage.Name = "lblMeasVoltage";
            this.lblMeasVoltage.Size = new Size(67, 15);
            this.lblMeasVoltage.TabIndex = 0;
            this.lblMeasVoltage.Text = "Voltage (V):";
            
            // 
            // txtMeasVoltage
            // 
            this.txtMeasVoltage.Font = new Font("Segoe UI", 9F);
            this.txtMeasVoltage.Location = new Point(100, 27);
            this.txtMeasVoltage.Name = "txtMeasVoltage";
            this.txtMeasVoltage.ReadOnly = true;
            this.txtMeasVoltage.Size = new Size(100, 23);
            this.txtMeasVoltage.TabIndex = 1;
            this.txtMeasVoltage.Text = "-- V";
            this.txtMeasVoltage.TextAlign = HorizontalAlignment.Center;
            
            // 
            // lblMeasCurrent
            // 
            this.lblMeasCurrent.AutoSize = true;
            this.lblMeasCurrent.Font = new Font("Segoe UI", 9F);
            this.lblMeasCurrent.Location = new Point(15, 60);
            this.lblMeasCurrent.Name = "lblMeasCurrent";
            this.lblMeasCurrent.Size = new Size(67, 15);
            this.lblMeasCurrent.TabIndex = 2;
            this.lblMeasCurrent.Text = "Current (A):";
            
            // 
            // txtMeasCurrent
            // 
            this.txtMeasCurrent.Font = new Font("Segoe UI", 9F);
            this.txtMeasCurrent.Location = new Point(100, 57);
            this.txtMeasCurrent.Name = "txtMeasCurrent";
            this.txtMeasCurrent.ReadOnly = true;
            this.txtMeasCurrent.Size = new Size(100, 23);
            this.txtMeasCurrent.TabIndex = 3;
            this.txtMeasCurrent.Text = "-- A";
            this.txtMeasCurrent.TextAlign = HorizontalAlignment.Center;
            
            // 
            // lblMeasPower
            // 
            this.lblMeasPower.AutoSize = true;
            this.lblMeasPower.Font = new Font("Segoe UI", 9F);
            this.lblMeasPower.Location = new Point(15, 90);
            this.lblMeasPower.Name = "lblMeasPower";
            this.lblMeasPower.Size = new Size(60, 15);
            this.lblMeasPower.TabIndex = 4;
            this.lblMeasPower.Text = "Power (W):";
            
            // 
            // txtMeasPower
            // 
            this.txtMeasPower.Font = new Font("Segoe UI", 9F);
            this.txtMeasPower.Location = new Point(100, 87);
            this.txtMeasPower.Name = "txtMeasPower";
            this.txtMeasPower.ReadOnly = true;
            this.txtMeasPower.Size = new Size(100, 23);
            this.txtMeasPower.TabIndex = 5;
            this.txtMeasPower.Text = "-- W";
            this.txtMeasPower.TextAlign = HorizontalAlignment.Center;
            
            // 
            // lblMeasPowerFactor
            // 
            this.lblMeasPowerFactor.AutoSize = true;
            this.lblMeasPowerFactor.Font = new Font("Segoe UI", 9F);
            this.lblMeasPowerFactor.Location = new Point(240, 30);
            this.lblMeasPowerFactor.Name = "lblMeasPowerFactor";
            this.lblMeasPowerFactor.Size = new Size(76, 15);
            this.lblMeasPowerFactor.TabIndex = 6;
            this.lblMeasPowerFactor.Text = "Power Factor:";
            
            // 
            // txtMeasPowerFactor
            // 
            this.txtMeasPowerFactor.Font = new Font("Segoe UI", 9F);
            this.txtMeasPowerFactor.Location = new Point(330, 27);
            this.txtMeasPowerFactor.Name = "txtMeasPowerFactor";
            this.txtMeasPowerFactor.ReadOnly = true;
            this.txtMeasPowerFactor.Size = new Size(100, 23);
            this.txtMeasPowerFactor.TabIndex = 7;
            this.txtMeasPowerFactor.Text = "-- PF";
            this.txtMeasPowerFactor.TextAlign = HorizontalAlignment.Center;
            
            // 
            // lblMeasFrequency
            // 
            this.lblMeasFrequency.AutoSize = true;
            this.lblMeasFrequency.Font = new Font("Segoe UI", 9F);
            this.lblMeasFrequency.Location = new Point(240, 60);
            this.lblMeasFrequency.Name = "lblMeasFrequency";
            this.lblMeasFrequency.Size = new Size(84, 15);
            this.lblMeasFrequency.TabIndex = 8;
            this.lblMeasFrequency.Text = "Frequency (Hz):";
            
            // 
            // txtMeasFrequency
            // 
            this.txtMeasFrequency.Font = new Font("Segoe UI", 9F);
            this.txtMeasFrequency.Location = new Point(330, 57);
            this.txtMeasFrequency.Name = "txtMeasFrequency";
            this.txtMeasFrequency.ReadOnly = true;
            this.txtMeasFrequency.Size = new Size(100, 23);
            this.txtMeasFrequency.TabIndex = 9;
            this.txtMeasFrequency.Text = "-- Hz";
            this.txtMeasFrequency.TextAlign = HorizontalAlignment.Center;
            
            // 
            // btnReadMeasurements
            // 
            this.btnReadMeasurements.Font = new Font("Segoe UI", 9F);
            this.btnReadMeasurements.Location = new Point(15, 130);
            this.btnReadMeasurements.Name = "btnReadMeasurements";
            this.btnReadMeasurements.Size = new Size(120, 30);
            this.btnReadMeasurements.TabIndex = 10;
            this.btnReadMeasurements.Text = "Read Measurements";
            this.btnReadMeasurements.UseVisualStyleBackColor = true;
            this.btnReadMeasurements.Click += new EventHandler(this.btnReadMeasurements_Click);
            
            // 
            // chkAutoRead
            // 
            this.chkAutoRead.AutoSize = true;
            this.chkAutoRead.Font = new Font("Segoe UI", 9F);
            this.chkAutoRead.Location = new Point(150, 137);
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
            this.grpDeviceInfo.Location = new Point(12, 430);
            this.grpDeviceInfo.Name = "grpDeviceInfo";
            this.grpDeviceInfo.Size = new Size(520, 100);
            this.grpDeviceInfo.TabIndex = 4;
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
            this.txtDeviceInfo.Size = new Size(370, 60);
            this.txtDeviceInfo.TabIndex = 0;
            this.txtDeviceInfo.Text = "Device not connected";
            
            // 
            // btnGetDeviceInfo
            // 
            this.btnGetDeviceInfo.Font = new Font("Segoe UI", 9F);
            this.btnGetDeviceInfo.Location = new Point(400, 25);
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
            this.btnCheckErrors.Location = new Point(400, 60);
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
            // AcPowerSupplyTestForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(550, 550);
            this.Controls.Add(this.grpDeviceInfo);
            this.Controls.Add(this.grpMeasurements);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.grpOutput);
            this.Controls.Add(this.grpConnection);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AcPowerSupplyTestForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "ITECH IT7321 AC Power Supply Test";
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpOutput.ResumeLayout(false);
            this.grpOutput.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).EndInit();
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
        private GroupBox grpOutput;
        private Label lblOutputStatus;
        private Button btnOutputOn;
        private Button btnOutputOff;
        private GroupBox grpSettings;
        private Label lblVoltage;
        private NumericUpDown numVoltage;
        private Button btnSetVoltage;
        private Label lblFrequency;
        private NumericUpDown numFrequency;
        private Button btnSetFrequency;
        private GroupBox grpMeasurements;
        private Label lblMeasVoltage;
        private TextBox txtMeasVoltage;
        private Label lblMeasCurrent;
        private TextBox txtMeasCurrent;
        private Label lblMeasPower;
        private TextBox txtMeasPower;
        private Label lblMeasPowerFactor;
        private TextBox txtMeasPowerFactor;
        private Label lblMeasFrequency;
        private TextBox txtMeasFrequency;
        private Button btnReadMeasurements;
        private CheckBox chkAutoRead;
        private GroupBox grpDeviceInfo;
        private TextBox txtDeviceInfo;
        private Button btnGetDeviceInfo;
        private Button btnCheckErrors;
        private System.Windows.Forms.Timer autoReadTimer;
    }
}