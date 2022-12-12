using Brainstormer.Classes;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using static Brainstormer.Classes.User;
using static Brainstormer.Databases.DBBackend.Checks;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Databases.DBBackend
{
    internal class AccountOperations
    {
        private static readonly List<Client> Clients = new();

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

        //Run after the login has been confirmed
        public static void CheckUserData(string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string LOGINQUERY = "SELECT Id, Type, FirstName, LastName, PhoneNum FROM [dbo].[User] WHERE Email = '" + email + "' AND Password = '" + EncryptDecrypt.Encrypt(password) + "'";
            string TABLENAME = "[dbo].[User]";

            DataSet details = Connection.getInstanceOfDBConnection().getDataSet(LOGINQUERY, TABLENAME);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            LoadData(details.Tables[TABLENAME].Rows[0]["Id"].ToString(),
                details.Tables[TABLENAME].Rows[0]["Type"].ToString(),
                details.Tables[TABLENAME].Rows[0]["FirstName"].ToString(),
                details.Tables[TABLENAME].Rows[0]["LastName"].ToString(),
                email,
                password,
                details.Tables[TABLENAME].Rows[0]["PhoneNum"].ToString());
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }

        public static string[] GetUserData(int id)
        {
            string QUERY = "SELECT Email, FirstName, LastName, PhoneNum FROM [dbo].[User] WHERE Id = " + id;

            DataTable details = (getInstanceOfDBConnection().getDataSet(QUERY, "[dbo].[User]")).Tables[0];

#pragma warning disable CS8601 // Possible null reference assignment.
            string[] result = { details.Rows[0]["Email"].ToString(), details.Rows[0]["FirstName"].ToString(), details.Rows[0]["LastName"].ToString(), details.Rows[0]["PhoneNum"].ToString() };
#pragma warning restore CS8601 // Possible null reference assignment.
            return result;
        }

        public static void CreateAccount(string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string query = $"INSERT INTO [dbo].[User] (Type,FirstName,LastName,Email,Password,PhoneNum) VALUES ('{type}','{firstName}','{lastName}','{email}','{EncryptDecrypt.Encrypt(password)}','{phoneNum}')";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);

            DataSet id = Connection.getInstanceOfDBConnection().getDataSet($"SELECT Id FROM [dbo].[User] WHERE Email = '{email}' AND Password = '{EncryptDecrypt.Encrypt(password)}'", "[dbo].[User]");
            int tempID = (int)id.Tables["[dbo].[User]"].Rows[0]["Id"];

            string preferences = $"INSERT INTO [dbo].[User_Preferences] (PreferredRiskRating,PreferredProductType,PreferredCurrency,PreferredMajorSector,PreferredMinorSector,CurrentRegion,UserID) VALUES (0,'none','none','none','none','none','{tempID}')";
            Connection.getInstanceOfDBConnection().nonQueryOperation(preferences);
        }

        public static void UpdateField(string columnName, int primaryKey, string value)
        {
            string query = "UPDATE [dbo].[User] SET " + columnName + " = '" + value + "' WHERE Id = " + primaryKey;
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }

        public static List<Client> getClients()
        {
            string QUERY = "SELECT Id, Type, Email, FirstName, LastName, PhoneNum FROM [dbo].[User] WHERE Type = 'Client'";
            DataTable clientTable = (getInstanceOfDBConnection().getDataSet(QUERY, "[dbo].[User]")).Tables[0];

            Clients.Clear();
            for (int i = 0; i < clientTable.Rows.Count; i++)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                Clients.Add(new Client(clientTable.Rows[i]["Id"].ToString(), clientTable.Rows[i]["Type"].ToString(), clientTable.Rows[i]["LastName"]
                    .ToString(), clientTable.Rows[i]["FirstName"].ToString(), clientTable.Rows[i]["Email"].ToString(), clientTable.Rows[i]["PhoneNum"].ToString()));
#pragma warning restore CS8604 // Possible null reference argument.
            }

            return Clients;
        }

    }
}
