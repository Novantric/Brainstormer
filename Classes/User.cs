using Brainstormer.Databases.DBBackend;

namespace Brainstormer.Classes
{
    //The name is confusing, but this handles the data linked to the current user's account. For password modification, the user has to through the DB directly.
    public class User
    {
        public static string? UserID { get; protected set; }
        public static string? UserType { get; protected set; }
        public static string? UserFirstName { get; protected set; }
        public static string? UserLastName { get; protected set; }
        public static string? UserEmail { get; protected set; }
        public static string? UserPassword { get; protected set; }
        public static string? UserPhoneNum { get; protected set; }

        //Updates the current user's information.
        public static void UpdateData(string ID, string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            string query = $"UPDATE [dbo].[User] SET Type = '{type}', FirstName = '{firstName}', LastName = '{lastName}', Email = '{email}', Password = '{password}', PhoneNum = '{phoneNum}' WHERE Id = " + ID;
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
            LoadData(ID, type, firstName, lastName, email, password, phoneNum);
        }

        //Loads the new information to the public variables above.
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

        //Deletes the user's data and logs them out.
        public static void DeleteAccount(int ID)
        {
            string query = "DELETE FROM [dbo].[User] WHERE Id = '" + ID + "'";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);

            Logout();
        }

        //Logs the user out by deleting the locally stored data.
        public static void Logout()
        {
            UserID = "";
            UserType = "";
            UserFirstName = "";
            UserLastName = "";
            UserEmail = "";
            UserPassword = "";
            UserPhoneNum = "";

            UserSettings.ClearPreferences();
        }
    }
}
