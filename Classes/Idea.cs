using Brainstormer.Databases.DBBackend;

namespace Brainstormer.Classes
{
    internal class Idea
    {
        public string IdeaID, IdeaTitle, IdeaType, IdeaSummary, IdeaContent, IdeaMajorSector, IdeaMinorSector, IdeaRegion, IdeaCurrency, IdeaRiskRating, CreationDate, ExpiryDate, SuggestedPrice, Views, CreatorID, Colour;

        public Idea(string ideaID, string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, string ideaRiskRating, string creationDate, string expiryDate, string suggestedPrice, string views, string creatorID, string colour, string ideaSummary, string ideaContent)
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

        public static void CreateIdea(string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, string ideaRiskRating, string creationDate, string expiryDate, string suggestedPrice, string creatorID, string colour, string ideaSummary, string ideaContent)
        {
            string query = $"INSERT INTO [dbo].[Idea] (Title,AssetType,MajorSector,MinorSector,Reigion,Currency,RiskRating,CreationDate,ExpiryDate,SuggestedPrice,Views,UserID,Colour,Summary,Content) VALUES ('{ideaTitle}','{ideaType}','{ideaMajorSector}','{ideaMinorSector}','{ideaRegion}','{ideaCurrency}','{ideaRiskRating}','{creationDate}','{expiryDate}','{suggestedPrice}','{0}','{creatorID},'{colour}','{ideaSummary}','{ideaContent}')";
            Connection.getInstanceOfDBConnection().nonQueryOperation(query);
        }
    }
}
