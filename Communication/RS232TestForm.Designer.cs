namespace HendersonvilleTrafficTest.Communication
{
    partial class RS232TestForm
    {
        private System.ComponentModel.IContainer components = null;
        private GroupBox grpPortSettings;
        private GroupBox grpCommands;
        private Label lblPortName;
        private ComboBox cmbPortName;
        private Button btnRefreshPorts;
        private Label lblBaudRate;
        private ComboBox cmbBaudRate;
        private Label lblParity;
        private ComboBox cmbParity;
        private Label lblDataBits;
        private ComboBox cmbDataBits;
        private Label lblStopBits;
        private ComboBox cmbStopBits;
        private Label lblReadTimeout;
        private NumericUpDown numReadTimeout;
        private Label lblWriteTimeout;
        private NumericUpDown numWriteTimeout;
        private Button btnConnect;
        private Label lblConnectionStatus;
        private Label lblPortCount;
        private TextBox txtCommand;
        private Button btnSendCommand;
        private CheckBox chkExpectResponse;
        private NumericUpDown numResponseTimeout;
        private ComboBox cmbCommandHistory;
        private TextBox txtLog;
        private Button btnClearLog;


        private void InitializeComponent()
        {
            this.grpPortSettings = new GroupBox();
            this.grpCommands = new GroupBox();
            this.lblPortName = new Label();
            this.cmbPortName = new ComboBox();
            this.btnRefreshPorts = new Button();
            this.lblBaudRate = new Label();
            this.cmbBaudRate = new ComboBox();
            this.lblParity = new Label();
            this.cmbParity = new ComboBox();
            this.lblDataBits = new Label();
            this.cmbDataBits = new ComboBox();
            this.lblStopBits = new Label();
            this.cmbStopBits = new ComboBox();
            this.lblReadTimeout = new Label();
            this.numReadTimeout = new NumericUpDown();
            this.lblWriteTimeout = new Label();
            this.numWriteTimeout = new NumericUpDown();
            this.btnConnect = new Button();
            this.lblConnectionStatus = new Label();
            this.lblPortCount = new Label();
            this.txtCommand = new TextBox();
            this.btnSendCommand = new Button();
            this.chkExpectResponse = new CheckBox();
            this.numResponseTimeout = new NumericUpDown();
            this.cmbCommandHistory = new ComboBox();
            this.txtLog = new TextBox();
            this.btnClearLog = new Button();
            
            this.grpPortSettings.SuspendLayout();
            this.grpCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numResponseTimeout)).BeginInit();
            this.SuspendLayout();

            // grpPortSettings
            this.grpPortSettings.Controls.Add(this.lblPortName);
            this.grpPortSettings.Controls.Add(this.cmbPortName);
            this.grpPortSettings.Controls.Add(this.btnRefreshPorts);
            this.grpPortSettings.Controls.Add(this.lblBaudRate);
            this.grpPortSettings.Controls.Add(this.cmbBaudRate);
            this.grpPortSettings.Controls.Add(this.lblParity);
            this.grpPortSettings.Controls.Add(this.cmbParity);
            this.grpPortSettings.Controls.Add(this.lblDataBits);
            this.grpPortSettings.Controls.Add(this.cmbDataBits);
            this.grpPortSettings.Controls.Add(this.lblStopBits);
            this.grpPortSettings.Controls.Add(this.cmbStopBits);
            this.grpPortSettings.Controls.Add(this.lblReadTimeout);
            this.grpPortSettings.Controls.Add(this.numReadTimeout);
            this.grpPortSettings.Controls.Add(this.lblWriteTimeout);
            this.grpPortSettings.Controls.Add(this.numWriteTimeout);
            this.grpPortSettings.Location = new Point(12, 12);
            this.grpPortSettings.Name = "grpPortSettings";
            this.grpPortSettings.Size = new Size(350, 200);
            this.grpPortSettings.TabIndex = 0;
            this.grpPortSettings.TabStop = false;
            this.grpPortSettings.Text = "Port Settings";

            // Port Name
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new Point(6, 25);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new Size(35, 15);
            this.lblPortName.Text = "Port:";
            
            this.cmbPortName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPortName.Location = new Point(80, 22);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new Size(120, 23);
            
            this.btnRefreshPorts.Location = new Point(210, 21);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new Size(60, 25);
            this.btnRefreshPorts.Text = "Refresh";
            this.btnRefreshPorts.UseVisualStyleBackColor = true;
            this.btnRefreshPorts.Click += new EventHandler(this.btnRefreshPorts_Click);

            // Baud Rate
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new Point(6, 55);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Text = "Baud Rate:";
            
            this.cmbBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbBaudRate.Location = new Point(80, 52);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new Size(120, 23);

            // Parity
            this.lblParity.AutoSize = true;
            this.lblParity.Location = new Point(6, 85);
            this.lblParity.Name = "lblParity";
            this.lblParity.Text = "Parity:";
            
            this.cmbParity.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbParity.Location = new Point(80, 82);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new Size(120, 23);

            // Data Bits
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Location = new Point(6, 115);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Text = "Data Bits:";
            
            this.cmbDataBits.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDataBits.Location = new Point(80, 112);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new Size(120, 23);

            // Stop Bits
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Location = new Point(6, 145);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Text = "Stop Bits:";
            
            this.cmbStopBits.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStopBits.Location = new Point(80, 142);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new Size(120, 23);

            // Timeouts
            this.lblReadTimeout.AutoSize = true;
            this.lblReadTimeout.Location = new Point(220, 85);
            this.lblReadTimeout.Text = "Read TO (ms):";
            
            this.numReadTimeout.Location = new Point(220, 102);
            this.numReadTimeout.Maximum = 10000;
            this.numReadTimeout.Minimum = 100;
            this.numReadTimeout.Name = "numReadTimeout";
            this.numReadTimeout.Size = new Size(80, 23);
            this.numReadTimeout.Value = 1000;

            this.lblWriteTimeout.AutoSize = true;
            this.lblWriteTimeout.Location = new Point(220, 125);
            this.lblWriteTimeout.Text = "Write TO (ms):";
            
            this.numWriteTimeout.Location = new Point(220, 142);
            this.numWriteTimeout.Maximum = 10000;
            this.numWriteTimeout.Minimum = 100;
            this.numWriteTimeout.Name = "numWriteTimeout";
            this.numWriteTimeout.Size = new Size(80, 23);
            this.numWriteTimeout.Value = 1000;

            // Connection Controls
            this.btnConnect.BackColor = Color.LightGreen;
            this.btnConnect.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnConnect.Location = new Point(380, 40);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new Size(100, 35);
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new EventHandler(this.btnConnect_Click);

            this.lblConnectionStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblConnectionStatus.ForeColor = Color.Red;
            this.lblConnectionStatus.Location = new Point(380, 80);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new Size(100, 19);
            this.lblConnectionStatus.Text = "Disconnected";
            this.lblConnectionStatus.TextAlign = ContentAlignment.MiddleCenter;

            this.lblPortCount.Location = new Point(380, 110);
            this.lblPortCount.Name = "lblPortCount";
            this.lblPortCount.Size = new Size(100, 15);
            this.lblPortCount.Text = "Available Ports: 0";
            this.lblPortCount.TextAlign = ContentAlignment.MiddleCenter;

            // Commands Group
            this.grpCommands.Controls.Add(this.cmbCommandHistory);
            this.grpCommands.Controls.Add(this.txtCommand);
            this.grpCommands.Controls.Add(this.btnSendCommand);
            this.grpCommands.Controls.Add(this.chkExpectResponse);
            this.grpCommands.Controls.Add(this.numResponseTimeout);
            this.grpCommands.Enabled = false;
            this.grpCommands.Location = new Point(12, 220);
            this.grpCommands.Name = "grpCommands";
            this.grpCommands.Size = new Size(470, 120);
            this.grpCommands.TabStop = false;
            this.grpCommands.Text = "Commands";

            this.cmbCommandHistory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCommandHistory.Location = new Point(10, 25);
            this.cmbCommandHistory.Name = "cmbCommandHistory";
            this.cmbCommandHistory.Size = new Size(200, 23);
            this.cmbCommandHistory.SelectedIndexChanged += new EventHandler(this.cmbCommandHistory_SelectedIndexChanged);

            this.txtCommand.Location = new Point(10, 55);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new Size(300, 23);

            this.btnSendCommand.Location = new Point(320, 54);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new Size(75, 25);
            this.btnSendCommand.Text = "Send";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new EventHandler(this.btnSendCommand_Click);

            this.chkExpectResponse.AutoSize = true;
            this.chkExpectResponse.Checked = true;
            this.chkExpectResponse.Location = new Point(10, 85);
            this.chkExpectResponse.Name = "chkExpectResponse";
            this.chkExpectResponse.Size = new Size(115, 19);
            this.chkExpectResponse.Text = "Expect Response";
            this.chkExpectResponse.UseVisualStyleBackColor = true;

            this.numResponseTimeout.Location = new Point(140, 83);
            this.numResponseTimeout.Maximum = 10000;
            this.numResponseTimeout.Minimum = 100;
            this.numResponseTimeout.Name = "numResponseTimeout";
            this.numResponseTimeout.Size = new Size(80, 23);
            this.numResponseTimeout.Value = 2000;

            // Log
            this.txtLog.Font = new Font("Consolas", 8.25F);
            this.txtLog.Location = new Point(12, 350);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.Size = new Size(470, 200);

            this.btnClearLog.Location = new Point(400, 320);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new Size(82, 25);
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new EventHandler(this.btnClearLog_Click);

            // RS232TestForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 565);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.grpCommands);
            this.Controls.Add(this.lblPortCount);
            this.Controls.Add(this.lblConnectionStatus);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.grpPortSettings);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RS232TestForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "RS232 Communication Test";
            
            this.grpPortSettings.ResumeLayout(false);
            this.grpPortSettings.PerformLayout();
            this.grpCommands.ResumeLayout(false);
            this.grpCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numResponseTimeout)).EndInit();
            this.ResumeLayout(false);
        }
    }
}