namespace HendersonvilleTrafficTest.Forms
{
    partial class ConfigAndTestForm
    {
        private System.ComponentModel.IContainer components = null;
        
        // Configuration Section
        private GroupBox grpConfiguration;
        private Button btnConfiguration;
        private Button btnManageUsers;
        
        // Equipment Testing Section
        private GroupBox grpEquipmentTesting;
        private Button btnTestRelayController;
        private Button btnTestTemperatureSensor;
        private Button btnTestPowerAnalyzer;
        private Button btnTestAcPowerSupply;
        private Button btnTestSpectrometer;
        
        // Calibration Section
        private GroupBox grpCalibration;
        private Button btnSpectrometerCalibration;
        private Button btnColorCalibration;

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
            this.grpConfiguration = new System.Windows.Forms.GroupBox();
            this.btnConfiguration = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.grpEquipmentTesting = new System.Windows.Forms.GroupBox();
            this.btnTestRelayController = new System.Windows.Forms.Button();
            this.btnTestTemperatureSensor = new System.Windows.Forms.Button();
            this.btnTestPowerAnalyzer = new System.Windows.Forms.Button();
            this.btnTestAcPowerSupply = new System.Windows.Forms.Button();
            this.btnTestSpectrometer = new System.Windows.Forms.Button();
            this.grpCalibration = new System.Windows.Forms.GroupBox();
            this.btnSpectrometerCalibration = new System.Windows.Forms.Button();
            this.btnColorCalibration = new System.Windows.Forms.Button();
            this.grpConfiguration.SuspendLayout();
            this.grpEquipmentTesting.SuspendLayout();
            this.grpCalibration.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConfiguration
            // 
            this.grpConfiguration.Controls.Add(this.btnManageUsers);
            this.grpConfiguration.Controls.Add(this.btnConfiguration);
            this.grpConfiguration.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpConfiguration.Location = new System.Drawing.Point(30, 30);
            this.grpConfiguration.Name = "grpConfiguration";
            this.grpConfiguration.Size = new System.Drawing.Size(300, 200);
            this.grpConfiguration.TabIndex = 0;
            this.grpConfiguration.TabStop = false;
            this.grpConfiguration.Text = "Configuration";
            // 
            // btnConfiguration
            // 
            this.btnConfiguration.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnConfiguration.Location = new System.Drawing.Point(30, 40);
            this.btnConfiguration.Name = "btnConfiguration";
            this.btnConfiguration.Size = new System.Drawing.Size(240, 60);
            this.btnConfiguration.TabIndex = 0;
            this.btnConfiguration.Text = "System Configuration";
            this.btnConfiguration.UseVisualStyleBackColor = true;
            this.btnConfiguration.Click += new System.EventHandler(this.btnConfiguration_Click);
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnManageUsers.Location = new System.Drawing.Point(30, 120);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(240, 60);
            this.btnManageUsers.TabIndex = 1;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // grpEquipmentTesting
            // 
            this.grpEquipmentTesting.Controls.Add(this.btnTestSpectrometer);
            this.grpEquipmentTesting.Controls.Add(this.btnTestAcPowerSupply);
            this.grpEquipmentTesting.Controls.Add(this.btnTestPowerAnalyzer);
            this.grpEquipmentTesting.Controls.Add(this.btnTestTemperatureSensor);
            this.grpEquipmentTesting.Controls.Add(this.btnTestRelayController);
            this.grpEquipmentTesting.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpEquipmentTesting.Location = new System.Drawing.Point(360, 30);
            this.grpEquipmentTesting.Name = "grpEquipmentTesting";
            this.grpEquipmentTesting.Size = new System.Drawing.Size(800, 200);
            this.grpEquipmentTesting.TabIndex = 1;
            this.grpEquipmentTesting.TabStop = false;
            this.grpEquipmentTesting.Text = "Equipment Testing";
            // 
            // btnTestRelayController
            // 
            this.btnTestRelayController.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestRelayController.Location = new System.Drawing.Point(30, 40);
            this.btnTestRelayController.Name = "btnTestRelayController";
            this.btnTestRelayController.Size = new System.Drawing.Size(180, 60);
            this.btnTestRelayController.TabIndex = 0;
            this.btnTestRelayController.Text = "Test Relay Controller";
            this.btnTestRelayController.UseVisualStyleBackColor = true;
            this.btnTestRelayController.Click += new System.EventHandler(this.btnTestRelayController_Click);
            // 
            // btnTestTemperatureSensor
            // 
            this.btnTestTemperatureSensor.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestTemperatureSensor.Location = new System.Drawing.Point(230, 40);
            this.btnTestTemperatureSensor.Name = "btnTestTemperatureSensor";
            this.btnTestTemperatureSensor.Size = new System.Drawing.Size(180, 60);
            this.btnTestTemperatureSensor.TabIndex = 1;
            this.btnTestTemperatureSensor.Text = "Test Temperature Sensor";
            this.btnTestTemperatureSensor.UseVisualStyleBackColor = true;
            this.btnTestTemperatureSensor.Click += new System.EventHandler(this.btnTestTemperatureSensor_Click);
            // 
            // btnTestPowerAnalyzer
            // 
            this.btnTestPowerAnalyzer.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestPowerAnalyzer.Location = new System.Drawing.Point(430, 40);
            this.btnTestPowerAnalyzer.Name = "btnTestPowerAnalyzer";
            this.btnTestPowerAnalyzer.Size = new System.Drawing.Size(180, 60);
            this.btnTestPowerAnalyzer.TabIndex = 2;
            this.btnTestPowerAnalyzer.Text = "Test Power Analyzer";
            this.btnTestPowerAnalyzer.UseVisualStyleBackColor = true;
            this.btnTestPowerAnalyzer.Click += new System.EventHandler(this.btnTestPowerAnalyzer_Click);
            // 
            // btnTestAcPowerSupply
            // 
            this.btnTestAcPowerSupply.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestAcPowerSupply.Location = new System.Drawing.Point(130, 120);
            this.btnTestAcPowerSupply.Name = "btnTestAcPowerSupply";
            this.btnTestAcPowerSupply.Size = new System.Drawing.Size(180, 60);
            this.btnTestAcPowerSupply.TabIndex = 3;
            this.btnTestAcPowerSupply.Text = "Test AC Power Supply";
            this.btnTestAcPowerSupply.UseVisualStyleBackColor = true;
            this.btnTestAcPowerSupply.Click += new System.EventHandler(this.btnTestAcPowerSupply_Click);
            // 
            // btnTestSpectrometer
            // 
            this.btnTestSpectrometer.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestSpectrometer.Location = new System.Drawing.Point(330, 120);
            this.btnTestSpectrometer.Name = "btnTestSpectrometer";
            this.btnTestSpectrometer.Size = new System.Drawing.Size(180, 60);
            this.btnTestSpectrometer.TabIndex = 4;
            this.btnTestSpectrometer.Text = "Test Spectrometer";
            this.btnTestSpectrometer.UseVisualStyleBackColor = true;
            this.btnTestSpectrometer.Click += new System.EventHandler(this.btnTestSpectrometer_Click);
            // 
            // grpCalibration
            // 
            this.grpCalibration.Controls.Add(this.btnColorCalibration);
            this.grpCalibration.Controls.Add(this.btnSpectrometerCalibration);
            this.grpCalibration.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpCalibration.Location = new System.Drawing.Point(30, 250);
            this.grpCalibration.Name = "grpCalibration";
            this.grpCalibration.Size = new System.Drawing.Size(300, 200);
            this.grpCalibration.TabIndex = 2;
            this.grpCalibration.TabStop = false;
            this.grpCalibration.Text = "Calibration";
            // 
            // btnSpectrometerCalibration
            // 
            this.btnSpectrometerCalibration.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSpectrometerCalibration.Location = new System.Drawing.Point(30, 40);
            this.btnSpectrometerCalibration.Name = "btnSpectrometerCalibration";
            this.btnSpectrometerCalibration.Size = new System.Drawing.Size(240, 60);
            this.btnSpectrometerCalibration.TabIndex = 0;
            this.btnSpectrometerCalibration.Text = "Spectrometer Calibration";
            this.btnSpectrometerCalibration.UseVisualStyleBackColor = true;
            this.btnSpectrometerCalibration.Click += new System.EventHandler(this.btnSpectrometerCalibration_Click);
            // 
            // btnColorCalibration
            // 
            this.btnColorCalibration.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnColorCalibration.Location = new System.Drawing.Point(30, 120);
            this.btnColorCalibration.Name = "btnColorCalibration";
            this.btnColorCalibration.Size = new System.Drawing.Size(240, 60);
            this.btnColorCalibration.TabIndex = 1;
            this.btnColorCalibration.Text = "Color Calibration";
            this.btnColorCalibration.UseVisualStyleBackColor = true;
            this.btnColorCalibration.Click += new System.EventHandler(this.btnColorCalibration_Click);
            // 
            // ConfigAndTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 530);
            this.Controls.Add(this.grpCalibration);
            this.Controls.Add(this.grpEquipmentTesting);
            this.Controls.Add(this.grpConfiguration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConfigAndTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration and Testing Dashboard";
            this.grpConfiguration.ResumeLayout(false);
            this.grpEquipmentTesting.ResumeLayout(false);
            this.grpCalibration.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}