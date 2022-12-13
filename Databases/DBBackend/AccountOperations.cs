using Brainstormer.Classes;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using static Brainstormer.Classes.User;
using static Brainstormer.Databases.DBBackend.Checks;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Databases.DBBackend
{
    //Handles operations that involve a user's account, for example logging in.
    internal class AccountOperations
    {
        //A list of all the clients in the database, only filled when called later.
        private static readonly List<Client> Clients = new();

        //Logs the user in after performing checks.
        public static bool Login(string username, string password)
        {
            string LOGINQUERY = "SELECT Email, Password FROM [dbo].[User] WHERE Email = '" + username + "'";
            string TABLENAME = "[dbo].[User]";

            //If the username and password do not contain invalic chars
            if (ValidateData(username, password))
            {
                //Query for the username and password
                DataTable loginDataset = GetInstanceOfDBConnection().GetDataSet(LOGINQUERY, TABLENAME);
                //If the user exists and the password matches
                if (CheckUserExist(loginDataset) && CheckIsMatch(loginDataset, password))
                {
                    //Tell the calling sub that the information is correct
                    return true;
                }
            }
            return false;
        }

        //Checks if the data has any errors, like spaces
        protected static bool ValidateData(string username, string password)
        {
            Debug.WriteLine("Field Validation Ran");
            if (CheckIsBlank(username) == false && CheckHasSpace(username) == false && CheckIsBlank(password) == false && CheckHasSpace(password) == false)
            {
                return true;
            }
            return false;
        }

        //Loads the user information to local storage. Presumes that the correct username and password have been entered.
        public static void CheckUserData(string email, string password)
        {
            string LOGINQUERY = "SELECT Id, Type, FirstName, LastName, PhoneNum FROM [dbo].[User] WHERE Email = '" + email + "' AND Password = '" + EncryptDecrypt.Encrypt(password) + "'";
            string TABLENAME = "[dbo].[User]";

            DataTable details = GetInstanceOfDBConnection().GetDataSet(LOGINQUERY, TABLENAME);

            //Disables the warning when importing data from a datatable.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            LoadData(details.Rows[0]["Id"].ToString(),
                details.Rows[0]["Type"].ToString(),
                details.Rows[0]["FirstName"].ToString(),
                details.Rows[0]["LastName"].ToString(),
                email,
                password,
                details.Rows[0]["PhoneNum"].ToString());
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }

        //Loads a user's data from their ID, used with e.g. tags.
        public static string[] GetUserData(int id)
        {
            string QUERY = "SELECT Email, FirstName, LastName, PhoneNum FROM [dbo].[User] WHERE Id = " + id;
            DataTable details = (GetInstanceOfDBConnection().GetDataSet(QUERY, "[dbo].[User]"));

            //Removes the warning for getting information from a datatable.
#pragma warning disable CS8601 // Possible null reference assignment.
            //Saves result to an array for storage efficiency.
            string[] result = { details.Rows[0]["Email"].ToString(), details.Rows[0]["FirstName"].ToString(), details.Rows[0]["LastName"].ToString(), details.Rows[0]["PhoneNum"].ToString() };
#pragma warning restore CS8601 // Possible null reference assignment.
            return result;
        }

        //Creates an account using the input data, presuming it's been validated.
        public static void CreateAccount(string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            //Creates the user account
            string query = $"INSERT INTO [dbo].[User] (Type,FirstName,LastName,Email,Password,PhoneNum) VALUES ('{type}','{firstName}','{lastName}','{email}','{EncryptDecrypt.Encrypt(password)}','{phoneNum}')";
            GetInstanceOfDBConnection().NonQueryOperation(query);

            //Gets the user ID of the new account
            DataTable id = GetInstanceOfDBConnection().GetDataSet($"SELECT Id FROM [dbo].[User] WHERE Email = '{email}' AND Password = '{EncryptDecrypt.Encrypt(password)}'", "[dbo].[User]");

            //Uses the user ID to create default user Preferences
            string preferences = $"INSERT INTO [dbo].[User_Preferences] (PreferredRiskRating,PreferredProductType,PreferredCurrency,PreferredMajorSector,PreferredMinorSector,CurrentRegion,UserID) VALUES (0,'none','none','none','none','none','{(int)id.Rows[0]["Id"]}')";
            GetInstanceOfDBConnection().NonQueryOperation(preferences);
        }

        //Allows for updating user information, when given the exact ID, column and value.
        public static void UpdateField(string columnName, int primaryKey, string value)
        {
            string query = "UPDATE [dbo].[User] SET " + columnName + " = '" + value + "' WHERE Id = " + primaryKey;
            GetInstanceOfDBConnection().NonQueryOperation(query);
        }

        //Returns a list of all the users that are clients
        public static List<Client> GetClients()
        {
            //Gets all the clients
            string QUERY = "SELECT Id, Type, Email, FirstName, LastName, PhoneNum FROM [dbo].[User] WHERE Type = 'Client'";
            DataTable clientTable = (GetInstanceOfDBConnection().GetDataSet(QUERY, "[dbo].[User]"));

            //Loads the clients into a object list
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