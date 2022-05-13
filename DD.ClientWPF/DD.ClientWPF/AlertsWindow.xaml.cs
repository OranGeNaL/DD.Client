using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DD.ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для AlertsWindow.xaml
    /// </summary>
    /// 

    public partial class AlertsWindow : Window
    {
        private List<AlertWidgetController> AlertWidgetControllers { get; set; } = new List<AlertWidgetController>();
        private List<Alert> Alerts { get; set; } = new List<Alert>();

        DispatcherTimer timer = new DispatcherTimer();

        public AlertsWindow()
        {
            InitializeComponent();

            timer.Tick += InfoUpdateTimerTick;
            timer.Interval = new TimeSpan(0, 0, 0, 5);
            timer.Start();

            AlertWidgetControllers.Add(new AlertWidgetController(new Alert() { Description = "First new alert", Header = "Header" }));
            AlertWidgetControllers.Add(new AlertWidgetController(new Alert() { Description = "Second new alert", Header = "Header" }));
            AlertWidgetControllers.Add(new AlertWidgetController(new Alert() { Status = 0, Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));
            AlertWidgetControllers.Add(new AlertWidgetController(new Alert() { Status = 1, Worker = "Not User", Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));
            AlertWidgetControllers.Add(new AlertWidgetController(new Alert() { Status = 1, Worker = "User User", Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));
            AlertWidgetControllers.Add(new AlertWidgetController(new Alert() { Status = 2, Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));
            AlertWidgetControllers.Add(new AlertWidgetController(new Alert() { Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));

            foreach (var alertWidget in AlertWidgetControllers)
                alertsPanel.Children.Add(alertWidget.AlertWidget);
        }

        private void InfoUpdateTimerTick(object sender, EventArgs e)
        {
            //Debug.WriteLine("tick");
            GetAllAlerts();

            foreach (var alertWidget in AlertWidgetControllers)
                alertWidget.UpdateAlertData();

            SortAlertWidgetControllers();
        }


        private async void GetAllAlerts()
        {
            WebRequest request = WebRequest.Create("http://192.168.3.62:1234/api/alerts/all/" + ParametersKeeper.SystemName);
            request.Method = "GET";

            WebResponse response = await request.GetResponseAsync();

            string responseText = "";

            using (Stream stream = response.GetResponseStream())
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    responseText = reader.ReadToEnd();
                }
            }

            var allAlertsResponse = new List<Alert>();//JsonConvert.DeserializeObject<List<Alert>>(responseText);

            //foreach (Alert alert in allAlertsResponse)
            //{
            //    Debug.WriteLine(alert.Header);
            //}

            foreach (Alert alertGot in allAlertsResponse)
            {
                var has = false;
                foreach (var alertWidget in AlertWidgetControllers)
                {
                    if (alertGot.ID == alertWidget.AlertData.ID)
                    {
                        has = true;
                        break;
                    }
                }

                if (!has)
                    AlertWidgetControllers.Add(new AlertWidgetController(alertGot));
            }
        }

        private void SortAlertWidgetControllers()
        {
            AlertWidgetControllers.Sort((a, b) => a.AlertData.Date.CompareTo(b.AlertData.Date));

            var closedAlerts = new List<AlertWidgetController>();

            foreach (AlertWidgetController alertWidget in AlertWidgetControllers)
            {
                if(alertWidget.AlertData.Status == 2)
                    closedAlerts.Add(alertWidget);
            }

            foreach (AlertWidgetController alertWidget in closedAlerts)
                if(AlertWidgetControllers.Contains(alertWidget))
                    AlertWidgetControllers.Remove(alertWidget);

            foreach (AlertWidgetController alertWidget in closedAlerts)
                AlertWidgetControllers.Add(alertWidget);
        }
    }
}
