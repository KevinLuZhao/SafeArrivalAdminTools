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
            this.btnInit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPageStatus = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRdsMutlAZ = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
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
            this.pnlExistRpc = new System.Windows.Forms.Panel();
            this.pnlNonExistRpc = new System.Windows.Forms.Panel();
            this.btnRpcCreate = new System.Windows.Forms.Button();
            this.lblRpcNotExists = new System.Windows.Forms.Label();
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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tPageStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tPagePeeringConnection.SuspendLayout();
            this.pnlExistRpc.SuspendLayout();
            this.pnlNonExistRpc.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(988, 482);
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
            this.tabControl1.Location = new System.Drawing.Point(2, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1113, 537);
            this.tabControl1.TabIndex = 3;
            // 
            // tPageStatus
            // 
            this.tPageStatus.Controls.Add(this.label4);
            this.tPageStatus.Controls.Add(this.listView2);
            this.tPageStatus.Controls.Add(this.btnRefresh);
            this.tPageStatus.Controls.Add(this.panel1);
            this.tPageStatus.Controls.Add(this.label2);
            this.tPageStatus.Controls.Add(this.label1);
            this.tPageStatus.Controls.Add(this.listView1);
            this.tPageStatus.Location = new System.Drawing.Point(4, 22);
            this.tPageStatus.Name = "tPageStatus";
            this.tPageStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tPageStatus.Size = new System.Drawing.Size(1105, 511);
            this.tPageStatus.TabIndex = 0;
            this.tPageStatus.Text = "Status";
            this.tPageStatus.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Scheduled Actions";
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(7, 233);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(1083, 84);
            this.listView2.TabIndex = 14;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(7, 479);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblRdsMutlAZ);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblRdsIdentifier);
            this.panel1.Controls.Add(this.lblRdsStatus);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblRdsArn);
            this.panel1.Location = new System.Drawing.Point(7, 363);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1083, 100);
            this.panel1.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(18, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Status:";
            // 
            // lblRdsMutlAZ
            // 
            this.lblRdsMutlAZ.AutoSize = true;
            this.lblRdsMutlAZ.Location = new System.Drawing.Point(278, 61);
            this.lblRdsMutlAZ.Name = "lblRdsMutlAZ";
            this.lblRdsMutlAZ.Size = new System.Drawing.Size(35, 13);
            this.lblRdsMutlAZ.TabIndex = 11;
            this.lblRdsMutlAZ.Text = "label4";
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(214, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Multi AZ:";
            // 
            // lblRdsIdentifier
            // 
            this.lblRdsIdentifier.AutoSize = true;
            this.lblRdsIdentifier.Location = new System.Drawing.Point(158, 15);
            this.lblRdsIdentifier.Name = "lblRdsIdentifier";
            this.lblRdsIdentifier.Size = new System.Drawing.Size(35, 13);
            this.lblRdsIdentifier.TabIndex = 5;
            this.lblRdsIdentifier.Text = "label4";
            // 
            // lblRdsStatus
            // 
            this.lblRdsStatus.AutoSize = true;
            this.lblRdsStatus.Location = new System.Drawing.Point(67, 61);
            this.lblRdsStatus.Name = "lblRdsStatus";
            this.lblRdsStatus.Size = new System.Drawing.Size(35, 13);
            this.lblRdsStatus.TabIndex = 9;
            this.lblRdsStatus.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(18, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "DB Instance Arn:";
            // 
            // lblRdsArn
            // 
            this.lblRdsArn.AutoSize = true;
            this.lblRdsArn.Location = new System.Drawing.Point(128, 37);
            this.lblRdsArn.Name = "lblRdsArn";
            this.lblRdsArn.Size = new System.Drawing.Size(35, 13);
            this.lblRdsArn.TabIndex = 7;
            this.lblRdsArn.Text = "label4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 334);
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
            this.tabPage2.Size = new System.Drawing.Size(1105, 511);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Auto-Scaling Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSave_ASG_Settings
            // 
            this.btnSave_ASG_Settings.Location = new System.Drawing.Point(7, 482);
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
            this.dataGridView1.Size = new System.Drawing.Size(1092, 451);
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
            this.tPagePeeringConnection.Controls.Add(this.pnlNonExistRpc);
            this.tPagePeeringConnection.Controls.Add(this.pnlExistRpc);
            this.tPagePeeringConnection.Controls.Add(this.label6);
            this.tPagePeeringConnection.Location = new System.Drawing.Point(4, 22);
            this.tPagePeeringConnection.Name = "tPagePeeringConnection";
            this.tPagePeeringConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tPagePeeringConnection.Size = new System.Drawing.Size(1105, 511);
            this.tPagePeeringConnection.TabIndex = 2;
            this.tPagePeeringConnection.Text = "VPC Peering Connection";
            this.tPagePeeringConnection.UseVisualStyleBackColor = true;
            // 
            // pnlExistRpc
            // 
            this.pnlExistRpc.Controls.Add(this.btnRpcDelete);
            this.pnlExistRpc.Controls.Add(this.lblRpcStatus);
            this.pnlExistRpc.Controls.Add(this.label14);
            this.pnlExistRpc.Controls.Add(this.lblRpcAcceptVpc);
            this.pnlExistRpc.Controls.Add(this.label13);
            this.pnlExistRpc.Controls.Add(this.lblRpcReuestVpc);
            this.pnlExistRpc.Controls.Add(this.label12);
            this.pnlExistRpc.Controls.Add(this.lblRpcId);
            this.pnlExistRpc.Controls.Add(this.label10);
            this.pnlExistRpc.Location = new System.Drawing.Point(26, 52);
            this.pnlExistRpc.Name = "pnlExistRpc";
            this.pnlExistRpc.Size = new System.Drawing.Size(784, 70);
            this.pnlExistRpc.TabIndex = 1;
            // 
            // pnlNonExistRpc
            // 
            this.pnlNonExistRpc.Controls.Add(this.btnRpcCreate);
            this.pnlNonExistRpc.Controls.Add(this.lblRpcNotExists);
            this.pnlNonExistRpc.Location = new System.Drawing.Point(26, 138);
            this.pnlNonExistRpc.Name = "pnlNonExistRpc";
            this.pnlNonExistRpc.Size = new System.Drawing.Size(787, 100);
            this.pnlNonExistRpc.TabIndex = 4;
            // 
            // btnRpcCreate
            // 
            this.btnRpcCreate.Location = new System.Drawing.Point(16, 63);
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
            // btnRpcDelete
            // 
            this.btnRpcDelete.Location = new System.Drawing.Point(630, 44);
            this.btnRpcDelete.Name = "btnRpcDelete";
            this.btnRpcDelete.Size = new System.Drawing.Size(122, 23);
            this.btnRpcDelete.TabIndex = 8;
            this.btnRpcDelete.Text = "Delete Connection";
            this.btnRpcDelete.UseVisualStyleBackColor = true;
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
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(6, 564);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop System";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(115, 564);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start System";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // FormSystemManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 608);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnStop);
            this.Name = "FormSystemManager";
            this.Text = "System Manager";
            this.Load += new System.EventHandler(this.FormSystemManager_Load);
            this.tabControl1.ResumeLayout(false);
            this.tPageStatus.ResumeLayout(false);
            this.tPageStatus.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tPagePeeringConnection.ResumeLayout(false);
            this.tPagePeeringConnection.PerformLayout();
            this.pnlExistRpc.ResumeLayout(false);
            this.pnlExistRpc.PerformLayout();
            this.pnlNonExistRpc.ResumeLayout(false);
            this.pnlNonExistRpc.PerformLayout();
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
        private System.Windows.Forms.Label lblRdsMutlAZ;
        private System.Windows.Forms.Label label9;
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
    }
}