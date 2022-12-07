using Brainstormer.Databases.DBBackend;
using System;
using System.Text;

namespace Brainstormer.Classes
{
    internal class Idea
    {
        public string IdeaID, IdeaTitle, IdeaType, IdeaSummary, IdeaContent, IdeaMajorSector, IdeaMinorSector, IdeaRegion, IdeaCurrency, CreationDate, ExpiryDate, Views, CreatorID, Colour;
        public decimal IdeaRiskRating, SuggestedPrice;

        //Used to track what ideas are to be loaded
        public static int loadedIdeaID;
        public static string? loadedIdeaOperation;

        public Idea(string ideaID, string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, decimal ideaRiskRating, string creationDate, string expiryDate, decimal suggestedPrice, string views, string creatorID, string colour, string ideaSummary, string ideaContent)
        {
            IdeaID = ideaID;
            IdeaTitle = ideaTitle;
            IdeaType = ideaType;
            IdeaMajorSector = ideaMajorSector;
            IdeaMinorSector = ideaMinorSector;
            IdeaRegion = ideaRegion;
            IdeaCurrency = ideaCurrency;
            IdeaRiskRating = ideaRiskRating;
            CreationDate = creationDate;
            ExpiryDate = expiryDate;
            SuggestedPrice = suggestedPrice;
            Views = views;
            CreatorID = creatorID;
            Colour = colour;
            IdeaSummary = ideaSummary;
            IdeaContent = ideaContent;
        }

        public static void CreateIdea(string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, decimal ideaRiskRating, string creationDate, string expiryDate, decimal suggestedPrice, string creatorID, string colour, string ideaSummary, string ideaContent)
        {
            string query = $"INSERT INTO [dbo].[Idea] (Title,AssetType,MajorSector,MinorSector,Reigion,Currency,RiskRating,CreationDate,ExpiryDate,SuggestedPrice,Views,UserID,Colour,Summary,Content) VALUES ('{ideaTitle}','{ideaType}','{ideaMajorSector}','{ideaMinorSector}','{ideaRegion}','{ideaCurrency}',{ideaRiskRating},{DateTime.Parse(creationDate):d},{DateTime.Parse(expiryDate):d},'{suggestedPrice}',{0},{Convert.ToInt32(creatorID)},'{colour}','{ideaSummary}','{ideaContent}')";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }
    }
}
