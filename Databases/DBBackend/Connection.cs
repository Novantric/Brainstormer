using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Brainstormer.Databases.DBBackend
{
    internal class Connection
    {
        //Obtained from the example code and modified
        private string DBConnStr = "Data Source=rmas-server.database.windows.net;Initial Catalog=RelationshipManagerAdministrationSystemDB;User ID=FreddieFaulkner;Password=Goonerfred03;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static Connection _instance;
        private SqlConnection DBConnection;

        /// constructor
        private Connection()
        {
            DBConnStr = "Data Source=rmas-server.database.windows.net;Initial Catalog=RelationshipManagerAdministrationSystemDB;User ID=FreddieFaulkner;Password=Goonerfred03;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
        public DataSet getDataSet(string sqlQuery, string tableName)
        {
            //create an empty dataset
            DataSet dataSet = new DataSet();

            using (DBConnection = new SqlConnection(DBConnStr))
            {
                //open the connection
                DBConnection.Open();


                SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, DBConnStr);

                adapter.Fill(dataSet, tableName);

                DBConnection.Close();

            }

            return dataSet;
        }

    }
}
