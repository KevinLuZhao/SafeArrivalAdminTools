﻿namespace SafeArrival.AdminTools.Presentation
{
    partial class FormDeploymentSwitch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeploymentSwitch));
            this.btnClearTargetGroups = new System.Windows.Forms.Button();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblWarn = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClearTargetGroups
            // 
            this.btnClearTargetGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearTargetGroups.Location = new System.Drawing.Point(751, 615);
            this.btnClearTargetGroups.Name = "btnClearTargetGroups";
            this.btnClearTargetGroups.Size = new System.Drawing.Size(180, 23);
            this.btnClearTargetGroups.TabIndex = 3;
            this.btnClearTargetGroups.Text = "Clear Target Group Attachments";
            this.btnClearTargetGroups.UseVisualStyleBackColor = true;
            this.btnClearTargetGroups.Click += new System.EventHandler(this.btnClearTargetGroups_Click);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSwitch.Location = new System.Drawing.Point(11, 615);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(147, 23);
            this.btnSwitch.TabIndex = 2;
            this.btnSwitch.Text = "Switch Deployment";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblWarn);
            this.panel1.Location = new System.Drawing.Point(12, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 608);
            this.panel1.TabIndex = 1;
            // 
            // lblWarn
            // 
            this.lblWarn.AutoSize = true;
            this.lblWarn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarn.ForeColor = System.Drawing.Color.Red;
            this.lblWarn.Location = new System.Drawing.Point(22, 265);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(46, 17);
            this.lblWarn.TabIndex = 0;
            this.lblWarn.Text = "label1";
            this.lblWarn.Visible = false;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(974, 615);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(135, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create Load Balancer";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // FormDeploymentSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 643);
            this.Controls.Add(this.btnClearTargetGroups);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCreate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDeploymentSwitch";
            this.Text = "Green/Blue Deployment Services";
            this.Load += new System.EventHandler(this.FormDeploymentSwitch_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnClearTargetGroups;
        private System.Windows.Forms.Label lblWarn;
        private System.Windows.Forms.Panel panel1;
    }
}