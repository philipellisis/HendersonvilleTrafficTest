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
            this.SuspendLayout();
            
            // 
            // btnConfiguration
            // 
            this.btnConfiguration.Font = new Font("Segoe UI", 10F);
            this.btnConfiguration.Location = new Point(12, 12);
            this.btnConfiguration.Name = "btnConfiguration";
            this.btnConfiguration.Size = new Size(120, 35);
            this.btnConfiguration.TabIndex = 0;
            this.btnConfiguration.Text = "Configuration";
            this.btnConfiguration.UseVisualStyleBackColor = true;
            this.btnConfiguration.Click += new EventHandler(this.btnConfiguration_Click);
            
            // 
            // btnTestRelayController
            // 
            this.btnTestRelayController.Font = new Font("Segoe UI", 10F);
            this.btnTestRelayController.Location = new Point(150, 12);
            this.btnTestRelayController.Name = "btnTestRelayController";
            this.btnTestRelayController.Size = new Size(150, 35);
            this.btnTestRelayController.TabIndex = 1;
            this.btnTestRelayController.Text = "Test Relay Controller";
            this.btnTestRelayController.UseVisualStyleBackColor = true;
            this.btnTestRelayController.Click += new EventHandler(this.btnTestRelayController_Click);
            
            // 
            // btnTestTemperatureSensor
            // 
            this.btnTestTemperatureSensor.Font = new Font("Segoe UI", 10F);
            this.btnTestTemperatureSensor.Location = new Point(320, 12);
            this.btnTestTemperatureSensor.Name = "btnTestTemperatureSensor";
            this.btnTestTemperatureSensor.Size = new Size(180, 35);
            this.btnTestTemperatureSensor.TabIndex = 2;
            this.btnTestTemperatureSensor.Text = "Test Temperature Sensor";
            this.btnTestTemperatureSensor.UseVisualStyleBackColor = true;
            this.btnTestTemperatureSensor.Click += new EventHandler(this.btnTestTemperatureSensor_Click);
            
            // 
            // btnTestPowerAnalyzer
            // 
            this.btnTestPowerAnalyzer.Font = new Font("Segoe UI", 10F);
            this.btnTestPowerAnalyzer.Location = new Point(520, 12);
            this.btnTestPowerAnalyzer.Name = "btnTestPowerAnalyzer";
            this.btnTestPowerAnalyzer.Size = new Size(160, 35);
            this.btnTestPowerAnalyzer.TabIndex = 3;
            this.btnTestPowerAnalyzer.Text = "Test Power Analyzer";
            this.btnTestPowerAnalyzer.UseVisualStyleBackColor = true;
            this.btnTestPowerAnalyzer.Click += new EventHandler(this.btnTestPowerAnalyzer_Click);
            
            // 
            // btnTestAcPowerSupply
            // 
            this.btnTestAcPowerSupply.Font = new Font("Segoe UI", 10F);
            this.btnTestAcPowerSupply.Location = new Point(700, 12);
            this.btnTestAcPowerSupply.Name = "btnTestAcPowerSupply";
            this.btnTestAcPowerSupply.Size = new Size(160, 35);
            this.btnTestAcPowerSupply.TabIndex = 4;
            this.btnTestAcPowerSupply.Text = "Test AC Power Supply";
            this.btnTestAcPowerSupply.UseVisualStyleBackColor = true;
            this.btnTestAcPowerSupply.Click += new EventHandler(this.btnTestAcPowerSupply_Click);
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(880, 450);
            this.Controls.Add(this.btnTestAcPowerSupply);
            this.Controls.Add(this.btnTestPowerAnalyzer);
            this.Controls.Add(this.btnTestTemperatureSensor);
            this.Controls.Add(this.btnTestRelayController);
            this.Controls.Add(this.btnConfiguration);
            this.Font = new Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.Text = "Hendersonville Traffic Test";
            this.ResumeLayout(false);
        }

        #endregion
    }
}