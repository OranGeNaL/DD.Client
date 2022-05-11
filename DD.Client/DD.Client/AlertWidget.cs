using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private Image ExpandDescription = null;
        private Image CollapseDescription = null;

        public Alert AlertData { get; set; }

        public Panel panel { get; set; }

        public Label AlertHeaderLabel { get; set; }
        public Label AlertDescriptionLabel { get; set; }

        public Panel AlertTakeCloseButton { get; set; }
        public Panel DescriptionButton { get; set; }

        public bool WidgedState = false; // false collapsed, true expanded

        public AlertWidget() { }

        public AlertWidget(Alert alert)
        {
            AlertData = alert;

            panel = new Panel();

            ExpandDescription = Image.FromFile("img\\button-description-expand.png");
            CollapseDescription = Image.FromFile("img\\button-description-collapse.png");

            if (alert.Status <= 1)
                panel.BackColor = ActiveColor;
            else
                panel.BackColor = ClosedColor;

            panel.Width = 350;
            panel.Height = 100;


            AlertHeaderLabel = new Label();
            AlertHeaderLabel.Text = AlertData.Header;
            AlertHeaderLabel.ForeColor = Color.FromArgb(79, 79, 79);
            AlertHeaderLabel.Font = new Font("Inter", 9, FontStyle.Regular);
            AlertHeaderLabel.Width = 250;
            AlertHeaderLabel.AutoSize = true;
            AlertHeaderLabel.Location = new Point(15, 15);

            AlertDescriptionLabel = new Label();
            AlertDescriptionLabel.Text = AlertData.Description;
            AlertDescriptionLabel.ForeColor = Color.FromArgb(79, 79, 79);
            AlertDescriptionLabel.Font = new Font("Inter", 9, FontStyle.Regular);
            //AlertDescriptionLabel.Width = 270;
            //AlertDescriptionLabel.Height = 50;
            AlertDescriptionLabel.MaximumSize = new Size(270, 50);
            AlertDescriptionLabel.AutoSize = true;
            AlertDescriptionLabel.Location = new Point(15, 50);

            DescriptionButton = new Panel();
            DescriptionButton.BackgroundImage = ExpandDescription;
            DescriptionButton.Width = 18;
            DescriptionButton.Height = 18;
            DescriptionButton.BackgroundImageLayout = ImageLayout.Zoom;
            DescriptionButton.Location = new Point(panel.Width - 18, panel.Height - 18);
            DescriptionButton.MouseClick += DescriptionButton_MouseClick;

            panel.Controls.Add(AlertHeaderLabel);
            panel.Controls.Add(AlertDescriptionLabel);
            panel.Controls.Add(DescriptionButton);
        }

        private void DescriptionButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (!WidgedState)
            {
                DescriptionButton.BackgroundImage = CollapseDescription;

                panel.Height = 100 + (AlertDescriptionLabel.PreferredHeight - 50) + 10;
                AlertDescriptionLabel.Height = AlertDescriptionLabel.PreferredHeight;
                DescriptionButton.Location = new Point(panel.Width - 18, panel.Height - 18);

                Debug.WriteLine(AlertDescriptionLabel.PreferredHeight);

                WidgedState = true;
            }
            else
            {
                DescriptionButton.BackgroundImage = ExpandDescription;

                panel.Height = 100;
                AlertDescriptionLabel.Height = 50;
                DescriptionButton.Location = new Point(panel.Width - 18, panel.Height - 18);

                WidgedState = false;
            }
        }

        public void UpdateData()
        {

        }
    }
}
