namespace SafeArrival.AdminTools.Presentation
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.parameterEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s3MonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infrastructureManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenBlueDeploymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsComboEnv = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsComboRegion = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsComboColor = new System.Windows.Forms.ToolStripComboBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parameterEditToolStripMenuItem,
            this.s3MonitorToolStripMenuItem,
            this.systemManagerToolStripMenuItem,
            this.infrastructureManagerToolStripMenuItem,
            this.greenBlueDeploymentToolStripMenuItem,
            this.viewLogsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1119, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // parameterEditToolStripMenuItem
            // 
            this.parameterEditToolStripMenuItem.Name = "parameterEditToolStripMenuItem";
            this.parameterEditToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.parameterEditToolStripMenuItem.Tag = "FormParametersEditor";
            this.parameterEditToolStripMenuItem.Text = "Edit Parameters";
            this.parameterEditToolStripMenuItem.Click += new System.EventHandler(this.OpenFormMenu_Click);
            // 
            // s3MonitorToolStripMenuItem
            // 
            this.s3MonitorToolStripMenuItem.Name = "s3MonitorToolStripMenuItem";
            this.s3MonitorToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.s3MonitorToolStripMenuItem.Tag = "FormS3Monitor";
            this.s3MonitorToolStripMenuItem.Text = "S3 Monitor";
            this.s3MonitorToolStripMenuItem.Click += new System.EventHandler(this.OpenFormMenu_Click);
            // 
            // systemManagerToolStripMenuItem
            // 
            this.systemManagerToolStripMenuItem.Name = "systemManagerToolStripMenuItem";
            this.systemManagerToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.systemManagerToolStripMenuItem.Tag = "FormSystemManager";
            this.systemManagerToolStripMenuItem.Text = "System Manager";
            this.systemManagerToolStripMenuItem.Click += new System.EventHandler(this.OpenFormMenu_Click);
            // 
            // infrastructureManagerToolStripMenuItem
            // 
            this.infrastructureManagerToolStripMenuItem.Name = "infrastructureManagerToolStripMenuItem";
            this.infrastructureManagerToolStripMenuItem.Size = new System.Drawing.Size(140, 20);
            this.infrastructureManagerToolStripMenuItem.Tag = "FormInfraManager";
            this.infrastructureManagerToolStripMenuItem.Text = "Infrastructure Manager";
            this.infrastructureManagerToolStripMenuItem.Click += new System.EventHandler(this.OpenFormMenu_Click);
            // 
            // greenBlueDeploymentToolStripMenuItem
            // 
            this.greenBlueDeploymentToolStripMenuItem.Name = "greenBlueDeploymentToolStripMenuItem";
            this.greenBlueDeploymentToolStripMenuItem.Size = new System.Drawing.Size(146, 20);
            this.greenBlueDeploymentToolStripMenuItem.Tag = "FormDeploymentSwitch";
            this.greenBlueDeploymentToolStripMenuItem.Text = "Green/Blue Deployment";
            this.greenBlueDeploymentToolStripMenuItem.Click += new System.EventHandler(this.OpenFormMenu_Click);
            // 
            // viewLogsToolStripMenuItem
            // 
            this.viewLogsToolStripMenuItem.Name = "viewLogsToolStripMenuItem";
            this.viewLogsToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.viewLogsToolStripMenuItem.Tag = "FormLogs";
            this.viewLogsToolStripMenuItem.Text = "View Logs";
            this.viewLogsToolStripMenuItem.Click += new System.EventHandler(this.OpenFormMenu_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 704);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1119, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel1.Tag = "   ";
            this.toolStripStatusLabel1.Text = "     ";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsComboEnv,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.tsComboRegion,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.tsComboColor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1119, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(81, 22);
            this.toolStripLabel1.Text = "Environment: ";
            // 
            // tsComboEnv
            // 
            this.tsComboEnv.Name = "tsComboEnv";
            this.tsComboEnv.Size = new System.Drawing.Size(110, 25);
            this.tsComboEnv.SelectedIndexChanged += new System.EventHandler(this.tsComboEnv_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(50, 22);
            this.toolStripLabel2.Text = "Region: ";
            // 
            // tsComboRegion
            // 
            this.tsComboRegion.Name = "tsComboRegion";
            this.tsComboRegion.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel3.Text = "Color: ";
            // 
            // tsComboColor
            // 
            this.tsComboColor.Name = "tsComboColor";
            this.tsComboColor.Size = new System.Drawing.Size(75, 25);
            this.tsComboColor.SelectedIndexChanged += new System.EventHandler(this.tsComboColor_SelectedIndexChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 726);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "SafeArrival Admin Tools";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MdiChildActivate += new System.EventHandler(this.FrmMain_MdiChildActivate);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem parameterEditToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tsComboEnv;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tsComboRegion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox tsComboColor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem s3MonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenBlueDeploymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infrastructureManagerToolStripMenuItem;
    }
}

