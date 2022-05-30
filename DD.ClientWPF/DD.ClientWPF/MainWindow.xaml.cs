using DD.ClientWPF.HttpObjects;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DD.ClientWPF
{
    public partial class MainWindow : Window
    {
        AlertsWindow alertsWindow = null;
        ToDoWindow toDoWindow = null;

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
            tasksButton.Click += TasksButton_Click;
            settingsButton.Click += SettingsButton_Click;

            PeopleOnSmeneListBox.MouseDoubleClick += PeopleOnSmeneListBox_MouseDoubleClick;

            this.Closing += MainWindow_Closing;
        }

        private void PeopleOnSmeneListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Debug.WriteLine(PeopleOnSmeneListBox.SelectedIndex.ToString()); //Встать на смену
            TakePlaceOnShift(PeopleOnSmeneListBox.SelectedIndex);
            UpdatePeopleOnShift();
            PeopleOnSmeneListBox.SelectedIndex = -1;
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            ApplyParameters();
            UpdatePeopleOnShift();
        }

        private async void UpdatePeopleOnShift()
        {
            WebRequest request = WebRequest.Create(ParametersKeeper.GetShiftComposition + ParametersKeeper.SystemName);
            request.Method = "GET";

            WebResponse response = await request.GetResponseAsync();

            string responseText = "";

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseText = reader.ReadToEnd();
                }
            }

            var shiftTextBlocks = new TextBlock[] { firstShiftPerson, secondShiftPerson, thirdShiftPerson, fourthShiftPerson };

            try
            {
                var composition = JsonConvert.DeserializeObject<List<CompositionOfTheShift>>(responseText);

                

                for (int i = 0; i < 4; i++)
                {
                    shiftTextBlocks[i].Text = composition[0].People[i];
                }
            }
            catch (Exception ex)
            {
                foreach (var block in shiftTextBlocks)
                    block.Text = "НЕВОЗМОЖНО ПОЛУЧИТЬ";
                Debug.WriteLine(ex.Message);
            }
        }

        private async void TakePlaceOnShift(int placeInd)
        {
            WebRequest request = WebRequest.Create(ParametersKeeper.TakeShiftPlace + ParametersKeeper.SystemName + "?slotInd=" + placeInd.ToString());
            request.Method = "PUT";
            request.ContentType = "application/json";


            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(ParametersKeeper.UserName);
            }

            WebResponse response = await request.GetResponseAsync();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            //if (settingsWindow == null)
            //{
                settingsWindow = new SettingsWindow();
                settingsWindow.Show();

                settingsWindow.OkButton.Click += SettingsWindowOkButton_Click;
            //}
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

            settingsWindow = null;
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

            SystemName.Text = ParametersKeeper.SystemName;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closing = true;
            if (alertsWindow != null)
                alertsWindow.Close();
            if (toDoWindow != null)
                toDoWindow.Close();
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

        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            if(toDoWindow == null)
            {
                toDoWindow = new ToDoWindow();
                toDoWindow.Closing += ToDoWindow_Closing;
                toDoWindow.Show();
            }
            else
            {
                toDoWindow.Show();
                toDoWindow.Focus();
            }
        }

        private void ToDoWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closing)
            {
                toDoWindow.Hide();
                e.Cancel = true;
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
