using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.Presentation.Controls
{
    public partial class CtrlApplicationLoadBalancer : UserControl
    {
        ApplicationLoadBalancerModel _loadBalancerModel;
        public CtrlApplicationLoadBalancer(ApplicationLoadBalancerModel loadBalancerModel)
        {
            _loadBalancerModel = loadBalancerModel;
            InitializeComponent();
        }

        private void CtrlApplicationLoadBalancer_Load(object sender, EventArgs e)
        {
            lblName.Text = _loadBalancerModel.LoadBalancer.LoadBalancerName;
            lblDns.Text = _loadBalancerModel.LoadBalancer.DNSName;
            int pointX = 40;
            int pointY = 30;
            int counter = 0;
            foreach (var listener in _loadBalancerModel.Listeners)
            {
                Label lblPort = new Label();
                lblPort.Text = "Port: " + listener.Port.ToString();
                //var font = new Font(null,FontStyle.Bold  );
                lblPort.Font = new Font("Arial", 9, FontStyle.Bold);
                lblPort.Location = new Point(pointX, pointY + counter * 22);
                Label lblProtocol = new Label();
                lblProtocol.Text = "Protocol: " + listener.Protocol;
                lblProtocol.Location = new Point(pointX + 100, pointY + counter * 22);
                Label lblDefaultAction = new Label();
                lblDefaultAction.Text = "Default Action: forwad to " + listener.TargetArn;
                lblDefaultAction.Width = 1000;
                lblDefaultAction.ForeColor = listener.TargetArn.Contains("green") ? 
                    System.Drawing.Color.Green : System.Drawing.Color.Blue;
                lblDefaultAction.Location = new Point(pointX + 200, pointY + counter * 22);
                panel1.Controls.Add(lblPort);
                panel1.Controls.Add(lblProtocol);
                panel1.Controls.Add(lblDefaultAction);
                counter = counter + 1;
            }
        }
    }
}
