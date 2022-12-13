using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Brainstormer.Databases.DBBackend
{
    //Handles all communication between the database and the rest of the program. This was obtained from the example code and modified.

    internal class Connection
    {
        //Connection string, public so can be used in other places e.g. EncryptDecrypt
        public string connStr;
        private static Connection? _instance;
        private readonly SqlConnection connectionDB;

        ///constructor, initialises the connection string
        private Connection()
        {
            //try Brainstore, then Oldestore
            try
            {
                //creates the connection string
                string path1 = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"Databases\Brainstore.mdf");
                connStr = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""{0}"";Integrated Security = True", path1);

                //Creates a new connection and tests it
                connectionDB = new SqlConnection(connStr);
                connectionDB.Open();
                connectionDB.Close();
                Debug.WriteLine("Connected to Brainstore!");
            }
            catch (Exception)
            {
                try
                {
                    string path2 = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"Databases\Oldestore.mdf");
                    connStr = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""{0}"";Integrated Security = True", path2);

                    connectionDB = new SqlConnection(connStr);
                    connectionDB.Open();
                    connectionDB.Close();
                    Debug.WriteLine("Connected to Oldestore!");
                }
                catch (Exception)
                {
                    //Allows for the program to connect to an external database if a local one doesn't exist
                    connStr = "Enter an external connection string here!";
                    throw;
                }
            }
        }

        //Every time the database is accessed, create a new instance if there isn't one.
        //Note: do this with other classes in the future, e.g. the home page display
        public static Connection GetInstanceOfDBConnection()
        {
            _instance ??= new Connection();
            return _instance;
        }

        //Returns a dataTabe of results, used with datagrid views and SELECT statements.
        public DataTable GetDataSet(string sqlQuery, string tableName)
        {
            //create an empty dataset
            DataSet dataSet = new();

            //Fill the dataset with data
            connectionDB.Open();
            SqlDataAdapter adapter = new(sqlQuery, connStr);
            adapter.Fill(dataSet, tableName);
            connectionDB.Close();

            //Return the dataset as a table
            return dataSet.Tables[0];
        }

        //Executes SQL commands that aren't SELECT, such as INSERT INTO.
        public void NonQueryOperation(string query)
        {
            //Creates the connection
            SqlConnection connectionDB = new(connStr);
            SqlCommand command = new(query, connectionDB);

            //Executes the query
            connectionDB.Open();
            command.ExecuteNonQuery();
            connectionDB.Close();
        }
    }
}
