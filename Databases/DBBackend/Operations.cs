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
        public static bool login(string username, string password)
        {
            string LOGINQUERY = "SELECT Email, Password FROM [dbo].[User] WHERE Email = '" + username + "'";
            string TABLENAME = "User";

            if (ValidateData(username, password))
            {
                DataSet loginDataset = Connection.getInstanceOfDBConnection().getDataSet(LOGINQUERY, "User");
                if (checkUserExist(loginDataset, TABLENAME) == true && checkIsMatch(loginDataset, TABLENAME, password) == true)
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
            if (checkIsBlank(username) == false && checkHasSpace(username) == false && checkIsBlank(password) == false && checkHasSpace(password) == false)
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
