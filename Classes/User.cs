using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormer.Classes
{
    public class User //better to choose an appropriate name
    {
        string Id, Type, FirstName, LastName, Email, Password, PhoneNum;

        public User(string id, string type, string firstName, string lastName, string email, string password, string phoneNum)
        {
            Id = id;
            Type = type;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNum = phoneNum;
        }
    }
}
