using System.Data;
using System.Data.SqlClient;

namespace Brainstormer.Databases.DBBackend
{
    internal class Connection
    {
        //Obtained from the example code and modified
        private readonly string connStr = Properties.Settings.Default.DatabaseConnectionString;
        private static Connection? _instance;
        private SqlConnection connectionDB;

        ///constructor
        private Connection()
        {
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
