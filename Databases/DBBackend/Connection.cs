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

        //@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\whyrl\OneDrive - Anglia Ruskin University\Documents\Year2\Tri1\Software Engineering\myturn\Brainstormer\Databases\Database.mdf"";Integrated Security=True";
        //Connstr works with local paths
        private string connStr;
        private static Connection? _instance;
        private SqlConnection connectionDB;

        ///constructor
        private Connection()
        {
            string fileName = @"Databases\Database.mdf";
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
    }
}
