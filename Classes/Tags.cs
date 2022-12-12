﻿using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Data;

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

        //Gets all the ideas with a specified tag 
        public static List<Idea> getIdeasWithTag(string tagName)
        {
            string QUERY = "SELECT IdeaID FROM [dbo].[Idea_Tags] WHERE Tag = '" + tagName + "' GROUP BY IdeaID";
            List<String> ideaIDlist = new();
            List<Idea> idealist = new();

            //Load Idea IDs that have the tag name
            DataTable tagTable = (Connection.getInstanceOfDBConnection().getDataSet(QUERY, "[dbo].[Idea_Tags]")).Tables[0];

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
                DataTable ideaTable = (Connection.getInstanceOfDBConnection().getDataSet(QUERYID, "[dbo].[Idea]")).Tables[0];

                //Add to the list
                idealist.Add(new Idea(ideaTable.Rows[i]["Id"]
                    .ToString(), ideaTable.Rows[i]["Title"].ToString(), ideaTable.Rows[i]["AssetType"].ToString(), ideaTable.Rows[i]["MajorSector"]
                    .ToString(), ideaTable.Rows[i]["MinorSector"].ToString(), ideaTable.Rows[i]["Reigion"].ToString(), ideaTable.Rows[i]["Currency"]
                    .ToString(), Convert.ToInt32(ideaTable.Rows[i]["RiskRating"]), DateTime.Parse(ideaTable.Rows[i]["CreationDate"]
                    .ToString()), DateTime.Parse(ideaTable.Rows[i]["ExpiryDate"].ToString()), Convert.ToDecimal(ideaTable.Rows[i]["SuggestedPrice"].ToString()), ideaTable.Rows[i]["Views"]
                    .ToString(), ideaTable.Rows[i]["UserID"].ToString(), ideaTable.Rows[i]["Colour"].ToString(), ideaTable.Rows[i]["Summary"].ToString(), ideaTable.Rows[i]["Content"].ToString()));
            }

            return idealist;
        }
    }
}
