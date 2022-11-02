using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Brainstormer.Databases.DBBackend
{
    internal class Connection
    {
        //Obtained from the example code and modified
        private String DBConnStr = Properties.Settings.Default.DatabaseConnectionString;
        private static Connection _instance;
        private SqlConnection DBConnection;

        /// constructor
        private Connection()
        {
            DBConnStr = Properties.Settings.Default.DatabaseConnectionString;
        }

        ///methods
        /**
         * a static method that creates an unique object of the class itself
         */
        public static Connection getInstanceOfDBConnection()
        {
            // create the object only if it doesn't exist  
            if (_instance == null)
                _instance = new Connection();
            return _instance;
        }

        /**
         * Returns a data set built based on the query sent as parameter
         */
        public DataSet getDataSet(string sqlQuery)
        {
            //create an empty dataset
            DataSet dataSet = new DataSet();

            using (DBConnection = new SqlConnection(DBConnStr))
            {
                //open the connection
                DBConnection.Open();

                //create the object dataAdapter to send a query to the DB
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, DBConnection);

                dataAdapter.Fill(dataSet);

            }

            return dataSet;
        }
    }
}
