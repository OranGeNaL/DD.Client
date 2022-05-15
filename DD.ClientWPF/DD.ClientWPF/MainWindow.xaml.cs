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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DD.ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AlertsWindow alertsWindow = null;

        SettingsWindow settingsWindow = null;

        bool closing = false;

        DispatcherTimer clockTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            clockTimer.Start();
            clockTimer.Interval = new TimeSpan(0, 0, 0, 5);
            clockTimer.Tick += ClockTimer_Tick;

            ParametersKeeper.ReadParameters();
            ApplyParameters();

            alertsButton.Click += AlertsButton_Click;
            settingsButton.Click += SettingsButton_Click;

            PeopleOnSmeneListBox.MouseDoubleClick += PeopleOnSmeneListBox_MouseDoubleClick;

            this.Closing += MainWindow_Closing;
        }

        private void PeopleOnSmeneListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine(PeopleOnSmeneListBox.SelectedIndex.ToString());

            PeopleOnSmeneListBox.SelectedIndex = -1;
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            ApplyParameters();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow = new SettingsWindow();
            settingsWindow.Show();

            settingsWindow.OkButton.Click += SettingsWindowOkButton_Click;
        }

        private void SettingsWindowOkButton_Click(object sender, RoutedEventArgs e)
        {
            ParametersKeeper.UserName = settingsWindow.UserNameTextBox.Text;
            ParametersKeeper.SystemIndex = settingsWindow.SystemIDCombo.SelectedIndex;
            ParametersKeeper.MainTimeArea = settingsWindow.MainTimeAreaCombo.SelectedIndex;
            ParametersKeeper.LeftTimeArea = settingsWindow.LeftTimeAreaCombo.SelectedIndex;
            ParametersKeeper.RightTimeArea = settingsWindow.RightTimeAreaCombo.SelectedIndex;

            ParametersKeeper.SaveParameters();
            ApplyParameters();
        }

        private void ApplyParameters()
        {
            UserNameTextBlock.Text = ParametersKeeper.UserName;
            MainClockArea.Text = ParametersKeeper.MainTimeAreaName;
            LeftClockArea.Text = ParametersKeeper.LeftTimeAreaName;
            RightClockArea.Text = ParametersKeeper.RightTimeAreaName;

            DateTime dateTime = DateTime.Now.ToUniversalTime();

            MainClockTime.Text = dateTime.AddHours(ParametersKeeper.MainTimeArea + 2).ToString("HH:mm");
            LeftClockTime.Text = dateTime.AddHours(ParametersKeeper.LeftTimeArea + 2).ToString("HH:mm");
            RightClockTime.Text = dateTime.AddHours(ParametersKeeper.RightTimeArea + 2).ToString("HH:mm");
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
