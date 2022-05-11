using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DD.Client
{
    public partial class AlertsForm : Form
    {
        private List<AlertWidget> alertWidgets = new List<AlertWidget>();
        private int startPosition = 15;

        public AlertsForm()
        {
            InitializeComponent();
        }

        private void AlertsForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 183, 183);
            this.FormClosed += BeforeClosing;

            alertWidgets.Add(new AlertWidget(new Alert() { Description = "Lorem ipsum dolor sid amet more words just to check how this will display" }));
            alertWidgets.Add(new AlertWidget(new Alert() { Description = "Недоступен хост hostname.aqua\nlast aviability: 22:14:13\nhostname: hostname.aqua\nСсылка на инструкцию: link\nСсылка на мониторинг: link"}));
            alertWidgets.Add(new AlertWidget(new Alert() { Status = 2 }));

            foreach (var alertWidget in alertWidgets)
            {
                this.Controls.Add(alertWidget.panel);
            }

            UpdateAlertWidgets();
        }

        private void BeforeClosing(object sender, FormClosedEventArgs e)
        {
            
        }



        private void UpdateAlertWidgets()
        {
            this.Controls.Clear();

            int position = startPosition;

            foreach (var alertWidget in alertWidgets)
            {
                alertWidget.panel.Location = new Point(15, position);
                this.Controls.Add(alertWidget.panel);

                position += alertWidget.panel.Height + 20;
            }
        }
    }
}
