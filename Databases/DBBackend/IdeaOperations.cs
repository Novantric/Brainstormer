using Brainstormer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Databases.DBBackend
{
    internal class IdeaOperations
    {
        private static List<Idea> IdeaList = new();

        public static List<Idea> preloadIdeas()
        {
            string QUERY = "SELECT * FROM [dbo].[Idea]";
            string TABLE = "[dbo].[Idea]";
            DataTable ideas = (getInstanceOfDBConnection().getDataSet(QUERY, TABLE)).Tables[0];

            IdeaList.Clear();
            for (int i = 0; i < ideas.Rows.Count; i++)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                IdeaList.Add(new Idea(ideas.Rows[i]["Id"].ToString(), ideas.Rows[i]["Title"].ToString(), ideas.Rows[i]["AssetType"].ToString(), ideas.Rows[i]["MajorSector"].ToString(), ideas.Rows[i]["MinorSector"].ToString(), ideas.Rows[i]["Reigion"].ToString(), ideas.Rows[i]["Currency"].ToString(), Convert.ToInt32(ideas.Rows[i]["RiskRating"]), DateTime.Parse(ideas.Rows[i]["CreationDate"].ToString()), DateTime.Parse(ideas.Rows[i]["ExpiryDate"].ToString()), Convert.ToDecimal(ideas.Rows[i]["SuggestedPrice"].ToString()), ideas.Rows[i]["Views"].ToString(), ideas.Rows[i]["UserID"].ToString(), ideas.Rows[i]["Colour"].ToString(), ideas.Rows[i]["Summary"].ToString(), ideas.Rows[i]["Content"].ToString()));
#pragma warning restore CS8604 // Possible null reference argument.
            }

            return IdeaList;
        }


    }
}
