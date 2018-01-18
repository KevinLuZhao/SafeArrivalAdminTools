using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormParameterFileCompare : Form
    {
        public FormParameterFileCompare()
        {
            InitializeComponent();
        }

        private void FormParameterFileCompare_Load(object sender, EventArgs e)
        {
            cboxLeft.DataSource = Enum.GetValues(typeof(Model.Environment));
            cboxLeft.SelectedIndex = 0;
            cboxRight.DataSource = Enum.GetValues(typeof(Model.Environment));
            cboxRight.SelectedIndex = 1;
        }

        private void cboxEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboxLeft.SelectedIndex < 0 || cboxRight.SelectedIndex < 0)
                {
                    cboxFiles.DataSource = new List<string>();
                    return;
                }

                if (cboxRight.SelectedIndex != cboxLeft.SelectedIndex)
                {
                    List<string> lstLeftFile = new List<string>();
                    foreach (var file in Directory.GetFiles(GetJsonFilesFolder(cboxLeft.SelectedValue.ToString())))
                    {
                        lstLeftFile.Add(Path.GetFileName(file));
                    }
                    List<string> lstRightFile = new List<string>();
                    foreach (var file in Directory.GetFiles(GetJsonFilesFolder(cboxRight.SelectedValue.ToString())))
                    {
                        lstRightFile.Add(Path.GetFileName(file));
                    }

                    List<string> lstFile = lstLeftFile.Union(lstRightFile).ToList();
                    cboxFiles.DataSource = lstFile;
                }
                else
                {
                    cboxFiles.DataSource = new List<string>();
                    cboxFiles.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if ((cboxRight.SelectedIndex == cboxLeft.SelectedIndex) || cboxFiles.Items.Count == 0)
                return;
            txtLeft.Text = GetFileContent(Path.Combine(GetJsonFilesFolder(cboxLeft.SelectedValue.ToString()), cboxFiles.SelectedValue.ToString()));
            txtRight.Text = GetFileContent(Path.Combine(GetJsonFilesFolder(cboxRight.SelectedValue.ToString()), cboxFiles.SelectedValue.ToString()));
        }

        private string GetJsonFilesFolder(string env)
        {
            return Path.Combine(ConfigurationSettings.AppSettings["ParammeterFilesFolder"], env);
        }

        private string GetFileContent(string path)
        {
            string content = string.Empty;
            if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    content = r.ReadToEnd();
                    r.Close();
                }
            }
            return content;
        }

        private void btnSaveLeft_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(GetJsonFilesFolder(cboxLeft.SelectedValue.ToString()), cboxFiles.SelectedValue.ToString());
                using (StreamWriter w = new StreamWriter(path))
                {
                    w.Write(txtLeft.Text);
                    toolStripStatusLabel1.Text = string.Format("'{0}' is saved!", path);
                    //WriteNotification(string.Format("{0} is changed!", path));
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }
        }
        private void btnSaveRight_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(GetJsonFilesFolder(cboxRight.SelectedValue.ToString()), cboxFiles.SelectedValue.ToString());
                using (StreamWriter w = new StreamWriter(path))
                {
                    w.Write(txtRight.Text);
                    toolStripStatusLabel1.Text = string.Format("'{0}' is saved!", path);
                }
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }
        }
    }
}
