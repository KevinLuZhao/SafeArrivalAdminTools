using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.BLL;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FrmMain : Form
    {
        public ToolStripStatusLabel MainStatusStrip;
        public List<FormMdiChildBase> OpendFormList = new List<FormMdiChildBase>();
        public FrmMain()
        {
            InitializeComponent();
            EnvironmentAccountService service = new EnvironmentAccountService();
            GlobalVariables.EnvironmentAccounts = service.GetEnvironmentAccounts();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> lstRegions = Regions.GetRegionList();
            tsComboRegion.ComboBox.DataSource = lstRegions;
            tsComboRegion.ComboBox.DisplayMember = "Value";
            tsComboRegion.ComboBox.ValueMember = "Key";
            //tsComboRegion.ComboBox.SelectedIndex = 1;
            tsComboRegion.Enabled = false;

            tsComboEnv.ComboBox.DataSource = AwsUtilities.AwsCommon.GetEnvironmentList();
            tsComboEnv.SelectedIndex = 0;
            tsComboColor.ComboBox.DataSource = Enum.GetValues(typeof(Model.Color));
            tsComboColor.SelectedIndex = 0;

            MainStatusStrip = toolStripStatusLabel1;
            var computer = Application.CompanyName;
            IAMHelper helper = new IAMHelper();
            helper.GetCurrentUser();
            if (!Utils.IsSuperAdmin())
            {
                infrastructureManagerToolStripMenuItem.Visible = false;
                greenBlueDeploymentToolStripMenuItem.Visible = false;
                helpToolStripMenuItem.Visible = false;
            }
        }

        private void OpenFormMenu_Click(object sender, EventArgs e)
        {
            //If the form is already opened, just make it active.
            foreach (var form in OpendFormList)
            {
                if (form.GetType().Name == ((ToolStripMenuItem)sender).Tag.ToString())
                {
                    form.Activate();
                    return;
                }
            }
            FormMdiChildBase frm = OpenMDIChildForm(((ToolStripMenuItem)sender).Name);
            if (frm != null)
            {
                OpendFormList.Add(frm);
                frm.MdiParent = this;
                frm.FormClosed += new FormClosedEventHandler(MDIChildFormClosed);
                //frm.Activate();
                frm.Show();
                frm.WindowState = FormWindowState.Maximized;
            }
        }

        private FormMdiChildBase OpenMDIChildForm(string formName)
        {
            FormMdiChildBase frm = null;
            switch (formName)
            {
                case "deliveryManagerToolStripMenuItem":
                    if (!Directory.Exists(ConfigurationManager.AppSettings["ParammeterFilesFolder"]))
                    {
                        MainStatusStrip.Text = string.Format(
                            "Parameter Editor need the system directory {0}. This directory is defined at config file",
                            ConfigurationManager.AppSettings["ParammeterFilesFolder"]);
                    }
                    frm = new FormDeliveryManager();
                    break;
                case "s3MonitorToolStripMenuItem":
                    frm = new FormS3Monitor();
                    break;
                case "systemManagerToolStripMenuItem":
                    frm = new FormSystemManager();
                    break;
                case "greenBlueDeploymentToolStripMenuItem":
                    frm = new FormDeploymentSwitch();
                    break;
                case "viewLogsToolStripMenuItem":
                    frm = new FormLogs();
                    break;
                case "infrastructureManagerToolStripMenuItem":
                    frm = new FormInfraManager();
                    break;
            }
            return frm;
        }

        private void tsComboEnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVariables.Enviroment = AwsUtilities.AwsCommon.GetEnvironmentList().Find(o => o == tsComboEnv.SelectedItem.ToString());
            GlobalVariables.Region = GlobalVariables.EnvironmentAccounts[GlobalVariables.Enviroment.ToString()].Region;
            tsComboRegion.SelectedItem = Regions.GetRegionList().Find(o => o.Key == GlobalVariables.Region);
            foreach (var frm in OpendFormList)
            {
                frm.OnEnvironmentChanged();
            }
            tsComboRegion.Focus();
        }

        private void tsComboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVariables.Color = tsComboColor.SelectedItem.ToString();
            foreach (var frm in OpendFormList)
            {
                frm.OnEnvironmentChanged();
            }
        }

        private void FrmMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                foreach (ToolStripMenuItem menu in menuStrip1.Items)
                {
                    menu.BackColor = System.Drawing.Color.Transparent;
                }
                return;
            }
            foreach (ToolStripMenuItem menu in menuStrip1.Items)
            {
                if (menu.Tag != null && menu.Tag.ToString() == this.ActiveMdiChild.Name)
                {
                    menu.BackColor = System.Drawing.Color.PowderBlue;
                }
                else
                {
                    menu.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }

        private void MDIChildFormClosed(object sender, FormClosedEventArgs e)
        {
            var s = sender;
            OpendFormList.Remove((FormMdiChildBase)sender);// OpendFormList.Find(o=>o.GetType().FullName == sender.GetType().FullName))
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),"resources", "SafeArrivalAdminToolKits.docx");
            Process.Start(path);
        }
    }
}