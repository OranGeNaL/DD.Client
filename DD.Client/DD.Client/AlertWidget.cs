using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DD.Client
{
    internal class AlertWidget
    {
        private Color ActiveColor = Color.FromArgb(255, 222, 222);
        private Color ClosedColor = Color.FromArgb(238, 234, 234);

        public Alert AlertData { get; set; }

        public Panel panel { get; set; }

        public Label AlertHeaderLabel { get; set; }
        public Label AlertDescriptionLabel { get; set; }

        public Panel AlertTakeCloseButton { get; set; }
        public Panel DescriptionExpandButton { get; set; }

        public AlertWidget() { }

        public AlertWidget(Alert alert)
        {
            AlertData = alert;

            panel = new Panel();
            
            if(alert.Status <= 1)
                panel.BackColor = ActiveColor;
            else
                panel.BackColor = ClosedColor;

            panel.Width = 350;
            panel.Height = 100;


            AlertHeaderLabel = new Label();
            AlertHeaderLabel.Text = AlertData.Header;
            AlertHeaderLabel.ForeColor = Color.FromArgb(79, 79, 79);
            AlertHeaderLabel.Font = new Font("Inter", 10, FontStyle.Regular);
            AlertHeaderLabel.Width = 180;
            AlertHeaderLabel.Location = new Point(15, 15);


            AlertDescriptionLabel = new Label();
            AlertDescriptionLabel.Text = AlertData.Description;
            AlertDescriptionLabel.ForeColor = Color.FromArgb(79, 79, 79);
            AlertDescriptionLabel.Font = new Font("Inter", 10, FontStyle.Regular);
            AlertDescriptionLabel.Width = 180;
            AlertDescriptionLabel.Location = new Point(15, 50);



            panel.Controls.Add(AlertHeaderLabel);
            panel.Controls.Add(AlertDescriptionLabel);
        }
    }
}
