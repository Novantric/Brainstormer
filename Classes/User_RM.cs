using Brainstormer.Databases.DBBackend;
using System;
using System.Windows.Automation.Peers;

namespace Brainstormer.Classes
{
    internal class User_RM
    {
        protected static int RMID, ClientID;
        public static string? clientScenario;

        public static void addClient(int clientID)
        {
            RMID = Convert.ToInt32(User.UserID);
            ClientID = clientID;

            string QUERY = $"INSERT INTO [dbo].[User_RM] (RMID, ClientID) VALUES ({RMID}, {clientID})";
            Connection.getInstanceOfDBConnection().nonQueryOperation(QUERY);
        }
    }
}
