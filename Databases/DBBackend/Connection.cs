using System.Data;
using System.Data.SqlClient;

namespace Brainstormer.Databases.DBBackend
{
    internal class Connection
    {
        //Obtained from the example code and modified
        private readonly string DBConnStr = Properties.Settings.Default.DatabaseConnectionString;
        private static Connection? _instance;
        private SqlConnection DBConnection;

        ///constructor
        private Connection()
        {
            DBConnection = new SqlConnection(DBConnStr);
        }

        public static Connection getInstanceOfDBConnection()
        {
            // create the object only if it doesn't exist  
            if (_instance == null)
                _instance = new Connection();
            return _instance;
        }


        //Returns a data set built based on the query. Used with datagrid views.
        public DataSet getDataSet(string sqlQuery, string tableName)
        {
            //create an empty dataset
            DataSet dataSet = new DataSet();

            DBConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, DBConnStr);
            adapter.Fill(dataSet, tableName);
            DBConnection.Close();

            return dataSet;
        }
    }
}
