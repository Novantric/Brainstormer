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
        //Creates a local connection string instance for testing
        private readonly string testConnectionStr = Connection.GetInstanceOfDBConnection().connStr;

        public DevOptions()
        {
            InitializeComponent();

            //Checks if the database can be reachwd
            try
            {
                SqlConnection connectionDB = new SqlConnection(testConnectionStr);
                connectionDB.Open();
                connectionDB.Close();
                DatabaseTestCheckBox.IsChecked = true;
                RefreshDataGrid();
            }
            catch (Exception)
            {
                DatabaseTestCheckBox.IsChecked = false;
                DevDB.Visibility = Visibility.Collapsed;
            }
        }

        //Realoads the datatable view of the database, in this case for the Users Table.
        private void RefreshDataGrid()
        {
            DataTable dataContext = GetInstanceOfDBConnection().GetDataSet("SELECT * FROM [dbo].[User]", "User");
            DevDB.ItemsSource = new DataView(dataContext);
        }

        //Generates test user and user preference data and loads it to the database.
        private void DummyDataButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName, lastName, email, password, phoneNum;
            
            //Allows for random variable names
            Random rnd = new();

            //Uses a random number to choose what user type the account will be
            string type = rnd.Next(1, 4) switch
            {
                1 => "Admin",
                2 => "RM",
                3 => "Client",
                _ => "RM",
            };

            firstName = "fname" + rnd.Next(9999);
            lastName = "lname" + rnd.Next(9999);
            email = rnd.Next(99999) + "@gmail.com";
            password = "dummy";
            phoneNum = rnd.Next(999999999).ToString() + rnd.Next(99).ToString();

            //Creates the account
            AccountOperations.CreateAccount(type, firstName, lastName, email, password, phoneNum);
            RefreshDataGrid();
        }

        //Deletes a specified user's account by ID
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserIDBox.Text.Length > 0)
            {
                User.DeleteAccount(Convert.ToInt32(UserIDBox.Text.ToString()));
                RefreshDataGrid();
                UserIDBox.Text = "";
            }
            else
            {
                MessageBox.Show("Enter an ID!");
            }
        }

        //Changes a user's data from the specified values
        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            //similar logic for checks used when creating an account
            //If the primary key exists
            if (Checks.CheckPKExists(GetInstanceOfDBConnection().GetDataSet("SELECT * FROM [dbo].[User]", "User"), Convert.ToInt32(UserIDModBox.Text)))
            {   
                //If the column exists
                if (Checks.CheckColumnExists(GetInstanceOfDBConnection().GetDataSet("SELECT * FROM [dbo].[User]", "User"), UserColumnBox.Text))
                {
                    //If the user wants to modify the account type
                    if (UserColumnBox.Text.Equals("Type"))
                    {
                        //Check if the entered account type exists
                        if (Checks.CheckIsValidAccountType(UserValueBox.Text))
                        {
                            AccountOperations.UpdateField(UserColumnBox.Text, Convert.ToInt32(UserIDModBox.Text), UserValueBox.Text);
                            UserColumnBox.Text = "";
                        }
                    }
                    else
                    {
                        AccountOperations.UpdateField(UserColumnBox.Text, Convert.ToInt32(UserIDModBox.Text), UserValueBox.Text);
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

            RefreshDataGrid();
        }

        //Shows the test window for editing an account
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new("Edit");
            createAccount.ShowDialog();
        }

        //Reloads the datagrid
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
