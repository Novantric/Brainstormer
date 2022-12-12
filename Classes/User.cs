using Brainstormer.Databases.DBBackend;

namespace Brainstormer.Classes
{
    public class User //better to choose an appropriate name
    {
        public static string? UserID { get; protected set; }
        public static string? UserType { get; protected set; }
        public static string? UserFirstName { get; protected set; }
        public static string? UserLastName { get; protected set; }
        public static string? UserEmail { get; protected set; }
        public static string? UserPassword { get; protected set; }
        public static string? UserPhoneNum { get; protected set; }

        public static void UpdateData(string ID, string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string query = $"UPDATE [dbo].[User] SET Type = '{type}', FirstName = '{firstName}', LastName = '{lastName}', Email = '{email}', Password = '{password}', PhoneNum = '{phoneNum}' WHERE Id = " + ID;
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
            LoadData(ID, type, firstName, lastName, email, password, phoneNum);

        }

        public static void LoadData(string ID, string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            UserID = ID;
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

            Logout();
        }

        public static void Logout()
        {
            UserID = "";
            UserType = "";
            UserFirstName = "";
            UserLastName = "";
            UserEmail = "";
            UserPassword = "";
            UserPhoneNum = "";
        }

    }
}
