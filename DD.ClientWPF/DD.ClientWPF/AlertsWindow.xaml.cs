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

        DispatcherTimer timer = new DispatcherTimer();

        public AlertsWindow()
        {
            InitializeComponent();

            timer.Tick += InfoUpdateTimerTick;
            timer.Interval = new TimeSpan(0, 0, 0, 3);
            timer.Start();


            GetAllAlerts();

            foreach (var alertWidget in AlertWidgetControllers)
                alertWidget.UpdateWidgetContent();

            SortAlertWidgetControllers();

            alertsPanel.Children.Clear();
            foreach (var alert in AlertWidgetControllers)
            {
                alertsPanel.Children.Add(alert.AlertWidget);
            }
        }

        private void InfoUpdateTimerTick(object sender, EventArgs e)
        {
            GetAllAlerts();

            foreach (var alertWidget in AlertWidgetControllers)
                alertWidget.UpdateWidgetContent();

            SortAlertWidgetControllers();

            alertsPanel.Children.Clear();
            foreach(var alert in AlertWidgetControllers)
            {
                alertsPanel.Children.Add(alert.AlertWidget);
            }
        }

        private async void GetAllAlerts()
        {
            WebRequest request = WebRequest.Create(ParametersKeeper.GetAllAlerts + ParametersKeeper.SystemName);
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

            var allAlertsResponse = JsonConvert.DeserializeObject<List<Alert>>(responseText);

            foreach (Alert alertGot in allAlertsResponse)
            {
                var has = false;
                foreach (var alertWidget in AlertWidgetControllers)
                {
                    if (alertGot.ID == alertWidget.AlertData.ID)
                    {
                        alertWidget.AlertData = alertGot;
                        
                        has = true;
                        break;
                    }
                }

                if (!has)
                {
                    AlertWidgetControllers.Add(new AlertWidgetController(alertGot));
                    //this.Focus();
                }
            }

            var widgetsToRemove = new List<AlertWidgetController>();

            foreach (var alertWidget in AlertWidgetControllers)
            {
                var has = false;

                foreach (var alertGot in allAlertsResponse)
                {
                    if (alertGot.ID == alertWidget.AlertData.ID)
                    {
                        has = true;
                        break;
                    }
                }

                if (!has)
                    widgetsToRemove.Add(alertWidget);
            }

            foreach (var alertWidget in widgetsToRemove)
                AlertWidgetControllers.Remove(alertWidget);
        }

        private void SortAlertWidgetControllers()
        {
            AlertWidgetControllers.Sort((a, b) => b.AlertData.Date.CompareTo(a.AlertData.Date));

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
