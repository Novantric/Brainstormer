namespace Brainstormer.Classes
{
    public class User //better to choose an appropriate name
    {
        string Type, FirstName, LastName, Email, Password, PhoneNum;

        public User(string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            Type = type;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNum = phoneNum;
        }
    }
}
