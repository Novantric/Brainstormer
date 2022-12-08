using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormer.Classes
{
    internal class Tags
    {
        public Tags() { }

        public static List<String> tagslist = new();

        public static void loadTags()
        {
            string QUERY = "SELECT Tag FROM [dbo].[Idea_Tags] GROUP BY Tag";
            DataTable tagsTable = (Connection.getInstanceOfDBConnection().getDataSet(QUERY, "[dbo].[Idea_Tags]")).Tables[0];

            for (int i = 0; i < tagsTable.Rows.Count; i++)
            {
                tagslist.Add(tagsTable.Rows[i]["Tag"].ToString());
            }
        }
    }
}
