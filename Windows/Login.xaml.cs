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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Brainstormer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DevMenuOpen(object sender, RoutedEventArgs e)
        {
            DevOptions showDevOptions = new DevOptions();
            showDevOptions.ShowDialog();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {

        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
