using SafeArrival.AdminTools.BLL;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            tsComboEnv.ComboBox.DataSource = Enum.GetValues(typeof(Model.Environment));
            tsComboEnv.SelectedIndex = 1;
            tsComboColor.ComboBox.DataSource = Enum.GetValues(typeof(Model.Color));
            tsComboColor.SelectedIndex = 0;

            List<KeyValuePair<string, string>> lstRegions = Regions.GetRegionList();
            tsComboRegion.ComboBox.DataSource = lstRegions;
            tsComboRegion.ComboBox.DisplayMember = "Value";
            tsComboRegion.ComboBox.ValueMember = "Key";
            //tsComboRegion.ComboBox.SelectedIndex = 1;
            tsComboRegion.Enabled = false;

            MainStatusStrip = toolStripStatusLabel1;

            if (ConfigurationManager.AppSettings["Role"] != "SAFE-Admin_role")
            {
                parameterEditToolStripMenuItem.Visible = false;
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
                case "parameterEditToolStripMenuItem":
                    if (!Directory.Exists(ConfigurationManager.AppSettings["ParammeterFilesFolder"]))
                    {
                        MainStatusStrip.Text = string.Format(
                            "Parameter Editor need the system directory {0}. This directory is defined at config file",
                            ConfigurationManager.AppSettings["ParammeterFilesFolder"]);
                    }
                    frm = new FormParametersEditor();
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
            GlobalVariables.Enviroment = (Model.Environment)Enum.Parse(typeof(Model.Environment), tsComboEnv.SelectedItem.ToString(), true);
            GlobalVariables.Region = GlobalVariables.EnvironmentAccounts[GlobalVariables.Enviroment.ToString()].Region;
            tsComboRegion.SelectedItem = Regions.GetRegionList().Find(o => o.Key == GlobalVariables.Region);

            foreach (var frm in OpendFormList)
            {
                frm.OnEnvironmentChanged();
            }
        }

        private void tsComboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GlobalVariables.Region = ((KeyValuePair<string, string>)tsComboRegion.SelectedItem).Key;
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
    }
}
