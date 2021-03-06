﻿using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.BLL;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormSystemManager : FormMdiChildBase
    {

        private List<AutoScalingGroupSettings> lstAutoScalingGroupSettings = new List<AutoScalingGroupSettings>();
        private List<SA_RdsInstance> lstRdsInstances = new List<SA_RdsInstance>();
        private List<SA_Vpc> lstVpcs = new List<SA_Vpc>();
        private SA_PeeringConnection vpcPeeringConnection;

        public FormSystemManager()
        {
            InitializeComponent();
        }

        public override async void OnEnvironmentChanged()
        {
            try
            {
                await PopulateSystemStatus();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void FormSystemManager_Load(object sender, EventArgs e)
        {
            try
            {
                listView1.View = View.Details;
                listView1.Columns.Add("Name", 200);
                listView1.Columns.Add("Max Size", 70);
                listView1.Columns.Add("Min Size", 70);
                listView1.Columns.Add("Running Instances", 120);

                listView2.View = View.Details;
                listView2.Columns.Add("Name", 200);
                listView2.Columns.Add("Desired Capacity", 120);
                listView2.Columns.Add("Max Size", 70);
                listView2.Columns.Add("Min Size", 70);
                listView2.Columns.Add("Recurrence", 120);
                listView2.Columns.Add("Start Time", 150);
                listView2.Columns.Add("End Time", 150);
                listView2.Columns.Add("Suspended", 70);

                PopulateEc2Regions();
                await PopulateSystemStatus();
                gvAsgSettings.AutoGenerateColumns = false;
                gvRDS.AutoGenerateColumns = false;
                gvAsg.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task PopulateSystemStatus()
        {
            if (GlobalVariables.Enviroment.ToString().IndexOf("production") >= 0)
            {
                MessageBox.Show("Starting/Stopping System is not supported in Production envirnment!");
                //Enabled = false;
                btnSystemStart.Visible = false;
                btnSystemStop.Visible = false;
                btnSystemRefresh.Visible = false;
                btnAsgSettingsInit.Enabled = btnAsgSettingsSave.Enabled = false;
            }
            else
            {
                //Enabled = true;
                btnSystemStart.Visible = true;
                btnSystemStop.Visible = true;
                btnSystemRefresh.Visible = true;
                btnAsgSettingsInit.Enabled = btnAsgSettingsSave.Enabled = true;
            }

            await PopulateAutoScalingGroup();
            await PopulateScheduleActions();
            await PopulateRDS();
            LoadAutoScalingGroupSettings();
            await PopulatePeeringConnection();
            await PopulateeeringConnectionVpcDropdownLists();
            await PopulateRDSList();
            await PopulateEc2Instances();
            await PopulateAsgList();
            await PopulateSnapshots();
        }

        //-----------------------------------------------Envroment Status--------------------------------------------------------------
        private async void btnSystemStart_Click(object sender, EventArgs e)
        {
            try
            {
                tabCtrlMain.SelectedIndex = 1;
                var confirmResult = MessageBox.Show(
                            string.Format("Are you sure to start all instances in: '{0}'? Pleae check the settings first and then click YES button!",
                            GlobalVariables.Enviroment.ToString()),
                            "Confirm Start System",
                            MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    imgAppStatus.Image = Properties.Resources.Button_Blank_Red_icon;
                    imgRdsStatus.Image = Properties.Resources.Button_Blank_Red_icon;
                    imgScheduleStatus.Image = Properties.Resources.Button_Blank_Red_icon;
                    var manager = new SystemManagement();
                    await manager.StartSystem(lstAutoScalingGroupSettings, cboxRdsMutlAZ.Checked);
                    WriteNotification($"{GlobalVariables.Enviroment.ToString()} system is started!");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                await PopulateSystemStatus();
            }
        }

        private async void btnSystemStop_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = MessageBox.Show(
                        string.Format("Are you sure to stop all instances in: '{0}'? If you click the system will be unavailable!",
                        GlobalVariables.Enviroment.ToString()),
                        "Confirm Shut Down System",
                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    imgAppStatus.Image = Properties.Resources.Button_Blank_Red_icon;
                    imgRdsStatus.Image = Properties.Resources.Button_Blank_Red_icon;
                    imgScheduleStatus.Image = Properties.Resources.Button_Blank_Red_icon;
                    var manager = new SystemManagement();
                    await manager.ShutDownSystem(cboxStopJumpbox.Checked);
                    WriteNotification(String.Format("{0} system is stopped!", GlobalVariables.Enviroment.ToString()));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                await PopulateSystemStatus();
            }
        }

        private async void btnSystemRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                await PopulateSystemStatus();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task PopulateScheduleActions()
        {
            listView2.Items.Clear();
            SystemManagement service = new SystemManagement();
            var lstAction = await service.GetApiScheduledActions();
            var counterSuspended = 0;
            foreach (var action in lstAction)
            {
                ListViewItem item = new ListViewItem();
                item.Text = action.Name;
                item.SubItems.Add(action.DesiredCapacity);
                item.SubItems.Add(action.MaxSize);
                item.SubItems.Add(action.MinSize);
                item.SubItems.Add(action.Recurrence);
                item.SubItems.Add(action.StartTime);
                item.SubItems.Add(action.EndTime);
                item.SubItems.Add(action.Suspend);
                listView2.Items.Add(item);
                if (action.Suspend == "True")
                {
                    counterSuspended++;
                }
            }
            if (0 == counterSuspended)
            {
                imgScheduleStatus.Image = Properties.Resources.Button_Blank_Green_icon;
            }
            else if (lstAction.Count == counterSuspended)
            {
                imgScheduleStatus.Image = Properties.Resources.Button_Blank_Gray_icon;
            }
            else
            {
                imgScheduleStatus.Image = Properties.Resources.Button_Blank_Red_icon;
            }
        }

        //-----------------------------------------------Auto-Scaling Settings--------------------------------------------------------------
        private void btnAsgSettingsInit_Click(object sender, EventArgs e)
        {
            SystemManagement service = new SystemManagement();
            service.InitAutoScalingGroupSettings(GlobalVariables.Enviroment);
        }

        private void btnAsgSettingsSave_Click(object sender, EventArgs e)
        {
            SystemManagement service = new SystemManagement();
            service.SaveAllAutoScalingGroupSettings(lstAutoScalingGroupSettings);
        }

        private async Task PopulateAutoScalingGroup()
        {
            var helper = new AwsUtilities.AutoScalingHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            var lstGroup = await helper.GetEnvironmentAutoScalingGroupList();
            int stoppedGroupCounter = 0;
            listView1.Items.Clear();
            foreach (var group in lstGroup)
            {
                ListViewItem item = new ListViewItem();
                item.Text = group.Name;
                item.SubItems.Add(group.MaxSize.ToString());
                item.SubItems.Add(group.MinSize.ToString());
                item.SubItems.Add(group.RunningInstances.ToString());
                listView1.Items.Add(item);
                if (group.RunningInstances == 0)
                    stoppedGroupCounter++;
            }
            if (stoppedGroupCounter == 0)
            {
                imgAppStatus.Image = Properties.Resources.Button_Blank_Green_icon;
                //return SystemStauts.Running;
            }

            else if (stoppedGroupCounter == lstGroup.Count)
            {
                imgAppStatus.Image = Properties.Resources.Button_Blank_Gray_icon;
                //return SystemStauts.Terminated;
            }
            else
            {
                imgAppStatus.Image = Properties.Resources.Button_Blank_Red_icon;
                //return SystemStauts.Abnormal;
            }
        }

        private void LoadAutoScalingGroupSettings()
        {
            SystemManagement service = new SystemManagement();
            lstAutoScalingGroupSettings = service.GetAutoScalingGroupSettingsByEnv(GlobalVariables.Enviroment);
            gvAsgSettings.DataSource = lstAutoScalingGroupSettings;
        }

        //-----------------------------------------------RDS Tab--------------------------------------------------------------
        private async Task<SystemStauts> PopulateRDS()
        {
            var helper = new AwsUtilities.RDSHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            var instance = await helper.GetRDSInstance();
            lblRdsIdentifier.Text = instance.DBInstanceIdentifier;
            lblRdsArn.Text = instance.DBInstanceArn;
            lblRdsStatus.Text = instance.Status;
            cboxRdsMutlAZ.Checked = instance.MultiAZ;
            if (instance.Status == "available")
            {
                imgRdsStatus.Image = Properties.Resources.Button_Blank_Green_icon;
                return SystemStauts.Running;
            }

            else if (instance.Status == "stopped")
            {
                imgRdsStatus.Image = Properties.Resources.Button_Blank_Gray_icon;
                return SystemStauts.Terminated;
            }
            else
            {
                imgRdsStatus.Image = Properties.Resources.Button_Blank_Red_icon;
                return SystemStauts.Abnormal;
            }
        }

        private async Task PopulateRDSList()
        {
            gvRDS.DataSource = null;
            lstRdsInstances.Clear();
            AwsUtilities.RDSHelper helper = new AwsUtilities.RDSHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            lstRdsInstances.AddRange(await helper.GetRDSInstanceList());
            gvRDS.DataSource = lstRdsInstances;
        }

        private async void btnRdsStop_Click(object sender, EventArgs e)
        {
            var service = new SystemManagement();
            var lstInstanceSelected = new List<SA_RdsInstance>();
            var strInstanceSelected = string.Empty;
            foreach (DataGridViewRow item in gvRDS.Rows)
            {
                if (item.Cells["ActionSelected"].Value == null)
                    continue;
                bool selected;
                if (bool.TryParse(item.Cells["ActionSelected"].Value.ToString(), out selected) &&
                    selected && item.Cells["Status"].Value.ToString() == "available")
                {
                    lstInstanceSelected.Add(lstRdsInstances.Find(
                        o => o.DBInstanceIdentifier == (string)item.Cells["DBInstanceIdentifier"].Value));
                    strInstanceSelected += (string)item.Cells["DBInstanceIdentifier"].Value + ",";
                }
            }
            if (lstInstanceSelected.Count > 0)
            {
                strInstanceSelected = strInstanceSelected.Substring(0, strInstanceSelected.Length - 1);
                var confirmResult = MessageBox.Show(
                            string.Format("RDS instances: {0} are selected. Pleae click YES button to stop them!",
                            strInstanceSelected),
                            "Confirm Stop RDS Instances",
                            MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {

                    try
                    {
                        NotifyToMainStatus($"RDS instances {strInstanceSelected} begin to stopped.", System.Drawing.Color.Green);
                        await service.StopRdsInstances(lstInstanceSelected);
                        await PopulateRDSList();
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        return;
                    }
                    WriteNotification($"RDS instances {strInstanceSelected} are stopped.");
                }
            }
        }

        private async void btnRdsStart_Click(object sender, EventArgs e)
        {
            var service = new SystemManagement();
            var lstInstanceSelected = new List<SA_RdsInstance>();
            var strInstanceSelected = string.Empty;
            foreach (DataGridViewRow item in gvRDS.Rows)
            {
                if (item.Cells["ActionSelected"].Value == null)
                    continue;
                bool selected;
                if (bool.TryParse(item.Cells["ActionSelected"].Value.ToString(), out selected) &&
                    selected && item.Cells["Status"].Value.ToString() == "stopped")
                {
                    lstInstanceSelected.Add(lstRdsInstances.Find(
                        o => o.DBInstanceIdentifier == (string)item.Cells["DBInstanceIdentifier"].Value));
                    strInstanceSelected += (string)item.Cells["DBInstanceIdentifier"].Value + ",";
                }
            }
            if (lstInstanceSelected.Count > 0)
            {
                strInstanceSelected = strInstanceSelected.Substring(0, strInstanceSelected.Length - 1);
                var confirmResult = MessageBox.Show(
                            string.Format("RDS instances: {0} are selected. Pleae click YES button to start them!",
                            strInstanceSelected),
                            "Confirm Start RDS Instances",
                            MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        NotifyToMainStatus($"RDS instances {strInstanceSelected} begin to start.", System.Drawing.Color.Green);
                        await service.StartRdsInstances(lstInstanceSelected);
                        await PopulateRDSList();
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        return;
                    }
                    WriteNotification($"RDS instances {strInstanceSelected} are started.");
                }
            }
        }

        private void btnRdsStart_Click_1(object sender, EventArgs e)
        {

        }

        private async void btnRdsRefresh(object sender, EventArgs e)
        {
            await PopulateRDSList();
        }

        //-----------------------------------------------EC2 Instances Tab--------------------------------------------------------------
        private void PopulateEc2Regions()
        {
            var lstRegions = Regions.GetRegionList();
            lstRegions.Add(new KeyValuePair<string, string>("All Regions", "All Regions"));
            ddlEC2Region.DataSource = lstRegions;
            ddlEC2Region.DisplayMember = "Value";
            ddlEC2Region.ValueMember = "Key";
            ddlEC2Region.SelectedValue = GlobalVariables.Region;

            ddlAsgRegion.DataSource = lstRegions;
            ddlAsgRegion.DisplayMember = "Value";
            ddlAsgRegion.ValueMember = "Key";
            ddlAsgRegion.SelectedValue = GlobalVariables.Region;
        }

        private async Task PopulateEc2Instances()
        {
            //gvEc2Instances.DataSource = null;
            var helper = new AwsUtilities.EC2Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            var selectRegion = ((KeyValuePair<string, string>)ddlEC2Region.SelectedItem).Key;
            var lstInstances = new List<SA_Ec2Instance>();
            if (selectRegion == "All Regions")
            {
                //var lstRegions = Regions.GetRegionList();
                //foreach (var region in lstRegions)
                //{
                //    lstInstances.AddRange(await helper.GetEc2InsatancesList(region.Key));
                //}
                lstInstances = await helper.GetEc2InsatancesList(null);
            }
            else
                lstInstances = await helper.GetEc2InsatancesList(((KeyValuePair<string, string>)ddlEC2Region.SelectedItem).Key);

            if (cboxStateFilter.Checked)
                gvEc2Instances.DataSource = lstInstances.FindAll(o => o.State == "running");
            else
                gvEc2Instances.DataSource = lstInstances;
            //gvEc2Instances.Sort(gvEc2InstancesColState, System.ComponentModel.ListSortDirection.Ascending);
        }

        private async void tsComboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            await PopulateEc2Instances();
        }

        private async void btnEc2Start_Click(object sender, EventArgs e)
        {
            var helper = new AwsUtilities.EC2Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            var instanceIds = new List<string>();

            foreach (DataGridViewRow row in gvEc2Instances.Rows)
            {
                if (row.Cells[5].Value != null && (bool)row.Cells[5].Value)
                {
                    instanceIds.Add(row.Cells[1].Value.ToString());
                }
            }
            var confirmResult = MessageBox.Show(
                          string.Format("Are you sure to start EC2 instances: '{0}'?",
                          string.Join(", ", instanceIds),
                          "Confirm Start EC2 Instances",
                          MessageBoxButtons.YesNo));
            if (confirmResult == DialogResult.Yes)
            {
                await helper.StartEc2Instances(instanceIds);
            }
        }

        private async void btnEc2Stop_Click(object sender, EventArgs e)
        {
            var helper = new AwsUtilities.EC2Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            var instanceIds = new List<string>();
            foreach (DataGridViewRow row in gvEc2Instances.Rows)
            {
                if (row.Cells[5].Value != null && (bool)row.Cells[5].Value)
                {
                    instanceIds.Add(row.Cells[1].Value.ToString());
                }
            }
            var confirmResult = MessageBox.Show(
                           string.Format("Are you sure to stop EC2 instances: '{0}'?",
                           string.Join(", ", instanceIds),
                           "Confirm Stop EC2 Instances",
                           MessageBoxButtons.YesNo));
            if (confirmResult == DialogResult.Yes)
            {
                await helper.StopEc2Instances(instanceIds);
            }
        }

        private async void btnEc2Refresh_Click(object sender, EventArgs e)
        {
            await PopulateEc2Instances();
        }

        //-----------------------------------------------Peering Connection Tab--------------------------------------------------------------

        private async Task PopulatePeeringConnection()
        {
            var service = new VpcPeeringConnectionServices();
            vpcPeeringConnection = await service.GetRdsPeeringConnection();
            if (vpcPeeringConnection == null || vpcPeeringConnection.Status.ToLower() == "deleted")
            {
                pnlNonExistRpc.Show();
                //pnlExistRpc.Visible = false;
                pnlExistRpc.Hide();
                pnlNonExistRpc.Location = pnlExistRpc.Location;
                //The peering connection which is under status of "deleted", 
                //should be considered as null when create a new one.
                vpcPeeringConnection = null;
            }
            else
            {
                pnlNonExistRpc.Hide();
                //pnlVPCPeeringConnection.Visible = false;
                pnlExistRpc.Show();
                pnlExistRpc.Visible = true;
                lblRpcId.Text = vpcPeeringConnection.VpcPeeringConnectionId;
                lblRpcReuestVpc.Text = vpcPeeringConnection.RequesterVpc;
                lblRpcAcceptVpc.Text = vpcPeeringConnection.AccepterVpc;
                lblRpcStatus.Text = vpcPeeringConnection.Status;
            }
        }

        private async Task PopulateeeringConnectionVpcDropdownLists()
        {
            ddlRequesterVpc.Items.Clear();
            ddlAccepterVpc.Items.Clear();
            var service = new VpcPeeringConnectionServices();
            lstVpcs = await service.GetAvailablePeeringVpcList();
            foreach (var vpc in lstVpcs)
            {
                ddlRequesterVpc.Items.Add($"{vpc.VpcId}|{vpc.Name}");
                ddlAccepterVpc.Items.Add($"{vpc.VpcId}|{vpc.Name}");
            }
        }

        private async void btnRpcCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (vpcPeeringConnection != null)
                {
                    MessageBox.Show("RDS VPC Peering Connection exists!");
                    return;
                }

                if (ddlAccepterVpc.SelectedItem == null ||
                    ddlRequesterVpc.SelectedItem == null)
                {
                    MessageBox.Show("Both Requester VPC and Accepter VPC are required!");
                    return;
                }

                if (ddlAccepterVpc.SelectedItem.ToString() == ddlRequesterVpc.SelectedItem.ToString())
                {
                    MessageBox.Show("VPC cannot create peering connection to itself, please pick another VPC");
                    return;
                }

                var confirmResult = MessageBox.Show(
                        "Are you sure to create a new RDS VPC Peering Connection?",
                        "Create RDS Peering Connection",
                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var services = new VpcPeeringConnectionServices();
                    var strRequesterItem = ddlRequesterVpc.SelectedItem.ToString()
                        .Substring(0, ddlRequesterVpc.SelectedItem.ToString().IndexOf("|"));
                    var strAccepterItem = ddlAccepterVpc.SelectedItem.ToString()
                        .Substring(0, ddlAccepterVpc.SelectedItem.ToString().IndexOf("|"));
                    var response = await services.CreatePeeringConnection(
                        lstVpcs.Find(o => o.VpcId == strRequesterItem),
                        lstVpcs.Find(o => o.VpcId == strAccepterItem));
                    WriteNotification($"RDS VPC Peering Connection {response} is created.");
                    await Task.Delay(3000);
                    await PopulatePeeringConnection();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnRpcDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (vpcPeeringConnection == null)
                    throw new Exception("There is no existing RDS VPC Peering Connection");

                var confirmResult = MessageBox.Show(
                        "Are you sure to delete the VPC Peering Connection?",
                        "Warn: Delete Peering Connection",
                        MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var service = new VpcPeeringConnectionServices();
                    await service.DeletePeeringConnection(
                        vpcPeeringConnection.VpcPeeringConnectionId,
                        lstVpcs.Find(o => o.VpcId == lblRpcReuestVpc.Text),
                        lstVpcs.Find(o => o.VpcId == lblRpcAcceptVpc.Text));
                    WriteNotification($"RDS VPC Peering Connection {vpcPeeringConnection.VpcPeeringConnectionId} is deleted.");
                    await Task.Delay(3000);
                    await PopulatePeeringConnection();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnRpcRefresh_Click(object sender, EventArgs e)
        {
            await PopulatePeeringConnection();
        }

        //-----------------------------------------------Auto Scaling Group Tab--------------------------------------------------------------

        private async void ddlAsgRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            await PopulateAsgList();
        }

        private async void btnAsgRefresh_Click(object sender, EventArgs e)
        {
            await PopulateAsgList();
        }

        private void SelectAsgEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gvAsg.Columns[6].Index && e.RowIndex != -1)
            {
                // Handle checkbox state change here
                if (((DataGridViewCheckBoxCell)gvAsg.CurrentCell).Selected)
                {
                    gvAsg.CurrentRow.Cells[2].ReadOnly = gvAsg.CurrentRow.Cells[3].ReadOnly = gvAsg.CurrentRow.Cells[4].ReadOnly = false;
                    gvAsg.CurrentRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
                }
                else
                {
                    gvAsg.CurrentRow.Cells[2].ReadOnly = gvAsg.CurrentRow.Cells[3].ReadOnly = gvAsg.CurrentRow.Cells[4].ReadOnly = true;
                    gvAsg.CurrentRow.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
        }

        private async void btnAsgUpdate_Click(object sender, EventArgs e)
        {
            var datasource = gvAsg.DataSource;
            var helper = new AwsUtilities.AutoScalingHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            int selectCol = gvAsg.ColumnCount - 1;
            foreach (DataGridViewRow row in gvAsg.Rows)
            {
                if (row.Cells[selectCol].Value != null && (bool)row.Cells[selectCol].Value)
                {
                    var rowDs = ((List<SA_AutoScalingGroup>)datasource)[row.Index];
                    var settings = new AutoScalingGroupSettings()
                    {
                        MaxSize = rowDs.MaxSize,
                        MinSize = rowDs.MinSize,
                        DesiredCapacity = rowDs.DesiredCapacity,
                        HealthCheckType = rowDs.HealthCheckType,
                        HealthCheckGracePeriod = rowDs.HealthCheckGracePeriod
                    };
                    var confirmResult = MessageBox.Show(
                            string.Format("Are you sure to change autoscaling group settings: '{0}' to be '{1}-{2}-{3}'? Pleae check the settings first and then click YES button!",
                            rowDs.AutoScalingGroupName, rowDs.MaxSize, rowDs.MinSize, rowDs.DesiredCapacity),
                            "Confirm Updating Auto Scaling Group Settings",
                            MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        await helper.ChangeScalingGroup(
                        rowDs.AutoScalingGroupName, settings,
                        ((KeyValuePair<string, string>)ddlAsgRegion.SelectedItem).Key);
                    }
                }
            }
        }

        private async Task PopulateAsgList()
        {
            var helper = new AwsUtilities.AutoScalingHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            gvAsg.AutoGenerateColumns = false;
            gvAsg.DataSource = await helper.GetAllAutoScalingGroupList(((KeyValuePair<string, string>)ddlAsgRegion.SelectedItem).Key);
            //foreach (DataGridViewRow row in gvAsg.Rows)
            //{
            //    row.DefaultCellStyle.BackColor = System.Drawing.Color.Green;
            //}
        }

        private async Task PopulateSnapshots()
        {
            var helper = new AwsUtilities.EC2Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region,
                GlobalVariables.Color);
            gvSnapshots.DataSource = await helper.GetAllSnapshots();
        }

        private async void cboxStateFilter_CheckedChanged(object sender, EventArgs e)
        {
            await PopulateEc2Instances();
        }
    }
}
