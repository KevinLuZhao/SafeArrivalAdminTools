﻿using SafeArrival.AdminTools.BLL;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormDeliveryManager : FormMdiChildBase
    {
        private string _parameterFolder = string.Empty;
        private SoftwareDeliveryService service;
        private static List<string> applicationExportingLogs;
        private static List<string> backupExportingLogs;
        private UtilsGitManager gitManager = new UtilsGitManager();
        private FormDeliveryMgr_SeleFiles SelectAppFileForm;

        public List<string> SelectedAppFiles { get; set; }

        public FormDeliveryManager()
        {
            InitializeComponent();
            SelectedAppFiles = new List<string>();
        }

        public override void OnEnvironmentChanged()
        {
            BindFolders();
            lblBranchName.Text = gitManager.GetLocalRepositoryBranch();

            if (!Utils.IsSuperAdmin())
            {
                tabMain.SelectedTab = tabApplications;
                DisableButtons(tabCloudFormation);
                DisableButtons(tabParams);
                if (GlobalVariables.Enviroment.Contains("production"))
                {
                    DisableButtons(tabApplications);
                    txtAppsProcess.Text = "You do not have permission to publish apps on Production.";
                }
            }
        }

        private void FormParametersEditor_Load(object sender, EventArgs e)
        {
            OnEnvironmentChanged();
        }

        private void BindFolders()
        {
            BindFolder(GetJsonFilesFolder());
            BindInfraFolders();
            service = new SoftwareDeliveryService();
            SelectedAppFiles = service.GetApplicationZipFiles();
        }

        private void BindFolder(string dir)
        {
            try
            {
                var lstFile = new List<KeyValuePair<string, string>>();
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
                    $"Are you sure to upload parameter.zip to S3 bucket '{GenerationParamsS3BucketName()}'",
                    "Confirm Export to S3 Bucket",
                    MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var ret = await service.ExportParameters();
                    confirmResult = MessageBox.Show(ret, "Export cloudformation file to AWS", MessageBoxButtons.OK);
                    WriteNotification(ret);
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
            p.StartInfo.WorkingDirectory = ConfigurationManager.AppSettings["ParammeterFilesFolder"];
            p.StartInfo.UseShellExecute = false;
            p.Start();
        }


        private string GetJsonFilesFolder()
        {
            return ConfigurationManager.AppSettings["ParammeterFilesFolder"] + GlobalVariables.Enviroment;
        }

        private string GenerationParamsS3BucketName()
        {
            return string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "parameters");
        }

        //-----------------------------------------CloudFormation------------------------------------------
        private void BindInfraFolders()
        {
            try
            {
                var dir = new DirectoryInfo(ConfigurationManager.AppSettings["InfraFileFolder"]);
                var lstFile = new List<KeyValuePair<string, string>>();
                foreach (var subFolder in dir.GetDirectories())
                {
                    if (subFolder.Name == ".git")
                        continue;
                    lstFile.Add(new KeyValuePair<string, string>(subFolder.Name, "Folder" + subFolder.Name));
                    foreach (var file in subFolder.GetFiles())
                    {
                        lstFile.Add(new KeyValuePair<string, string>("    " + file.Name, file.FullName));
                    }
                }
                lstCFFiles.DataSource = lstFile;
                lstCFFiles.DisplayMember = "Key";
                lstCFFiles.ValueMember = "Value";
                lstCFFiles.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void lstCFFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstCFFiles.SelectedIndex < 0)
                {
                    txtCFViewer.Text = string.Empty;
                    return;
                }
                var itemName = ((KeyValuePair<string, string>)lstCFFiles.SelectedItem).Value;
                if (itemName.IndexOf("Folder") == 0)
                    return;
                using (StreamReader r = new StreamReader(itemName))
                {
                    txtCFViewer.Text = r.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnExportCF_Click(object sender, EventArgs e)
        {
            string message;
            if (!gitManager.CheckGitRepository(out message))
            {
                var confirmResult = MessageBox.Show(
                        $"{message} This operation can't be completed.",
                        "Warn: Evironment And Repository Branch Doesn't Match",
                        MessageBoxButtons.OK);
                return;
            }
            var confirmResult1 = MessageBox.Show(
                    $"Are you sure to upload safearrival-infra.zip to S3 bucket '{GeneratioCFS3BucketName()}'",
                    "Confirm Export to S3 Bucket",
                    MessageBoxButtons.YesNo);
            if (confirmResult1 == DialogResult.Yes)
            {
                var ret = await service.ExportCloudFormation();
                var confirmResult = MessageBox.Show(ret, "Export cloudformation file to AWS", MessageBoxButtons.OK);
                WriteNotification(ret);
            }
        }

        private string GeneratioCFS3BucketName()
        {
            return string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "cloudformation");
        }

        //-----------------------------------------Applications------------------------------------------
        private async void btnAppsExport_Click(object sender, EventArgs e)
        {
            var confirmResult1 = MessageBox.Show(
                    $"Will start the applications delivery process on {(GlobalVariables.Enviroment).ToUpper()}, please click 'Yes' to continue",
                    "Applications Delivery",
                    MessageBoxButtons.YesNo);
            if (confirmResult1 == DialogResult.Yes)
            {
                timer1.Enabled = true;
                btnAppsExport.Enabled = false;
                applicationExportingLogs = new List<string>();
                txtAppsProcess.Text = string.Empty;
                await service.DeliverApplications(applicationExportingLogs, cboxCopyApps.Checked, SelectedAppFiles, cboxUpdateLambdas.Checked, cboxUpdateVersions.Checked);
                //Wait 1 second to allow the DeliverApplications finish all tasks and write log to screen before the timer1 disabled, 
                //but the waiting will not block the main threa, to let the timer1_Tick keeps listening the applicationExportingLogs value changes.
                await Task.Run(() =>
                {
                    Thread.Sleep(5000);
                });
                btnAppsExport.Enabled = true;
                timer1.Enabled = false;

                var ret = $"Export applications to AWS. Operation includes: ";
                var counter = 1;
                if (cboxCopyApps.Checked)
                {
                    ret += $"{Environment.NewLine}{counter}. Copy zip files to S3 bucket: {string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "artifact")}\\application. ";
                    counter += 1;
                }
                if (cboxUpdateLambdas.Checked)
                {
                    ret += $"{Environment.NewLine}{counter}. Update lambda functions. ";
                    counter += 1;
                }
                if (cboxUpdateVersions.Checked)
                {
                    ret += $"{Environment.NewLine}{counter}. Update lambda function versions. ";
                }
                var confirmResult = MessageBox.Show(ret, "Export applications to AWS", MessageBoxButtons.OK);
                WriteNotification(ret);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //txtAppsProcess.Text = string.Join(Environment.NewLine, applicationExportingLogs);
            //txtAppsProcess.Text = string.Empty;
            string[] copy = new string[applicationExportingLogs.Count];
            applicationExportingLogs.CopyTo(copy);
            foreach (var text in copy)
            {
                if (text.IndexOf("Warn:") >= 0)
                {
                    AppendText(txtAppsProcess, System.Drawing.Color.Red, text + Environment.NewLine + Environment.NewLine);
                }
                else
                {
                    txtAppsProcess.AppendText(text + Environment.NewLine + Environment.NewLine);
                }
                txtAppsProcess.SelectionStart = txtAppsProcess.Text.Length;
                txtAppsProcess.ScrollToCaret();
            }
            applicationExportingLogs.RemoveRange(0, copy.Length);

            string[] copy2 = new string[backupExportingLogs.Count];
            backupExportingLogs.CopyTo(copy2);
            foreach (var text in copy2)
            {
                if (text.IndexOf("Warn:") >= 0)
                {
                    AppendText(txtBackup, System.Drawing.Color.Red, text + Environment.NewLine + Environment.NewLine);
                }
                else
                {
                    txtBackup.AppendText(text + Environment.NewLine + Environment.NewLine);
                }
                txtBackup.SelectionStart = txtBackup.Text.Length;
                txtBackup.ScrollToCaret();
            }
            backupExportingLogs.RemoveRange(0, copy2.Length);
        }

        void AppendText(RichTextBox box, System.Drawing.Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
            }
            box.SelectionLength = 0;
        }

        private void btnDeliverySave_Click(object sender, EventArgs e)
        {
            try
            {
                var path = "DeliveryLog.txt";
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                File.AppendAllText(path, txtAppsProcess.Text);
                var confirmResult = MessageBox.Show(
                        $"Delivery logs have been saved to '{Path.GetFullPath(path)}'. ",
                        "Save logs",
                        MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnPerview_Click(object sender, EventArgs e)
        {
            try
            {
                txtAppsProcess.Text = string.Empty;
                var data = await service.GeneratePreviewData();
                foreach (var dataRow in data)
                {
                    txtAppsProcess.AppendText(dataRow);
                }
            }
            catch (Exception ex)
            {

                HandleException(ex);
            }
        }

        private void GenerateSelectedAppFilesForm()
        {
            var applicationFiles = service.GetApplicationZipFiles();
            if (SelectAppFileForm == null)
            {
                SelectAppFileForm = new FormDeliveryMgr_SeleFiles(this, SelectedAppFiles, service.GetApplicationZipFiles());
            }
            SelectAppFileForm.ShowDialog();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GenerateSelectedAppFilesForm();
        }

        private async void btnBackupDB_Click(object sender, EventArgs e)
        {
            txtBackup.AppendText($"Taking database snapshot begin...{Environment.NewLine}");
            var deliverySvr = new SoftwareDeliveryService();
            var response = await deliverySvr.TakeDBSnapshot();
            txtBackup.AppendText($"Snapshot {response} is created.{Environment.NewLine}");
        }

        private async void btnBackupApplications_Click(object sender, EventArgs e)
        {
            txtBackup.AppendText($"Backup files begin...{Environment.NewLine}");
            backupExportingLogs = new List<string>();
            var deliverySvr = new SoftwareDeliveryService();
            await deliverySvr.BackupFiles(backupExportingLogs);
            foreach (var log in backupExportingLogs)
            {
                txtBackup.AppendText($"{log}{Environment.NewLine}");
            }
        }
    }
}
