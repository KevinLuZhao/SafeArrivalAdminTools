using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormDeliveryMgr_SeleFiles : Form
    {
        private List<string> selectedFiles;
        private List<string> allFiles;
        FormDeliveryManager sourceForm;

        public FormDeliveryMgr_SeleFiles(FormDeliveryManager sourceForm, List<string> selectedFiles, List<string> allFiles)
        {
            InitializeComponent();
            this.selectedFiles = selectedFiles;
            this.allFiles = allFiles;
            this.sourceForm = sourceForm;
            chkListFiles.CheckOnClick = true;
            chkAll.Checked = true;
        }

        private void FormDeliveryMgr_SeleFiles_Load(object sender, EventArgs e)
        {
            chkListFiles.Items.Clear();
            foreach (var file in allFiles)
            {
                if (selectedFiles.Contains(file))
                    chkListFiles.Items.Add(file, true);
                else
                    chkListFiles.Items.Add(file, false);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectedFiles.Clear();
            foreach (var file in chkListFiles.CheckedItems)
            {
                selectedFiles.Add(file.ToString());
            }
            sourceForm.SelectedAppFiles = selectedFiles;
            this.Close();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int i = 0; i < chkListFiles.Items.Count; i++)
                {
                    chkListFiles.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < chkListFiles.Items.Count; i++)
                {
                    chkListFiles.SetItemChecked(i, false);
                }
            }
        }
    }
}
