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

            switch (HomeMenu.scenario)
            {
                case "none":
                    Debug.WriteLine("is null");
                    break;
                default:
                    string QUERY = "SELECT * FROM[dbo].[Idea] WHERE Id = '" + HomeMenu.scenario + "'";
                    string TABLE = "[dbo].[Idea]";

                    DataTable ideaInfo = (getInstanceOfDBConnection().getDataSet(QUERY, TABLE)).Tables[0];

                    TitleBox.Text = ideaInfo.Rows[0]["Title"].ToString();
                    break;
            }
        }

        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RiskLabel.Content = (RiskRatingSlider.Value / 2) + "/5";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
