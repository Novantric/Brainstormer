using Brainstormer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using static Brainstormer.Databases.DBBackend.Connection;

namespace Brainstormer.Databases.DBBackend
{
    //Handles the manipulation and loading of ideas
    internal class IdeaOperations
    {
        //loads ideas into the public list above when called.
        public static List<Idea> PreloadIdeas()
        {
            //Stores all the ideas that are in the database.
            List<Idea> ideaList = new();
            string QUERY = "SELECT * FROM [dbo].[Idea]";
            string TABLE = "[dbo].[Idea]";

            //Each idea is loaded into the list.
            for (int i = 0; i < GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows.Count; i++)
            {
                ideaList.Add(new Idea(GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["Id"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["Title"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["AssetType"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["MajorSector"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["MinorSector"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["Reigion"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["Currency"].ToString(), Convert.ToInt32(GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["RiskRating"]), DateTime.Parse(GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["CreationDate"].ToString()), DateTime.Parse(GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["ExpiryDate"].ToString()), Convert.ToDecimal(GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["SuggestedPrice"].ToString()), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["Views"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["UserID"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["Colour"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["Summary"].ToString(), GetInstanceOfDBConnection().GetDataSet(QUERY, TABLE).Rows[i]["Content"].ToString()));
            }

            return ideaList;
        }

        //Creates an idea from the information passed in, and adds it to the database.
        public static void CreateIdea(string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, decimal ideaRiskRating, DateTime creationDate, DateTime expiryDate, decimal suggestedPrice, string creatorID, string colour, string ideaSummary, string ideaContent)
        {
            string query = $"INSERT INTO [dbo].[Idea] (Title,AssetType,MajorSector,MinorSector,Reigion,Currency,RiskRating,CreationDate,ExpiryDate,SuggestedPrice,Views,UserID,Colour,Summary,Content) VALUES ('{ideaTitle}','{ideaType}','{ideaMajorSector}','{ideaMinorSector}','{ideaRegion}','{ideaCurrency}',{ideaRiskRating},{DateOnly.FromDateTime(creationDate)},{DateOnly.FromDateTime(expiryDate)},{suggestedPrice},{0},{Convert.ToInt32(creatorID)},'{colour}','{ideaSummary}','{ideaContent}')";
            GetInstanceOfDBConnection().NonQueryOperation(query);
        }
    }
}
