using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private bool expended = false;

        public AlertWidgetController(Alert alert)
        {
            AlertData = alert;
            AlertWidget = new AlertWidget();

            UpdateWidgetContent();

            AlertWidget.ExpandCollapseButton.Click += ExpandCollapseButton_Click;
        }

        public void UpdateWidgetContent()
        {
            switch (AlertData.Status)
            {
                case 0:
                    AlertWidget.TakeButton.Content = AlertWidget.TryFindResource("TakeAlert");
                    AlertWidget.Worker.Content = "Не в работе";
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
                    AlertWidget.Container.Background = (Brush)AlertWidget.TryFindResource("ClosedAlertBackColor");
                    AlertWidget.Worker.Content = "Закрыт: " + AlertData.Worker;
                    break;
            }

            AlertWidget.AlertHeader.Content = AlertData.Header;
            AlertWidget.AlertDescriptionContent.Text = AlertData.Description;
        }

        private void ExpandCollapseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(expended)
            {
                expended = false;
                AlertWidget.AlertDescriptionContent.MaxHeight = 100;

                AlertWidget.ExpandCollapseButton.Content = AlertWidget.TryFindResource("ExpandDescription");
            }
            else
            {
                expended = true;
                AlertWidget.AlertDescriptionContent.MaxHeight = 600;
                AlertWidget.ExpandCollapseButton.Content = AlertWidget.TryFindResource("CollapseDescription");
            }
        }
    }
}
