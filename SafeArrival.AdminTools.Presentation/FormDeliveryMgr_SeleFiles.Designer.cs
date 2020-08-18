namespace SafeArrival.AdminTools.Presentation
{
    partial class FormDeliveryMgr_SeleFiles
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
            this.chkListFiles = new System.Windows.Forms.CheckedListBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkListFiles
            // 
            this.chkListFiles.FormattingEnabled = true;
            this.chkListFiles.Location = new System.Drawing.Point(2, 28);
            this.chkListFiles.Name = "chkListFiles";
            this.chkListFiles.Size = new System.Drawing.Size(279, 334);
            this.chkListFiles.TabIndex = 0;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(225, 371);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(55, 23);
            this.btnSelect.TabIndex = 1;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(4, 6);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(101, 17);
            this.chkAll.TabIndex = 2;
            this.chkAll.Text = "Select All/None";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // FormDeliveryMgr_SeleFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 399);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.chkListFiles);
            this.Name = "FormDeliveryMgr_SeleFiles";
            this.Text = "Select Files";
            this.Load += new System.EventHandler(this.FormDeliveryMgr_SeleFiles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkListFiles;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.CheckBox chkAll;
    }
}