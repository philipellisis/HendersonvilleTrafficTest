namespace HendersonvilleTrafficTest.Forms
{
    partial class TemperatureSensorTestForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblConnectionStatus;
        private GroupBox grpSensorConfig;
        private Label lblSensorType;
        private ComboBox cmbSensorType;
        private GroupBox grpReadings;
        private Label lblTemperature;
        private TextBox txtTemperature;
        private Label lblHumidity;
        private TextBox txtHumidity;
        private Label lblErrorCount;
        private TextBox txtErrorCount;
        private CheckBox chkTempValid;
        private CheckBox chkHumValid;
        private CheckBox chkHeating;
        private CheckBox chkErrorOverflow;
        private GroupBox grpControls;
        private Button btnConnect;
        private Button btnReadOnce;
        private CheckBox chkAutoRead;
        private Button btnClose;
        private System.Windows.Forms.Timer autoReadTimer;

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblConnectionStatus = new Label();
            this.grpSensorConfig = new GroupBox();
            this.lblSensorType = new Label();
            this.cmbSensorType = new ComboBox();
            this.grpReadings = new GroupBox();
            this.lblTemperature = new Label();
            this.txtTemperature = new TextBox();
            this.lblHumidity = new Label();
            this.txtHumidity = new TextBox();
            this.lblErrorCount = new Label();
            this.txtErrorCount = new TextBox();
            this.chkTempValid = new CheckBox();
            this.chkHumValid = new CheckBox();
            this.chkHeating = new CheckBox();
            this.chkErrorOverflow = new CheckBox();
            this.grpControls = new GroupBox();
            this.btnConnect = new Button();
            this.btnReadOnce = new Button();
            this.chkAutoRead = new CheckBox();
            this.btnClose = new Button();
            this.autoReadTimer = new System.Windows.Forms.Timer();
            this.grpSensorConfig.SuspendLayout();
            this.grpReadings.SuspendLayout();
            this.grpControls.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(267, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Temperature Sensor Test Form";

            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblConnectionStatus.Location = new Point(12, 40);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new Size(120, 19);
            this.lblConnectionStatus.TabIndex = 1;
            this.lblConnectionStatus.Text = "Connecting...";
            this.lblConnectionStatus.ForeColor = Color.Orange;

            // 
            // grpSensorConfig
            // 
            this.grpSensorConfig.Controls.Add(this.cmbSensorType);
            this.grpSensorConfig.Controls.Add(this.lblSensorType);
            this.grpSensorConfig.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpSensorConfig.Location = new Point(12, 70);
            this.grpSensorConfig.Name = "grpSensorConfig";
            this.grpSensorConfig.Size = new Size(460, 60);
            this.grpSensorConfig.TabIndex = 2;
            this.grpSensorConfig.TabStop = false;
            this.grpSensorConfig.Text = "Sensor Configuration";

            // 
            // lblSensorType
            // 
            this.lblSensorType.AutoSize = true;
            this.lblSensorType.Font = new Font("Segoe UI", 9F);
            this.lblSensorType.Location = new Point(15, 25);
            this.lblSensorType.Name = "lblSensorType";
            this.lblSensorType.Size = new Size(75, 15);
            this.lblSensorType.TabIndex = 0;
            this.lblSensorType.Text = "Sensor Type:";

            // 
            // cmbSensorType
            // 
            this.cmbSensorType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSensorType.Font = new Font("Segoe UI", 9F);
            this.cmbSensorType.FormattingEnabled = true;
            this.cmbSensorType.Location = new Point(100, 22);
            this.cmbSensorType.Name = "cmbSensorType";
            this.cmbSensorType.Size = new Size(200, 23);
            this.cmbSensorType.TabIndex = 1;
            this.cmbSensorType.SelectedIndexChanged += new EventHandler(this.cmbSensorType_SelectedIndexChanged);

            // 
            // grpReadings
            // 
            this.grpReadings.Controls.Add(this.chkErrorOverflow);
            this.grpReadings.Controls.Add(this.chkHeating);
            this.grpReadings.Controls.Add(this.chkHumValid);
            this.grpReadings.Controls.Add(this.chkTempValid);
            this.grpReadings.Controls.Add(this.txtErrorCount);
            this.grpReadings.Controls.Add(this.lblErrorCount);
            this.grpReadings.Controls.Add(this.txtHumidity);
            this.grpReadings.Controls.Add(this.lblHumidity);
            this.grpReadings.Controls.Add(this.txtTemperature);
            this.grpReadings.Controls.Add(this.lblTemperature);
            this.grpReadings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpReadings.Location = new Point(12, 140);
            this.grpReadings.Name = "grpReadings";
            this.grpReadings.Size = new Size(460, 160);
            this.grpReadings.TabIndex = 3;
            this.grpReadings.TabStop = false;
            this.grpReadings.Text = "Sensor Readings";

            // 
            // lblTemperature
            // 
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Font = new Font("Segoe UI", 9F);
            this.lblTemperature.Location = new Point(15, 25);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new Size(80, 15);
            this.lblTemperature.TabIndex = 0;
            this.lblTemperature.Text = "Temperature:";

            // 
            // txtTemperature
            // 
            this.txtTemperature.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.txtTemperature.Location = new Point(110, 20);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.ReadOnly = true;
            this.txtTemperature.Size = new Size(100, 29);
            this.txtTemperature.TabIndex = 1;
            this.txtTemperature.Text = "-- °C";
            this.txtTemperature.TextAlign = HorizontalAlignment.Center;

            // 
            // lblHumidity
            // 
            this.lblHumidity.AutoSize = true;
            this.lblHumidity.Font = new Font("Segoe UI", 9F);
            this.lblHumidity.Location = new Point(15, 60);
            this.lblHumidity.Name = "lblHumidity";
            this.lblHumidity.Size = new Size(58, 15);
            this.lblHumidity.TabIndex = 2;
            this.lblHumidity.Text = "Humidity:";

            // 
            // txtHumidity
            // 
            this.txtHumidity.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.txtHumidity.Location = new Point(110, 55);
            this.txtHumidity.Name = "txtHumidity";
            this.txtHumidity.ReadOnly = true;
            this.txtHumidity.Size = new Size(100, 29);
            this.txtHumidity.TabIndex = 3;
            this.txtHumidity.Text = "-- %";
            this.txtHumidity.TextAlign = HorizontalAlignment.Center;

            // 
            // lblErrorCount
            // 
            this.lblErrorCount.AutoSize = true;
            this.lblErrorCount.Font = new Font("Segoe UI", 9F);
            this.lblErrorCount.Location = new Point(15, 95);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new Size(70, 15);
            this.lblErrorCount.TabIndex = 4;
            this.lblErrorCount.Text = "Error Count:";

            // 
            // txtErrorCount
            // 
            this.txtErrorCount.Font = new Font("Segoe UI", 9F);
            this.txtErrorCount.Location = new Point(110, 92);
            this.txtErrorCount.Name = "txtErrorCount";
            this.txtErrorCount.ReadOnly = true;
            this.txtErrorCount.Size = new Size(60, 23);
            this.txtErrorCount.TabIndex = 5;
            this.txtErrorCount.Text = "0";
            this.txtErrorCount.TextAlign = HorizontalAlignment.Center;

            // 
            // chkTempValid
            // 
            this.chkTempValid.AutoSize = true;
            this.chkTempValid.Enabled = false;
            this.chkTempValid.Font = new Font("Segoe UI", 9F);
            this.chkTempValid.Location = new Point(240, 25);
            this.chkTempValid.Name = "chkTempValid";
            this.chkTempValid.Size = new Size(109, 19);
            this.chkTempValid.TabIndex = 6;
            this.chkTempValid.Text = "Temp Valid";
            this.chkTempValid.UseVisualStyleBackColor = true;

            // 
            // chkHumValid
            // 
            this.chkHumValid.AutoSize = true;
            this.chkHumValid.Enabled = false;
            this.chkHumValid.Font = new Font("Segoe UI", 9F);
            this.chkHumValid.Location = new Point(240, 60);
            this.chkHumValid.Name = "chkHumValid";
            this.chkHumValid.Size = new Size(106, 19);
            this.chkHumValid.TabIndex = 7;
            this.chkHumValid.Text = "Humidity Valid";
            this.chkHumValid.UseVisualStyleBackColor = true;

            // 
            // chkHeating
            // 
            this.chkHeating.AutoSize = true;
            this.chkHeating.Enabled = false;
            this.chkHeating.Font = new Font("Segoe UI", 9F);
            this.chkHeating.Location = new Point(240, 95);
            this.chkHeating.Name = "chkHeating";
            this.chkHeating.Size = new Size(82, 19);
            this.chkHeating.TabIndex = 8;
            this.chkHeating.Text = "Heating On";
            this.chkHeating.UseVisualStyleBackColor = true;

            // 
            // chkErrorOverflow
            // 
            this.chkErrorOverflow.AutoSize = true;
            this.chkErrorOverflow.Enabled = false;
            this.chkErrorOverflow.Font = new Font("Segoe UI", 9F);
            this.chkErrorOverflow.Location = new Point(240, 125);
            this.chkErrorOverflow.Name = "chkErrorOverflow";
            this.chkErrorOverflow.Size = new Size(103, 19);
            this.chkErrorOverflow.TabIndex = 9;
            this.chkErrorOverflow.Text = "Error Overflow";
            this.chkErrorOverflow.UseVisualStyleBackColor = true;

            // 
            // grpControls
            // 
            this.grpControls.Controls.Add(this.chkAutoRead);
            this.grpControls.Controls.Add(this.btnReadOnce);
            this.grpControls.Controls.Add(this.btnConnect);
            this.grpControls.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpControls.Location = new Point(12, 310);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new Size(460, 80);
            this.grpControls.TabIndex = 4;
            this.grpControls.TabStop = false;
            this.grpControls.Text = "Controls";

            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = Color.LightGreen;
            this.btnConnect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnConnect.Location = new Point(15, 25);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new Size(100, 35);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new EventHandler(this.btnConnect_Click);

            // 
            // btnReadOnce
            // 
            this.btnReadOnce.Enabled = false;
            this.btnReadOnce.Font = new Font("Segoe UI", 9F);
            this.btnReadOnce.Location = new Point(130, 25);
            this.btnReadOnce.Name = "btnReadOnce";
            this.btnReadOnce.Size = new Size(100, 35);
            this.btnReadOnce.TabIndex = 1;
            this.btnReadOnce.Text = "Read Once";
            this.btnReadOnce.UseVisualStyleBackColor = true;
            this.btnReadOnce.Click += new EventHandler(this.btnReadOnce_Click);

            // 
            // chkAutoRead
            // 
            this.chkAutoRead.AutoSize = true;
            this.chkAutoRead.Enabled = false;
            this.chkAutoRead.Font = new Font("Segoe UI", 9F);
            this.chkAutoRead.Location = new Point(250, 35);
            this.chkAutoRead.Name = "chkAutoRead";
            this.chkAutoRead.Size = new Size(150, 19);
            this.chkAutoRead.TabIndex = 2;
            this.chkAutoRead.Text = "Auto Read (2 seconds)";
            this.chkAutoRead.UseVisualStyleBackColor = true;
            this.chkAutoRead.CheckedChanged += new EventHandler(this.chkAutoRead_CheckedChanged);

            // 
            // btnClose
            // 
            this.btnClose.Font = new Font("Segoe UI", 9F);
            this.btnClose.Location = new Point(397, 400);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(75, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler((s, e) => this.Close());

            // 
            // autoReadTimer
            // 
            this.autoReadTimer.Interval = 2000;
            this.autoReadTimer.Tick += new EventHandler(this.autoReadTimer_Tick);

            // 
            // TemperatureSensorTestForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(484, 442);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpControls);
            this.Controls.Add(this.grpReadings);
            this.Controls.Add(this.grpSensorConfig);
            this.Controls.Add(this.lblConnectionStatus);
            this.Controls.Add(this.lblTitle);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemperatureSensorTestForm";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Temperature Sensor Test";
            this.grpSensorConfig.ResumeLayout(false);
            this.grpSensorConfig.PerformLayout();
            this.grpReadings.ResumeLayout(false);
            this.grpReadings.PerformLayout();
            this.grpControls.ResumeLayout(false);
            this.grpControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}