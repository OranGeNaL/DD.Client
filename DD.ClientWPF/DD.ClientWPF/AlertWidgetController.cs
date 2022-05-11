using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.ClientWPF
{
    internal class AlertWidgetController
    {
        public AlertWidget AlertWidget { get; set; }

        public Alert AlertData { get; set; }

        public AlertWidgetController() { }

        public AlertWidgetController(Alert alert)
        {
            AlertData = alert;
            AlertWidget = new AlertWidget();


            AlertWidget.AlertHeader.Content = AlertData.Header;
            AlertWidget.AlertDescription.Content = AlertData.Description;
        }
    }
}
