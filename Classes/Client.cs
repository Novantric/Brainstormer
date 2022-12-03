﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormer.Classes
{
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