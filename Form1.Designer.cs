namespace HendersonvilleTrafficTest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Button btnConfiguration;
        private Button btnTestRelayController;
        private Button btnTestTemperatureSensor;
        private Button btnTestPowerAnalyzer;
        private Button btnTestAcPowerSupply;
        private Button btnTestSpectrometer;
        private Button btnSpectrometerCalibration;
        
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConfiguration = new Button();
            this.btnTestRelayController = new Button();
            this.btnTestTemperatureSensor = new Button();
            this.btnTestPowerAnalyzer = new Button();
            this.btnTestAcPowerSupply = new Button();
            this.btnTestSpectrometer = new Button();
            this.btnSpectrometerCalibration = new Button();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.SuspendLayout();
            
            // 
            // btnConfiguration
            // 
            this.btnConfiguration.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnConfiguration.Location = new Point(50, 20);
            this.btnConfiguration.Name = "btnConfiguration";
            this.btnConfiguration.Size = new Size(150, 50);
            this.btnConfiguration.TabIndex = 0;
            this.btnConfiguration.Text = "Configuration";
            this.btnConfiguration.UseVisualStyleBackColor = true;
            this.btnConfiguration.Click += new EventHandler(this.btnConfiguration_Click);
            
            // 
            // btnTestRelayController
            // 
            this.btnTestRelayController.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnTestRelayController.Location = new Point(220, 20);
            this.btnTestRelayController.Name = "btnTestRelayController";
            this.btnTestRelayController.Size = new Size(180, 50);
            this.btnTestRelayController.TabIndex = 1;
            this.btnTestRelayController.Text = "Test Relay Controller";
            this.btnTestRelayController.UseVisualStyleBackColor = true;
            this.btnTestRelayController.Click += new EventHandler(this.btnTestRelayController_Click);
            
            // 
            // btnTestTemperatureSensor
            // 
            this.btnTestTemperatureSensor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnTestTemperatureSensor.Location = new Point(420, 20);
            this.btnTestTemperatureSensor.Name = "btnTestTemperatureSensor";
            this.btnTestTemperatureSensor.Size = new Size(200, 50);
            this.btnTestTemperatureSensor.TabIndex = 2;
            this.btnTestTemperatureSensor.Text = "Test Temperature Sensor";
            this.btnTestTemperatureSensor.UseVisualStyleBackColor = true;
            this.btnTestTemperatureSensor.Click += new EventHandler(this.btnTestTemperatureSensor_Click);
            
            // 
            // btnTestPowerAnalyzer
            // 
            this.btnTestPowerAnalyzer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnTestPowerAnalyzer.Location = new Point(640, 20);
            this.btnTestPowerAnalyzer.Name = "btnTestPowerAnalyzer";
            this.btnTestPowerAnalyzer.Size = new Size(180, 50);
            this.btnTestPowerAnalyzer.TabIndex = 3;
            this.btnTestPowerAnalyzer.Text = "Test Power Analyzer";
            this.btnTestPowerAnalyzer.UseVisualStyleBackColor = true;
            this.btnTestPowerAnalyzer.Click += new EventHandler(this.btnTestPowerAnalyzer_Click);
            
            // 
            // btnTestAcPowerSupply
            // 
            this.btnTestAcPowerSupply.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnTestAcPowerSupply.Location = new Point(840, 20);
            this.btnTestAcPowerSupply.Name = "btnTestAcPowerSupply";
            this.btnTestAcPowerSupply.Size = new Size(180, 50);
            this.btnTestAcPowerSupply.TabIndex = 4;
            this.btnTestAcPowerSupply.Text = "Test AC Power Supply";
            this.btnTestAcPowerSupply.UseVisualStyleBackColor = true;
            this.btnTestAcPowerSupply.Click += new EventHandler(this.btnTestAcPowerSupply_Click);
            
            // 
            // btnTestSpectrometer
            // 
            this.btnTestSpectrometer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnTestSpectrometer.Location = new Point(1040, 20);
            this.btnTestSpectrometer.Name = "btnTestSpectrometer";
            this.btnTestSpectrometer.Size = new Size(180, 50);
            this.btnTestSpectrometer.TabIndex = 5;
            this.btnTestSpectrometer.Text = "Test Spectrometer";
            this.btnTestSpectrometer.UseVisualStyleBackColor = true;
            this.btnTestSpectrometer.Click += new EventHandler(this.btnTestSpectrometer_Click);
            
            // 
            // btnSpectrometerCalibration
            // 
            this.btnSpectrometerCalibration.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSpectrometerCalibration.Location = new Point(50, 90);
            this.btnSpectrometerCalibration.Name = "btnSpectrometerCalibration";
            this.btnSpectrometerCalibration.Size = new Size(200, 50);
            this.btnSpectrometerCalibration.TabIndex = 11;
            this.btnSpectrometerCalibration.Text = "Spectrometer Calibration";
            this.btnSpectrometerCalibration.UseVisualStyleBackColor = true;
            this.btnSpectrometerCalibration.Click += new EventHandler(this.btnSpectrometerCalibration_Click);
            
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblUsername.Location = new Point(400, 250);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new Size(135, 32);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Username:";
            
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new Font("Segoe UI", 18F);
            this.txtUsername.Location = new Point(560, 247);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(300, 39);
            this.txtUsername.TabIndex = 7;
            
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblPassword.Location = new Point(400, 320);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(127, 32);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password:";
            
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new Font("Segoe UI", 18F);
            this.txtPassword.Location = new Point(560, 317);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(300, 39);
            this.txtPassword.TabIndex = 9;
            
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.btnLogin.Location = new Point(580, 390);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(150, 60);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1280, 720);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.btnSpectrometerCalibration);
            this.Controls.Add(this.btnTestSpectrometer);
            this.Controls.Add(this.btnTestAcPowerSupply);
            this.Controls.Add(this.btnTestPowerAnalyzer);
            this.Controls.Add(this.btnTestTemperatureSensor);
            this.Controls.Add(this.btnTestRelayController);
            this.Controls.Add(this.btnConfiguration);
            this.Font = new Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hendersonville Traffic Test - Operator Dashboard";
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}