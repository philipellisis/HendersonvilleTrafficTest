namespace HendersonvilleTrafficTest.Forms
{
    partial class RelayControllerTestForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblConnectionStatus;
        private Panel pnlControls;
        private CheckBox chkAutoRead;
        private Button btnRefreshAll;
        private Button btnAllOn;
        private Button btnAllOff;
        private Button btnTestAll;
        private Button btnClose;


        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblConnectionStatus = new Label();
            this.pnlControls = new Panel();
            this.chkAutoRead = new CheckBox();
            this.btnRefreshAll = new Button();
            this.btnAllOn = new Button();
            this.btnAllOff = new Button();
            this.btnTestAll = new Button();
            this.btnClose = new Button();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(243, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Relay Controller Test Form";

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
            // pnlControls
            // 
            this.pnlControls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlControls.AutoScroll = true;
            this.pnlControls.BorderStyle = BorderStyle.FixedSingle;
            this.pnlControls.Location = new Point(12, 65);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new Size(480, 320);
            this.pnlControls.TabIndex = 2;

            // 
            // chkAutoRead
            // 
            this.chkAutoRead.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.chkAutoRead.AutoSize = true;
            this.chkAutoRead.Font = new Font("Segoe UI", 9F);
            this.chkAutoRead.Location = new Point(12, 395);
            this.chkAutoRead.Name = "chkAutoRead";
            this.chkAutoRead.Size = new Size(162, 19);
            this.chkAutoRead.TabIndex = 3;
            this.chkAutoRead.Text = "Auto-read ADC (1 second)";
            this.chkAutoRead.UseVisualStyleBackColor = true;
            this.chkAutoRead.CheckedChanged += new EventHandler(this.chkAutoRead_CheckedChanged);

            // 
            // btnRefreshAll
            // 
            this.btnRefreshAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnRefreshAll.Font = new Font("Segoe UI", 9F);
            this.btnRefreshAll.Location = new Point(12, 420);
            this.btnRefreshAll.Name = "btnRefreshAll";
            this.btnRefreshAll.Size = new Size(90, 30);
            this.btnRefreshAll.TabIndex = 4;
            this.btnRefreshAll.Text = "Refresh All";
            this.btnRefreshAll.UseVisualStyleBackColor = true;
            this.btnRefreshAll.Click += new EventHandler(this.btnRefreshAll_Click);

            // 
            // btnAllOn
            // 
            this.btnAllOn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnAllOn.BackColor = Color.LightGreen;
            this.btnAllOn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAllOn.Location = new Point(110, 420);
            this.btnAllOn.Name = "btnAllOn";
            this.btnAllOn.Size = new Size(70, 30);
            this.btnAllOn.TabIndex = 5;
            this.btnAllOn.Text = "All ON";
            this.btnAllOn.UseVisualStyleBackColor = false;
            this.btnAllOn.Click += new EventHandler(this.btnAllOn_Click);

            // 
            // btnAllOff
            // 
            this.btnAllOff.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnAllOff.BackColor = Color.LightCoral;
            this.btnAllOff.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAllOff.Location = new Point(190, 420);
            this.btnAllOff.Name = "btnAllOff";
            this.btnAllOff.Size = new Size(70, 30);
            this.btnAllOff.TabIndex = 6;
            this.btnAllOff.Text = "All OFF";
            this.btnAllOff.UseVisualStyleBackColor = false;
            this.btnAllOff.Click += new EventHandler(this.btnAllOff_Click);

            // 
            // btnTestAll
            // 
            this.btnTestAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnTestAll.BackColor = Color.LightBlue;
            this.btnTestAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnTestAll.Location = new Point(270, 420);
            this.btnTestAll.Name = "btnTestAll";
            this.btnTestAll.Size = new Size(80, 30);
            this.btnTestAll.TabIndex = 7;
            this.btnTestAll.Text = "Test All";
            this.btnTestAll.UseVisualStyleBackColor = false;
            this.btnTestAll.Click += new EventHandler(this.btnTestAll_Click);

            // 
            // btnClose
            // 
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.Font = new Font("Segoe UI", 9F);
            this.btnClose.Location = new Point(417, 420);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(75, 30);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler((s, e) => this.Close());

            // 
            // RelayControllerTestForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(504, 462);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnTestAll);
            this.Controls.Add(this.btnAllOff);
            this.Controls.Add(this.btnAllOn);
            this.Controls.Add(this.btnRefreshAll);
            this.Controls.Add(this.chkAutoRead);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.lblConnectionStatus);
            this.Controls.Add(this.lblTitle);
            this.Font = new Font("Segoe UI", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RelayControllerTestForm";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Relay Controller Test";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}