using SafeArrival.AdminTools.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormLogs : FormMdiChildBase
    {
        public FormLogs()
        {
            InitializeComponent();
        }

        private void FormLogs_Load(object sender, EventArgs e)
        {
            gvLogs.DataSource = LogServices.GetLogList();
            gvLogs.Columns[0].Visible = gvLogs.Columns[4].Visible= false;
            gvLogs.Columns[3].Width = 680;
            gvLogs.Columns[5].Width = 60;
            gvLogs.Columns[6].Width = 120;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gvLogs.DataSource = LogServices.GetLogList();
        }
    }
}
