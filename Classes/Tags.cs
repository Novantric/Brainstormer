using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Data;

namespace Brainstormer.Classes
{
    //Handles information relating to tags.
    internal class Tags
    {
        public Tags() { }

        //Stores all the tags in the database.
        public static List<string> tagslist = new();

        //Loads tags into the list above.
        public static void LoadTags()
        {
            string QUERY = "SELECT Tag FROM [dbo].[Idea_Tags] GROUP BY Tag";
            DataTable tagsTable = Connection.GetInstanceOfDBConnection().GetDataSet(QUERY, "[dbo].[Idea_Tags]");

            tagslist.Clear();
            for (int i = 0; i < tagsTable.Rows.Count; i++)
            {
                tagslist.Add(tagsTable.Rows[i]["Tag"].ToString());
            }
        }

        //Gets all the ideas with a specified tag 
        public static List<Idea> getIdeasWithTag(string tagName)
        {
            string QUERY = "SELECT IdeaID FROM [dbo].[Idea_Tags] WHERE Tag = '" + tagName + "' GROUP BY IdeaID";
            List<string> ideaIDlist = new();
            List<Idea> idealist = new();

            //Load Idea IDs that have the tag name
            DataTable tagTable = (Connection.GetInstanceOfDBConnection().GetDataSet(QUERY, "[dbo].[Idea_Tags]"));

            //Add the IDs to a list
            for (int i = 0; i < tagTable.Rows.Count; i++)
            {
                ideaIDlist.Add(tagTable.Rows[i]["IdeaID"].ToString());
            }


            //Add each matching idea to the list
            for (int i = 0; i < ideaIDlist.Count; i++)
            {
                //Query the next idea
                string QUERYID = "SELECT * FROM [dbo].[Idea] WHERE Id = " + ideaIDlist[i];
                DataTable ideaTable = (Connection.GetInstanceOfDBConnection().GetDataSet(QUERYID, "[dbo].[Idea]"));

                //Add to the list
                idealist.Add(new Idea(ideaTable.Rows[0]["Id"]
                    .ToString(), ideaTable.Rows[0]["Title"].ToString(), ideaTable.Rows[0]["AssetType"].ToString(), ideaTable.Rows[0]["MajorSector"]
                    .ToString(), ideaTable.Rows[0]["MinorSector"].ToString(), ideaTable.Rows[0]["Reigion"].ToString(), ideaTable.Rows[0]["Currency"]
                    .ToString(), Convert.ToInt32(ideaTable.Rows[0]["RiskRating"]), DateOnly.FromDateTime(DateTime.Parse(ideaTable.Rows[0]["CreationDate"].ToString())), DateOnly.FromDateTime(DateTime.Parse(ideaTable.Rows[0]["ExpiryDate"].ToString())),
                    Convert.ToDecimal(ideaTable.Rows[0]["SuggestedPrice"].ToString()), ideaTable.Rows[0]["Views"].ToString(),
                    ideaTable.Rows[0]["UserID"].ToString(), ideaTable.Rows[0]["Colour"].ToString(), ideaTable.Rows[0]["Summary"].ToString(), ideaTable.Rows[0]["Content"].ToString()));
            }

            return idealist;
        }

        //Deletes all tags with the matching ideaID
        public static void DeleteTags(int ideaID)
        {
            Connection.GetInstanceOfDBConnection().NonQueryOperation("DELETE FROM [dbo].[Idea_Tags] WHERE IdeaID = " + ideaID);
        }
    }
}
