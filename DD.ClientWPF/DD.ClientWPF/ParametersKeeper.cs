using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


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

        public const string ApiHost = "http://192.168.3.62:1234";
        //public const string ApiHost = "http://192.168.3.67:31993";

        public const string GetAllAlerts = ApiHost + "/api/alerts/all/";
        public const string PutAlert = ApiHost + "/api/alerts/";
        public const string GetShiftComposition = ApiHost + "/api/people/all/";
        public const string TakeShiftPlace = ApiHost + "/api/people/";


        public static void SaveParameters()
        {

            XmlDocument xDoc = new XmlDocument();

            xDoc.AppendChild(xDoc.CreateElement("root"));

            var xRoot = xDoc.DocumentElement;

            var userNameNode = xDoc.CreateElement("UserName");
            userNameNode.InnerText = ParametersKeeper.UserName;
            xRoot.AppendChild(userNameNode);

            var systemIndexNode = xDoc.CreateElement("SystemIndex");
            systemIndexNode.InnerText = SystemIndex.ToString();
            xRoot.AppendChild(systemIndexNode);

            var mainTime = xDoc.CreateElement("MainTime");
            mainTime.InnerText = MainTimeArea.ToString();
            xRoot.AppendChild(mainTime);

            var leftTime = xDoc.CreateElement("LeftTime");
            leftTime.InnerText = LeftTimeArea.ToString();
            xRoot.AppendChild(leftTime);

            var rightTime = xDoc.CreateElement("RightTime");
            rightTime.InnerText = RightTimeArea.ToString();
            xRoot.AppendChild(rightTime);

            xDoc.Save("settings.xml");
        }

        public static void ReadParameters()
        {
            FileInfo fileInfo = new FileInfo("settings.xml");

            if (fileInfo.Exists)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("settings.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                if (xRoot != null)
                {
                    foreach (XmlElement xnode in xRoot)
                    {
                        if(xnode.Name == "UserName")
                            UserName = xnode.InnerText;

                        else if (xnode.Name == "SystemIndex")
                            SystemIndex = int.Parse(xnode.InnerText);

                        else if (xnode.Name == "MainTime")
                            MainTimeArea = int.Parse(xnode.InnerText);

                        else if (xnode.Name == "LeftTime")
                            LeftTimeArea = int.Parse(xnode.InnerText);

                        else if (xnode.Name == "RightTime")
                            RightTimeArea = int.Parse(xnode.InnerText);
                    }
                }
            }
            else
            {
                SaveParameters();
            }
        }
    }
}
