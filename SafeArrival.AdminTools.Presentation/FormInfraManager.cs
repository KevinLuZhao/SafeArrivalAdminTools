using LibGit2Sharp;
using SafeArrival.AdminTools.BLL;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormInfraManager : FormMdiChildBase
    {
        private InfraManagementService service;
        private List<SA_Stack> stacks;
        private bool stopFlag = false;
        public FormInfraManager()
        {
            InitializeComponent();
            service = new InfraManagementService();
        }

        public override void OnEnvironmentChanged()
        {
            try
            {
                PopulateStacks();
                PopulateCodePipelineInfo();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void FormInfraManager_Load(object sender, EventArgs e)
        {
            try
            {
                int counter = 0;
                while (counter < cListBoxApps.Items.Count)
                {
                    cListBoxApps.SetItemChecked(counter, true);
                    counter++;
                }
                btnSuspend.Enabled = false;
                await PopulateStacks();
                await PopulateCodePipelineInfo();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            stopFlag = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(ConfigurationManager.AppSettings["InfraFileFolder"]);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void btnCmd_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.WorkingDirectory = ConfigurationManager.AppSettings["InfraFileFolder"];
                p.StartInfo.UseShellExecute = false;
                p.Start();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["GithubToken"].Trim() == string.Empty)
                {
                    MessageBox.Show("Please set value to GithubToken in config file.");
                }
                var radioLevels = new List<RadioButton>();
                radioLevels.Add(radioButton1);
                radioLevels.Add(radioButton2);
                radioLevels.Add(radioButton3);
                //radioLevels.Add(radioButtonDNS);
                int level = int.Parse(radioLevels.Find(o => o.Checked).Tag.ToString());
                if (level <= 2)
                {
                    await service.BuildCodePipelinelevel_1And2(
                        level,
                        ConfigurationManager.AppSettings["GithubToken"]);
                    NotifyToMainStatus(
                        $"{GlobalVariables.Enviroment}-level-{level}-{GlobalVariables.Color} is created.",
                        System.Drawing.Color.Green);
                }
                else if (level == 3)
                {
                    var apps = new List<string>();
                    if (cListBoxApps.CheckedItems.Count == 0)
                        return;
                    foreach (var item in cListBoxApps.CheckedItems)
                    {
                        apps.Add(item.ToString());
                    }
                    await service.BuildCodePipelinelevel_3(apps);
                    NotifyToMainStatus(
                        $"{GlobalVariables.Enviroment}-level-{level}-{GlobalVariables.Color} is created.",
                        System.Drawing.Color.Green);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                btnSuspend.Enabled = true;
                stopFlag = false;
                var selectedStacks = new List<string>();
                var selectedStackNames = new List<string>();
                foreach (DataGridViewRow row in gvStacks.Rows)
                {
                    var oCell = row.Cells["Select"] as DataGridViewCheckBoxCell;
                    bool bChecked = (null != oCell && null != oCell.Value && true == (bool)oCell.Value);
                    if (bChecked)
                    {
                        selectedStacks.Add(row.Cells[1].Value.ToString());
                        selectedStackNames.Add(row.Cells["StackName"].Value.ToString());
                    }
                }
                var confirmResult = MessageBox.Show(
                        $"Are you sure to delete the {selectedStacks.Count} stacks: {string.Join(", ", selectedStacks)}?",
                        "Confirm Switching Deployments",
                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    WriteNotification($"Begin to delete stacks: {string.Join(", ", selectedStacks)}.");
                    foreach (var stackName in selectedStackNames)
                    {
                        var response = await service.DeleteStack(stackName, DeleteCallBack);
                        WriteNotification($"Deleting stack: {stackName} complete. Deleting result: {response}");
                        if (stopFlag)
                        {
                            MessageBox.Show("Deleting stacks is cancelled by user");
                            btnSuspend.Enabled = false;
                            return;
                        }
                        await PopulateStacks();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void DeleteCallBack(System.IAsyncResult result)
        {
            await PopulateStacks();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await PopulateStacks();
        }

        private async void btnCreateSisEvent_Click(object sender, EventArgs e)
        {
            try
            {
                await service.SetSisEventTrigger();
                WriteNotification($"{GlobalVariables.Enviroment} SIS event trigger has been successfully created.");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnSetDns_Click(object sender, EventArgs e)
        {
            try
            {
                await service.SetDNS();
                WriteNotification($"{GlobalVariables.Enviroment} DNS is set.");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task PopulateStacks()
        {
            var stacks = await service.GetStackList();

            gvStacks.AutoGenerateColumns = false;
            gvStacks.ReadOnly = false;
            gvStacks.DataSource = stacks;

            this.stacks = stacks;
        }

        private async Task PopulateCodePipelineInfo()
        {
            string path = ConfigurationManager.AppSettings["InfraFileFolder"];
            if (LibGit2Sharp.Repository.IsValid(path))
            {
                var repo = new Repository(path);
                lblBranchName.Text = repo.Head.FriendlyName;
            }
            else
            {
                lblBranchName.Text = $"No repository is found from the path of {path}, please check your config file";
            }
            gvCodePiplines.AutoGenerateColumns = false;
            gvCodePiplines.DataSource = await service.GetCodePipelinList();
        } 
    }
}
