namespace HendersonvilleTrafficTest.Forms
{
    partial class SpectrometerTestForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblConnectionStatus;
        private Label lblWavelengthRange;
        private PictureBox picSpectrum;
        private GroupBox grpSpectrumControls;
        private Button btnConnect;
        private Button btnCapture;
        private Button btnAutoRange;
        private Button btnSaveSpectrum;
        private CheckBox chkAutoCapture;
        private GroupBox grpSpectrumData;
        private Label lblPeakWavelength;
        private Label lblPeakIntensity;
        private Label lblTotalIntensity;
        private Label lblCieCoordinates;
        private TextBox txtSpectrumData;
        private Button btnClose;
        private System.Windows.Forms.Timer autoTimer;
        private Label lblIntegrationTime;
        private TextBox txtIntegrationTime;
        private Button btnSetIntegrationTime;

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblConnectionStatus = new Label();
            this.lblWavelengthRange = new Label();
            this.picSpectrum = new PictureBox();
            this.grpSpectrumControls = new GroupBox();
            this.btnConnect = new Button();
            this.btnCapture = new Button();
            this.btnAutoRange = new Button();
            this.btnSaveSpectrum = new Button();
            this.chkAutoCapture = new CheckBox();
            this.grpSpectrumData = new GroupBox();
            this.lblPeakWavelength = new Label();
            this.lblPeakIntensity = new Label();
            this.lblTotalIntensity = new Label();
            this.lblCieCoordinates = new Label();
            this.txtSpectrumData = new TextBox();
            this.btnClose = new Button();
            this.autoTimer = new System.Windows.Forms.Timer();
            this.lblIntegrationTime = new Label();
            this.txtIntegrationTime = new TextBox();
            this.btnSetIntegrationTime = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.picSpectrum)).BeginInit();
            this.grpSpectrumControls.SuspendLayout();
            this.grpSpectrumData.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(202, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Spectrometer Test Form";

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
            // lblWavelengthRange
            // 
            this.lblWavelengthRange.AutoSize = true;
            this.lblWavelengthRange.Font = new Font("Segoe UI", 9F);
            this.lblWavelengthRange.Location = new Point(12, 65);
            this.lblWavelengthRange.Name = "lblWavelengthRange";
            this.lblWavelengthRange.Size = new Size(100, 15);
            this.lblWavelengthRange.TabIndex = 2;
            this.lblWavelengthRange.Text = "Range: -- nm";

            // 
            // picSpectrum
            // 
            this.picSpectrum.BackColor = Color.White;
            this.picSpectrum.BorderStyle = BorderStyle.FixedSingle;
            this.picSpectrum.Location = new Point(12, 90);
            this.picSpectrum.Name = "picSpectrum";
            this.picSpectrum.Size = new Size(760, 350);
            this.picSpectrum.TabIndex = 3;
            this.picSpectrum.TabStop = false;
            this.picSpectrum.Paint += new PaintEventHandler(this.picSpectrum_Paint);

            // 
            // grpSpectrumControls
            // 
            this.grpSpectrumControls.Controls.Add(this.btnSetIntegrationTime);
            this.grpSpectrumControls.Controls.Add(this.txtIntegrationTime);
            this.grpSpectrumControls.Controls.Add(this.lblIntegrationTime);
            this.grpSpectrumControls.Controls.Add(this.chkAutoCapture);
            this.grpSpectrumControls.Controls.Add(this.btnSaveSpectrum);
            this.grpSpectrumControls.Controls.Add(this.btnAutoRange);
            this.grpSpectrumControls.Controls.Add(this.btnCapture);
            this.grpSpectrumControls.Controls.Add(this.btnConnect);
            this.grpSpectrumControls.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpSpectrumControls.Location = new Point(12, 450);
            this.grpSpectrumControls.Name = "grpSpectrumControls";
            this.grpSpectrumControls.Size = new Size(760, 100);
            this.grpSpectrumControls.TabIndex = 4;
            this.grpSpectrumControls.TabStop = false;
            this.grpSpectrumControls.Text = "Controls";

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
            // btnCapture
            // 
            this.btnCapture.Enabled = false;
            this.btnCapture.Font = new Font("Segoe UI", 9F);
            this.btnCapture.Location = new Point(130, 25);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new Size(120, 35);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "Capture Spectrum";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new EventHandler(this.btnCapture_Click);

            // 
            // btnAutoRange
            // 
            this.btnAutoRange.Enabled = false;
            this.btnAutoRange.Font = new Font("Segoe UI", 9F);
            this.btnAutoRange.Location = new Point(270, 25);
            this.btnAutoRange.Name = "btnAutoRange";
            this.btnAutoRange.Size = new Size(100, 35);
            this.btnAutoRange.TabIndex = 2;
            this.btnAutoRange.Text = "Auto Range";
            this.btnAutoRange.UseVisualStyleBackColor = true;
            this.btnAutoRange.Click += new EventHandler(this.btnAutoRange_Click);

            // 
            // btnSaveSpectrum
            // 
            this.btnSaveSpectrum.Enabled = false;
            this.btnSaveSpectrum.Font = new Font("Segoe UI", 9F);
            this.btnSaveSpectrum.Location = new Point(390, 25);
            this.btnSaveSpectrum.Name = "btnSaveSpectrum";
            this.btnSaveSpectrum.Size = new Size(100, 35);
            this.btnSaveSpectrum.TabIndex = 3;
            this.btnSaveSpectrum.Text = "Save CSV";
            this.btnSaveSpectrum.UseVisualStyleBackColor = true;
            this.btnSaveSpectrum.Click += new EventHandler(this.btnSaveSpectrum_Click);

            // 
            // chkAutoCapture
            // 
            this.chkAutoCapture.AutoSize = true;
            this.chkAutoCapture.Enabled = false;
            this.chkAutoCapture.Font = new Font("Segoe UI", 9F);
            this.chkAutoCapture.Location = new Point(510, 30);
            this.chkAutoCapture.Name = "chkAutoCapture";
            this.chkAutoCapture.Size = new Size(180, 19);
            this.chkAutoCapture.TabIndex = 4;
            this.chkAutoCapture.Text = "Auto Capture (every 3 seconds)";
            this.chkAutoCapture.UseVisualStyleBackColor = true;
            this.chkAutoCapture.CheckedChanged += new EventHandler(this.chkAutoCapture_CheckedChanged);

            // 
            // grpSpectrumData
            // 
            this.grpSpectrumData.Controls.Add(this.txtSpectrumData);
            this.grpSpectrumData.Controls.Add(this.lblCieCoordinates);
            this.grpSpectrumData.Controls.Add(this.lblTotalIntensity);
            this.grpSpectrumData.Controls.Add(this.lblPeakIntensity);
            this.grpSpectrumData.Controls.Add(this.lblPeakWavelength);
            this.grpSpectrumData.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.grpSpectrumData.Location = new Point(12, 560);
            this.grpSpectrumData.Name = "grpSpectrumData";
            this.grpSpectrumData.Size = new Size(760, 140);
            this.grpSpectrumData.TabIndex = 5;
            this.grpSpectrumData.TabStop = false;
            this.grpSpectrumData.Text = "Spectrum Analysis";

            // 
            // lblPeakWavelength
            // 
            this.lblPeakWavelength.AutoSize = true;
            this.lblPeakWavelength.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblPeakWavelength.Location = new Point(15, 25);
            this.lblPeakWavelength.Name = "lblPeakWavelength";
            this.lblPeakWavelength.Size = new Size(75, 19);
            this.lblPeakWavelength.TabIndex = 0;
            this.lblPeakWavelength.Text = "Peak: -- nm";
            this.lblPeakWavelength.ForeColor = Color.DarkBlue;

            // 
            // lblPeakIntensity
            // 
            this.lblPeakIntensity.AutoSize = true;
            this.lblPeakIntensity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblPeakIntensity.Location = new Point(200, 25);
            this.lblPeakIntensity.Name = "lblPeakIntensity";
            this.lblPeakIntensity.Size = new Size(120, 19);
            this.lblPeakIntensity.TabIndex = 1;
            this.lblPeakIntensity.Text = "Peak Intensity: --";
            this.lblPeakIntensity.ForeColor = Color.DarkGreen;

            // 
            // lblTotalIntensity
            // 
            this.lblTotalIntensity.AutoSize = true;
            this.lblTotalIntensity.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalIntensity.Location = new Point(400, 25);
            this.lblTotalIntensity.Name = "lblTotalIntensity";
            this.lblTotalIntensity.Size = new Size(65, 19);
            this.lblTotalIntensity.TabIndex = 2;
            this.lblTotalIntensity.Text = "Total: --";
            this.lblTotalIntensity.ForeColor = Color.DarkRed;

            // 
            // lblCieCoordinates
            // 
            this.lblCieCoordinates.AutoSize = true;
            this.lblCieCoordinates.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblCieCoordinates.Location = new Point(550, 25);
            this.lblCieCoordinates.Name = "lblCieCoordinates";
            this.lblCieCoordinates.Size = new Size(180, 15);
            this.lblCieCoordinates.TabIndex = 3;
            this.lblCieCoordinates.Text = "CIE: x=-- y=-- u'=-- v'=--";
            this.lblCieCoordinates.ForeColor = Color.DarkMagenta;

            // 
            // txtSpectrumData
            // 
            this.txtSpectrumData.Font = new Font("Consolas", 9F);
            this.txtSpectrumData.Location = new Point(15, 55);
            this.txtSpectrumData.Multiline = true;
            this.txtSpectrumData.Name = "txtSpectrumData";
            this.txtSpectrumData.ReadOnly = true;
            this.txtSpectrumData.ScrollBars = ScrollBars.Vertical;
            this.txtSpectrumData.Size = new Size(730, 75);
            this.txtSpectrumData.TabIndex = 4;
            this.txtSpectrumData.Text = "No spectrum data captured yet...";

            // 
            // btnClose
            // 
            this.btnClose.Font = new Font("Segoe UI", 9F);
            this.btnClose.Location = new Point(697, 710);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(75, 30);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler((s, e) => this.Close());

            // 
            // autoTimer
            // 
            this.autoTimer.Interval = 3000;
            this.autoTimer.Tick += new EventHandler(this.autoTimer_Tick);

            // 
            // lblIntegrationTime
            // 
            this.lblIntegrationTime.AutoSize = true;
            this.lblIntegrationTime.Font = new Font("Segoe UI", 9F);
            this.lblIntegrationTime.Location = new Point(15, 70);
            this.lblIntegrationTime.Name = "lblIntegrationTime";
            this.lblIntegrationTime.Size = new Size(100, 15);
            this.lblIntegrationTime.TabIndex = 5;
            this.lblIntegrationTime.Text = "Integration Time (ms):";

            // 
            // txtIntegrationTime
            // 
            this.txtIntegrationTime.Enabled = false;
            this.txtIntegrationTime.Font = new Font("Segoe UI", 9F);
            this.txtIntegrationTime.Location = new Point(130, 67);
            this.txtIntegrationTime.Name = "txtIntegrationTime";
            this.txtIntegrationTime.Size = new Size(80, 23);
            this.txtIntegrationTime.TabIndex = 6;
            this.txtIntegrationTime.Text = "200";

            // 
            // btnSetIntegrationTime
            // 
            this.btnSetIntegrationTime.Enabled = false;
            this.btnSetIntegrationTime.Font = new Font("Segoe UI", 9F);
            this.btnSetIntegrationTime.Location = new Point(220, 65);
            this.btnSetIntegrationTime.Name = "btnSetIntegrationTime";
            this.btnSetIntegrationTime.Size = new Size(50, 27);
            this.btnSetIntegrationTime.TabIndex = 7;
            this.btnSetIntegrationTime.Text = "Set";
            this.btnSetIntegrationTime.UseVisualStyleBackColor = true;
            this.btnSetIntegrationTime.Click += new EventHandler(this.btnSetIntegrationTime_Click);

            // 
            // SpectrometerTestForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(784, 752);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpSpectrumData);
            this.Controls.Add(this.grpSpectrumControls);
            this.Controls.Add(this.picSpectrum);
            this.Controls.Add(this.lblWavelengthRange);
            this.Controls.Add(this.lblConnectionStatus);
            this.Controls.Add(this.lblTitle);
            this.Font = new Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpectrometerTestForm";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Spectrometer Test";
            ((System.ComponentModel.ISupportInitialize)(this.picSpectrum)).EndInit();
            this.grpSpectrumControls.ResumeLayout(false);
            this.grpSpectrumControls.PerformLayout();
            this.grpSpectrumData.ResumeLayout(false);
            this.grpSpectrumData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}