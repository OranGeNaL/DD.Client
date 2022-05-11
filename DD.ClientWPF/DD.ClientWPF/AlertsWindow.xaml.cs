using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private List<AlertWidgetController> AlertWidgets { get; set; } = new List<AlertWidgetController>();

        DispatcherTimer timer = new DispatcherTimer();

        public AlertsWindow()
        {
            InitializeComponent();

            timer.Tick += InfoUpdateTimerTick;
            timer.Interval = new TimeSpan(0, 0, 0, 5);
            timer.Start();

            AlertWidgets.Add(new AlertWidgetController(new Alert() { Description = "First new alert", Header = "Header" }));
            AlertWidgets.Add(new AlertWidgetController(new Alert() { Description = "Second new alert", Header = "Header" }));
            AlertWidgets.Add(new AlertWidgetController(new Alert() { Status = 0, Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));
            AlertWidgets.Add(new AlertWidgetController(new Alert() { Status = 1, Worker = "Not User", Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));
            AlertWidgets.Add(new AlertWidgetController(new Alert() { Status = 1, Worker = "User User", Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));
            AlertWidgets.Add(new AlertWidgetController(new Alert() { Status = 2, Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));
            AlertWidgets.Add(new AlertWidgetController(new Alert() { Description = "Недоступен хост hostname.aqua last aviability: 22:14:13 hostname: hostname.aqua Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link Ссылка на инструкцию: link", Header = "Header" }));

            foreach (var alertWidget in AlertWidgets)
                alertsPanel.Children.Add(alertWidget.AlertWidget);
        }

        private void InfoUpdateTimerTick(object sender, EventArgs e)
        {
            Debug.WriteLine("tick");
        }
    }
}
