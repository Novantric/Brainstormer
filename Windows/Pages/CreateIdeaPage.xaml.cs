using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static Brainstormer.Databases.DBBackend.Connection;


namespace Brainstormer.Windows.Pages
{
    /// <summary>
    /// Interaction logic for CreateIdea.xaml
    /// </summary>
    public partial class CreateIdea : Page
    {
        //Loads the current scenario and changes UI elements accordingly
        public CreateIdea()
        {
            InitializeComponent();

            SubmitButton.Visibility = Visibility.Visible;
            //Do different actions depending on if creating, viewing or editing
            switch (Idea.loadedIdeaOperation)
            {
                case "none": default:
                    Debug.WriteLine("is null");
                    Author.Content = User.UserEmail;
                    LoadTypes();
                    break;
                case "View":
                    Debug.WriteLine("Viewing!");
                    titleText.Content = "Viewing an idea";
                    DisableBoxes();
                    LoadData();
                    SubmitButton.Visibility = Visibility.Collapsed;
                    break;
                case "Edit":
                    Debug.WriteLine("Editing!");
                    SubmitButton.Content = "Save";
                    titleText.Content = "Editing an idea";
                    LoadData();
                    LoadTypes();
                    break;
            }
        }

        //Loads the currently loaded idea into the UI
        private void LoadData()
        {
            //Get all the idea data from the 
            DataTable ideaInfo = (GetInstanceOfDBConnection().GetDataSet("SELECT * FROM[dbo].[Idea] WHERE Id = '" + Idea.loadedIdeaID + "'", "[dbo].[Idea]"));
            //Setting this once saves resources
            int userID = Convert.ToInt32(ideaInfo.Rows[0]["UserID"]);

            TitleBox.Text = ideaInfo.Rows[0]["Title"].ToString();
            SummaryBox.Text = ideaInfo.Rows[0]["Summary"].ToString();
            ContentBox.Text = ideaInfo.Rows[0]["Content"].ToString();
            PriceBox.Text = ideaInfo.Rows[0]["SuggestedPrice"].ToString();
            RiskRatingSlider.Value = Convert.ToDouble(ideaInfo.Rows[0]["SuggestedPrice"]);
            MinorBox.Text = ideaInfo.Rows[0]["MinorSector"].ToString();
            MajorBox.Text = ideaInfo.Rows[0]["MajorSector"].ToString();
            CurrencyBox.Text = ideaInfo.Rows[0]["Currency"].ToString();
            RegionBox.Text = ideaInfo.Rows[0]["Reigion"].ToString();
            ExpiryDatePicker.SelectedDate = DateTime.Parse(ideaInfo.Rows[0]["ExpiryDate"].ToString());
            ColourBox.Text = ideaInfo.Rows[0]["Colour"].ToString();
            TypeBox.Text= ideaInfo.Rows[0]["AssetType"].ToString();

            LoadAuthor(userID);
            LoadTags(Idea.loadedIdeaID);
        }

        //Loads the Email address of the person who created the idea
        private void LoadAuthor(int UserID)
        {
            Author.Content = GetInstanceOfDBConnection().GetDataSet("SELECT Email FROM [dbo].[User] WHERE Id = " + UserID, "[dbo].[User]").Rows[0]["Email"].ToString();
        }

        //Loads all the Tags, and adds them to the textbox
        private void LoadTags(int ideaID)
        {
            StringBuilder tagBuilder = new();

            //Get all the tags for an Idea
            DataTable tags = (GetInstanceOfDBConnection().GetDataSet("SELECT Tag FROM [dbo].[Idea_Tags] WHERE IdeaID = " + ideaID + " GROUP BY Tag", "[dbo].[Idea_Tags]"));

            //Add the tags to a stringbuilder
            for (int i = 0; i < tags.Rows.Count; i++)
            {
                tagBuilder.Append(tags.Rows[i]["Tag"].ToString().Trim());

                //Only append a comma if not the last tag
                if (i + 1 != tags.Rows.Count)
                {
                    tagBuilder.Append(',');
                }
            }

            TagsBox.Text = tagBuilder.ToString();
        }

        //Load all the product types to the box when editing or creating
        private void LoadTypes()
        {
            DataTable TypeInfo = GetInstanceOfDBConnection().GetDataSet("SELECT AssetType FROM [dbo].[Idea] GROUP BY AssetType", "[dbo].[Idea]");

            for (int i = 0; i < TypeInfo.Rows.Count; i++)
            {
                TypeBox.Items.Add(TypeInfo.Rows[0]["AssetType"].ToString());
            }
        }

        //Disables all the UI elements when viewing
        private void DisableBoxes()
        {
            TitleBox.IsEnabled = false;
            TypeBox.IsEnabled = false;
            SummaryBox.IsEnabled = false;
            ContentBox.IsEnabled = false;
            PriceBox.IsEnabled = false;
            RiskRatingSlider.IsEnabled = false;
            MinorBox.IsEnabled = false;
            MajorBox.IsEnabled = false;
            CurrencyBox.IsEnabled = false;
            RegionBox.IsEnabled = false;
            ExpiryDatePicker.IsEnabled = false;
            ColourBox.IsEnabled = false;
            TagsBox.IsEnabled = false;
            Author.Content = "";
            SubmitButton.IsEnabled = false;
        }

        //Allows the slider to match the text preview
        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Allows for displaying decimal values. The actual max slider value is 10, not 5.
            RiskLabel.Content = (RiskRatingSlider.Value / 2) + "/5";
        }

        //Saves the idea. In future, will suppoort editing.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Data validation
#pragma warning disable CS8629 // Nullable value type may be null.
            if (decimal.TryParse(PriceBox.Text, out decimal price) == false)
            {
                MessageBox.Show("Enter a number into the price!");
                return;
            }
            else if (ExpiryDatePicker.SelectedDate.Value.Date.CompareTo(DateTime.Today.Date) <= 0)
            {
                MessageBox.Show("The expiry date can't be today!");
                return;
            }
#pragma warning restore CS8629 // Nullable value type may be null.

            int newIdeaID = 0;
            //Convert the slider to the correct value
            decimal riskRating = (decimal)(RiskRatingSlider.Value / 2);
            //Create a list of the tags
            List<string> tags = new(TagsBox.Text.Split(','));
            //Set the date values
            DateOnly expiry = DateOnly.FromDateTime(ExpiryDatePicker.SelectedDate.Value.Date);
            DateOnly creation = DateOnly.FromDateTime(DateTime.Today.Date);

            if (Idea.loadedIdeaOperation.Equals("none"))
            {
                IdeaOperations.CreateIdea(TitleBox.Text, TypeBox.Text, MajorBox.Text, MinorBox.Text, RegionBox.Text, CurrencyBox.Text, riskRating, creation, expiry, price, User.UserID, ColourBox.Text, SummaryBox.Text, ContentBox.Text);

                //Load the Id of the tag that was just created
                DataTable ideaid = GetInstanceOfDBConnection().GetDataSet($"SELECT Id FROM [dbo].[Idea] WHERE Title = '{TitleBox.Text}' AND UserID = '{User.UserID}'", "[dbo].[Idea]");
                newIdeaID = (int)ideaid.Rows[0]["Id"];

            }
            else if (Idea.loadedIdeaOperation.Equals("Edit"))
            {
                IdeaOperations.UpdateIdea(TitleBox.Text, TypeBox.Text, MajorBox.Text, MinorBox.Text, RegionBox.Text, CurrencyBox.Text, riskRating, creation, expiry, price, ColourBox.Text, SummaryBox.Text, ContentBox.Text);

                //Delete the old tags
                Tags.DeleteTags(Idea.loadedIdeaID);
                newIdeaID = Idea.loadedIdeaID;
            }

            //Save the entered tags
            for (int i = 0; i < tags.Count; i++)
            {
                string tempTags = $"INSERT INTO [dbo].[Idea_Tags] (IdeaID,Tag) VALUES ({newIdeaID},'{tags[i]}')";
                GetInstanceOfDBConnection().NonQueryOperation(tempTags);
            }
            ReturnToMenu();
        }
        
        //Navigate back to the main menu
        private static void ReturnToMenu()
        {
            HomeMenu.navFrame.Navigate(new Uri(@"Windows\Pages\Home.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
