using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace DD.ClientWPF
{
    internal class AlertWidgetController
    {
        public AlertWidget AlertWidget { get; set; }

        public Alert AlertData { get; set; }

        public AlertWidgetController() { }

        private bool expanded = false;

        public bool Expanded { get { return expanded; } set { expanded = value; } }

        private CloseCommentWindow CommentWindow { get; set; }

        public AlertWidgetController(Alert alert)
        {
            AlertData = alert;
            AlertWidget = new AlertWidget();

            UpdateWidgetContent();

            AlertWidget.ExpandCollapseButton.Click += ExpandCollapseButton_Click;
            AlertWidget.TakeButton.Click += TakeButton_Click;
        }

        public void UpdateWidgetContent()
        {
            AlertWidget.Worker.Background = (SolidColorBrush)AlertWidget.TryFindResource("TakenAlertWorkerBackground");
            AlertWidget.Worker.Foreground = (SolidColorBrush)AlertWidget.TryFindResource("TakenAlertWorkerForeground");
       
            switch (AlertData.Status)
            {
                case 0:
                    AlertWidget.TakeButton.Content = AlertWidget.TryFindResource("TakeAlert");
                    AlertWidget.Worker.Content = "Не в работе";
                    AlertWidget.Worker.Background = (SolidColorBrush)AlertWidget.TryFindResource("NewAlertWorkerBackground");
                    AlertWidget.Worker.Foreground = (SolidColorBrush)AlertWidget.TryFindResource("NewAlertWorkerForeground");
                    break;
                case 1:
                    if (AlertData.Worker != ParametersKeeper.UserName)
                        AlertWidget.TakeButton.Content = AlertWidget.TryFindResource("TakeAlert");
                    else
                        AlertWidget.TakeButton.Content = AlertWidget.TryFindResource("CloseAlert");
                    AlertWidget.Worker.Content = "В работе: " + AlertData.Worker;
                    break;
                case 2:
                    AlertWidget.TakeButton.Visibility = System.Windows.Visibility.Collapsed;
                    AlertWidget.AlertHeader.Width = 370;
                    AlertWidget.Container.Background = (SolidColorBrush)AlertWidget.TryFindResource("ClosedAlertBackColor");
                    AlertWidget.Worker.Content = "Закрыт: " + AlertData.CommentToClose;
                    break;
            }

            AlertWidget.AlertHeader.Content = AlertData.Header;
            AlertWidget.AlertDescriptionContent.Text = "Дата создания: " + AlertData.Date.ToString("G") + "\n\n" + AlertData.Description;
        }

        private void ExpandCollapseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.expanded)
            {
                this.expanded = false;
                AlertWidget.AlertDescriptionContent.MaxHeight = 100;

                AlertWidget.ExpandCollapseButton.Content = AlertWidget.TryFindResource("ExpandDescription");
            }
            else
            {
                this.expanded = true;
                AlertWidget.AlertDescriptionContent.MaxHeight = 600;
                AlertWidget.ExpandCollapseButton.Content = AlertWidget.TryFindResource("CollapseDescription");
            }
        }

        public AlertWidgetController SetExpandCollapseWidget()
        {
            //Debug.WriteLine(AlertData.Header + expanded.ToString());

            if (!expanded)
            {
                AlertWidget.AlertDescriptionContent.MaxHeight = 100;

                AlertWidget.ExpandCollapseButton.Content = AlertWidget.TryFindResource("ExpandDescription");
            }
            else
            {
                AlertWidget.AlertDescriptionContent.MaxHeight = 600;
                AlertWidget.ExpandCollapseButton.Content = AlertWidget.TryFindResource("CollapseDescription");
            }

            return this;
        }

        private void TakeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AlertData.Status <= 1 && AlertData.Worker != ParametersKeeper.UserName)
                TakeAlert();
            else if (AlertData.Status == 1 && AlertData.Worker == ParametersKeeper.UserName)
            {
                CommentWindow = new CloseCommentWindow();
                CommentWindow.OkButton.Click += OkComment_Click;

                CommentWindow.Show();
            }    
        }

        private async void TakeAlert()
        {
            WebRequest request = WebRequest.Create(ParametersKeeper.PutAlert + AlertData.ID);
            request.Method = "PUT";
            request.ContentType = "application/json";

            AlertData.Status = 1;
            AlertData.Worker = ParametersKeeper.UserName;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(AlertData));
            }

            WebResponse response = await request.GetResponseAsync();
        }

        private async void CloseAlert()
        {
            WebRequest request = WebRequest.Create(ParametersKeeper.PutAlert + AlertData.ID);
            request.Method = "PUT";
            request.ContentType = "application/json";

            AlertData.Status = 2;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(AlertData));
            }

            WebResponse response = await request.GetResponseAsync();
        }

        private void OkComment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AlertData.CommentToClose = CommentWindow.CloseComment.Text;
            CloseAlert();
        }
    }
}
