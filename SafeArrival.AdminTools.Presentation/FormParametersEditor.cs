using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using SafeArrival.AdminTools.Model;
using SafeArrival.AdminTools.AwsUtilities;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormParametersEditor : FormMdiChildBase
    {
        private string _parameterFolder = string.Empty;
        //private string _s3bucket; 
        public FormParametersEditor()
        {
            InitializeComponent();
        }

        public override void OnEnvironmentChanged()
        {
            BindFolder(GetJsonFilesFolder());
        }

        private void BindFolder(string dir)
        {
            try
            {
                List<KeyValuePair<string, string>> lstFile = new List<KeyValuePair<string, string>>();
                foreach (var file in Directory.GetFiles(dir, "*.json"))
                {
                    lstFile.Add(new KeyValuePair<string, string>(Path.GetFileNameWithoutExtension(file), file));
                }
                lstJsonFiles.DataSource = lstFile;
                lstJsonFiles.DisplayMember = "Key";
                lstJsonFiles.ValueMember = "Value";
                lstJsonFiles.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void lstJsonFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstJsonFiles.SelectedIndex < 0)
                {
                    txtFileEditor.Text = string.Empty;
                    return;
                }
                using (StreamReader r = new StreamReader(
                ((System.Collections.Generic.KeyValuePair<string, string>)lstJsonFiles.SelectedItem).Value))
                {
                    txtFileEditor.Text = r.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter w = new StreamWriter(
                ((System.Collections.Generic.KeyValuePair<string, string>)lstJsonFiles.SelectedItem).Value))
                {
                    w.Write(txtFileEditor.Text);
                    //MessageBox.Show(string.Format("{0} saved!", ((System.Collections.Generic.KeyValuePair<string, string>)lstJsonFiles.SelectedItem).Key));
                    //NotifyToMainStatus(string.Format("{0} saved!", ((System.Collections.Generic.KeyValuePair<string, string>)lstJsonFiles.SelectedItem).Key));
                    WriteNotification(string.Format("{0} is changed!", ((System.Collections.Generic.KeyValuePair<string, string>)lstJsonFiles.SelectedItem).Key));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = MessageBox.Show(
                    $"Are you sure to upload parameter.zip to S3 bucket '{GenerationS3BucketName()}'",
                    "Confirm Export to S3 Bucket",
                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    await GenerateParameterZip();
                    WriteNotification("File parameter.zip updated at " + GenerationS3BucketName());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            var frm = new FormParameterFileCompare();
            frm.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(GetJsonFilesFolder());
        }

        private void btnCmd_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = ConfigurationSettings.AppSettings["ParammeterFilesFolder"];
            p.StartInfo.UseShellExecute = false;
            p.Start();
        }

        private async Task GenerateParameterZip()
        {
            if (!Directory.Exists(@"c:\temp"))
            {
                NotifyToMainStatus(string.Format("Folder {0} is needed to store the tempary zip file", @"c:\temp"), System.Drawing.Color.Red);
                return;
            }
            if (File.Exists(@"c:\temp\parameter.zip"))
            {
                File.Delete(@"c:\temp\parameter.zip");
            }

            using (FileStream zipToOpen = new FileStream(@"c:\temp\parameter.zip", FileMode.OpenOrCreate))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry;
                    DirectoryInfo d = new DirectoryInfo(GetJsonFilesFolder());
                    FileInfo[] Files = d.GetFiles("*.json");
                    foreach (FileInfo file in Files)
                    {
                        readmeEntry = archive.CreateEntryFromFile(file.FullName, file.Name);
                    }
                }
                zipToOpen.Close();
            }
            await UploadParameterZipToS3();
        }

        private async Task UploadParameterZipToS3()
        {
            S3Helper s3Helper = new S3Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color,
                GenerationS3BucketName()
                );

            await s3Helper.UploadFile("parameter.zip", new FileStream(@"c:\temp\parameter.zip", FileMode.Open));
        } 

        private string GetJsonFilesFolder()
        {
            return ConfigurationSettings.AppSettings["ParammeterFilesFolder"] + GlobalVariables.Enviroment;
        }

        private string GenerationS3BucketName()
        {
            return string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "parameters");
        }

        private void FormParametersEditor_Load(object sender, EventArgs e)
        {
            BindFolder(GetJsonFilesFolder());
        }
    }
}
