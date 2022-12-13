using Brainstormer.Databases.DBBackend;
using System;

namespace Brainstormer.Classes
{
    //Handles information regarding the currently loaded idea
    internal class Idea
    {
        public string IdeaID, IdeaTitle, IdeaType, IdeaSummary, IdeaContent, IdeaMajorSector, IdeaMinorSector, IdeaRegion, IdeaCurrency, Views, CreatorID, Colour;
        public decimal IdeaRiskRating, SuggestedPrice;
        public DateOnly CreationDate, ExpiryDate;

        //Used to track what idea is to be loaded, and what to do with that information.
        public static int loadedIdeaID;
        public static string? loadedIdeaOperation;

        public Idea(string ideaID, string ideaTitle, string ideaType, string ideaMajorSector, string ideaMinorSector, string ideaRegion, string ideaCurrency, decimal ideaRiskRating, DateOnly creationDate, DateOnly expiryDate, decimal suggestedPrice, string views, string creatorID, string colour, string ideaSummary, string ideaContent)
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
    }
}
