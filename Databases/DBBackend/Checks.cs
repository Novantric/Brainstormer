using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Brainstormer.Databases.DBBackend
{
    internal class Checks
    {
        //Checks if the input string has no characters
        public static bool CheckIsBlank(string var)
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

        //Checks if the input string has spaces
        public static bool CheckHasSpace(string var)
        {
            Debug.WriteLine("Has spaces?");

            if (var.Contains(' '))
            {
                Debug.WriteLine("Yes!");
                return true;
            }
            Debug.WriteLine("No!");
            return false;
        }

        //Checks if the input string is all numbers (e.g. for entering a phone number)
        public static bool CheckIsAllInt(string var)
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

        //Checks if the user Email exists in the database
        public static bool CheckUserExist(DataTable currentDataTable)
        {
            Debug.WriteLine("Does this user exist in the table?");

            //If the datatable has no rows, the the user doesn't exist
            if (currentDataTable.Rows.Count == 0)
            {
                Debug.WriteLine("No!");
                return false;
            }
            Debug.WriteLine("Yes!");
            return true;
        }

        //Checks if the entered password matches with the database
        public static bool CheckIsMatch(DataTable currentDataTable, string var)
        {
            Debug.WriteLine("Do the passwords match?");
            //from the db, load the password, makes it a string and decrypt it
            if (var.Equals(EncryptDecrypt.Decrypt(currentDataTable.Rows[0][1].ToString())))
            {
                Debug.WriteLine("Yes!");
                return true;
            }
            Debug.WriteLine("No!");
            return false;
        }

        //The following are mostly used when editing account data.
        //Checks if the entered account type is valid, 
        public static bool CheckIsValidAccountType(string input)
        {
            Debug.WriteLine("Does this account type exist?");
            if (input.Equals("Admin") || input.Equals("RM") || input.Equals("Client"))
            {
                return true;
            }
            else
            {
                MessageBox.Show("The specified account type does not exist!");
                return false;
            }
        }

        //Checks if the entered column name exists
        public static bool CheckColumnExists(DataTable currentDataTable, string column)
        {
            Debug.WriteLine("Does column '" + column + "' exist?");
            //Checks every column for a match
            foreach (DataColumn datcolumn in currentDataTable.Columns)
            {
                if (datcolumn.ColumnName == column)
                {
                    Debug.WriteLine("Yes");
                    return true;
                }
            }
            Debug.WriteLine("No!");
            return false;
        }

        //Checks if the primary key exists in the datatable
        public static bool CheckPKExists(DataTable currentDataTable, int input)
        {
            Debug.WriteLine("Does primary key '" + input + "' exist?");

            DataRow[] dataRow = currentDataTable.Select("Id = '" + input + "'");
            if (dataRow.Length == 0)
            {
                Debug.WriteLine("No!");
                return false;
            }
            Debug.WriteLine("Yes");
            return true;
        }
    }
}
