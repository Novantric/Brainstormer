using Brainstormer.Classes;
using System;
using System.Data;
using System.Diagnostics;
using Windows.Networking;
using static Brainstormer.Databases.DBBackend.Checks;
using static Brainstormer.Databases.DBBackend.Connection;
using static Brainstormer.Classes.User;

namespace Brainstormer.Databases.DBBackend
{
    internal class AccountOperations
    {

        public static bool Login(string username, string password)
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

        public static void GetUserData(string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string LOGINQUERY = "SELECT Id, Type, FirstName, LastName, PhoneNum FROM [dbo].[User] WHERE Email = '" + email + "' AND Password = '" + password + "'";
            string TABLENAME = "[dbo].[User]";

            DataSet details = Connection.getInstanceOfDBConnection().getDataSet(LOGINQUERY, TABLENAME);
            
            UserID = details.Tables[TABLENAME].Rows[0]["Id"].ToString();
            UserType = details.Tables[TABLENAME].Rows[0]["Type"].ToString();
            UserFirstName = details.Tables[TABLENAME].Rows[0]["FirstName"].ToString();
            UserLastName = details.Tables[TABLENAME].Rows[0]["LastName"].ToString();
            UserEmail = email;
            UserPassword = password;
            UserPhoneNum = details.Tables[TABLENAME].Rows[0]["PhoneNum"].ToString();
        }

        public static void CreateAccount(string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string query = $"INSERT INTO [dbo].[User] (Type,FirstName,LastName,Email,Password,PhoneNum) VALUES ('{type}','{firstName}','{lastName}','{email}','{password}','{phoneNum}')";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }

        public static void DeleteAccount(int ID)
        {
            string query = "DELETE FROM [dbo].[User] WHERE Id = '" + ID + "'";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }

        public static void UpdateField(string columnName, int primaryKey, string value)
        {
            string query = "UPDATE [dbo].[User] SET " + columnName + " = '" + value + "' WHERE Id = " + primaryKey;
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }

        public static void UpdateEverything(int primaryKey, string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string query = $"UPDATE [dbo].[User] SET Type = '{type}', FirstName = '{firstName}', LastName = '{lastName}', Email = '{email}', Password = '{password}', PhoneNum = '{phoneNum}' WHERE Id = " + primaryKey;
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);

            UserType = type;
            UserFirstName = firstName;
            UserLastName = lastName;
            UserEmail = email;
            UserPassword = password;
            UserPhoneNum = phoneNum;
        }
    }
}
