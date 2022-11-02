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

        public static bool login(String role, String username, String password)
        {
            string[] loginDetails = { role, username, password };
            if (ValidateData(loginDetails))
            {
                DataSet loginDataset = Connection.getInstanceOfDBConnection().getDataSet("SELECT Email, Password FROM [dbo].[User] WHERE Email = '" + username +"'");
                if (checkUserExist(loginDataset, "User"))
                {
                    return true;
                }
            }
            return false;
        }

        //Checks if the data has any errors
        protected static bool ValidateData(string[] values)
        {
            Debug.WriteLine("CC RAN");
            int counter = 0;

            for (int i = 0; i < values.Length; i++)
            {
                if (!(checkIsBlank(values[i]) && checkHasSpace(values[i])))
                {
                    counter += 1;
                }
            }

            if (counter == values.Length)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
