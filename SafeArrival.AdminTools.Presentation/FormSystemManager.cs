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

        private void FormSystemManager_Load(object sender, EventArgs e)
        {
            try
            {
                listView1.View = View.Details;
                listView1.Columns.Add("Name", 200);
                listView1.Columns.Add("Max Size", 70);
                listView1.Columns.Add("Min Size", 70);
                listView1.Columns.Add("Running Instances", 120);

                PopulateSystemStatus();
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
                    PopulateSystemStatus();
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
                    PopulateSystemStatus();
                    WriteNotification(String.Format("{0} system is stoping!", GlobalVariables.Enviroment.ToString()));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
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

        private void PopulateSystemStatus()
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
            PopulateAutoScalingGroup();
            PopulateRDS();
            LoadAutoScalingGroupSettings();
        }

        private void PopulateAutoScalingGroup()
        {
            listView1.Items.Clear();
            AwsUtilities.AutoScalingHelper helper = new AwsUtilities.AutoScalingHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region);
            var lstGroup = helper.GetAutoScalingGroupList();
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

        private void LoadAutoScalingGroupSettings()
        {
            SystemManagement service = new SystemManagement();
            lstAutoScalingGroupSettings = service.GetAutoScalingGroupSettingsByEnv(GlobalVariables.Enviroment);
            dataGridView1.DataSource = lstAutoScalingGroupSettings;
        }

        private void PopulateRDS()
        {
            AwsUtilities.RDSHelper helper = new AwsUtilities.RDSHelper(
                GlobalVariables.Enviroment,
                GlobalVariables.Region);
            var instance = helper.GetRDSInstance();
            lblRdsIdentifier.Text = instance.DBInstanceIdentifier;
            lblRdsArn.Text = instance.DBInstanceArn;
            lblRdsStatus.Text = instance.Status;
            lblRdsMutlAZ.Text = instance.MultiAZ.ToString();
        }

        private void btnSave_ASG_Settings_Click(object sender, EventArgs e)
        {
            SystemManagement service = new SystemManagement();
            service.SaveAllAutoScalingGroupSettings(lstAutoScalingGroupSettings);
        }
    }
}
