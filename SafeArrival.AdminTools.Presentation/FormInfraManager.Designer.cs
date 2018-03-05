namespace SafeArrival.AdminTools.Presentation
{
    partial class FormInfraManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInfraManager));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gvStacks = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StackStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StackId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StackName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSuspend = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gboxDnsSettings = new System.Windows.Forms.GroupBox();
            this.lblAdmin = new System.Windows.Forms.Label();
            this.lblDnsMode = new System.Windows.Forms.Label();
            this.btnSwitchDNS = new System.Windows.Forms.Button();
            this.gboxCICD = new System.Windows.Forms.GroupBox();
            this.lblCicdStatus = new System.Windows.Forms.Label();
            this.btnSwitchLeve3CICDMode = new System.Windows.Forms.Button();
            this.btnSwitchLeve2CICDMode = new System.Windows.Forms.Button();
            this.linkLabelTeamcity = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTcBuild = new System.Windows.Forms.Button();
            this.btnSetDns = new System.Windows.Forms.Button();
            this.btnCreateSisEvent = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cListBoxApps = new System.Windows.Forms.CheckedListBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.btnCreate = new System.Windows.Forms.Button();
            this.gvCodePiplines = new System.Windows.Forms.DataGridView();
            this.btnCmd = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblBranchName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gboxInfraDeployment = new System.Windows.Forms.GroupBox();
            this.CodePipelineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Created = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Updated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLiveColor = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvStacks)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.gboxDnsSettings.SuspendLayout();
            this.gboxCICD.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCodePiplines)).BeginInit();
            this.gboxInfraDeployment.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1036, 647);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnRefresh);
            this.tabPage1.Controls.Add(this.gvStacks);
            this.tabPage1.Controls.Add(this.btnSuspend);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1028, 621);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CloudFormation Stacks";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::SafeArrival.AdminTools.Presentation.Properties.Resources.Reload;
            this.btnRefresh.Location = new System.Drawing.Point(239, 582);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(43, 39);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gvStacks
            // 
            this.gvStacks.AllowUserToAddRows = false;
            this.gvStacks.AllowUserToDeleteRows = false;
            this.gvStacks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvStacks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvStacks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.DisplayName,
            this.CreationTime,
            this.StackStatus,
            this.Description,
            this.StackId,
            this.StackName});
            this.gvStacks.Location = new System.Drawing.Point(6, 6);
            this.gvStacks.Name = "gvStacks";
            this.gvStacks.Size = new System.Drawing.Size(1016, 575);
            this.gvStacks.TabIndex = 0;
            // 
            // Select
            // 
            this.Select.FillWeight = 30F;
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Select.Width = 30;
            // 
            // DisplayName
            // 
            this.DisplayName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DisplayName.DataPropertyName = "DisplayName";
            this.DisplayName.HeaderText = "Name";
            this.DisplayName.Name = "DisplayName";
            this.DisplayName.ReadOnly = true;
            this.DisplayName.Width = 200;
            // 
            // CreationTime
            // 
            this.CreationTime.DataPropertyName = "CreationTime";
            this.CreationTime.FillWeight = 50F;
            this.CreationTime.HeaderText = "Creation Time";
            this.CreationTime.Name = "CreationTime";
            this.CreationTime.ReadOnly = true;
            this.CreationTime.Width = 150;
            // 
            // StackStatus
            // 
            this.StackStatus.DataPropertyName = "StackStatus";
            this.StackStatus.HeaderText = "Status";
            this.StackStatus.Name = "StackStatus";
            this.StackStatus.ReadOnly = true;
            this.StackStatus.Width = 200;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // StackId
            // 
            this.StackId.DataPropertyName = "StackId";
            this.StackId.HeaderText = "";
            this.StackId.Name = "StackId";
            this.StackId.Visible = false;
            // 
            // StackName
            // 
            this.StackName.DataPropertyName = "StackName";
            this.StackName.HeaderText = "StackName";
            this.StackName.Name = "StackName";
            this.StackName.ReadOnly = true;
            this.StackName.Visible = false;
            // 
            // btnSuspend
            // 
            this.btnSuspend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSuspend.Location = new System.Drawing.Point(109, 591);
            this.btnSuspend.Name = "btnSuspend";
            this.btnSuspend.Size = new System.Drawing.Size(112, 23);
            this.btnSuspend.TabIndex = 2;
            this.btnSuspend.Text = "Suspend Deletion";
            this.btnSuspend.UseVisualStyleBackColor = true;
            this.btnSuspend.Click += new System.EventHandler(this.btnSuspend_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(5, 591);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete Stacks";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gboxInfraDeployment);
            this.tabPage2.Controls.Add(this.gboxDnsSettings);
            this.tabPage2.Controls.Add(this.gboxCICD);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1028, 621);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Code Pipeline";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gboxDnsSettings
            // 
            this.gboxDnsSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxDnsSettings.Controls.Add(this.lblAdmin);
            this.gboxDnsSettings.Controls.Add(this.lblDnsMode);
            this.gboxDnsSettings.Controls.Add(this.btnSwitchDNS);
            this.gboxDnsSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxDnsSettings.Location = new System.Drawing.Point(9, 350);
            this.gboxDnsSettings.Name = "gboxDnsSettings";
            this.gboxDnsSettings.Size = new System.Drawing.Size(1010, 113);
            this.gboxDnsSettings.TabIndex = 20;
            this.gboxDnsSettings.TabStop = false;
            this.gboxDnsSettings.Text = "DNS Settings";
            // 
            // lblAdmin
            // 
            this.lblAdmin.AutoSize = true;
            this.lblAdmin.Location = new System.Drawing.Point(13, 43);
            this.lblAdmin.Name = "lblAdmin";
            this.lblAdmin.Size = new System.Drawing.Size(13, 13);
            this.lblAdmin.TabIndex = 24;
            this.lblAdmin.Text = "  ";
            // 
            // lblDnsMode
            // 
            this.lblDnsMode.AutoSize = true;
            this.lblDnsMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDnsMode.Location = new System.Drawing.Point(13, 20);
            this.lblDnsMode.Name = "lblDnsMode";
            this.lblDnsMode.Size = new System.Drawing.Size(182, 13);
            this.lblDnsMode.TabIndex = 23;
            this.lblDnsMode.Text = "Current DNS Recordsets Value";
            // 
            // btnSwitchDNS
            // 
            this.btnSwitchDNS.Location = new System.Drawing.Point(9, 83);
            this.btnSwitchDNS.Name = "btnSwitchDNS";
            this.btnSwitchDNS.Size = new System.Drawing.Size(184, 23);
            this.btnSwitchDNS.TabIndex = 22;
            this.btnSwitchDNS.Text = "Set Regular/Maintenance DNS";
            this.btnSwitchDNS.UseVisualStyleBackColor = true;
            // 
            // gboxCICD
            // 
            this.gboxCICD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxCICD.Controls.Add(this.label3);
            this.gboxCICD.Controls.Add(this.lblLiveColor);
            this.gboxCICD.Controls.Add(this.label2);
            this.gboxCICD.Controls.Add(this.lblCicdStatus);
            this.gboxCICD.Controls.Add(this.btnSwitchLeve3CICDMode);
            this.gboxCICD.Controls.Add(this.btnSwitchLeve2CICDMode);
            this.gboxCICD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxCICD.Location = new System.Drawing.Point(9, 228);
            this.gboxCICD.Name = "gboxCICD";
            this.gboxCICD.Size = new System.Drawing.Size(1010, 107);
            this.gboxCICD.TabIndex = 19;
            this.gboxCICD.TabStop = false;
            this.gboxCICD.Text = "Enable/Disable CI/CD PipeLine";
            // 
            // lblCicdStatus
            // 
            this.lblCicdStatus.AutoSize = true;
            this.lblCicdStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCicdStatus.Location = new System.Drawing.Point(148, 41);
            this.lblCicdStatus.MaximumSize = new System.Drawing.Size(900, 0);
            this.lblCicdStatus.Name = "lblCicdStatus";
            this.lblCicdStatus.Size = new System.Drawing.Size(19, 13);
            this.lblCicdStatus.TabIndex = 22;
            this.lblCicdStatus.Text = "    ";
            // 
            // btnSwitchLeve3CICDMode
            // 
            this.btnSwitchLeve3CICDMode.Location = new System.Drawing.Point(162, 75);
            this.btnSwitchLeve3CICDMode.Name = "btnSwitchLeve3CICDMode";
            this.btnSwitchLeve3CICDMode.Size = new System.Drawing.Size(147, 23);
            this.btnSwitchLeve3CICDMode.TabIndex = 21;
            this.btnSwitchLeve3CICDMode.Text = "Switch Level 3 CICD Mode";
            this.btnSwitchLeve3CICDMode.UseVisualStyleBackColor = true;
            this.btnSwitchLeve3CICDMode.Click += new System.EventHandler(this.btnSwitchLeve3CICDMode_Click);
            // 
            // btnSwitchLeve2CICDMode
            // 
            this.btnSwitchLeve2CICDMode.Location = new System.Drawing.Point(9, 75);
            this.btnSwitchLeve2CICDMode.Name = "btnSwitchLeve2CICDMode";
            this.btnSwitchLeve2CICDMode.Size = new System.Drawing.Size(147, 23);
            this.btnSwitchLeve2CICDMode.TabIndex = 20;
            this.btnSwitchLeve2CICDMode.Text = "Switch Level 2 CICD Mode";
            this.btnSwitchLeve2CICDMode.UseVisualStyleBackColor = true;
            this.btnSwitchLeve2CICDMode.Click += new System.EventHandler(this.btnSwitchLeve2CICDMode_Click);
            // 
            // linkLabelTeamcity
            // 
            this.linkLabelTeamcity.AutoSize = true;
            this.linkLabelTeamcity.Location = new System.Drawing.Point(8, 56);
            this.linkLabelTeamcity.Name = "linkLabelTeamcity";
            this.linkLabelTeamcity.Size = new System.Drawing.Size(120, 13);
            this.linkLabelTeamcity.TabIndex = 18;
            this.linkLabelTeamcity.TabStop = true;
            this.linkLabelTeamcity.Text = "Open Teamcity Console";
            this.linkLabelTeamcity.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTeamcity_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnTcBuild);
            this.panel2.Controls.Add(this.btnSetDns);
            this.panel2.Controls.Add(this.btnCreateSisEvent);
            this.panel2.Location = new System.Drawing.Point(307, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(155, 121);
            this.panel2.TabIndex = 16;
            // 
            // btnTcBuild
            // 
            this.btnTcBuild.Location = new System.Drawing.Point(4, 61);
            this.btnTcBuild.Name = "btnTcBuild";
            this.btnTcBuild.Size = new System.Drawing.Size(147, 23);
            this.btnTcBuild.TabIndex = 18;
            this.btnTcBuild.Text = "Teamcity Build ZIP Files";
            this.btnTcBuild.UseVisualStyleBackColor = true;
            this.btnTcBuild.Click += new System.EventHandler(this.btnTcBuild_Click);
            // 
            // btnSetDns
            // 
            this.btnSetDns.Location = new System.Drawing.Point(3, 3);
            this.btnSetDns.Name = "btnSetDns";
            this.btnSetDns.Size = new System.Drawing.Size(147, 23);
            this.btnSetDns.TabIndex = 17;
            this.btnSetDns.Text = "Set Environment DNS";
            this.btnSetDns.UseVisualStyleBackColor = true;
            this.btnSetDns.Click += new System.EventHandler(this.btnSetDns_Click);
            // 
            // btnCreateSisEvent
            // 
            this.btnCreateSisEvent.Location = new System.Drawing.Point(3, 32);
            this.btnCreateSisEvent.Name = "btnCreateSisEvent";
            this.btnCreateSisEvent.Size = new System.Drawing.Size(147, 23);
            this.btnCreateSisEvent.TabIndex = 16;
            this.btnCreateSisEvent.Text = "Create SIS Event Trigger";
            this.btnCreateSisEvent.UseVisualStyleBackColor = true;
            this.btnCreateSisEvent.Click += new System.EventHandler(this.btnCreateSisEvent_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cListBoxApps);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.btnCreate);
            this.panel1.Location = new System.Drawing.Point(6, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 121);
            this.panel1.TabIndex = 15;
            // 
            // cListBoxApps
            // 
            this.cListBoxApps.FormattingEnabled = true;
            this.cListBoxApps.Items.AddRange(new object[] {
            "Admin",
            "Super",
            "API",
            "Worker",
            "Customer"});
            this.cListBoxApps.Location = new System.Drawing.Point(205, 3);
            this.cListBoxApps.Name = "cListBoxApps";
            this.cListBoxApps.Size = new System.Drawing.Size(82, 109);
            this.cListBoxApps.TabIndex = 15;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(4, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.Tag = "1";
            this.radioButton1.Text = "Level 1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(71, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Tag = "2";
            this.radioButton2.Text = "Level 2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(138, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(60, 17);
            this.radioButton3.TabIndex = 4;
            this.radioButton3.TabStop = true;
            this.radioButton3.Tag = "3";
            this.radioButton3.Text = "Level 3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(12, 89);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // gvCodePiplines
            // 
            this.gvCodePiplines.AllowUserToAddRows = false;
            this.gvCodePiplines.AllowUserToDeleteRows = false;
            this.gvCodePiplines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvCodePiplines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCodePiplines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodePipelineName,
            this.Created,
            this.Updated,
            this.Version});
            this.gvCodePiplines.Location = new System.Drawing.Point(469, 27);
            this.gvCodePiplines.Name = "gvCodePiplines";
            this.gvCodePiplines.ReadOnly = true;
            this.gvCodePiplines.Size = new System.Drawing.Size(535, 176);
            this.gvCodePiplines.TabIndex = 9;
            // 
            // btnCmd
            // 
            this.btnCmd.Image = ((System.Drawing.Image)(resources.GetObject("btnCmd.Image")));
            this.btnCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCmd.Location = new System.Drawing.Point(345, 26);
            this.btnCmd.Name = "btnCmd";
            this.btnCmd.Size = new System.Drawing.Size(117, 23);
            this.btnCmd.TabIndex = 6;
            this.btnCmd.Text = "Command Prompt";
            this.btnCmd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCmd.UseVisualStyleBackColor = true;
            this.btnCmd.Click += new System.EventHandler(this.btnCmd_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(242, 31);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(85, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Open in Explorer";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblBranchName
            // 
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.ForeColor = System.Drawing.Color.Red;
            this.lblBranchName.Location = new System.Drawing.Point(164, 31);
            this.lblBranchName.Name = "lblBranchName";
            this.lblBranchName.Size = new System.Drawing.Size(10, 13);
            this.lblBranchName.TabIndex = 1;
            this.lblBranchName.Text = " ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Git Local Branch:";
            // 
            // gboxInfraDeployment
            // 
            this.gboxInfraDeployment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxInfraDeployment.Controls.Add(this.label1);
            this.gboxInfraDeployment.Controls.Add(this.lblBranchName);
            this.gboxInfraDeployment.Controls.Add(this.linkLabel1);
            this.gboxInfraDeployment.Controls.Add(this.linkLabelTeamcity);
            this.gboxInfraDeployment.Controls.Add(this.btnCmd);
            this.gboxInfraDeployment.Controls.Add(this.panel2);
            this.gboxInfraDeployment.Controls.Add(this.gvCodePiplines);
            this.gboxInfraDeployment.Controls.Add(this.panel1);
            this.gboxInfraDeployment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxInfraDeployment.Location = new System.Drawing.Point(9, 6);
            this.gboxInfraDeployment.Name = "gboxInfraDeployment";
            this.gboxInfraDeployment.Size = new System.Drawing.Size(1010, 210);
            this.gboxInfraDeployment.TabIndex = 21;
            this.gboxInfraDeployment.TabStop = false;
            this.gboxInfraDeployment.Text = "Deploy Infrastructure";
            // 
            // CodePipelineName
            // 
            this.CodePipelineName.DataPropertyName = "Name";
            this.CodePipelineName.HeaderText = "Name";
            this.CodePipelineName.Name = "CodePipelineName";
            this.CodePipelineName.ReadOnly = true;
            this.CodePipelineName.Width = 200;
            // 
            // Created
            // 
            this.Created.DataPropertyName = "Created";
            this.Created.HeaderText = "Created";
            this.Created.Name = "Created";
            this.Created.ReadOnly = true;
            this.Created.Width = 140;
            // 
            // Updated
            // 
            this.Updated.DataPropertyName = "Updated";
            this.Updated.HeaderText = "Updated";
            this.Updated.Name = "Updated";
            this.Updated.ReadOnly = true;
            this.Updated.Width = 140;
            // 
            // Version
            // 
            this.Version.DataPropertyName = "Version";
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            this.Version.Width = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Current Live Color Environment:";
            // 
            // lblLiveColor
            // 
            this.lblLiveColor.AutoSize = true;
            this.lblLiveColor.Location = new System.Drawing.Point(207, 20);
            this.lblLiveColor.Name = "lblLiveColor";
            this.lblLiveColor.Size = new System.Drawing.Size(16, 13);
            this.lblLiveColor.TabIndex = 24;
            this.lblLiveColor.Text = "   ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Current CI/CD Status:";
            // 
            // FormInfraManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 652);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormInfraManager";
            this.Text = "Infrastructure Manager";
            this.Load += new System.EventHandler(this.FormInfraManager_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvStacks)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.gboxDnsSettings.ResumeLayout(false);
            this.gboxDnsSettings.PerformLayout();
            this.gboxCICD.ResumeLayout(false);
            this.gboxCICD.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCodePiplines)).EndInit();
            this.gboxInfraDeployment.ResumeLayout(false);
            this.gboxInfraDeployment.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gvStacks;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreationTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn StackStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn StackId;
        private System.Windows.Forms.DataGridViewTextBoxColumn StackName;
        private System.Windows.Forms.Button btnSuspend;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblBranchName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btnCmd;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.DataGridView gvCodePiplines;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckedListBox cListBoxApps;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCreateSisEvent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSetDns;
        private System.Windows.Forms.Button btnTcBuild;
        private System.Windows.Forms.LinkLabel linkLabelTeamcity;
        private System.Windows.Forms.GroupBox gboxDnsSettings;
        private System.Windows.Forms.GroupBox gboxCICD;
        private System.Windows.Forms.Label lblAdmin;
        private System.Windows.Forms.Label lblDnsMode;
        private System.Windows.Forms.Button btnSwitchDNS;
        private System.Windows.Forms.Button btnSwitchLeve3CICDMode;
        private System.Windows.Forms.Button btnSwitchLeve2CICDMode;
        private System.Windows.Forms.Label lblCicdStatus;
        private System.Windows.Forms.GroupBox gboxInfraDeployment;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodePipelineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Created;
        private System.Windows.Forms.DataGridViewTextBoxColumn Updated;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.Label lblLiveColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}