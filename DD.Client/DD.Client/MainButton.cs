using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DD.Client
{
    internal class MainButton
    {
        public Panel panel;

        public string Name { get; set; }

        public Image DefaultBackground { get; set; }
        public Image HoveredBackground { get; set; }
        public Image PressedBackground { get; set; }

        public MainButton() { }

        public bool Started { get; set; } = false;

        public Form form = null;

        private int formID = 1;

        public MainButton(string name, int formID = 1)
        {
            panel = new Panel();
            panel.BorderStyle = BorderStyle.None;
            panel.Width = 115;
            panel.Height = 115;

            Name = name;

            switch (formID)
            {
                case 1:
                    this.form = new AlertsForm();
                    break;
                case 2:
                    //this.form = new TasksForm();
                    break;
                case 3:
                    //this.form = new ChatForm();
                    break;
            }

            this.formID = formID;

            if (form != null)
            {
                form.FormClosed += OnFormClosing;
            }

            DefaultBackground = Image.FromFile("img\\button-" + Name + ".png");
            HoveredBackground = Image.FromFile("img\\button-hovered-" + Name + ".png");
            PressedBackground = Image.FromFile("img\\button-pressed-" + Name + ".png");

            panel.BackgroundImageLayout = ImageLayout.Zoom;
            panel.BackgroundImage = DefaultBackground;

            panel.MouseEnter += OnMouseEnter;
            panel.MouseLeave += OnMouseLeave;
            panel.MouseDown += OnMouseDown;
            panel.MouseUp += OnMouseUp;
        }

        private void OnFormClosing(object sender, FormClosedEventArgs e)
        {
            if (Started == true)
            {
                Started = false;
            }

            switch (formID)
            {
                case 1:
                    this.form = new AlertsForm();
                    break;
                case 2:
                    //this.form = new TasksForm();
                    break;
                case 3:
                    //this.form = new ChatForm();
                    break;
            }

            if (form != null)
            {
                form.FormClosed += OnFormClosing;
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            panel.BackgroundImage = HoveredBackground;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            panel.BackgroundImage = PressedBackground;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            panel.BackgroundImage = DefaultBackground;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            panel.BackgroundImage = HoveredBackground;
        }

        public void StartForm()
        {
            Debug.WriteLine("Clicket start new");
            if (Started == false)
            {
                form.Show();
                Started = true;
            }

            else
            {
                form.Focus();
            }
        }
    }
}
