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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tPageStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.tabControl1.Location = new System.Drawing.Point(2, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1113, 537);
            this.tabControl1.TabIndex = 3;
            // 
            // tPageStatus
            // 
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
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(10, 384);
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
            this.panel1.Location = new System.Drawing.Point(10, 268);
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
            this.label2.Location = new System.Drawing.Point(10, 239);
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
            this.listView1.Size = new System.Drawing.Size(1083, 186);
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
    }
}