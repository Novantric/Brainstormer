using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Data;

namespace Brainstormer.Classes
{
    //Handles the relationshp between two User types, mostly regarding client information.
    //In the future, Client could potentially be merged with this class.
    internal class User_RM
    {
        //Related to the scenarios client or RM data is accessed, e.g. save, delete, update.
        protected static int RMID, ClientID;
        public static string? clientScenario;

        //Sets the user to be the RM of another user.
        public static void addClient(int clientID)
        {
            RMID = Convert.ToInt32(User.UserID);
            ClientID = clientID;

            string QUERY = $"INSERT INTO [dbo].[User_RM] (RMID, ClientID) VALUES ({User.UserID}, {clientID})";
            Connection.getInstanceOfDBConnection().nonQueryOperation(QUERY);
        }

        //Returns a list of client IDs, from all the clients in the database.
        public static List<int> getClientIDs()
        {
            string QUERY = "SELECT ClientID FROM [dbo].[User_RM] WHERE RMID = " + User.UserID;
            DataTable resultsTable = (Connection.getInstanceOfDBConnection().getDataSet(QUERY, "[dbo].[User_RM]")).Tables[0];
            List<int> results = new();

            for (int i = 0; i < resultsTable.Rows.Count; i++)
            {
                results.Add((int)resultsTable.Rows[i]["ClientID"]);
            }
            return results;
        }

        //Removes the user-client relationship created by addClient.
        public static void removeClient(int clientID)
        {
            string QUERY = "DELETE FROM [dbo].[User_RM] WHERE ClientID = " + clientID + "AND RMID = " + User.UserID;
            Connection.getInstanceOfDBConnection().nonQueryOperation(QUERY);
        }
    }
}
