using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Brainstormer.Databases.DBBackend.Connection;


namespace Brainstormer.Windows.Pages
{
    /// <summary>
    /// Interaction logic for CreateIdea.xaml
    /// </summary>
    public partial class CreateIdea : Page
    {
        public CreateIdea()
        {
            InitializeComponent();

            if (Idea.loadedIdeaOperation == "none")
            {
                Debug.WriteLine("is null");
            }
            else if (Idea.loadedIdeaOperation == "Edit" || Idea.loadedIdeaOperation == "View")
            {
                if (Idea.loadedIdeaOperation == "View")
                {
                    DisableBoxes();
                    loadData();
                    SubmitButton.Content = "Save";
                }
                else
                {

                }
            }
        }

        private void loadData()
        {
            string QUERY = "SELECT * FROM[dbo].[Idea] WHERE Id = '" + Idea.loadedIdeaID + "'";
            string TABLE = "[dbo].[Idea]";

            DataTable ideaInfo = (getInstanceOfDBConnection().getDataSet(QUERY, TABLE)).Tables[0];

            TitleBox.Text = ideaInfo.Rows[0]["Title"].ToString();
            TypeBox.Text = ideaInfo.Rows[0]["AssetType"].ToString();
            SummaryBox.Text = ideaInfo.Rows[0]["Summary"].ToString();
            ContentBox.Text = ideaInfo.Rows[0]["Content"].ToString();
            PriceBox.Text = ideaInfo.Rows[0]["SuggestedPrice"].ToString();
            RiskRatingSlider.Value = Convert.ToDouble(ideaInfo.Rows[0]["SuggestedPrice"]);
            MinorBox.Text = ideaInfo.Rows[0]["MinorSector"].ToString();
            MajorBox.Text = ideaInfo.Rows[0]["MajorSector"].ToString();
            CurrencyBox.Text = ideaInfo.Rows[0]["Currency"].ToString();
            RegionBox.Text = ideaInfo.Rows[0]["Reigion"].ToString();
            ExpiryDatePicker.Text = ideaInfo.Rows[0]["ExpiryDate"].ToString();
            ColourBox.Text = ideaInfo.Rows[0]["Colour"].ToString();

            loadAuthor(Convert.ToInt32(ideaInfo.Rows[0]["UserID"]));
            loadTags(Convert.ToInt32(ideaInfo.Rows[0]["UserID"]));
        }

        private void loadAuthor(int UserID)
        {
            string QUERYAUTHOR = "SELECT Email FROM [dbo].[User] WHERE Id = " + UserID;
            string TABLEAUTHOR = "[dbo].[User]";
            DataTable AuthorInfo = (getInstanceOfDBConnection().getDataSet(QUERYAUTHOR, TABLEAUTHOR)).Tables[0];
            Author.Content = AuthorInfo.Rows[0]["Email"].ToString();
        }

        private void loadTags(int UserID)
        {
            string QUERYTAGS = "SELECT Tag FROM [dbo].[Idea_Tags] WHERE IdeaID = " + UserID;
            string TABLETAGS = "[dbo].[Idea_Tags]";
            DataTable tags = (getInstanceOfDBConnection().getDataSet(QUERYTAGS, TABLETAGS)).Tables[0];

            List<string> tagList = new();
            for (int i = 0; i < tags.Rows.Count; i++)
            {
                tagList.Add(tags.Rows[0]["IdeaID"].ToString());
            }

            StringBuilder tagBuilder = new();
            foreach (string item in tagList)
            {
                tagBuilder.Append(item);
                tagBuilder.Append(',');
            }
            TagsBox.Text = tagBuilder.ToString();
        }

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

        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RiskLabel.Content = (RiskRatingSlider.Value / 2) + "/5";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(PriceBox.Text, out decimal price) == false || DateTime.Compare(DateTime.Parse(ExpiryDatePicker.Text), DateTime.Today) <= 0)
            {
                MessageBox.Show("Enter a number into the price!");
                return;
            }

            string title = TitleBox.Text;
            string type = TypeBox.Text;
            string summary = SummaryBox.Text;
            string content = ContentBox.Text;
            List<string> tags = new(TagsBox.Text.Split(','));
            decimal riskRating = (decimal)(RiskRatingSlider.Value / 2);
            string minor = MinorBox.Text;
            string major = MajorBox.Text;
            string region = RegionBox.Text;

            
            DateTime expiry = DateTime.Parse(ExpiryDatePicker.Text);
            DateTime creation = DateTime.Parse(DateTime.Today.ToString("d"));
            string currency = CurrencyBox.Text;
            string colour = ColourBox.Text;

            Idea.CreateIdea(title, type, major, minor, region, currency, riskRating, creation, expiry, price, User.UserID, colour, summary, content);

            DataSet ideaid = getInstanceOfDBConnection().getDataSet($"SELECT Id FROM [dbo].[Idea] WHERE Title = '{title}' AND UserID = '{User.UserID}'", "[dbo].[Idea]");
            int tempID = (int)ideaid.Tables["[dbo].[Idea]"].Rows[0]["Id"];

            //Save the inputted tags
            for (int i = 0; i < tags.Count; i++)
            {
                string tempTags = $"INSERT INTO [dbo].[Idea_Tags] (IdeaID,Tag) VALUES ({tempID},'{tags[i]}')";
                getInstanceOfDBConnection().nonQueryOperation(tempTags);
            }


            ReturnToMenu();
        }

        private static void ReturnToMenu()
        {
            Uri resource = new(@"Windows\Pages\Home.xaml", System.UriKind.RelativeOrAbsolute);
            HomeMenu.navFrame.Navigate(resource);
        }
    }
}
