namespace HendersonvilleTrafficTest.Forms
{
    partial class DcPowerSupplyTestForm
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
            this.lblCurrent = new Label();
            this.numCurrent = new NumericUpDown();
            this.btnSetCurrent = new Button();
            this.grpMeasurements = new GroupBox();
            this.lblMeasVoltage = new Label();
            this.txtMeasVoltage = new TextBox();
            this.lblMeasCurrent = new Label();
            this.txtMeasCurrent = new TextBox();
            this.lblMeasPower = new Label();
            this.txtMeasPower = new TextBox();
            this.btnReadMeasurements = new Button();
            this.chkAutoRead = new CheckBox();
            this.grpDeviceInfo = new GroupBox();
            this.txtDeviceInfo = new TextBox();
            this.btnGetDeviceInfo = new Button();
            this.btnCheckErrors = new Button();
            this.autoReadTimer = new System.Windows.Forms.Timer(this.components);
            this.grpConnection.SuspendLayout();
            this.grpOutput.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVoltage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrent)).BeginInit();
            this.grpMeasurements.SuspendLayout();
            this.grpDeviceInfo.SuspendLayout();
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
            this.lblConnectionStatus.Font = new Font("Segoe UI", 9F);
            this.lblConnectionStatus.ForeColor = Color.Red;
            this.lblConnectionStatus.Location = new Point(6, 22);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new Size(188, 23);
            this.lblConnectionStatus.TabIndex = 0;
            this.lblConnectionStatus.Text = "Disconnected";
            this.lblConnectionStatus.TextAlign = ContentAlignment.MiddleCenter;
            
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = Color.LightGreen;
            this.btnConnect.Font = new Font("Segoe UI", 9F);
            this.btnConnect.Location = new Point(50, 48);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new Size(100, 25);
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
            this.grpOutput.Enabled = false;
            this.grpOutput.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpOutput.Location = new Point(230, 12);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new Size(200, 120);
            this.grpOutput.TabIndex = 1;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output Control";
            
            // 
            // lblOutputStatus
            // 
            this.lblOutputStatus.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblOutputStatus.ForeColor = Color.Gray;
            this.lblOutputStatus.Location = new Point(6, 22);
            this.lblOutputStatus.Name = "lblOutputStatus";
            this.lblOutputStatus.Size = new Size(188, 30);
            this.lblOutputStatus.TabIndex = 0;
            this.lblOutputStatus.Text = "UNKNOWN";
            this.lblOutputStatus.TextAlign = ContentAlignment.MiddleCenter;
            
            // 
            // btnOutputOn
            // 
            this.btnOutputOn.BackColor = Color.LightGreen;
            this.btnOutputOn.Font = new Font("Segoe UI", 9F);
            this.btnOutputOn.Location = new Point(20, 60);
            this.btnOutputOn.Name = "btnOutputOn";
            this.btnOutputOn.Size = new Size(75, 30);
            this.btnOutputOn.TabIndex = 1;
            this.btnOutputOn.Text = "ON";
            this.btnOutputOn.UseVisualStyleBackColor = false;
            this.btnOutputOn.Click += new EventHandler(this.btnOutputOn_Click);
            
            // 
            // btnOutputOff
            // 
            this.btnOutputOff.BackColor = Color.LightCoral;
            this.btnOutputOff.Font = new Font("Segoe UI", 9F);
            this.btnOutputOff.Location = new Point(105, 60);
            this.btnOutputOff.Name = "btnOutputOff";
            this.btnOutputOff.Size = new Size(75, 30);
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
            this.grpSettings.Controls.Add(this.lblCurrent);
            this.grpSettings.Controls.Add(this.numCurrent);
            this.grpSettings.Controls.Add(this.btnSetCurrent);
            this.grpSettings.Enabled = false;
            this.grpSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpSettings.Location = new Point(12, 100);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new Size(418, 100);
            this.grpSettings.TabIndex = 2;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings";
            
            // 
            // lblVoltage
            // 
            this.lblVoltage.AutoSize = true;
            this.lblVoltage.Font = new Font("Segoe UI", 9F);
            this.lblVoltage.Location = new Point(15, 30);
            this.lblVoltage.Name = "lblVoltage";
            this.lblVoltage.Size = new Size(60, 15);
            this.lblVoltage.TabIndex = 0;
            this.lblVoltage.Text = "Voltage (V):";
            
            // 
            // numVoltage
            // 
            this.numVoltage.DecimalPlaces = 1;
            this.numVoltage.Font = new Font("Segoe UI", 9F);
            this.numVoltage.Location = new Point(85, 28);
            this.numVoltage.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            this.numVoltage.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numVoltage.Name = "numVoltage";
            this.numVoltage.Size = new Size(80, 23);
            this.numVoltage.TabIndex = 1;
            this.numVoltage.Value = new decimal(new int[] { 0, 0, 0, 0 });
            
            // 
            // btnSetVoltage
            // 
            this.btnSetVoltage.Font = new Font("Segoe UI", 8F);
            this.btnSetVoltage.Location = new Point(175, 27);
            this.btnSetVoltage.Name = "btnSetVoltage";
            this.btnSetVoltage.Size = new Size(50, 25);
            this.btnSetVoltage.TabIndex = 2;
            this.btnSetVoltage.Text = "Set";
            this.btnSetVoltage.UseVisualStyleBackColor = true;
            this.btnSetVoltage.Click += new EventHandler(this.btnSetVoltage_Click);
            
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new Font("Segoe UI", 9F);
            this.lblCurrent.Location = new Point(15, 65);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new Size(60, 15);
            this.lblCurrent.TabIndex = 3;
            this.lblCurrent.Text = "Current (A):";
            
            // 
            // numCurrent
            // 
            this.numCurrent.DecimalPlaces = 2;
            this.numCurrent.Font = new Font("Segoe UI", 9F);
            this.numCurrent.Location = new Point(85, 63);
            this.numCurrent.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.numCurrent.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numCurrent.Name = "numCurrent";
            this.numCurrent.Size = new Size(80, 23);
            this.numCurrent.TabIndex = 4;
            this.numCurrent.Value = new decimal(new int[] { 0, 0, 0, 0 });
            
            // 
            // btnSetCurrent
            // 
            this.btnSetCurrent.Font = new Font("Segoe UI", 8F);
            this.btnSetCurrent.Location = new Point(175, 62);
            this.btnSetCurrent.Name = "btnSetCurrent";
            this.btnSetCurrent.Size = new Size(50, 25);
            this.btnSetCurrent.TabIndex = 5;
            this.btnSetCurrent.Text = "Set";
            this.btnSetCurrent.UseVisualStyleBackColor = true;
            this.btnSetCurrent.Click += new EventHandler(this.btnSetCurrent_Click);
            
            // 
            // grpMeasurements
            // 
            this.grpMeasurements.Controls.Add(this.lblMeasVoltage);
            this.grpMeasurements.Controls.Add(this.txtMeasVoltage);
            this.grpMeasurements.Controls.Add(this.lblMeasCurrent);
            this.grpMeasurements.Controls.Add(this.txtMeasCurrent);
            this.grpMeasurements.Controls.Add(this.lblMeasPower);
            this.grpMeasurements.Controls.Add(this.txtMeasPower);
            this.grpMeasurements.Controls.Add(this.btnReadMeasurements);
            this.grpMeasurements.Controls.Add(this.chkAutoRead);
            this.grpMeasurements.Enabled = false;
            this.grpMeasurements.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpMeasurements.Location = new Point(12, 210);
            this.grpMeasurements.Name = "grpMeasurements";
            this.grpMeasurements.Size = new Size(418, 120);
            this.grpMeasurements.TabIndex = 3;
            this.grpMeasurements.TabStop = false;
            this.grpMeasurements.Text = "Measurements";
            
            // 
            // lblMeasVoltage
            // 
            this.lblMeasVoltage.AutoSize = true;
            this.lblMeasVoltage.Font = new Font("Segoe UI", 9F);
            this.lblMeasVoltage.Location = new Point(15, 25);
            this.lblMeasVoltage.Name = "lblMeasVoltage";
            this.lblMeasVoltage.Size = new Size(50, 15);
            this.lblMeasVoltage.TabIndex = 0;
            this.lblMeasVoltage.Text = "Voltage:";
            
            // 
            // txtMeasVoltage
            // 
            this.txtMeasVoltage.Font = new Font("Segoe UI", 9F);
            this.txtMeasVoltage.Location = new Point(75, 22);
            this.txtMeasVoltage.Name = "txtMeasVoltage";
            this.txtMeasVoltage.ReadOnly = true;
            this.txtMeasVoltage.Size = new Size(80, 23);
            this.txtMeasVoltage.TabIndex = 1;
            this.txtMeasVoltage.Text = "-- V";
            
            // 
            // lblMeasCurrent
            // 
            this.lblMeasCurrent.AutoSize = true;
            this.lblMeasCurrent.Font = new Font("Segoe UI", 9F);
            this.lblMeasCurrent.Location = new Point(170, 25);
            this.lblMeasCurrent.Name = "lblMeasCurrent";
            this.lblMeasCurrent.Size = new Size(50, 15);
            this.lblMeasCurrent.TabIndex = 2;
            this.lblMeasCurrent.Text = "Current:";
            
            // 
            // txtMeasCurrent
            // 
            this.txtMeasCurrent.Font = new Font("Segoe UI", 9F);
            this.txtMeasCurrent.Location = new Point(230, 22);
            this.txtMeasCurrent.Name = "txtMeasCurrent";
            this.txtMeasCurrent.ReadOnly = true;
            this.txtMeasCurrent.Size = new Size(80, 23);
            this.txtMeasCurrent.TabIndex = 3;
            this.txtMeasCurrent.Text = "-- A";
            
            // 
            // lblMeasPower
            // 
            this.lblMeasPower.AutoSize = true;
            this.lblMeasPower.Font = new Font("Segoe UI", 9F);
            this.lblMeasPower.Location = new Point(320, 25);
            this.lblMeasPower.Name = "lblMeasPower";
            this.lblMeasPower.Size = new Size(40, 15);
            this.lblMeasPower.TabIndex = 4;
            this.lblMeasPower.Text = "Power:";
            
            // 
            // txtMeasPower
            // 
            this.txtMeasPower.Font = new Font("Segoe UI", 9F);
            this.txtMeasPower.Location = new Point(320, 45);
            this.txtMeasPower.Name = "txtMeasPower";
            this.txtMeasPower.ReadOnly = true;
            this.txtMeasPower.Size = new Size(80, 23);
            this.txtMeasPower.TabIndex = 5;
            this.txtMeasPower.Text = "-- W";
            
            // 
            // btnReadMeasurements
            // 
            this.btnReadMeasurements.Font = new Font("Segoe UI", 9F);
            this.btnReadMeasurements.Location = new Point(15, 55);
            this.btnReadMeasurements.Name = "btnReadMeasurements";
            this.btnReadMeasurements.Size = new Size(120, 25);
            this.btnReadMeasurements.TabIndex = 6;
            this.btnReadMeasurements.Text = "Read Measurements";
            this.btnReadMeasurements.UseVisualStyleBackColor = true;
            this.btnReadMeasurements.Click += new EventHandler(this.btnReadMeasurements_Click);
            
            // 
            // chkAutoRead
            // 
            this.chkAutoRead.AutoSize = true;
            this.chkAutoRead.Font = new Font("Segoe UI", 9F);
            this.chkAutoRead.Location = new Point(15, 90);
            this.chkAutoRead.Name = "chkAutoRead";
            this.chkAutoRead.Size = new Size(149, 19);
            this.chkAutoRead.TabIndex = 7;
            this.chkAutoRead.Text = "Auto-read (every 2 sec)";
            this.chkAutoRead.UseVisualStyleBackColor = true;
            this.chkAutoRead.CheckedChanged += new EventHandler(this.chkAutoRead_CheckedChanged);
            
            // 
            // grpDeviceInfo
            // 
            this.grpDeviceInfo.Controls.Add(this.txtDeviceInfo);
            this.grpDeviceInfo.Controls.Add(this.btnGetDeviceInfo);
            this.grpDeviceInfo.Controls.Add(this.btnCheckErrors);
            this.grpDeviceInfo.Enabled = false;
            this.grpDeviceInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpDeviceInfo.Location = new Point(12, 340);
            this.grpDeviceInfo.Name = "grpDeviceInfo";
            this.grpDeviceInfo.Size = new Size(418, 100);
            this.grpDeviceInfo.TabIndex = 4;
            this.grpDeviceInfo.TabStop = false;
            this.grpDeviceInfo.Text = "Device Information";
            
            // 
            // txtDeviceInfo
            // 
            this.txtDeviceInfo.Font = new Font("Segoe UI", 8F);
            this.txtDeviceInfo.Location = new Point(15, 22);
            this.txtDeviceInfo.Multiline = true;
            this.txtDeviceInfo.Name = "txtDeviceInfo";
            this.txtDeviceInfo.ReadOnly = true;
            this.txtDeviceInfo.ScrollBars = ScrollBars.Vertical;
            this.txtDeviceInfo.Size = new Size(390, 40);
            this.txtDeviceInfo.TabIndex = 0;
            this.txtDeviceInfo.Text = "Device not connected";
            
            // 
            // btnGetDeviceInfo
            // 
            this.btnGetDeviceInfo.Font = new Font("Segoe UI", 9F);
            this.btnGetDeviceInfo.Location = new Point(15, 68);
            this.btnGetDeviceInfo.Name = "btnGetDeviceInfo";
            this.btnGetDeviceInfo.Size = new Size(120, 25);
            this.btnGetDeviceInfo.TabIndex = 1;
            this.btnGetDeviceInfo.Text = "Get Device Info";
            this.btnGetDeviceInfo.UseVisualStyleBackColor = true;
            this.btnGetDeviceInfo.Click += new EventHandler(this.btnGetDeviceInfo_Click);
            
            // 
            // btnCheckErrors
            // 
            this.btnCheckErrors.Font = new Font("Segoe UI", 9F);
            this.btnCheckErrors.Location = new Point(150, 68);
            this.btnCheckErrors.Name = "btnCheckErrors";
            this.btnCheckErrors.Size = new Size(100, 25);
            this.btnCheckErrors.TabIndex = 2;
            this.btnCheckErrors.Text = "Check Errors";
            this.btnCheckErrors.UseVisualStyleBackColor = true;
            this.btnCheckErrors.Click += new EventHandler(this.btnCheckErrors_Click);
            
            // 
            // autoReadTimer
            // 
            this.autoReadTimer.Interval = 2000;
            this.autoReadTimer.Tick += new EventHandler(this.autoReadTimer_Tick);
            
            // 
            // DcPowerSupplyTestForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(450, 460);
            this.Controls.Add(this.grpDeviceInfo);
            this.Controls.Add(this.grpMeasurements);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.grpOutput);
            this.Controls.Add(this.grpConnection);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DcPowerSupplyTestForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "DC Power Supply Test - ITECH IT6922A";
            this.FormClosing += new FormClosingEventHandler(this.DcPowerSupplyTestForm_FormClosing);
            this.grpConnection.ResumeLayout(false);
            this.grpOutput.ResumeLayout(false);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVoltage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrent)).EndInit();
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
        private Label lblCurrent;
        private NumericUpDown numCurrent;
        private Button btnSetCurrent;
        private GroupBox grpMeasurements;
        private Label lblMeasVoltage;
        private TextBox txtMeasVoltage;
        private Label lblMeasCurrent;
        private TextBox txtMeasCurrent;
        private Label lblMeasPower;
        private TextBox txtMeasPower;
        private Button btnReadMeasurements;
        private CheckBox chkAutoRead;
        private GroupBox grpDeviceInfo;
        private TextBox txtDeviceInfo;
        private Button btnGetDeviceInfo;
        private Button btnCheckErrors;
        private System.Windows.Forms.Timer autoReadTimer;
    }
}