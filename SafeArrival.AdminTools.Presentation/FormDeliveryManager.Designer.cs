namespace SafeArrival.AdminTools.Presentation
{
    partial class FormDeliveryManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeliveryManager));
            this.splitContainerParams = new System.Windows.Forms.SplitContainer();
            this.btnCmd = new System.Windows.Forms.Button();
            this.linkLabParams = new System.Windows.Forms.LinkLabel();
            this.lstJsonFiles = new System.Windows.Forms.ListBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFileEditor = new System.Windows.Forms.RichTextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabCloudFormation = new System.Windows.Forms.TabPage();
            this.splitContainerCF = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.linkLabCF = new System.Windows.Forms.LinkLabel();
            this.lstCFFiles = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBranchName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExportCF = new System.Windows.Forms.Button();
            this.txtCFViewer = new System.Windows.Forms.RichTextBox();
            this.tabParams = new System.Windows.Forms.TabPage();
            this.tabApplications = new System.Windows.Forms.TabPage();
            this.pnlApplications = new System.Windows.Forms.Panel();
            this.btnDeliverySave = new System.Windows.Forms.Button();
            this.cboxUpdateVersions = new System.Windows.Forms.CheckBox();
            this.cboxUpdateLambdas = new System.Windows.Forms.CheckBox();
            this.cboxCopyApps = new System.Windows.Forms.CheckBox();
            this.txtAppsProcess = new System.Windows.Forms.RichTextBox();
            this.btnAppsExport = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnPerview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerParams)).BeginInit();
            this.splitContainerParams.Panel1.SuspendLayout();
            this.splitContainerParams.Panel2.SuspendLayout();
            this.splitContainerParams.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabCloudFormation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCF)).BeginInit();
            this.splitContainerCF.Panel1.SuspendLayout();
            this.splitContainerCF.Panel2.SuspendLayout();
            this.splitContainerCF.SuspendLayout();
            this.tabParams.SuspendLayout();
            this.tabApplications.SuspendLayout();
            this.pnlApplications.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerParams
            // 
            this.splitContainerParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerParams.Location = new System.Drawing.Point(3, 3);
            this.splitContainerParams.Name = "splitContainerParams";
            // 
            // splitContainerParams.Panel1
            // 
            this.splitContainerParams.Panel1.Controls.Add(this.btnCmd);
            this.splitContainerParams.Panel1.Controls.Add(this.linkLabParams);
            this.splitContainerParams.Panel1.Controls.Add(this.lstJsonFiles);
            // 
            // splitContainerParams.Panel2
            // 
            this.splitContainerParams.Panel2.Controls.Add(this.btnCompare);
            this.splitContainerParams.Panel2.Controls.Add(this.label1);
            this.splitContainerParams.Panel2.Controls.Add(this.btnExport);
            this.splitContainerParams.Panel2.Controls.Add(this.btnSave);
            this.splitContainerParams.Panel2.Controls.Add(this.txtFileEditor);
            this.splitContainerParams.Size = new System.Drawing.Size(976, 537);
            this.splitContainerParams.SplitterDistance = 229;
            this.splitContainerParams.TabIndex = 0;
            // 
            // btnCmd
            // 
            this.btnCmd.Image = ((System.Drawing.Image)(resources.GetObject("btnCmd.Image")));
            this.btnCmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCmd.Location = new System.Drawing.Point(94, 5);
            this.btnCmd.Name = "btnCmd";
            this.btnCmd.Size = new System.Drawing.Size(113, 23);
            this.btnCmd.TabIndex = 5;
            this.btnCmd.Text = "Command Prompt";
            this.btnCmd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCmd.UseVisualStyleBackColor = true;
            this.btnCmd.Click += new System.EventHandler(this.btnCmd_Click);
            // 
            // linkLabParams
            // 
            this.linkLabParams.AutoSize = true;
            this.linkLabParams.Location = new System.Drawing.Point(3, 10);
            this.linkLabParams.Name = "linkLabParams";
            this.linkLabParams.Size = new System.Drawing.Size(85, 13);
            this.linkLabParams.TabIndex = 5;
            this.linkLabParams.TabStop = true;
            this.linkLabParams.Text = "Open in Explorer";
            this.linkLabParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lstJsonFiles
            // 
            this.lstJsonFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstJsonFiles.FormattingEnabled = true;
            this.lstJsonFiles.Location = new System.Drawing.Point(4, 34);
            this.lstJsonFiles.Name = "lstJsonFiles";
            this.lstJsonFiles.Size = new System.Drawing.Size(222, 498);
            this.lstJsonFiles.TabIndex = 2;
            this.lstJsonFiles.SelectedIndexChanged += new System.EventHandler(this.lstJsonFiles_SelectedIndexChanged);
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompare.Location = new System.Drawing.Point(361, 5);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(153, 23);
            this.btnCompare.TabIndex = 4;
            this.btnCompare.Text = "Compare Parameter Files";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "    ";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(526, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(128, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Publish to AWS S3";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(665, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFileEditor
            // 
            this.txtFileEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileEditor.Location = new System.Drawing.Point(3, 33);
            this.txtFileEditor.Name = "txtFileEditor";
            this.txtFileEditor.Size = new System.Drawing.Size(737, 501);
            this.txtFileEditor.TabIndex = 0;
            this.txtFileEditor.Text = "";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabCloudFormation);
            this.tabMain.Controls.Add(this.tabParams);
            this.tabMain.Controls.Add(this.tabApplications);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(990, 569);
            this.tabMain.TabIndex = 1;
            // 
            // tabCloudFormation
            // 
            this.tabCloudFormation.Controls.Add(this.splitContainerCF);
            this.tabCloudFormation.Location = new System.Drawing.Point(4, 22);
            this.tabCloudFormation.Name = "tabCloudFormation";
            this.tabCloudFormation.Padding = new System.Windows.Forms.Padding(3);
            this.tabCloudFormation.Size = new System.Drawing.Size(982, 543);
            this.tabCloudFormation.TabIndex = 1;
            this.tabCloudFormation.Text = "CloudFormation";
            this.tabCloudFormation.UseVisualStyleBackColor = true;
            // 
            // splitContainerCF
            // 
            this.splitContainerCF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCF.Location = new System.Drawing.Point(3, 3);
            this.splitContainerCF.Name = "splitContainerCF";
            // 
            // splitContainerCF.Panel1
            // 
            this.splitContainerCF.Panel1.Controls.Add(this.button1);
            this.splitContainerCF.Panel1.Controls.Add(this.linkLabCF);
            this.splitContainerCF.Panel1.Controls.Add(this.lstCFFiles);
            // 
            // splitContainerCF.Panel2
            // 
            this.splitContainerCF.Panel2.Controls.Add(this.label3);
            this.splitContainerCF.Panel2.Controls.Add(this.lblBranchName);
            this.splitContainerCF.Panel2.Controls.Add(this.label2);
            this.splitContainerCF.Panel2.Controls.Add(this.btnExportCF);
            this.splitContainerCF.Panel2.Controls.Add(this.txtCFViewer);
            this.splitContainerCF.Size = new System.Drawing.Size(976, 537);
            this.splitContainerCF.SplitterDistance = 229;
            this.splitContainerCF.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(94, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Command Prompt";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // linkLabCF
            // 
            this.linkLabCF.AutoSize = true;
            this.linkLabCF.Location = new System.Drawing.Point(3, 9);
            this.linkLabCF.Name = "linkLabCF";
            this.linkLabCF.Size = new System.Drawing.Size(85, 13);
            this.linkLabCF.TabIndex = 5;
            this.linkLabCF.TabStop = true;
            this.linkLabCF.Text = "Open in Explorer";
            // 
            // lstCFFiles
            // 
            this.lstCFFiles.AllowDrop = true;
            this.lstCFFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCFFiles.FormattingEnabled = true;
            this.lstCFFiles.Location = new System.Drawing.Point(4, 34);
            this.lstCFFiles.Name = "lstCFFiles";
            this.lstCFFiles.Size = new System.Drawing.Size(222, 498);
            this.lstCFFiles.TabIndex = 2;
            this.lstCFFiles.SelectedIndexChanged += new System.EventHandler(this.lstCFFiles_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Current Git Local Branch:";
            // 
            // lblBranchName
            // 
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.ForeColor = System.Drawing.Color.Red;
            this.lblBranchName.Location = new System.Drawing.Point(169, 9);
            this.lblBranchName.Name = "lblBranchName";
            this.lblBranchName.Size = new System.Drawing.Size(10, 13);
            this.lblBranchName.TabIndex = 5;
            this.lblBranchName.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "    ";
            // 
            // btnExportCF
            // 
            this.btnExportCF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportCF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportCF.Location = new System.Drawing.Point(629, 4);
            this.btnExportCF.Name = "btnExportCF";
            this.btnExportCF.Size = new System.Drawing.Size(109, 23);
            this.btnExportCF.TabIndex = 2;
            this.btnExportCF.Text = "Publish to AWS S3";
            this.btnExportCF.UseVisualStyleBackColor = true;
            this.btnExportCF.Click += new System.EventHandler(this.btnExportCF_Click);
            // 
            // txtCFViewer
            // 
            this.txtCFViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCFViewer.Location = new System.Drawing.Point(3, 33);
            this.txtCFViewer.Name = "txtCFViewer";
            this.txtCFViewer.ReadOnly = true;
            this.txtCFViewer.Size = new System.Drawing.Size(737, 501);
            this.txtCFViewer.TabIndex = 0;
            this.txtCFViewer.Text = "";
            // 
            // tabParams
            // 
            this.tabParams.Controls.Add(this.splitContainerParams);
            this.tabParams.Location = new System.Drawing.Point(4, 22);
            this.tabParams.Name = "tabParams";
            this.tabParams.Padding = new System.Windows.Forms.Padding(3);
            this.tabParams.Size = new System.Drawing.Size(982, 543);
            this.tabParams.TabIndex = 0;
            this.tabParams.Text = "Parameters";
            this.tabParams.UseVisualStyleBackColor = true;
            // 
            // tabApplications
            // 
            this.tabApplications.Controls.Add(this.pnlApplications);
            this.tabApplications.Location = new System.Drawing.Point(4, 22);
            this.tabApplications.Name = "tabApplications";
            this.tabApplications.Padding = new System.Windows.Forms.Padding(3);
            this.tabApplications.Size = new System.Drawing.Size(982, 543);
            this.tabApplications.TabIndex = 2;
            this.tabApplications.Text = "Applications";
            this.tabApplications.UseVisualStyleBackColor = true;
            // 
            // pnlApplications
            // 
            this.pnlApplications.Controls.Add(this.btnPerview);
            this.pnlApplications.Controls.Add(this.btnDeliverySave);
            this.pnlApplications.Controls.Add(this.cboxUpdateVersions);
            this.pnlApplications.Controls.Add(this.cboxUpdateLambdas);
            this.pnlApplications.Controls.Add(this.cboxCopyApps);
            this.pnlApplications.Controls.Add(this.txtAppsProcess);
            this.pnlApplications.Controls.Add(this.btnAppsExport);
            this.pnlApplications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlApplications.Location = new System.Drawing.Point(3, 3);
            this.pnlApplications.Name = "pnlApplications";
            this.pnlApplications.Size = new System.Drawing.Size(976, 537);
            this.pnlApplications.TabIndex = 0;
            // 
            // btnDeliverySave
            // 
            this.btnDeliverySave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeliverySave.Location = new System.Drawing.Point(905, 5);
            this.btnDeliverySave.Name = "btnDeliverySave";
            this.btnDeliverySave.Size = new System.Drawing.Size(65, 23);
            this.btnDeliverySave.TabIndex = 5;
            this.btnDeliverySave.Text = "Save";
            this.btnDeliverySave.UseVisualStyleBackColor = true;
            this.btnDeliverySave.Click += new System.EventHandler(this.btnDeliverySave_Click);
            // 
            // cboxUpdateVersions
            // 
            this.cboxUpdateVersions.AutoSize = true;
            this.cboxUpdateVersions.Checked = true;
            this.cboxUpdateVersions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxUpdateVersions.Location = new System.Drawing.Point(454, 8);
            this.cboxUpdateVersions.Name = "cboxUpdateVersions";
            this.cboxUpdateVersions.Size = new System.Drawing.Size(145, 17);
            this.cboxUpdateVersions.TabIndex = 4;
            this.cboxUpdateVersions.Text = "Update Lambda Versions";
            this.cboxUpdateVersions.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.cboxUpdateVersions.UseVisualStyleBackColor = true;
            // 
            // cboxUpdateLambdas
            // 
            this.cboxUpdateLambdas.AutoSize = true;
            this.cboxUpdateLambdas.Checked = true;
            this.cboxUpdateLambdas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxUpdateLambdas.Location = new System.Drawing.Point(314, 8);
            this.cboxUpdateLambdas.Name = "cboxUpdateLambdas";
            this.cboxUpdateLambdas.Size = new System.Drawing.Size(126, 17);
            this.cboxUpdateLambdas.TabIndex = 3;
            this.cboxUpdateLambdas.Text = "Update Lambda Files";
            this.cboxUpdateLambdas.UseVisualStyleBackColor = true;
            // 
            // cboxCopyApps
            // 
            this.cboxCopyApps.AutoSize = true;
            this.cboxCopyApps.Checked = true;
            this.cboxCopyApps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxCopyApps.Location = new System.Drawing.Point(204, 8);
            this.cboxCopyApps.Name = "cboxCopyApps";
            this.cboxCopyApps.Size = new System.Drawing.Size(96, 17);
            this.cboxCopyApps.TabIndex = 2;
            this.cboxCopyApps.Text = "Copy App Files";
            this.cboxCopyApps.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cboxCopyApps.UseVisualStyleBackColor = true;
            // 
            // txtAppsProcess
            // 
            this.txtAppsProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppsProcess.Location = new System.Drawing.Point(6, 34);
            this.txtAppsProcess.Name = "txtAppsProcess";
            this.txtAppsProcess.ReadOnly = true;
            this.txtAppsProcess.Size = new System.Drawing.Size(965, 498);
            this.txtAppsProcess.TabIndex = 1;
            this.txtAppsProcess.Text = "";
            // 
            // btnAppsExport
            // 
            this.btnAppsExport.Location = new System.Drawing.Point(6, 5);
            this.btnAppsExport.Name = "btnAppsExport";
            this.btnAppsExport.Size = new System.Drawing.Size(103, 23);
            this.btnAppsExport.TabIndex = 0;
            this.btnAppsExport.Text = "Publish to AWS";
            this.btnAppsExport.UseVisualStyleBackColor = true;
            this.btnAppsExport.Click += new System.EventHandler(this.btnAppsExport_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnPerview
            // 
            this.btnPerview.Location = new System.Drawing.Point(118, 5);
            this.btnPerview.Name = "btnPerview";
            this.btnPerview.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPerview.Size = new System.Drawing.Size(75, 23);
            this.btnPerview.TabIndex = 6;
            this.btnPerview.Text = "Preview";
            this.btnPerview.UseVisualStyleBackColor = true;
            this.btnPerview.Click += new System.EventHandler(this.btnPerview_Click);
            // 
            // FormDeliveryManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 569);
            this.Controls.Add(this.tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDeliveryManager";
            this.Text = "Delivery Manager";
            this.Load += new System.EventHandler(this.FormParametersEditor_Load);
            this.splitContainerParams.Panel1.ResumeLayout(false);
            this.splitContainerParams.Panel1.PerformLayout();
            this.splitContainerParams.Panel2.ResumeLayout(false);
            this.splitContainerParams.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerParams)).EndInit();
            this.splitContainerParams.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabCloudFormation.ResumeLayout(false);
            this.splitContainerCF.Panel1.ResumeLayout(false);
            this.splitContainerCF.Panel1.PerformLayout();
            this.splitContainerCF.Panel2.ResumeLayout(false);
            this.splitContainerCF.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCF)).EndInit();
            this.splitContainerCF.ResumeLayout(false);
            this.tabParams.ResumeLayout(false);
            this.tabApplications.ResumeLayout(false);
            this.pnlApplications.ResumeLayout(false);
            this.pnlApplications.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerParams;
        private System.Windows.Forms.ListBox lstJsonFiles;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RichTextBox txtFileEditor;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.LinkLabel linkLabParams;
        private System.Windows.Forms.Button btnCmd;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabCloudFormation;
        private System.Windows.Forms.SplitContainer splitContainerCF;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel linkLabCF;
        private System.Windows.Forms.ListBox lstCFFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExportCF;
        private System.Windows.Forms.RichTextBox txtCFViewer;
        private System.Windows.Forms.TabPage tabParams;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBranchName;
        private System.Windows.Forms.TabPage tabApplications;
        private System.Windows.Forms.Panel pnlApplications;
        private System.Windows.Forms.Button btnAppsExport;
        private System.Windows.Forms.RichTextBox txtAppsProcess;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cboxUpdateVersions;
        private System.Windows.Forms.CheckBox cboxUpdateLambdas;
        private System.Windows.Forms.CheckBox cboxCopyApps;
        private System.Windows.Forms.Button btnDeliverySave;
        private System.Windows.Forms.Button btnPerview;
    }
}