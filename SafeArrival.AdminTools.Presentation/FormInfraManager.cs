using LibGit2Sharp;
using SafeArrival.AdminTools.BLL;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormInfraManager : FormMdiChildBase
    {
        private InfraManagementService service;
        private List<SA_Stack> stacks;
        private bool stopFlag = false;
        private List<SA_PipelineSummary> codePipelineList;
        private List<DeployDnsModel> availableDnsSets;

        public FormInfraManager()
        {
            InitializeComponent();
            service = new InfraManagementService();
        }

        public override async void OnEnvironmentChanged()
        {
            try
            {
                await PopulateCodePipelineInfo();
                await PopulateLiveColorEnv();
                await PopulateCodePipelinesCICDStatus();
                await PopulateCurrentPublicDNS();
                await PopulateStacks();
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
                await PopulateCodePipelineInfo();
                await PopulateLiveColorEnv();
                await PopulateCodePipelinesCICDStatus();
                await PopulateCurrentPublicDNS();
                await PopulateStacks();
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
            if (!CheckGitRepository())
            {
                return;
            }
            var radioLevels = new List<RadioButton>();
            radioLevels.Add(radioButton1);
            radioLevels.Add(radioButton2);
            radioLevels.Add(radioButton3);
            int level = int.Parse(radioLevels.Find(o => o.Checked).Tag.ToString());

            var confirmResult = MessageBox.Show(
                        $"Are you sure to create code pipeline level {level}?",
                        "Confirm Creating Stacks",
                        MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string githubTokenPath = Path.Combine(
                        System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "gittoken");
                    string gitToken = File.ReadAllText(githubTokenPath);

                    string levelName = string.Empty;
                    if (level == 1)
                    {
                        levelName = $"{GlobalVariables.Enviroment}-level-{level}";
                    }
                    else if (level == 2)
                    {
                        levelName = $"{GlobalVariables.Enviroment}-level-{level}-{GlobalVariables.Color}";
                    }

                    if (level <= 2)
                    {
                        if (codePipelineList.Find(o => o.Name == levelName) != null)
                        {
                            MessageBox.Show($"{levelName} exists. Please teardown cloudformation stacks before create.");
                            return;
                        }
                        await service.BuildCodePipelinelevel_1And2(level, gitToken);
                        NotifyToMainStatus(
                            $"{levelName} is created.", System.Drawing.Color.Green);
                    }
                    else if (level == 3)
                    {
                        var apps = new List<string>();
                        if (cListBoxApps.CheckedItems.Count == 0)
                            return;
                        foreach (var item in cListBoxApps.CheckedItems)
                        {
                            levelName = $"{GlobalVariables.Enviroment}-level-{level}-{GlobalVariables.Color}-{item.ToString()}";
                            if (codePipelineList.Find(o => o.Name == levelName) != null)
                            {
                                MessageBox.Show($"{levelName} exists. Please teardown cloudformation stacks before create.");
                                return;
                            }
                            apps.Add(item.ToString());
                        }
                        await service.BuildCodePipelinelevel_3(apps);
                        NotifyToMainStatus(
                            $"{GlobalVariables.Enviroment}-level-{level}-{GlobalVariables.Color} is created.",
                            System.Drawing.Color.Green);
                    }
                    await PopulateStacks();
                    await PopulateCodePipelineInfo();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
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
                        "Confirm Deleting Stacks",
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
                        await PopulateCodePipelineInfo();
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
            if (!CheckGitRepository())
            {
                return;
            }
            try
            {
                string s3Name = $"safe-arrival-{GlobalVariables.Region}-{GlobalVariables.Enviroment}-sisbucket";
                string lambdaName = $"SafeArrival-SIS-{GlobalVariables.Enviroment.ToString()}-{GlobalVariables.Color}";
                string eventName = $"sis-raw-event-{GlobalVariables.Enviroment.ToString()}";
                var confirmResult = MessageBox.Show(
                        $"Are you sure to create following SIS event?{Environment.NewLine}S3 Bucket Name: {s3Name}{Environment.NewLine}Event Name: {eventName}{Environment.NewLine}Lambda Name: {lambdaName}",
                        "Teamcity Build",
                        MessageBoxButtons.YesNo);
                if (confirmResult== DialogResult.Yes)
                {
                    await service.SetSisEventTrigger(s3Name, lambdaName, eventName);
                    WriteNotification($"{GlobalVariables.Enviroment} SIS event trigger has been successfully created.");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnSetDns_Click(object sender, EventArgs e)
        {
            if (!CheckGitRepository())
            {
                return;
            }
            try
            {
                await service.SetDNS();
                WriteNotification($"{GlobalVariables.Enviroment} DNS is set.");
                await PopulateStacks();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void btnTcBuild_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = MessageBox.Show(
                        $"Are you sure to run the Teamcity build on {GlobalVariables.Enviroment}?",
                        "Teamcity Build",
                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var tcService = new TeamCityService();
                    tcService.RunBuildConfig(GlobalVariables.EnvironmentAccounts[GlobalVariables.Enviroment].TCBuildConfig);
                    WriteNotification($"Building the appliction zip files on {GlobalVariables.Enviroment}.");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void linkLabelTeamcity_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = $"http://{Utils.GetTeamcityCredential().HostName}/project.html?projectId=AwsCompleteDeploy";
            Process.Start(url);
        }

        private async Task PopulateStacks()
        {
            var stacks = await service.GetStackList();

            gvStacks.AutoGenerateColumns = false;
            gvStacks.ReadOnly = false;
            gvStacks.DataSource = stacks;

            this.stacks = stacks;
        }

        private async void btnSwitchDnsTarget_Click(object sender, EventArgs e)
        {
            string type = ((Button)sender).Tag.ToString();
            try
            {
                var confirmResult = MessageBox.Show(
                        $"Are you sure to switch {GlobalVariables.EnvironmentAccounts[GlobalVariables.Enviroment].DNS} pointing to {type}?",
                        "Set DNS Target",
                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    if (availableDnsSets == null)
                    {
                        availableDnsSets = await service.GetAvailableDnsList();
                    }
                    lblAdmin.Text = string.Empty;
                    await service.SwitchDnsTarget(availableDnsSets, type);
                    WriteNotification(
                        $"Has switch {GlobalVariables.EnvironmentAccounts[GlobalVariables.Enviroment].DNS} pointing to {type}.");
                    await Task.Delay(5000);
                    await PopulateCurrentPublicDNS();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnSwitchLeve3CICDMode_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = MessageBox.Show(
                        $"Are you sure to switch the level 3 code pipeline CI/CD mode?",
                        "Code Pipeline CI/CD Mode",
                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    await service.SwitchLevel3CicdMode();
                    await PopulateCodePipelinesCICDStatus();
                    WriteNotification($"Has switched the level 3 code pipeline CI/CD mode.");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnSwitchLeve2CICDMode_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = MessageBox.Show(
                        $"Are you sure to switch the level 2 code pipeline CI/CD mode?",
                        "Code Pipeline CI/CD Mode",
                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    await service.SwitchLevel2CicdMode();
                    await PopulateCodePipelinesCICDStatus();
                    WriteNotification($"Has switched the level 2 code pipeline CI/CD mode.");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task PopulateCodePipelineInfo()
        {
            codePipelineList = await service.GetCodePipelineList();
            lblBranchName.Text = GetLocalRepositoryBranch();
            gvCodePiplines.AutoGenerateColumns = false;
            gvCodePiplines.DataSource = codePipelineList;
        }

        private async Task PopulateCodePipelinesCICDStatus()
        {
            var ret = await service.GetAllColdPipelinesCicdStatus();
            lblCicdStatus.Text = "";
            foreach (var status in ret)
            {
                lblCicdStatus.Text += $"{status.Key}:{status.Value.ToString()}      ";
            }
        }

        private async Task PopulateLiveColorEnv()
        {
            lblLiveColor.Text = await service.GetLiveColorEnv();
        }

        private async Task PopulateCurrentPublicDNS()
        {
            var dnsList = await service.GetCurrentPublicDnsList();
            lblAdmin.Text = string.Join(System.Environment.NewLine, dnsList);
            //lblAdmin.Text = dnsList.Find(o => o.ToLower().Contains("admin"));
            //lblSuper.Text = dnsList.Find(o => o.ToLower().Contains("super"));
        }

        private bool CheckGitRepository()
        {
            string githubTokenPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile), "gittoken");
            if (!File.Exists(githubTokenPath))
            {
                MessageBox.Show($"Please set value to {githubTokenPath}.");
                return false;
            }

            var gitBranch = GetLocalRepositoryBranch();
            var ret = (GlobalVariables.Enviroment.ToString() == gitBranch);
            if (!ret)
                MessageBox.Show($"Current target environment is '{GlobalVariables.Enviroment.ToString().ToUpper()}', while the Github local branch is '{gitBranch.ToUpper()}'. Please check.");
            return ret;
        }

        private string GetLocalRepositoryBranch()
        {
            string path = ConfigurationManager.AppSettings["InfraFileFolder"];
            if (LibGit2Sharp.Repository.IsValid(path))
            {
                var repo = new Repository(path);
                return repo.Head.FriendlyName;
            }
            else
            {
                return $"No repository is found from the path of {path}, please check your config file";
            }
        }
    }
}
