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
            Debug.WriteLine("Is Blank check RAN");

            Debug.WriteLine(String.IsNullOrEmpty(var));

            if ((String.IsNullOrEmpty(var)))
            {
                return true;
            }
            return false;
        }
        public static bool checkHasSpace(string var)
        {
            Debug.WriteLine("Has Space Check RAN");

            if (var.Contains(" ") || var.Contains(" "))
            {
                return true;
            }
            return false;
        }
        public static bool checkUserExist(DataSet currentDataSet, string tableName)
        {
            Debug.WriteLine("CUE RAN");

            if (currentDataSet.Tables[tableName].Rows.Count == 0)
            {
                return false;
            }
            return true;
        }
        public static bool checkIsMatch(DataSet currentDataSet, string tableName, string var)
        {
            Debug.WriteLine("match check RAN");
            if (var.Equals(currentDataSet.Tables[tableName].Rows[0][1].ToString()))
            {
                return true;
            }
            return false;
        }
    }
}
