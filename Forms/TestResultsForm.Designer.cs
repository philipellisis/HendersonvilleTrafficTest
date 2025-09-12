namespace HendersonvilleTrafficTest.Forms
{
    partial class TestResultsForm
    {
        private System.ComponentModel.IContainer components = null;
        
        private Label lblPassFail;
        private Label lblOperator;
        private TextBox txtOperator;
        private Label lblTimeDate;
        private TextBox txtTimeDate;
        private Label lblCount;
        private TextBox txtCount;
        private Label lblSerialNo;
        private TextBox txtSerialNo;
        private Label lblAcDcTest;
        private TextBox txtAcDcTest;
        private Label lblTestCode;
        private ComboBox txtTestCode;
        private Label lblCatNo;
        private TextBox txtCatNo;
        private Label lblProdType;
        private TextBox txtProdType;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblMfgNo;
        private TextBox txtMfgNo;
        
        private FlowLayoutPanel pnlTestParameters;
        private GroupBox grpProductInfo;
        private Button btnColorCalibration;
        private Button btnStartTest;
        private TextBox txtTestStatus;
        private Label lblTestStatus;
        private ProgressBar prgTestProgress;
        
        // Status fields
        private Label lblNetworkStatus;
        private Label lblIpAddress;
        private Label lblMachineId;
        private Label lblLineId;
        private Label lblSoftwareRev;

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
            this.lblPassFail = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.lblTimeDate = new System.Windows.Forms.Label();
            this.txtTimeDate = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblSerialNo = new System.Windows.Forms.Label();
            this.txtSerialNo = new System.Windows.Forms.TextBox();
            this.lblAcDcTest = new System.Windows.Forms.Label();
            this.txtAcDcTest = new System.Windows.Forms.TextBox();
            this.lblTestCode = new System.Windows.Forms.Label();
            this.txtTestCode = new System.Windows.Forms.ComboBox();
            this.lblCatNo = new System.Windows.Forms.Label();
            this.txtCatNo = new System.Windows.Forms.TextBox();
            this.lblProdType = new System.Windows.Forms.Label();
            this.txtProdType = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblMfgNo = new System.Windows.Forms.Label();
            this.txtMfgNo = new System.Windows.Forms.TextBox();
            this.pnlTestParameters = new System.Windows.Forms.FlowLayoutPanel();
            this.grpProductInfo = new System.Windows.Forms.GroupBox();
            this.btnColorCalibration = new System.Windows.Forms.Button();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.txtTestStatus = new System.Windows.Forms.TextBox();
            this.lblTestStatus = new System.Windows.Forms.Label();
            this.prgTestProgress = new System.Windows.Forms.ProgressBar();
            this.lblNetworkStatus = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.lblMachineId = new System.Windows.Forms.Label();
            this.lblLineId = new System.Windows.Forms.Label();
            this.lblSoftwareRev = new System.Windows.Forms.Label();
            this.grpProductInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPassFail
            // 
            this.lblPassFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPassFail.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPassFail.Location = new System.Drawing.Point(50, 50);
            this.lblPassFail.Name = "lblPassFail";
            this.lblPassFail.Size = new System.Drawing.Size(500, 150);
            this.lblPassFail.TabIndex = 0;
            this.lblPassFail.Text = "PASS";
            this.lblPassFail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblOperator.Location = new System.Drawing.Point(20, 30);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(115, 30);
            this.lblOperator.TabIndex = 1;
            this.lblOperator.Text = "Operator:";
            // 
            // txtOperator
            // 
            this.txtOperator.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOperator.Location = new System.Drawing.Point(150, 27);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(200, 36);
            this.txtOperator.TabIndex = 2;
            // 
            // lblTimeDate
            // 
            this.lblTimeDate.AutoSize = true;
            this.lblTimeDate.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTimeDate.Location = new System.Drawing.Point(20, 70);
            this.lblTimeDate.Name = "lblTimeDate";
            this.lblTimeDate.Size = new System.Drawing.Size(129, 30);
            this.lblTimeDate.TabIndex = 3;
            this.lblTimeDate.Text = "Time/Date:";
            // 
            // txtTimeDate
            // 
            this.txtTimeDate.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTimeDate.Location = new System.Drawing.Point(150, 67);
            this.txtTimeDate.Name = "txtTimeDate";
            this.txtTimeDate.ReadOnly = true;
            this.txtTimeDate.Size = new System.Drawing.Size(200, 36);
            this.txtTimeDate.TabIndex = 4;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCount.Location = new System.Drawing.Point(20, 110);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(81, 30);
            this.lblCount.TabIndex = 5;
            this.lblCount.Text = "Count:";
            // 
            // txtCount
            // 
            this.txtCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCount.Location = new System.Drawing.Point(150, 107);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(200, 36);
            this.txtCount.TabIndex = 6;
            // 
            // lblSerialNo
            // 
            this.lblSerialNo.AutoSize = true;
            this.lblSerialNo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSerialNo.Location = new System.Drawing.Point(20, 150);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(112, 30);
            this.lblSerialNo.TabIndex = 7;
            this.lblSerialNo.Text = "Serial No:";
            // 
            // txtSerialNo
            // 
            this.txtSerialNo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSerialNo.Location = new System.Drawing.Point(150, 147);
            this.txtSerialNo.Name = "txtSerialNo";
            this.txtSerialNo.Size = new System.Drawing.Size(200, 36);
            this.txtSerialNo.TabIndex = 8;
            // 
            // lblAcDcTest
            // 
            this.lblAcDcTest.AutoSize = true;
            this.lblAcDcTest.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAcDcTest.Location = new System.Drawing.Point(380, 30);
            this.lblAcDcTest.Name = "lblAcDcTest";
            this.lblAcDcTest.Size = new System.Drawing.Size(136, 30);
            this.lblAcDcTest.TabIndex = 9;
            this.lblAcDcTest.Text = "AC/DC Test:";
            // 
            // txtAcDcTest
            // 
            this.txtAcDcTest.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAcDcTest.Location = new System.Drawing.Point(520, 27);
            this.txtAcDcTest.Name = "txtAcDcTest";
            this.txtAcDcTest.Size = new System.Drawing.Size(200, 36);
            this.txtAcDcTest.TabIndex = 10;
            // 
            // lblTestCode
            // 
            this.lblTestCode.AutoSize = true;
            this.lblTestCode.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTestCode.Location = new System.Drawing.Point(380, 70);
            this.lblTestCode.Name = "lblTestCode";
            this.lblTestCode.Size = new System.Drawing.Size(120, 30);
            this.lblTestCode.TabIndex = 11;
            this.lblTestCode.Text = "Test Code:";
            // 
            // txtTestCode
            // 
            this.txtTestCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtTestCode.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTestCode.Location = new System.Drawing.Point(520, 67);
            this.txtTestCode.Name = "txtTestCode";
            this.txtTestCode.Size = new System.Drawing.Size(200, 38);
            this.txtTestCode.TabIndex = 12;
            this.txtTestCode.SelectedIndexChanged += new System.EventHandler(this.txtTestCode_SelectedIndexChanged);
            // 
            // lblCatNo
            // 
            this.lblCatNo.AutoSize = true;
            this.lblCatNo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCatNo.Location = new System.Drawing.Point(380, 110);
            this.lblCatNo.Name = "lblCatNo";
            this.lblCatNo.Size = new System.Drawing.Size(94, 30);
            this.lblCatNo.TabIndex = 13;
            this.lblCatNo.Text = "Cat NO:";
            // 
            // txtCatNo
            // 
            this.txtCatNo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCatNo.Location = new System.Drawing.Point(520, 107);
            this.txtCatNo.Name = "txtCatNo";
            this.txtCatNo.Size = new System.Drawing.Size(200, 36);
            this.txtCatNo.TabIndex = 14;
            // 
            // lblProdType
            // 
            this.lblProdType.AutoSize = true;
            this.lblProdType.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProdType.Location = new System.Drawing.Point(380, 150);
            this.lblProdType.Name = "lblProdType";
            this.lblProdType.Size = new System.Drawing.Size(125, 30);
            this.lblProdType.TabIndex = 15;
            this.lblProdType.Text = "Prod Type:";
            // 
            // txtProdType
            // 
            this.txtProdType.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtProdType.Location = new System.Drawing.Point(520, 147);
            this.txtProdType.Name = "txtProdType";
            this.txtProdType.Size = new System.Drawing.Size(200, 36);
            this.txtProdType.TabIndex = 16;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblDescription.Location = new System.Drawing.Point(20, 190);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(138, 30);
            this.lblDescription.TabIndex = 17;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDescription.Location = new System.Drawing.Point(150, 187);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(200, 36);
            this.txtDescription.TabIndex = 18;
            // 
            // lblMfgNo
            // 
            this.lblMfgNo.AutoSize = true;
            this.lblMfgNo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMfgNo.Location = new System.Drawing.Point(380, 190);
            this.lblMfgNo.Name = "lblMfgNo";
            this.lblMfgNo.Size = new System.Drawing.Size(103, 30);
            this.lblMfgNo.TabIndex = 19;
            this.lblMfgNo.Text = "MFG No:";
            // 
            // txtMfgNo
            // 
            this.txtMfgNo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMfgNo.Location = new System.Drawing.Point(520, 187);
            this.txtMfgNo.Name = "txtMfgNo";
            this.txtMfgNo.Size = new System.Drawing.Size(200, 36);
            this.txtMfgNo.TabIndex = 20;
            // 
            // pnlTestParameters
            // 
            this.pnlTestParameters.AutoScroll = true;
            this.pnlTestParameters.BackColor = System.Drawing.Color.White;
            this.pnlTestParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTestParameters.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.pnlTestParameters.Location = new System.Drawing.Point(50, 320);
            this.pnlTestParameters.Name = "pnlTestParameters";
            this.pnlTestParameters.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTestParameters.Size = new System.Drawing.Size(1900, 1300);
            this.pnlTestParameters.TabIndex = 21;
            this.pnlTestParameters.WrapContents = true;
            // 
            // grpProductInfo
            // 
            this.grpProductInfo.Controls.Add(this.txtMfgNo);
            this.grpProductInfo.Controls.Add(this.lblMfgNo);
            this.grpProductInfo.Controls.Add(this.txtDescription);
            this.grpProductInfo.Controls.Add(this.lblDescription);
            this.grpProductInfo.Controls.Add(this.txtProdType);
            this.grpProductInfo.Controls.Add(this.lblProdType);
            this.grpProductInfo.Controls.Add(this.txtCatNo);
            this.grpProductInfo.Controls.Add(this.lblCatNo);
            this.grpProductInfo.Controls.Add(this.txtTestCode);
            this.grpProductInfo.Controls.Add(this.lblTestCode);
            this.grpProductInfo.Controls.Add(this.txtAcDcTest);
            this.grpProductInfo.Controls.Add(this.lblAcDcTest);
            this.grpProductInfo.Controls.Add(this.txtSerialNo);
            this.grpProductInfo.Controls.Add(this.lblSerialNo);
            this.grpProductInfo.Controls.Add(this.txtCount);
            this.grpProductInfo.Controls.Add(this.lblCount);
            this.grpProductInfo.Controls.Add(this.txtTimeDate);
            this.grpProductInfo.Controls.Add(this.lblTimeDate);
            this.grpProductInfo.Controls.Add(this.txtOperator);
            this.grpProductInfo.Controls.Add(this.lblOperator);
            this.grpProductInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpProductInfo.Location = new System.Drawing.Point(600, 50);
            this.grpProductInfo.Name = "grpProductInfo";
            this.grpProductInfo.Size = new System.Drawing.Size(750, 250);
            this.grpProductInfo.TabIndex = 22;
            this.grpProductInfo.TabStop = false;
            this.grpProductInfo.Text = "Product Information";
            // 
            // btnColorCalibration
            // 
            this.btnColorCalibration.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnColorCalibration.Location = new System.Drawing.Point(1400, 50);
            this.btnColorCalibration.Name = "btnColorCalibration";
            this.btnColorCalibration.Size = new System.Drawing.Size(150, 60);
            this.btnColorCalibration.TabIndex = 23;
            this.btnColorCalibration.Text = "Color Calibration";
            this.btnColorCalibration.UseVisualStyleBackColor = true;
            this.btnColorCalibration.Click += new System.EventHandler(this.btnColorCalibration_Click);
            // 
            // btnStartTest
            // 
            this.btnStartTest.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartTest.Location = new System.Drawing.Point(1570, 50);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(150, 60);
            this.btnStartTest.TabIndex = 24;
            this.btnStartTest.Text = "Start Test";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // lblTestStatus
            // 
            this.lblTestStatus.AutoSize = true;
            this.lblTestStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTestStatus.Location = new System.Drawing.Point(1400, 140);
            this.lblTestStatus.Name = "lblTestStatus";
            this.lblTestStatus.Size = new System.Drawing.Size(92, 21);
            this.lblTestStatus.TabIndex = 25;
            this.lblTestStatus.Text = "Test Status:";
            // 
            // txtTestStatus
            // 
            this.txtTestStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtTestStatus.Location = new System.Drawing.Point(1400, 170);
            this.txtTestStatus.Multiline = true;
            this.txtTestStatus.Name = "txtTestStatus";
            this.txtTestStatus.ReadOnly = true;
            this.txtTestStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTestStatus.Size = new System.Drawing.Size(320, 100);
            this.txtTestStatus.TabIndex = 26;
            // 
            // prgTestProgress
            // 
            this.prgTestProgress.Location = new System.Drawing.Point(1400, 280);
            this.prgTestProgress.Name = "prgTestProgress";
            this.prgTestProgress.Size = new System.Drawing.Size(320, 20);
            this.prgTestProgress.TabIndex = 27;
            // 
            // lblNetworkStatus
            // 
            this.lblNetworkStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNetworkStatus.Location = new System.Drawing.Point(50, 220);
            this.lblNetworkStatus.Name = "lblNetworkStatus";
            this.lblNetworkStatus.Size = new System.Drawing.Size(200, 25);
            this.lblNetworkStatus.TabIndex = 28;
            this.lblNetworkStatus.Text = "Network: ACTIVE";
            this.lblNetworkStatus.ForeColor = System.Drawing.Color.Green;
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIpAddress.Location = new System.Drawing.Point(270, 220);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(200, 25);
            this.lblIpAddress.TabIndex = 29;
            this.lblIpAddress.Text = "IP: 192.168.1.100";
            // 
            // lblMachineId
            // 
            this.lblMachineId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMachineId.Location = new System.Drawing.Point(50, 250);
            this.lblMachineId.Name = "lblMachineId";
            this.lblMachineId.Size = new System.Drawing.Size(200, 25);
            this.lblMachineId.TabIndex = 30;
            this.lblMachineId.Text = "Machine ID: PC001";
            // 
            // lblLineId
            // 
            this.lblLineId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLineId.Location = new System.Drawing.Point(270, 250);
            this.lblLineId.Name = "lblLineId";
            this.lblLineId.Size = new System.Drawing.Size(200, 25);
            this.lblLineId.TabIndex = 31;
            this.lblLineId.Text = "Line ID: Line001";
            // 
            // lblSoftwareRev
            // 
            this.lblSoftwareRev.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSoftwareRev.Location = new System.Drawing.Point(50, 280);
            this.lblSoftwareRev.Name = "lblSoftwareRev";
            this.lblSoftwareRev.Size = new System.Drawing.Size(300, 25);
            this.lblSoftwareRev.TabIndex = 32;
            this.lblSoftwareRev.Text = "Software Rev: 1.0.0";
            // 
            // TestResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2000, 1700);
            this.Controls.Add(this.lblSoftwareRev);
            this.Controls.Add(this.lblLineId);
            this.Controls.Add(this.lblMachineId);
            this.Controls.Add(this.lblIpAddress);
            this.Controls.Add(this.lblNetworkStatus);
            this.Controls.Add(this.prgTestProgress);
            this.Controls.Add(this.txtTestStatus);
            this.Controls.Add(this.lblTestStatus);
            this.Controls.Add(this.btnStartTest);
            this.Controls.Add(this.btnColorCalibration);
            this.Controls.Add(this.grpProductInfo);
            this.Controls.Add(this.lblPassFail);
            this.Controls.Add(this.pnlTestParameters);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "TestResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Results - Operator Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.grpProductInfo.ResumeLayout(false);
            this.grpProductInfo.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}