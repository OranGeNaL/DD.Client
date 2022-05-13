using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DD.ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AlertsWindow alertsWindow = null;

        bool closing = false;

        public MainWindow()
        {
            InitializeComponent();

            alertsButton.Click += AlertsButton_Click;

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closing = true;
            if (alertsWindow != null)
                alertsWindow.Close();
        }

        private void AlertsButton_Click(object sender, RoutedEventArgs e)
        {
            if(alertsWindow == null)
            {
                alertsWindow = new AlertsWindow();
                alertsWindow.Closing += AlertsWindow_Closing;
                alertsWindow.Show();
            }
            else
            {
                alertsWindow.Show();
                alertsWindow.Focus();
            }
        }

        private void AlertsWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closing)
            {
                alertsWindow.Hide();
                e.Cancel = true;
            }

        }
    }
}
