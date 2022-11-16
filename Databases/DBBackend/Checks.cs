using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

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
            if (var.Equals(EncryptDecrypt.Decrypt(currentDataSet.Tables[tableName].Rows[0][1].ToString())))
            {
                return true;
            }
            return false;
        }
        public static bool checkIsValidAccountType(string input)
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
        public static bool checkColumnExists(DataSet currentDataSet, string column)
        {
            Debug.WriteLine("Does column '" + column + "' exist?");
            foreach (DataColumn datcolumn in currentDataSet.Tables[0].Columns)
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

        public static bool checkPKExists(DataSet currentDataSet, int input)
        {
            Debug.WriteLine("Does primary key '" + input + "' exist?");

            DataRow[] dataRow = currentDataSet.Tables[0].Select("Id = '" + input + "'");
            if (dataRow.Length != 0)
            {
                Debug.WriteLine("Yes");
                return true;
            }
            Debug.WriteLine("No!");
            return false;
        }
    }
}
