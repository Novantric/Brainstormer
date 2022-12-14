using Brainstormer.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Documents;
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
            DataTable table = GetInstanceOfDBConnection().GetDataSet("SELECT * FROM [dbo].[Idea]", "[dbo].[Idea]");

            //Each idea is loaded into the list.
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ideaList.Add(new Idea(
                    ideaID: table.Rows[i]["Id"].ToString(),
                    ideaTitle: table.Rows[i]["Title"].ToString(),
                    ideaType: table.Rows[i]["AssetType"].ToString(),
                    ideaMajorSector: table.Rows[i]["MajorSector"].ToString(),
                    ideaMinorSector: table.Rows[i]["MinorSector"].ToString(),
                    ideaRegion: table.Rows[i]["Reigion"].ToString(),
                    ideaCurrency: table.Rows[i]["Currency"].ToString(),
                    ideaRiskRating: Convert.ToInt32(table.Rows[i]["RiskRating"]),
                    creationDate: DateOnly.FromDateTime(DateTime.Parse(table.Rows[i]["CreationDate"].ToString())),
                    expiryDate: DateOnly.FromDateTime(DateTime.Parse(table.Rows[i]["ExpiryDate"].ToString())),
                    suggestedPrice: Convert.ToDecimal(table.Rows[i]["SuggestedPrice"]),
                    views: table.Rows[i]["Views"].ToString(),
                    creatorID: table.Rows[i]["UserID"].ToString(),
                    colour: table.Rows[i]["Colour"].ToString(),
                    ideaSummary: table.Rows[i]["Summary"].ToString(),
                    ideaContent: table.Rows[i]["Content"].ToString()));
            }
            return ideaList;
        }

        //Creates an idea from the information passed in, and adds it to the database.
        public static void CreateIdea(string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, decimal ideaRiskRating, DateOnly creationDate, DateOnly expiryDate, decimal suggestedPrice, string creatorID, string colour, string ideaSummary, string ideaContent)
        {
            string query = $"INSERT INTO [dbo].[Idea] (Title,AssetType,MajorSector,MinorSector,Reigion,Currency,RiskRating,CreationDate,ExpiryDate,SuggestedPrice,Views,UserID,Colour,Summary,Content) VALUES ('{ideaTitle}','{ideaType}','{ideaMajorSector}','{ideaMinorSector}','{ideaRegion}','{ideaCurrency}',{ideaRiskRating}, CONVERT(DATE, '{creationDate.ToString().Replace("/", "-")}', 3), CONVERT(DATE, '{expiryDate.ToString().Replace("/", "-")}', 3),{suggestedPrice},{0},{Convert.ToInt32(creatorID)},'{colour}','{ideaSummary}','{ideaContent}')";
            GetInstanceOfDBConnection().NonQueryOperation(query);
        }

        //Updates the currently loaded idea
        public static void UpdateIdea(string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, decimal ideaRiskRating, DateOnly creationDate, DateOnly expiryDate, decimal suggestedPrice, string colour, string ideaSummary, string ideaContent)
        {
            string query = $"UPDATE [dbo].[Idea] SET Title = '{ideaTitle}', MajorSector = '{ideaMajorSector}', AssetType = '{ideaType}', Currency = '{ideaCurrency}' , MinorSector = '{ideaMinorSector}', Reigion = '{ideaRegion}', RiskRating = {ideaRiskRating}, CreationDate = CONVERT(DATE, '{creationDate.ToString().Replace("/", "-")}', 3), ExpiryDate = CONVERT(DATE, '{expiryDate.ToString().Replace("/", "-")}', 3), SuggestedPrice = {suggestedPrice}, Colour = '{colour}', Summary = '{ideaSummary}', Content = '{ideaContent}' WHERE Id = {Idea.loadedIdeaID}";
            GetInstanceOfDBConnection().NonQueryOperation(query);
        }

        public static void AddView(int ideaID)
        {
            DataTable viewsTable = GetInstanceOfDBConnection().GetDataSet($"SELECT Views FROM [dbo].[Idea] WHERE Id = {ideaID}" , "[dbo].[Idea]");
            int oldView = (int)viewsTable.Rows[0]["Views"];

            string query = $"UPDATE [dbo].[Idea] SET Views = {oldView + 1} WHERE Id = {ideaID}";
            GetInstanceOfDBConnection().NonQueryOperation(query);
        }
    }
}