using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.ClientWPF
{
    public static class ParametersKeeper
    {
        private static string[] systemNames = new string[] { "TEMP", "TEMP1", "TEMP2", "TEMP3"};

        public static string[] SystemNames { get { return systemNames; } }

        private static string[] timeAreas = new string[] { "Калининград", "Москва", "Самара", "Екатеринбург", "Омск", "Красноярск", "Иркутск", "Якутия", "Владивосток", "Магадан", "Камчатка"};

        public static string[] TimeAreas { get { return timeAreas; } }

        public static int SystemIndex = 1;

        public static string SystemName { get { return systemNames[SystemIndex]; } }
        public static string MainTimeAreaName { get { return timeAreas[MainTimeArea]; } }
        public static string LeftTimeAreaName { get { return timeAreas[LeftTimeArea]; } }
        public static string RightTimeAreaName { get { return timeAreas[RightTimeArea]; } }

        public static string UserName = "User User";

        public static int MainTimeArea = 8;
        public static int LeftTimeArea = 1;
        public static int RightTimeArea = 5;


        public const string GetAllAlerts = "http://192.168.3.62:1234/api/alerts/all/";
        public const string PutAlert = "http://192.168.3.62:1234/api/alerts/";


        public static void SaveParameters()
        {
            throw new NotImplementedException();
        }

        public static void ReadParameters()
        {
            throw new NotImplementedException();
        }
    }
}
