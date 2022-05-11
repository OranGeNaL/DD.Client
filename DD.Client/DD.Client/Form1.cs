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
    public partial class Form1 : Form
    {

        private MainButton AlertsButton;
        private MainButton TaskButton;
        private MainButton ChatButton;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 183, 183);

            AlertsButton = new MainButton("alerts", 1);
            TaskButton = new MainButton("tasks", 2);
            ChatButton = new MainButton("chat", 3);

            AlertsButton.panel.Location = new Point(25, 260);
            ChatButton.panel.Location = new Point(25, 395);
            TaskButton.panel.Location = new Point(160, 260);

            this.Controls.Add(AlertsButton.panel);
            this.Controls.Add(ChatButton.panel);
            this.Controls.Add(TaskButton.panel);

            AlertsButton.panel.MouseClick += AlertsMouseClick;
        }

        private void AlertsMouseClick(object sender, MouseEventArgs e)
        {
            AlertsButton.StartForm();
        }

    }
}
