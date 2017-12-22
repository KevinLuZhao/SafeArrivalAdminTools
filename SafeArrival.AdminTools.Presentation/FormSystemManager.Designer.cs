namespace SafeArrival.AdminTools.Presentation
{
    partial class FormSystemManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSystemManager));
            this.btnInit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPageStatus = new System.Windows.Forms.TabPage();
            this.imgRdsStatus = new System.Windows.Forms.PictureBox();
            this.imgAppStatus = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboxRdsMutlAZ = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRdsIdentifier = new System.Windows.Forms.Label();
            this.lblRdsStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRdsArn = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnSave_ASG_Settings = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Environment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Application = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesiredCapacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tPagePeeringConnection = new System.Windows.Forms.TabPage();
            this.btnRpcRefresh = new System.Windows.Forms.Button();
            this.pnlNonExistRpc = new System.Windows.Forms.Panel();
            this.ddlAccepterVpc = new System.Windows.Forms.ComboBox();
            this.ddlRequesterVpc = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnRpcCreate = new System.Windows.Forms.Button();
            this.lblRpcNotExists = new System.Windows.Forms.Label();
            this.pnlExistRpc = new System.Windows.Forms.Panel();
            this.btnRpcDelete = new System.Windows.Forms.Button();
            this.lblRpcStatus = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblRpcAcceptVpc = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblRpcReuestVpc = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblRpcId = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tPageStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgRdsStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAppStatus)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tPagePeeringConnection.SuspendLayout();
            this.pnlNonExistRpc.SuspendLayout();
            this.pnlExistRpc.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(985, 538);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(114, 23);
            this.btnInit.TabIndex = 4;
            this.btnInit.Text = "Init DB Records";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPageStatus);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tPagePeeringConnection);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1113, 602);
            this.tabControl1.TabIndex = 3;
            // 
            // tPageStatus
            // 
            this.tPageStatus.Controls.Add(this.imgRdsStatus);
            this.tPageStatus.Controls.Add(this.imgAppStatus);
            this.tPageStatus.Controls.Add(this.btnStart);
            this.tPageStatus.Controls.Add(this.label4);
            this.tPageStatus.Controls.Add(this.btnStop);
            this.tPageStatus.Controls.Add(this.listView2);
            this.tPageStatus.Controls.Add(this.btnRefresh);
            this.tPageStatus.Controls.Add(this.panel1);
            this.tPageStatus.Controls.Add(this.label2);
            this.tPageStatus.Controls.Add(this.label1);
            this.tPageStatus.Controls.Add(this.listView1);
            this.tPageStatus.Location = new System.Drawing.Point(4, 22);
            this.tPageStatus.Name = "tPageStatus";
            this.tPageStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tPageStatus.Size = new System.Drawing.Size(1105, 576);
            this.tPageStatus.TabIndex = 0;
            this.tPageStatus.Text = "Status";
            this.tPageStatus.UseVisualStyleBackColor = true;
            // 
            // imgRdsStatus
            // 
            this.imgRdsStatus.Location = new System.Drawing.Point(58, 390);
            this.imgRdsStatus.Name = "imgRdsStatus";
            this.imgRdsStatus.Size = new System.Drawing.Size(17, 17);
            this.imgRdsStatus.TabIndex = 17;
            this.imgRdsStatus.TabStop = false;
            // 
            // imgAppStatus
            // 
            this.imgAppStatus.Location = new System.Drawing.Point(165, 12);
            this.imgAppStatus.Name = "imgAppStatus";
            this.imgAppStatus.Size = new System.Drawing.Size(17, 17);
            this.imgAppStatus.TabIndex = 16;
            this.imgAppStatus.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(116, 533);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start System";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Scheduled Actions";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(7, 533);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop System";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(7, 247);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(1083, 126);
            this.listView2.TabIndex = 14;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::SafeArrival.AdminTools.Presentation.Properties.Resources.Reload;
            this.btnRefresh.Location = new System.Drawing.Point(232, 523);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 42);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cboxRdsMutlAZ);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblRdsIdentifier);
            this.panel1.Controls.Add(this.lblRdsStatus);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblRdsArn);
            this.panel1.Location = new System.Drawing.Point(7, 419);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1083, 100);
            this.panel1.TabIndex = 12;
            // 
            // cboxRdsMutlAZ
            // 
            this.cboxRdsMutlAZ.AutoSize = true;
            this.cboxRdsMutlAZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxRdsMutlAZ.Location = new System.Drawing.Point(156, 64);
            this.cboxRdsMutlAZ.Name = "cboxRdsMutlAZ";
            this.cboxRdsMutlAZ.Size = new System.Drawing.Size(73, 17);
            this.cboxRdsMutlAZ.TabIndex = 12;
            this.cboxRdsMutlAZ.Text = "Multi AZ";
            this.cboxRdsMutlAZ.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Status:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "DB Instance Identifier:";
            // 
            // lblRdsIdentifier
            // 
            this.lblRdsIdentifier.AutoSize = true;
            this.lblRdsIdentifier.Location = new System.Drawing.Point(158, 15);
            this.lblRdsIdentifier.Name = "lblRdsIdentifier";
            this.lblRdsIdentifier.Size = new System.Drawing.Size(13, 13);
            this.lblRdsIdentifier.TabIndex = 5;
            this.lblRdsIdentifier.Text = "  ";
            // 
            // lblRdsStatus
            // 
            this.lblRdsStatus.AutoSize = true;
            this.lblRdsStatus.Location = new System.Drawing.Point(67, 66);
            this.lblRdsStatus.Name = "lblRdsStatus";
            this.lblRdsStatus.Size = new System.Drawing.Size(13, 13);
            this.lblRdsStatus.TabIndex = 9;
            this.lblRdsStatus.Text = "  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "DB Instance Arn:";
            // 
            // lblRdsArn
            // 
            this.lblRdsArn.AutoSize = true;
            this.lblRdsArn.Location = new System.Drawing.Point(128, 40);
            this.lblRdsArn.Name = "lblRdsArn";
            this.lblRdsArn.Size = new System.Drawing.Size(13, 13);
            this.lblRdsArn.TabIndex = 7;
            this.lblRdsArn.Text = "  ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 390);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "RDS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Auto Scaling Group";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(10, 35);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1083, 160);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnInit);
            this.tabPage2.Controls.Add(this.btnSave_ASG_Settings);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1105, 576);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Auto-Scaling Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSave_ASG_Settings
            // 
            this.btnSave_ASG_Settings.Location = new System.Drawing.Point(7, 538);
            this.btnSave_ASG_Settings.Name = "btnSave_ASG_Settings";
            this.btnSave_ASG_Settings.Size = new System.Drawing.Size(75, 23);
            this.btnSave_ASG_Settings.TabIndex = 1;
            this.btnSave_ASG_Settings.Text = "Save";
            this.btnSave_ASG_Settings.UseVisualStyleBackColor = true;
            this.btnSave_ASG_Settings.Click += new System.EventHandler(this.btnSave_ASG_Settings_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Environment,
            this.Application,
            this.MaxSize,
            this.MinSize,
            this.DesiredCapacity});
            this.dataGridView1.Location = new System.Drawing.Point(7, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1092, 515);
            this.dataGridView1.TabIndex = 0;
            // 
            // Environment
            // 
            this.Environment.DataPropertyName = "Environment";
            this.Environment.HeaderText = "Environment";
            this.Environment.Name = "Environment";
            this.Environment.ReadOnly = true;
            this.Environment.Width = 150;
            // 
            // Application
            // 
            this.Application.DataPropertyName = "Application";
            this.Application.HeaderText = "Application";
            this.Application.Name = "Application";
            this.Application.ReadOnly = true;
            // 
            // MaxSize
            // 
            this.MaxSize.DataPropertyName = "MaxSize";
            this.MaxSize.HeaderText = "Max Size";
            this.MaxSize.Name = "MaxSize";
            this.MaxSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MinSize
            // 
            this.MinSize.DataPropertyName = "MinSize";
            this.MinSize.HeaderText = "Min Size";
            this.MinSize.Name = "MinSize";
            // 
            // DesiredCapacity
            // 
            this.DesiredCapacity.DataPropertyName = "DesiredCapacity";
            this.DesiredCapacity.HeaderText = "Desired Capacity";
            this.DesiredCapacity.Name = "DesiredCapacity";
            this.DesiredCapacity.Width = 150;
            // 
            // tPagePeeringConnection
            // 
            this.tPagePeeringConnection.Controls.Add(this.btnRpcRefresh);
            this.tPagePeeringConnection.Controls.Add(this.pnlNonExistRpc);
            this.tPagePeeringConnection.Controls.Add(this.pnlExistRpc);
            this.tPagePeeringConnection.Controls.Add(this.label6);
            this.tPagePeeringConnection.Location = new System.Drawing.Point(4, 22);
            this.tPagePeeringConnection.Name = "tPagePeeringConnection";
            this.tPagePeeringConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tPagePeeringConnection.Size = new System.Drawing.Size(1105, 576);
            this.tPagePeeringConnection.TabIndex = 2;
            this.tPagePeeringConnection.Text = "VPC Peering Connection";
            this.tPagePeeringConnection.UseVisualStyleBackColor = true;
            // 
            // btnRpcRefresh
            // 
            this.btnRpcRefresh.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRpcRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRpcRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRpcRefresh.Image")));
            this.btnRpcRefresh.Location = new System.Drawing.Point(797, 6);
            this.btnRpcRefresh.Name = "btnRpcRefresh";
            this.btnRpcRefresh.Size = new System.Drawing.Size(38, 40);
            this.btnRpcRefresh.TabIndex = 5;
            this.btnRpcRefresh.UseVisualStyleBackColor = true;
            this.btnRpcRefresh.Click += new System.EventHandler(this.btnRpcRefresh_Click);
            // 
            // pnlNonExistRpc
            // 
            this.pnlNonExistRpc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNonExistRpc.Controls.Add(this.ddlAccepterVpc);
            this.pnlNonExistRpc.Controls.Add(this.ddlRequesterVpc);
            this.pnlNonExistRpc.Controls.Add(this.label8);
            this.pnlNonExistRpc.Controls.Add(this.label11);
            this.pnlNonExistRpc.Controls.Add(this.btnRpcCreate);
            this.pnlNonExistRpc.Controls.Add(this.lblRpcNotExists);
            this.pnlNonExistRpc.Location = new System.Drawing.Point(26, 196);
            this.pnlNonExistRpc.Name = "pnlNonExistRpc";
            this.pnlNonExistRpc.Size = new System.Drawing.Size(809, 163);
            this.pnlNonExistRpc.TabIndex = 4;
            this.pnlNonExistRpc.Visible = false;
            // 
            // ddlAccepterVpc
            // 
            this.ddlAccepterVpc.FormattingEnabled = true;
            this.ddlAccepterVpc.Location = new System.Drawing.Point(434, 43);
            this.ddlAccepterVpc.Name = "ddlAccepterVpc";
            this.ddlAccepterVpc.Size = new System.Drawing.Size(191, 21);
            this.ddlAccepterVpc.TabIndex = 8;
            // 
            // ddlRequesterVpc
            // 
            this.ddlRequesterVpc.FormattingEnabled = true;
            this.ddlRequesterVpc.Location = new System.Drawing.Point(124, 43);
            this.ddlRequesterVpc.Name = "ddlRequesterVpc";
            this.ddlRequesterVpc.Size = new System.Drawing.Size(191, 21);
            this.ddlRequesterVpc.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(338, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Accepter VPC:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Requester VPC:";
            // 
            // btnRpcCreate
            // 
            this.btnRpcCreate.Location = new System.Drawing.Point(16, 124);
            this.btnRpcCreate.Name = "btnRpcCreate";
            this.btnRpcCreate.Size = new System.Drawing.Size(108, 23);
            this.btnRpcCreate.TabIndex = 3;
            this.btnRpcCreate.Text = "Create Connection";
            this.btnRpcCreate.UseVisualStyleBackColor = true;
            this.btnRpcCreate.Click += new System.EventHandler(this.btnRpcCreate_Click);
            // 
            // lblRpcNotExists
            // 
            this.lblRpcNotExists.AutoSize = true;
            this.lblRpcNotExists.Location = new System.Drawing.Point(13, 13);
            this.lblRpcNotExists.Name = "lblRpcNotExists";
            this.lblRpcNotExists.Size = new System.Drawing.Size(269, 13);
            this.lblRpcNotExists.TabIndex = 2;
            this.lblRpcNotExists.Text = "RDS-Application VPC peering connection is not existing";
            // 
            // pnlExistRpc
            // 
            this.pnlExistRpc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlExistRpc.Controls.Add(this.btnRpcDelete);
            this.pnlExistRpc.Controls.Add(this.lblRpcStatus);
            this.pnlExistRpc.Controls.Add(this.label14);
            this.pnlExistRpc.Controls.Add(this.lblRpcAcceptVpc);
            this.pnlExistRpc.Controls.Add(this.label13);
            this.pnlExistRpc.Controls.Add(this.lblRpcReuestVpc);
            this.pnlExistRpc.Controls.Add(this.label12);
            this.pnlExistRpc.Controls.Add(this.lblRpcId);
            this.pnlExistRpc.Controls.Add(this.label10);
            this.pnlExistRpc.Location = new System.Drawing.Point(26, 57);
            this.pnlExistRpc.Name = "pnlExistRpc";
            this.pnlExistRpc.Size = new System.Drawing.Size(809, 109);
            this.pnlExistRpc.TabIndex = 1;
            this.pnlExistRpc.Visible = false;
            // 
            // btnRpcDelete
            // 
            this.btnRpcDelete.Location = new System.Drawing.Point(9, 74);
            this.btnRpcDelete.Name = "btnRpcDelete";
            this.btnRpcDelete.Size = new System.Drawing.Size(122, 23);
            this.btnRpcDelete.TabIndex = 8;
            this.btnRpcDelete.Text = "Delete Connection";
            this.btnRpcDelete.UseVisualStyleBackColor = true;
            this.btnRpcDelete.Click += new System.EventHandler(this.btnRpcDelete_Click);
            // 
            // lblRpcStatus
            // 
            this.lblRpcStatus.AutoSize = true;
            this.lblRpcStatus.Location = new System.Drawing.Point(67, 46);
            this.lblRpcStatus.Name = "lblRpcStatus";
            this.lblRpcStatus.Size = new System.Drawing.Size(41, 13);
            this.lblRpcStatus.TabIndex = 7;
            this.lblRpcStatus.Text = "label11";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(20, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 13);
            this.label14.TabIndex = 6;
            this.label14.Text = "Status:";
            // 
            // lblRpcAcceptVpc
            // 
            this.lblRpcAcceptVpc.AutoSize = true;
            this.lblRpcAcceptVpc.Location = new System.Drawing.Point(621, 13);
            this.lblRpcAcceptVpc.Name = "lblRpcAcceptVpc";
            this.lblRpcAcceptVpc.Size = new System.Drawing.Size(41, 13);
            this.lblRpcAcceptVpc.TabIndex = 5;
            this.lblRpcAcceptVpc.Text = "label11";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(531, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Accepter VPC:";
            // 
            // lblRpcReuestVpc
            // 
            this.lblRpcReuestVpc.AutoSize = true;
            this.lblRpcReuestVpc.Location = new System.Drawing.Point(424, 13);
            this.lblRpcReuestVpc.Name = "lblRpcReuestVpc";
            this.lblRpcReuestVpc.Size = new System.Drawing.Size(41, 13);
            this.lblRpcReuestVpc.TabIndex = 3;
            this.lblRpcReuestVpc.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(329, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Requester VPC:";
            // 
            // lblRpcId
            // 
            this.lblRpcId.AutoSize = true;
            this.lblRpcId.Location = new System.Drawing.Point(190, 13);
            this.lblRpcId.Name = "lblRpcId";
            this.lblRpcId.Size = new System.Drawing.Size(41, 13);
            this.lblRpcId.TabIndex = 1;
            this.lblRpcId.Text = "label11";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(20, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "VPC Peering Connection ID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(393, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Peering Connection of RDS VPC and Application VPC";
            // 
            // FormSystemManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 608);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormSystemManager";
            this.Text = "System Manager";
            this.Load += new System.EventHandler(this.FormSystemManager_Load);
            this.tabControl1.ResumeLayout(false);
            this.tPageStatus.ResumeLayout(false);
            this.tPageStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgRdsStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgAppStatus)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tPagePeeringConnection.ResumeLayout(false);
            this.tPagePeeringConnection.PerformLayout();
            this.pnlNonExistRpc.ResumeLayout(false);
            this.pnlNonExistRpc.PerformLayout();
            this.pnlExistRpc.ResumeLayout(false);
            this.pnlExistRpc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPageStatus;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRdsIdentifier;
        private System.Windows.Forms.Label lblRdsStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRdsArn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave_ASG_Settings;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Environment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Application;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesiredCapacity;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.TabPage tPagePeeringConnection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRpcNotExists;
        private System.Windows.Forms.Panel pnlExistRpc;
        private System.Windows.Forms.Button btnRpcDelete;
        private System.Windows.Forms.Label lblRpcStatus;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblRpcAcceptVpc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblRpcReuestVpc;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblRpcId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnRpcCreate;
        private System.Windows.Forms.Panel pnlNonExistRpc;
        private System.Windows.Forms.ComboBox ddlAccepterVpc;
        private System.Windows.Forms.ComboBox ddlRequesterVpc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnRpcRefresh;
        private System.Windows.Forms.CheckBox cboxRdsMutlAZ;
        private System.Windows.Forms.PictureBox imgAppStatus;
        private System.Windows.Forms.PictureBox imgRdsStatus;
    }
}