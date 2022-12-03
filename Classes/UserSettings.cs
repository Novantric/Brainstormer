using Brainstormer.Databases.DBBackend;
using System.Data;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Classes
{
    internal class UserSettings
    {
        public static string? PrefferedRegion { get; private set; }
        public static string? PrefferedCurrency { get; private set; }
        public static string? PrefferedMajorSector { get; private set; }
        public static string? PrefferedMinorSector { get; private set; }
        public static string? PrefferedProductType { get; private set; }
        public static string? PrefferedRiskRating { get; private set; }

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
            PrefferedRegion = region;
            PrefferedCurrency = currency;
            PrefferedMajorSector = major;
            PrefferedMinorSector = minor;
            PrefferedProductType = product;
            PrefferedRiskRating = (risk / 2).ToString();

            Connection.getInstanceOfDBConnection().nonQueryOperation(($"UPDATE [dbo].[User_Preferences] SET CurrentRegion = '{PrefferedRegion}', PreferredCurrency = '{PrefferedCurrency}', PreferredMajorSector = '{PrefferedMajorSector}', PreferredMinorSector = '{PrefferedMinorSector}', PreferredProductType = '{PrefferedProductType}', PreferredRiskRating = '{PrefferedRiskRating}' WHERE Id = " + User.UserID));
        }
    }
}
