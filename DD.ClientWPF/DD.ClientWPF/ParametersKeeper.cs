using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.ClientWPF
{
    public static class ParametersKeeper
    {
        public static int SystemIndex = 0;

        public static string SystemName = "TEMP1";

        public static string UserName = "User User";

        public const string GetAllAlerts = "http://192.168.3.62:1234/api/alerts/all/";
        public const string PutAlert = "http://192.168.3.62:1234/api/alerts/";
    }
}
