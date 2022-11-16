using Brainstormer.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using static Brainstormer.Databases.DBBackend.Checks;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Databases.DBBackend
{
    internal class AccountOperations
    {
        public static bool login(string username, string password)
        {
            string LOGINQUERY = "SELECT Email, Password FROM [dbo].[User] WHERE Email = '" + username + "'";
            string TABLENAME = "[dbo].[User]";

            if (ValidateData(username, password))
            {
                DataSet loginDataset = getInstanceOfDBConnection().getDataSet(LOGINQUERY, TABLENAME);
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

        public static void getUserData(string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string LOGINQUERY = "SELECT Type, FirstName, LastName, PhoneNum FROM [dbo].[User] WHERE Email = '" + email + "' AND Password = '" + password + "'";
            string TABLENAME = "User";

            DataSet details = Connection.getInstanceOfDBConnection().getDataSet(LOGINQUERY, TABLENAME);

            type = details.Tables[TABLENAME].Rows[0]["Type"].ToString();
            firstName = details.Tables[TABLENAME].Rows[0]["FirstName"].ToString();
            lastName = details.Tables[TABLENAME].Rows[0]["LastName"].ToString();
            phoneNum = details.Tables[TABLENAME].Rows[0]["PhoneNum"].ToString();

            User currentUser = new User(type, firstName, lastName, email, password, phoneNum);
        }

        public static void createAccount(string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string query = $"INSERT INTO [dbo].[User] (Type,FirstName,LastName,Email,Password,PhoneNum) VALUES ('{type}','{firstName}','{lastName}','{email}','{password}','{phoneNum}')";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }

        public static void deleteAccount(int ID)
        {
            string query = "DELETE FROM [dbo].[User] WHERE Id = '" + ID + "'";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }

        public static void updateField(string columnName, int primaryKey, string value)
        {
            string query = "UPDATE [dbo].[User] SET " + columnName + " = '" + value + "' WHERE Id = " + primaryKey;
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }
    }
}
