namespace HendersonvilleTrafficTest.Forms
{
    partial class ConfigurationForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbProperties;
        private Panel pnlEditor;
        private Label lblDescription;
        private Button btnSave;
        private Button btnCancel;
        private Label lblSelectProperty;

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
            this.cmbProperties = new ComboBox();
            this.pnlEditor = new Panel();
            this.lblDescription = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblSelectProperty = new Label();
            this.SuspendLayout();

            // 
            // lblSelectProperty
            // 
            this.lblSelectProperty.AutoSize = true;
            this.lblSelectProperty.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSelectProperty.Location = new Point(12, 15);
            this.lblSelectProperty.Name = "lblSelectProperty";
            this.lblSelectProperty.Size = new Size(156, 19);
            this.lblSelectProperty.TabIndex = 0;
            this.lblSelectProperty.Text = "Select Configuration:";

            // 
            // cmbProperties
            // 
            this.cmbProperties.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.cmbProperties.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbProperties.Font = new Font("Segoe UI", 10F);
            this.cmbProperties.FormattingEnabled = true;
            this.cmbProperties.Location = new Point(12, 40);
            this.cmbProperties.Name = "cmbProperties";
            this.cmbProperties.Size = new Size(560, 25);
            this.cmbProperties.TabIndex = 1;
            this.cmbProperties.SelectedIndexChanged += new EventHandler(this.cmbProperties_SelectedIndexChanged);

            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblDescription.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblDescription.ForeColor = Color.Gray;
            this.lblDescription.Location = new Point(12, 75);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(560, 40);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Select a configuration property to edit";

            // 
            // pnlEditor
            // 
            this.pnlEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlEditor.BorderStyle = BorderStyle.FixedSingle;
            this.pnlEditor.Location = new Point(12, 125);
            this.pnlEditor.Name = "pnlEditor";
            this.pnlEditor.Padding = new Padding(5);
            this.pnlEditor.Size = new Size(560, 200);
            this.pnlEditor.TabIndex = 3;

            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancel.Font = new Font("Segoe UI", 10F);
            this.btnCancel.Location = new Point(410, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(80, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // 
            // btnSave
            // 
            this.btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.Location = new Point(500, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(80, 35);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(584, 391);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlEditor);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.cmbProperties);
            this.Controls.Add(this.lblSelectProperty);
            this.Font = new Font("Segoe UI", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Application Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}