using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SafeArrival.AdminTools.Model;
using SafeArrival.AdminTools.BLL;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormSystemManager : FormMdiChildBase
    {

        private List<AutoScalingGroupSettings> lstAutoScalingGroupSettings = new List<AutoScalingGroupSettings>();

        public FormSystemManager()
        {
            InitializeComponent();
        }

        public override void OnEnvironmentChanged()
        {
            try
            {
                PopulateSystemStatus();
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

                await PopulateSystemStatus();
                dataGridView1.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            SystemManagement service = new SystemManagement();
            service.InitAutoScalingGroupSettings(GlobalVariables.Enviroment);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectedIndex = 1;
                var confirmResult = MessageBox.Show(
                            string.Format("Are you sure to start all instances in: '{0}'? Pleae check the settings first and then click YES button!",
                            GlobalVariables.Enviroment.ToString()),
                            "Confirm Start System",
                            MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var manager = new SystemManagement();
                    await manager.StartSystem(GlobalVariables.Enviroment, GlobalVariables.Region, lstAutoScalingGroupSettings);
                    await PopulateSystemStatus();
                    WriteNotification(String.Format("{0} system is starting!", GlobalVariables.Enviroment.ToString()));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnStop_Click(object sender, EventArgs e)
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
                    var manager = new SystemManagement();
                    await manager.ShutDownSystem(GlobalVariables.Enviroment, GlobalVariables.Region);
                    await PopulateSystemStatus();
                    WriteNotification(String.Format("{0} system is stoping!", GlobalVariables.Enviroment.ToString()));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
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

        private async Task PopulateSystemStatus()
        {
            if (GlobalVariables.Enviroment.ToString().IndexOf("production") >= 0)
            {
                MessageBox.Show("This function is not supported in Production envirnment!");
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
            }
            await PopulateAutoScalingGroup();
            await PopulateScheduleActions();
            await PopulateRDS();
            LoadAutoScalingGroupSettings();
            await PopulatePeeringConnection();
        }

        private async Task PopulateAutoScalingGroup()
        {
            listView1.Items.Clear();
            AwsUtilities.AutoScalingHelper helper = new AwsUtilities.AutoScalingHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region);
            var lstGroup = await helper.GetAutoScalingGroupList();
            foreach (var group in lstGroup)
            {
                ListViewItem item = new ListViewItem();
                item.Text = group.Name;
                item.SubItems.Add(group.MaxSize.ToString());
                item.SubItems.Add(group.MinSize.ToString());
                item.SubItems.Add(group.RunningInstances.ToString());
                listView1.Items.Add(item);
            }
        }

        private async Task PopulateScheduleActions()
        {
            listView2.Items.Clear();
            SystemManagement service = new SystemManagement();
            var lstAction = await service.GetApiScheduledActions();
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
            }
        }

        private void LoadAutoScalingGroupSettings()
        {
            SystemManagement service = new SystemManagement();
            lstAutoScalingGroupSettings = service.GetAutoScalingGroupSettingsByEnv(GlobalVariables.Enviroment);
            dataGridView1.DataSource = lstAutoScalingGroupSettings;
        }

        private async Task PopulateRDS()
        {
            AwsUtilities.RDSHelper helper = new AwsUtilities.RDSHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region);
            var instance = await helper.GetRDSInstance();
            lblRdsIdentifier.Text = instance.DBInstanceIdentifier;
            lblRdsArn.Text = instance.DBInstanceArn;
            lblRdsStatus.Text = instance.Status;
            lblRdsMutlAZ.Text = instance.MultiAZ.ToString();
        }

        private async Task PopulatePeeringConnection()
        {
            var helper = new AwsUtilities.EC2Helper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region);
            var connection = await helper.GetPeeringConnection(Utils.FormatresourceName(AwsResourceTypeName.RdsPeeringConnection));
            pnlNonExistRpc.Hide();
            pnlExistRpc.Hide();
            if (connection == null || connection.Status.ToLower() == "deleted")
            {
                pnlNonExistRpc.Show();
                //pnlExistRpc.Visible = false;
                pnlExistRpc.Hide();
                pnlNonExistRpc.Location = pnlExistRpc.Location;
            }
            else
            {
                pnlNonExistRpc.Hide();
                //pnlVPCPeeringConnection.Visible = false;
                pnlExistRpc.Show();
                pnlExistRpc.Visible = true;
                lblRpcId.Text = connection.VpcPeeringConnectionId;
                lblRpcReuestVpc.Text = connection.RequesterVpc;
                lblRpcAcceptVpc.Text = connection.AccepterVpc;
                lblRpcStatus.Text = connection.Status;
            }
        }

        private void btnSave_ASG_Settings_Click(object sender, EventArgs e)
        {
            SystemManagement service = new SystemManagement();
            service.SaveAllAutoScalingGroupSettings(lstAutoScalingGroupSettings);
        }

        private async void btnRpcCreate_Click(object sender, EventArgs e)
        {
            var services = new VpcPeeringConnectionServices();
            await services.CreatePeeringConnection();
            await PopulatePeeringConnection();
        }
    }
}
