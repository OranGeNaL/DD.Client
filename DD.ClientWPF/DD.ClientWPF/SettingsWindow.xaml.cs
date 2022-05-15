﻿using System;
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
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            UserNameTextBox.Text = ParametersKeeper.UserName;

            SystemIDCombo.SelectedIndex = ParametersKeeper.SystemIndex;

            MainTimeAreaCombo.SelectedIndex = ParametersKeeper.MainTimeArea;
            LeftTimeAreaCombo.SelectedIndex = ParametersKeeper.LeftTimeArea;
            RightTimeAreaCombo.SelectedIndex = ParametersKeeper.RightTimeArea;

            OkButton.Click += OkButton_Click;
            CancelButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
