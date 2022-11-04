using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormer.Databases.DBBackend
{
    internal class Checks
    {
        public static bool checkIsBlank(string var)
        {
            Debug.WriteLine("Is blank?");

            Debug.WriteLine(string.IsNullOrEmpty(var));

            if (string.IsNullOrEmpty(var))
            {
                Debug.WriteLine("Yes!");
                return true;
            }
            Debug.WriteLine("No!");
            return false;
        }

        public static bool checkHasSpace(string var)
        {
            Debug.WriteLine("Has spaces?");

            if (var.Contains(" ") || var.Contains(" "))
            {
                Debug.WriteLine("Yes!");
                return true;
            }
            Debug.WriteLine("No!");
            return false;
        }
        public static bool checkIsAllInt(string var)
        {
            Debug.WriteLine("Is this string all numbers?");
            if (var.All(char.IsDigit))
            {
                Debug.WriteLine("Yes!");
                return true;
            }
            Debug.WriteLine("No!");
            return false;
        }

        public static bool checkUserExist(DataSet currentDataSet, string tableName)
        {
            Debug.WriteLine("Does this user exist in the table?");

            if (currentDataSet.Tables[tableName].Rows.Count == 0)
            {
                Debug.WriteLine("No!");
                return false;
            }
            Debug.WriteLine("Yes!");
            return true;
        }

        public static bool checkIsMatch(DataSet currentDataSet, string tableName, string var)
        {
            Debug.WriteLine("Do the passwords match?");
            if (var.Equals(currentDataSet.Tables[tableName].Rows[0][1].ToString()))
            {
                return true;
            }
            return false;
        }
    }
}
