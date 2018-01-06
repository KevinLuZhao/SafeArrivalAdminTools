using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SafeArrival.AdminTools.BLL;
using SafeArrival.AdminTools.Model;
using SafeArrival.AdminTools.Presentation.Controls;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormDeploymentSwitch : FormMdiChildBase
    {
        List<ApplicationLoadBalancerModel> applicationLoadBalancers = new List<ApplicationLoadBalancerModel>();

        public FormDeploymentSwitch()
        {
            InitializeComponent();
        }

        public override void OnEnvironmentChanged()
        {
            try
            {
                PopulateLoadBalancerControls();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void FormDeploymentSwitch_Load(object sender, EventArgs e)
        {
            try
            {
                await PopulateLoadBalancerControls();
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
                SwitchDeploymentService service = new SwitchDeploymentService();
                await service.GenerateExternalLoadBalancers();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async Task PopulateLoadBalancerControls()
        {
            btnCreate.Visible = false;
            lblWarn.Visible = false;
            SwitchDeploymentService service = new SwitchDeploymentService();
            applicationLoadBalancers = await service.GetApplicationLoadBalancers();
            if (applicationLoadBalancers == null || applicationLoadBalancers.Count == 0)
            {
                lblWarn.Visible = true;
                lblWarn.Text = "Application load balances are NOT create yet.";
                btnCreate.Visible = true;
                return;
            }
            if (applicationLoadBalancers.Count < (Enum.GetNames(typeof(ApplicationServer)).Count() - 1))
            {
                lblWarn.Visible = true;
                lblWarn.Text = "Not all application load balances are created. Please manually delete all load balancers and create again.";
                return;
            }
            int counter = 0;
            foreach (var alb in applicationLoadBalancers)
            {
                var ctrl = new CtrlApplicationLoadBalancer(alb);
                ctrl.Location = new Point(0, counter * 120);
                panel1.Controls.Add(ctrl);
                counter++;
            }
        }
    }
}
