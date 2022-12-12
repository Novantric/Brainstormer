using Brainstormer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Databases.DBBackend
{
    internal class IdeaOperations
    {
        //Stores all the idea that are in the database.
        private static readonly List<Idea> IdeaList = new();

        //loads ideas into the public list above when called.
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

        //Creates an idea from the information passed in, and adds it to the database.
        public static void CreateIdea(string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, decimal ideaRiskRating, DateTime creationDate, DateTime expiryDate, decimal suggestedPrice, string creatorID, string colour, string ideaSummary, string ideaContent)
        {
            string query = $"INSERT INTO [dbo].[Idea] (Title,AssetType,MajorSector,MinorSector,Reigion,Currency,RiskRating,CreationDate,ExpiryDate,SuggestedPrice,Views,UserID,Colour,Summary,Content) VALUES ('{ideaTitle}','{ideaType}','{ideaMajorSector}','{ideaMinorSector}','{ideaRegion}','{ideaCurrency}',{ideaRiskRating},{DateOnly.FromDateTime(creationDate)},{DateOnly.FromDateTime(expiryDate)},{suggestedPrice},{0},{Convert.ToInt32(creatorID)},'{colour}','{ideaSummary}','{ideaContent}')";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }
    }


}
