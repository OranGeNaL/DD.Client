using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using DD.ClientWPF.HttpObjects;
using System.Diagnostics;

namespace DD.ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для ToDoWindow.xaml
    /// </summary>
    public partial class ToDoWindow : Window
    {
        private List<ToDoWidgetController> ToDoWidgetControllers { get; set; } = new List<ToDoWidgetController>();
        CreateToDoWindow createToDoWindow = null;

        DispatcherTimer timer = new DispatcherTimer();

        public ToDoWindow()
        {
            InitializeComponent();

            timer.Tick += InfoUpdateTimerTick;
            timer.Interval = new TimeSpan(0, 0, 0, 3);
            timer.Start();

            addTaskButton.Click += AddTaskButton_Click;

            GetAllTodos();

            foreach (var todoWidget in ToDoWidgetControllers)
                todoWidget.UpdateWidgetContent();

            SortToDoWidgetControllers();

            todoPanel.Children.Clear();
            foreach (var todoWidget in ToDoWidgetControllers)
            {
                todoPanel.Children.Add(todoWidget.ToDoWidget);
            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (createToDoWindow == null)
            {
                createToDoWindow = new CreateToDoWindow();
                createToDoWindow.OkButton.Click += OkButton_Click;
                createToDoWindow.CancelButton.Click += CancelButton_Click;

                createToDoWindow.Show();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            createToDoWindow.Close();
            createToDoWindow = null;
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoItem newTodo = new ToDoItem(
                createToDoWindow.taskNameTextBox.Text, createToDoWindow.taskDescriptionTextBox.Text);

            WebRequest request = WebRequest.Create(ParametersKeeper.CreateTodo + ParametersKeeper.SystemName);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(newTodo));
            }

            try
            {
                WebResponse response = await request.GetResponseAsync();
            }
            catch(WebException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            createToDoWindow.Close();
            createToDoWindow = null;
        }

        private void InfoUpdateTimerTick(object sender, EventArgs e)
        {
            GetAllTodos();

            foreach (var todoWidget in ToDoWidgetControllers)
                todoWidget.UpdateWidgetContent();

            SortToDoWidgetControllers();

            todoPanel.Children.Clear();
            foreach (var todoWidget in ToDoWidgetControllers)
            {
                todoPanel.Children.Add(todoWidget.ToDoWidget);
            }
        }

        private async void GetAllTodos()
        {
            WebRequest request = WebRequest.Create(ParametersKeeper.GetAllTodos + ParametersKeeper.SystemName);
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

            var allTodosTesponse = JsonConvert.DeserializeObject<List<ToDoItem>>(responseText);

            foreach (ToDoItem todoGot in allTodosTesponse)
            {
                var has = false;
                foreach (var todoWidget in ToDoWidgetControllers)
                {
                    if (todoGot.ID == todoWidget.ToDoData.ID)
                    {
                        todoWidget.ToDoData = todoGot;

                        has = true;
                        break;
                    }
                }

                if (!has)
                {
                    ToDoWidgetControllers.Add(new ToDoWidgetController(todoGot));
                    //this.Focus();
                }
            }

            var widgetsToRemove = new List<ToDoWidgetController>();

            foreach (var todoWidget in ToDoWidgetControllers)
            {
                var has = false;

                foreach (var todoGot in allTodosTesponse)
                {
                    if (todoGot.ID == todoWidget.ToDoData.ID)
                    {
                        has = true;
                        break;
                    }
                }

                if (!has)
                    widgetsToRemove.Add(todoWidget);
            }

            foreach (var todoWidget in widgetsToRemove)
                ToDoWidgetControllers.Remove(todoWidget);
        }

        private void SortToDoWidgetControllers()
        {
            ToDoWidgetControllers.Sort((a, b) => b.ToDoData.CreationTime.CompareTo(a.ToDoData.CreationTime));

            var closedTodos = new List<ToDoWidgetController>();

            foreach (ToDoWidgetController todoWidget in ToDoWidgetControllers)
            {
                if (todoWidget.ToDoData.Status == 2)
                    closedTodos.Add(todoWidget);
            }

            foreach (ToDoWidgetController todoWidget in closedTodos)
                if (ToDoWidgetControllers.Contains(todoWidget))
                    ToDoWidgetControllers.Remove(todoWidget);

            foreach (ToDoWidgetController todoWidget in closedTodos)
                ToDoWidgetControllers.Add(todoWidget);
        }
    }
}
