using SafeArrival.AdminTools.DAL;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeArrival.AdminTools.BLL
{
    public class EnvironmentAccountService
    {
        EnvironmentAccountDb db = new EnvironmentAccountDb();
        public Dictionary<string, EnvironmentAccount> GetEnvironmentAccounts()
        {
            return db.GetEnvironmentAccounts();
        }
    }
}
