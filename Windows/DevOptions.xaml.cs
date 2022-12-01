using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using Brainstormer.Windows;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer
{
    /// <summary>
    /// Interaction logic for DevOptions.xaml
    /// </summary>
    public partial class DevOptions : Window
    {
        private string testConnectionStr = Connection.getInstanceOfDBConnection().connStr;

        public DevOptions()
        {
            InitializeComponent();

            try
            {
                SqlConnection connectionDB = new SqlConnection(testConnectionStr);
                connectionDB.Open();
                connectionDB.Close();
                DatabaseTestCheckBox.IsChecked = true;
                refreshDataGrid();
            }
            catch (Exception)
            {
                DatabaseTestCheckBox.IsChecked = false;
                DevDB.Visibility = Visibility.Collapsed;
            }


        }

        private void refreshDataGrid()
        {
            DataSet dataContext = getInstanceOfDBConnection().getDataSet("SELECT * FROM [dbo].[User]", "User");
            DevDB.ItemsSource = new DataView(dataContext.Tables["User"]);
        }

        private void DummyDataButton_Click(object sender, RoutedEventArgs e)
        {
            string type, firstName, lastName, email, password, phoneNum;

            Random rnd = new Random();
            SqlConnection connectionDB = new SqlConnection(testConnectionStr);

            int ranType = rnd.Next(1, 4);
            switch (ranType)
            {
                case 1:
                    type = "Admin";
                    break;
                case 2:
                    type = "RM";
                    break;
                case 3:
                    type = "Client";
                    break;
                default:
                    type = "RM";
                    break;
            }

            firstName = "fname" + rnd.Next(9999);
            lastName = "lname" + rnd.Next(9999);
            email = rnd.Next(99999) + "@gmail.com";
            password = "dummy";
            phoneNum = rnd.Next(999999999).ToString() + rnd.Next(99).ToString();

            AccountOperations.CreateAccount(type, firstName, lastName, email, password, phoneNum);
            refreshDataGrid();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserIDBox.Text.Length > 0)
            {
                User.DeleteAccount(Convert.ToInt32(UserIDBox.Text.ToString()));
                refreshDataGrid();
                UserIDBox.Text = "";
            }
            else
            {
                MessageBox.Show("Enter an ID!");
            }

        }

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            string value = UserValueBox.Text;
            int primaryKey = Convert.ToInt32(UserIDModBox.Text);
            string columnName = UserColumnBox.Text;

            if (Checks.checkPKExists(getInstanceOfDBConnection().getDataSet("SELECT * FROM [dbo].[User]", "User"), primaryKey))
            {
                if (Checks.checkColumnExists(getInstanceOfDBConnection().getDataSet("SELECT * FROM [dbo].[User]", "User"), columnName))
                {
                    if (columnName.Equals("Type"))
                    {
                        if (Checks.checkIsValidAccountType(value))
                        {
                            AccountOperations.UpdateField(columnName, primaryKey, value);
                            UserColumnBox.Text = "";
                        }
                    }
                    else
                    {
                        AccountOperations.UpdateField(columnName, primaryKey, value);
                    }
                    UserValueBox.Text = "";
                }
                else
                {
                    UserColumnBox.Text = "";
                    MessageBox.Show("Column doesn't exist.");
                }
            }
            else
            {
                MessageBox.Show("PK doesn't exist.");
                UserIDModBox.Text = "";
            }



            refreshDataGrid();



        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new("Edit");
            createAccount.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            refreshDataGrid();
        }

        private void OpenIdeaWindow(object sender, RoutedEventArgs e)
        {
            CreateIdea createidea = new();
            createidea.ShowDialog();
        }
    }
}
