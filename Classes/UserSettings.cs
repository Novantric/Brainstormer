using Brainstormer.Databases.DBBackend;
using System;
using System.Data;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Classes
{
    internal class UserSettings
    {
        public static string? PrefferedRegion { get; protected set; }
        public static string? PrefferedCurrency { get; protected set; }
        public static string? PrefferedMajorSector { get; protected set; }
        public static string? PrefferedMinorSector { get; protected set; }
        public static string? PrefferedProductType { get; protected set; }
        public static string? PrefferedRiskRating { get; protected set; }

        public static void loadPreferences()
        {
            string QUERY = $"SELECT * FROM [dbo].[User_Preferences] WHERE UserID = '{User.UserID}'";
            string TABLE = "[dbo].[User_Preferences]";
            DataTable preferences = (getInstanceOfDBConnection().getDataSet(QUERY, TABLE)).Tables[0];

            PrefferedRegion = preferences.Rows[0]["CurrentRegion"].ToString();
            PrefferedCurrency = preferences.Rows[0]["PreferredCurrency"].ToString();
            PrefferedMajorSector = preferences.Rows[0]["PreferredMajorSector"].ToString();
            PrefferedMinorSector = preferences.Rows[0]["PreferredMinorSector"].ToString();
            PrefferedProductType = preferences.Rows[0]["PreferredProductType"].ToString();
            PrefferedRiskRating = preferences.Rows[0]["PreferredRiskRating"].ToString();

        }
        public static void savePreferences(string region, string currency, string major, string minor, string product, double risk)
        {
            string query = ($"UPDATE [dbo].[User_Preferences] SET CurrentRegion = '{region}', PreferredCurrency = '{currency}', PreferredMajorSector = '{major}', PreferredMinorSector = '{minor}', PreferredProductType = '{product}', PreferredRiskRating = '{risk}' WHERE UserID = " + Convert.ToInt32(User.UserID));
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);

            PrefferedRegion = region;
            PrefferedCurrency = currency;
            PrefferedMajorSector = major;
            PrefferedMinorSector = minor;
            PrefferedProductType = product;
            PrefferedRiskRating = risk.ToString();


        }
        public static void DeletePreferences()
        {
            Connection.getInstanceOfDBConnection().nonQueryOperation("DELETE FROM [dbo].[User_Preferences] WHERE UserID = '" + User.UserID + "'");

            PrefferedRegion = "";
            PrefferedCurrency = "";
            PrefferedMajorSector = "";
            PrefferedMinorSector = "";
            PrefferedProductType = "";
            PrefferedRiskRating = "";
        }
    }
}
