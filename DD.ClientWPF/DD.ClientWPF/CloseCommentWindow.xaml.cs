using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DD.ClientWPF
{
    public partial class CloseCommentWindow : Window
    {
        public CloseCommentWindow()
        {
            InitializeComponent();

            OkButton.Click += OkButton_Click;
            CancelButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
