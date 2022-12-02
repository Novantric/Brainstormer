namespace Brainstormer.Classes
{
    internal class Idea
    {
        public string IdeaID, IdeaTitle, IdeaType, IdeaMajorSector, IdeaMinorSector, IdeaRegion, IdeaCurrency, IdeaRiskRating, CreationDate, ExpiryDate, SuggestedPrice, Views;

        public Idea(string ideaID, string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, string ideaRiskRating, string creationDate, string expiryDate, string suggestedPrice, string views)
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
        }
    }
}
