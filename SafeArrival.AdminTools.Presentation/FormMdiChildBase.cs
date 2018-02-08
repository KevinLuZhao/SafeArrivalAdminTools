using SafeArrival.AdminTools.BLL;
using SafeArrival.AdminTools.Model;
using System;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public class FormMdiChildBase : Form
    {
        //public event EventHandler EnvironmentChanged;
        Timer timer = new Timer();
        public virtual void OnEnvironmentChanged() { }

        public virtual void HandleException(Exception ex)
        {
            LogServices.WriteLog(ex.Message + " Stack Trace: " + ex.StackTrace, Model.LogType.Error, GlobalVariables.Enviroment.ToString());
            NotifyToMainStatus(ex.Message, System.Drawing.Color.Red);
        }

        public virtual void WriteNotification(string message)
        {
            LogServices.WriteLog(message, Model.LogType.Information, GlobalVariables.Enviroment.ToString());
            NotifyToMainStatus(message, System.Drawing.Color.ForestGreen);
        }

        protected void NotifyToMainStatus(string message, System.Drawing.Color color)
        {
            try
            {
                ((FrmMain)this.ParentForm).MainStatusStrip.Text = message;
                ((FrmMain)this.ParentForm).MainStatusStrip.ForeColor = color;

                timer.Tick += TickerTicked;
                timer.Interval = 30000;
                timer.Start();
            }
            catch
            {
                ;
            }
        }

        private void TickerTicked(object sender, EventArgs args)
        {
            try
            {
                ((FrmMain)this.ParentForm).MainStatusStrip.Text = string.Empty;
                timer.Stop();
            }
            catch (Exception ex)
            {
                NotifyToMainStatus(ex.Message, System.Drawing.Color.Red);
            }
        }
    }
}
