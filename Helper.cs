using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SITUFishery
{
    public static class Helper
    {
        public static string ConnectionVal(string n)
        {
            return ConfigurationManager.ConnectionStrings[n].ConnectionString;
        }
    }
}
