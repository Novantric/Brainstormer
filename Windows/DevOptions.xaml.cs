﻿using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer
{
    /// <summary>
    /// Interaction logic for DevOptions.xaml
    /// </summary>
    public partial class DevOptions : Window
    {
        public DevOptions()
        {
            InitializeComponent();

            try
            {
                String testConnectionStr = Properties.Settings.Default.DatabaseConnectionString;
                DatabaseTestCheckBox.IsChecked = true;
            }
            catch (Exception)
            {
                DatabaseTestCheckBox.IsChecked = false;
                throw;
            }

            getInstanceOfDBConnection();

            DataSet dataContext = getInstanceOfDBConnection().getDataSet("SELECT * FROM [dbo].[tblRelationshipManagers]", "RM");
            DevDB.DataContext = dataContext;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DummyDataButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FillDataGrid()
        {


        }



    }
}
