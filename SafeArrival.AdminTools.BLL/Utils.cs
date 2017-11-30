using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafeArrival.AdminTools.Model;

namespace SafeArrival.AdminTools.BLL
{
    public class Utils
    {
        static public string FormatresourceName(string resourTypeName)
        {
            return $"{GlobalVariables.Enviroment.ToString()}_{resourTypeName}";
        }
    }
}
