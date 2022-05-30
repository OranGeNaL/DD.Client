using DD.ClientWPF.HttpObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DD.ClientWPF
{
    internal class ToDoWidgetController
    {
        public ToDoWidget ToDoWidget { get; set; }

        public ToDoItem ToDoData { get; set; }

        public ToDoWidgetController() { }

        private bool expanded = false;

        public bool Expanded { get { return expanded; } set { expanded = value; } }

        public ToDoWidgetController(ToDoItem todo)
        {
            ToDoData = todo;
            ToDoWidget = new ToDoWidget();

            UpdateWidgetContent();

            ToDoWidget.ExpandCollapseButton.Click += ExpandCollapseButton_Click;
            ToDoWidget.TakeButton.Click += TakeButton_Click;
        }

        public void UpdateWidgetContent()
        {
            ToDoWidget.Worker.Background = (SolidColorBrush)ToDoWidget.TryFindResource("TakenAlertWorkerBackground");
            ToDoWidget.Worker.Foreground = (SolidColorBrush)ToDoWidget.TryFindResource("TakenAlertWorkerForeground");

            switch (ToDoData.Status)
            {
                case 0:
                    ToDoWidget.TakeButton.Content = ToDoWidget.TryFindResource("TakeAlert");
                    ToDoWidget.Worker.Content = "Не в работе";
                    ToDoWidget.Worker.Background = (SolidColorBrush)ToDoWidget.TryFindResource("NewAlertWorkerBackground");
                    ToDoWidget.Worker.Foreground = (SolidColorBrush)ToDoWidget.TryFindResource("NewAlertWorkerForeground");
                    break;
                case 1:
                    if (ToDoData.Worker != ParametersKeeper.UserName)
                        ToDoWidget.TakeButton.Content = ToDoWidget.TryFindResource("TakeAlert");
                    else
                        ToDoWidget.TakeButton.Content = ToDoWidget.TryFindResource("CloseAlert");
                    ToDoWidget.Worker.Content = "В работе: " + ToDoData.Worker;
                    break;
                case 2:
                    ToDoWidget.TakeButton.Visibility = System.Windows.Visibility.Collapsed;
                    ToDoWidget.ToDoHeader.Width = 370;
                    ToDoWidget.Container.Background = (SolidColorBrush)ToDoWidget.TryFindResource("ClosedAlertBackColor");
                    ToDoWidget.Worker.Content = "Закрыта";
                    break;
            }

            ToDoWidget.ToDoHeader.Content = ToDoData.TaskName;
            ToDoWidget.AlertDescriptionContent.Text = "Дата создания: " + ToDoData.CreationTime.ToString("G") + "\n\n" + ToDoData.Description;
        }

        private void ExpandCollapseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.expanded)
            {
                this.expanded = false;
                ToDoWidget.AlertDescriptionContent.MaxHeight = 100;

                ToDoWidget.ExpandCollapseButton.Content = ToDoWidget.TryFindResource("ExpandDescription");
            }
            else
            {
                this.expanded = true;
                ToDoWidget.AlertDescriptionContent.MaxHeight = 600;
                ToDoWidget.ExpandCollapseButton.Content = ToDoWidget.TryFindResource("CollapseDescription");
            }
        }

        public ToDoWidgetController SetExpandCollapseWidget()
        {
            //Debug.WriteLine(AlertData.Header + expanded.ToString());

            if (!expanded)
            {
                ToDoWidget.AlertDescriptionContent.MaxHeight = 100;

                ToDoWidget.ExpandCollapseButton.Content = ToDoWidget.TryFindResource("ExpandDescription");
            }
            else
            {
                ToDoWidget.AlertDescriptionContent.MaxHeight = 600;
                ToDoWidget.ExpandCollapseButton.Content = ToDoWidget.TryFindResource("CollapseDescription");
            }

            return this;
        }

        private void TakeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ToDoData.Status <= 1 && ToDoData.Worker != ParametersKeeper.UserName)
                TakeToDo();
            else if (ToDoData.Status == 1 && ToDoData.Worker == ParametersKeeper.UserName)
                CloseToDo();
        }

        private async void TakeToDo()
        {
            WebRequest request = WebRequest.Create(ParametersKeeper.PutTodo + ToDoData.ID);
            request.Method = "PUT";
            request.ContentType = "application/json";

            ToDoData.Status = 1;
            ToDoData.Worker = ParametersKeeper.UserName;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(ToDoData));
            }

            WebResponse response = await request.GetResponseAsync();
        }

        private async void CloseToDo()
        {
            WebRequest request = WebRequest.Create(ParametersKeeper.PutTodo + ToDoData.ID);
            request.Method = "PUT";
            request.ContentType = "application/json";

            ToDoData.Status = 2;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(ToDoData));
            }

            WebResponse response = await request.GetResponseAsync();
        }
    }
}
