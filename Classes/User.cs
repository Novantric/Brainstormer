using Brainstormer.Databases.DBBackend;
using Windows.Networking;

namespace Brainstormer.Classes
{
    public class User //better to choose an appropriate name
    {                       
        public static string? UserID { get; set; }
        public static string? UserType { get; set; }
        public static string? UserFirstName { get; set; }
        public static string? UserLastName { get; set; }
        public static string? UserEmail { get; set; }
        public static string? UserPassword { get; set; }
        public static string? UserPhoneNum { get; set; }

        public static void UpdateData(int primaryKey, string type, string firstName, string lastName, string email, string password, string phoneNum)
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

        public static void DeleteAccount(int ID)
        {
            string query = "DELETE FROM [dbo].[User] WHERE Id = '" + ID + "'";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);

            UserType = "";
            UserFirstName = "";
            UserLastName = "";
            UserEmail = "";
            UserPassword = "";
            UserPhoneNum = "";
        }

    }
}
