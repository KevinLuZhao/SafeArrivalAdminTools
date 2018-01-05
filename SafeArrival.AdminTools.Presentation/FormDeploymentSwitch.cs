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

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormDeploymentSwitch : FormMdiChildBase
    {
        public FormDeploymentSwitch()
        {
            InitializeComponent();
        }

        public override void OnEnvironmentChanged()
        {
            try
            {
                //PopulateSystemStatus();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void FormDeploymentSwitch_Load(object sender, EventArgs e)
        {
                      
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var service = new SwitchDeploymentService();
                await service.GenerateExternalLoadBalancers();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
