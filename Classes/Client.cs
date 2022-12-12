namespace Brainstormer.Classes
{
    //An object class, used to represent a type of user called a Client.
    class Client
    {
        public string UserID, UserType, UserFirstName, UserLastName, UserEmail, UserPhoneNum;

        public Client(string userID, string userType, string userLastName, string userFirstName, string userEmail, string userPhoneNum)
        {
            UserID = userID;
            UserType = userType;
            UserLastName = userLastName;
            UserFirstName = userFirstName;
            UserEmail = userEmail;
            UserPhoneNum = userPhoneNum;
        }
    }
}
