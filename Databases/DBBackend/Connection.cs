using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Brainstormer.Databases.DBBackend
{
    internal class Connection
    {
        //Obtained from the example code and modified

        //Connstr works with local paths
        public string connStr;
        private static Connection? _instance;
        private SqlConnection connectionDB;

        ///constructor
        private Connection()
        {
            //try brainstore, then another
            string path1 = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"Databases\Brainstore.mdf");
            string path2 = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"Databases\Oldestore.mdf");

            try
            {
                connStr = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""{0}"";Integrated Security = True", path1);
                connectionDB = new SqlConnection(connStr);
                connectionDB.Open();
                connectionDB.Close();
                Debug.WriteLine("Connected to Brainstore!");
            }
            catch (Exception)
            {
                connStr = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""{0}"";Integrated Security = True", path2);
                connectionDB = new SqlConnection(connStr);
                Debug.WriteLine("Connected to Oldestore!");
            }
        }

        public static Connection getInstanceOfDBConnection()
        {
            //create the object only if it doesn't exist  
            if (_instance == null)
                _instance = new Connection();
            return _instance;
        }


        //Returns a data set built based on the query. Used with datagrid views.
        public DataSet getDataSet(string sqlQuery, string tableName)
        {
            //create an empty dataset
            DataSet dataSet = new DataSet();

            connectionDB.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connStr);
            adapter.Fill(dataSet, tableName);
            connectionDB.Close();

            return dataSet;
        }

        public void nonQueryOperation(string query)
        {
            SqlConnection connectionDB = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand(query, connectionDB);

            connectionDB.Open();
            command.ExecuteNonQuery();
            connectionDB.Close();
        }
    }
}
