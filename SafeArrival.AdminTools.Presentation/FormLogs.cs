using SafeArrival.AdminTools.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormLogs : FormMdiChildBase
    {
        List<Model.Log> dataSource;
        public FormLogs()
        {
            InitializeComponent();
        }

        private void FormLogs_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> lstEnv = AwsUtilities.AwsCommon.GetEnvironmentList();
                lstEnv.Insert(0, string.Empty);
                ddlEnvironment.DataSource = lstEnv;
                ddlEnvironment.SelectedItem = GlobalVariables.Enviroment.ToString();

                List<string> lstLogType = (Enum.GetNames(typeof(Model.LogType))).OfType<string>().ToList();
                lstLogType.Insert(0, string.Empty);
                ddlType.DataSource = lstLogType;

                dtPickerFrom.Value = DateTime.Today.AddDays(-1);
                dtPickerTo.Value = DateTime.Today.AddDays(2);

                BindData();
                gvLogs.Columns[1].HeaderText = "Type";
                gvLogs.Columns[2].HeaderText = "Environment";
                gvLogs.Columns[0].Visible = gvLogs.Columns[4].Visible = false;
                gvLogs.Columns[1].Width = 70;
                gvLogs.Columns[2].Width = 80;
                gvLogs.Columns[3].Width = 660;
                gvLogs.Columns[5].Width = 100;
                gvLogs.Columns[6].Width = 120;
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
                BindData();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void BindData()
        {
            gvLogs.DataSource = dataSource = LogServices.GetLogList(
                ddlEnvironment.SelectedItem.ToString(),
                ddlType.SelectedItem.ToString(), 
                txtMessage.Text, dtPickerFrom.Value, dtPickerTo.Value);
        }

        private void gvLogs_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var log = dataSource.Find(o => o.Id == gvLogs.Rows[e.RowIndex].Cells[0].Value.ToString());
                var form = new FormLogDetails(log);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}
