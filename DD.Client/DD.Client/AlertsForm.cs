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

        public AlertsForm()
        {
            InitializeComponent();
        }

        private void AlertsForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 183, 183);
            this.FormClosed += BeforeClosing;

            alertWidgets.Add(new AlertWidget(new Alert()));

            foreach (var alertWidget in alertWidgets)
            {
                this.Controls.Add(alertWidget.panel);
            }
        }

        private void BeforeClosing(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
