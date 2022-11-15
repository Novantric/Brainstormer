using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;

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
            string fileName = @"Databases\Brainstore.mdf";
            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, fileName);
            connStr = string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""{0}"";Integrated Security = True", path);
            connectionDB = new SqlConnection(connStr);
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
