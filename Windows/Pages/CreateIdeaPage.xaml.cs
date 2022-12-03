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

            if (HomeMenu.operation == "none")
            {
                Debug.WriteLine("is null");
            }
            else if (HomeMenu.operation == "Edit" || HomeMenu.operation == "View")
            {
                string QUERY = "SELECT * FROM[dbo].[Idea] WHERE Id = '" + HomeMenu.IdeaID + "'";
                string TABLE = "[dbo].[Idea]";

                DataTable ideaInfo = (getInstanceOfDBConnection().getDataSet(QUERY, TABLE)).Tables[0];

                TitleBox.Text = ideaInfo.Rows[0]["Title"].ToString();

                if (HomeMenu.operation == "View")
                {
                    TitleBox.IsEnabled = false;
                }
            }
        }

        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RiskLabel.Content = (RiskRatingSlider.Value / 2) + "/5";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string type = TypeBox.Text;
            string summary = SummaryBox.Text;
            string content = ContentBox.Text;
            List<string> tags = new(TagsBox.Text.Split(','));
            string riskRating = (RiskRatingSlider.Value / 2).ToString();
            string minor = MinorBox.Text;
            string major = MajorBox.Text;
            string region = RegionBox.Text;
            string price = PriceBox.Text;
            string expiry = ExpiryDatePicker.Text;
            string creation = DateTime.Today.ToString("d");
            string currency = CurrencyBox.Text;
            string colour = ColourBox.Text;

            Idea.CreateIdea(title, type, major, minor, region, currency, riskRating, creation, expiry, price, User.UserID, colour, summary, content);

            DataSet ideaid = getInstanceOfDBConnection().getDataSet($"SELECT Id FROM [dbo].[Idea] WHERE Title = '{title}' AND UserID = '{User.UserID}'", "[dbo].[Idea]");
            int tempID = (int)ideaid.Tables["[dbo].[Idea]"].Rows[0]["Id"];

            for (int i = 0; i < tags.Count; i++)
            {
                string tempTags = $"INSERT INTO [dbo].[Idea_Tags] (IdeaID,Tag) VALUES ({tempID},{tags[i]})";
                getInstanceOfDBConnection().nonQueryOperation(tempTags);
            }


            Uri resource = new(@"Windows\Pages\Home.xaml", System.UriKind.RelativeOrAbsolute);
            HomeMenu.navFrame.Navigate(resource);
        }
    }
}
