namespace SafeArrival.AdminTools.Presentation
{
    partial class FormParameterFileCompare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParameterFileCompare));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxLeft = new System.Windows.Forms.ComboBox();
            this.cboxRight = new System.Windows.Forms.ComboBox();
            this.cboxFiles = new System.Windows.Forms.ComboBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.txtLeft = new System.Windows.Forms.RichTextBox();
            this.txtRight = new System.Windows.Forms.RichTextBox();
            this.btnSaveLeft = new System.Windows.Forms.Button();
            this.btnSaveRight = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(197, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Right:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(392, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "File Name: ";
            // 
            // cboxLeft
            // 
            this.cboxLeft.FormattingEnabled = true;
            this.cboxLeft.Location = new System.Drawing.Point(52, 10);
            this.cboxLeft.Name = "cboxLeft";
            this.cboxLeft.Size = new System.Drawing.Size(121, 21);
            this.cboxLeft.TabIndex = 3;
            this.cboxLeft.SelectedIndexChanged += new System.EventHandler(this.cboxEnvironment_SelectedIndexChanged);
            // 
            // cboxRight
            // 
            this.cboxRight.FormattingEnabled = true;
            this.cboxRight.Location = new System.Drawing.Point(244, 10);
            this.cboxRight.Name = "cboxRight";
            this.cboxRight.Size = new System.Drawing.Size(113, 21);
            this.cboxRight.TabIndex = 4;
            this.cboxRight.SelectedIndexChanged += new System.EventHandler(this.cboxEnvironment_SelectedIndexChanged);
            // 
            // cboxFiles
            // 
            this.cboxFiles.FormattingEnabled = true;
            this.cboxFiles.Location = new System.Drawing.Point(464, 10);
            this.cboxFiles.Name = "cboxFiles";
            this.cboxFiles.Size = new System.Drawing.Size(152, 21);
            this.cboxFiles.TabIndex = 5;
            // 
            // btnCompare
            // 
            this.btnCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompare.Location = new System.Drawing.Point(622, 10);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 6;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // txtLeft
            // 
            this.txtLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLeft.Location = new System.Drawing.Point(7, 68);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new System.Drawing.Size(554, 577);
            this.txtLeft.TabIndex = 7;
            this.txtLeft.Text = "";
            // 
            // txtRight
            // 
            this.txtRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRight.Location = new System.Drawing.Point(569, 68);
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new System.Drawing.Size(554, 577);
            this.txtRight.TabIndex = 8;
            this.txtRight.Text = "";
            // 
            // btnSaveLeft
            // 
            this.btnSaveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveLeft.Location = new System.Drawing.Point(10, 39);
            this.btnSaveLeft.Name = "btnSaveLeft";
            this.btnSaveLeft.Size = new System.Drawing.Size(75, 23);
            this.btnSaveLeft.TabIndex = 9;
            this.btnSaveLeft.Text = "Save";
            this.btnSaveLeft.UseVisualStyleBackColor = true;
            this.btnSaveLeft.Click += new System.EventHandler(this.btnSaveLeft_Click);
            // 
            // btnSaveRight
            // 
            this.btnSaveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRight.Location = new System.Drawing.Point(569, 39);
            this.btnSaveRight.Name = "btnSaveRight";
            this.btnSaveRight.Size = new System.Drawing.Size(75, 23);
            this.btnSaveRight.TabIndex = 10;
            this.btnSaveRight.Text = "Save";
            this.btnSaveRight.UseVisualStyleBackColor = true;
            this.btnSaveRight.Click += new System.EventHandler(this.btnSaveRight_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 648);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1130, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // FormParameterFileCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 670);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSaveRight);
            this.Controls.Add(this.btnSaveLeft);
            this.Controls.Add(this.txtRight);
            this.Controls.Add(this.txtLeft);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.cboxFiles);
            this.Controls.Add(this.cboxRight);
            this.Controls.Add(this.cboxLeft);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormParameterFileCompare";
            this.Text = "Parameter File Compare";
            this.Load += new System.EventHandler(this.FormParameterFileCompare_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboxLeft;
        private System.Windows.Forms.ComboBox cboxRight;
        private System.Windows.Forms.ComboBox cboxFiles;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.RichTextBox txtLeft;
        private System.Windows.Forms.RichTextBox txtRight;
        private System.Windows.Forms.Button btnSaveLeft;
        private System.Windows.Forms.Button btnSaveRight;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}