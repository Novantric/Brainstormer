using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Controls.Primitives;
using static Brainstormer.Databases.DBBackend.Checks;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Databases.DBBackend
{
    internal class Operations
    {
        //Obtained from the example code and modified
        private String DBConnStr = Properties.Settings.Default.DatabaseConnectionString;
        private static Operations _instance;
        private SqlConnection DBConnection;

        /// constructor
        private Operations()
        {
            DBConnStr = Properties.Settings.Default.DatabaseConnectionString;

        }
        public static Operations getInstanceOfDBConnection()
        {
            // create the object only if it doesn't exist  
            if (_instance == null)
                _instance = new Operations();
            return _instance;
        }

        public static bool login(string username, string password)
        {
            if (ValidateData(username, password))
            {
                DataSet loginDataset = Connection.getInstanceOfDBConnection().getDataSet("SELECT Email, Password FROM [dbo].[tblRelationshipManagers] WHERE Email = '" + username + "'", "RM");
                if (checkUserExist(loginDataset, "RM") == true && checkIsMatch(loginDataset, "RM", password) == true)
                {
                    return true;
                }
            }
            return false;
        }

        //Checks if the data has any errors
        protected static bool ValidateData(string username, string password)
        {
            Debug.WriteLine("CC RAN");
            if (checkIsBlank(username) == false && checkHasSpace(username) == false)
            {
                if (checkIsBlank(password) == false && checkHasSpace(password) == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}
