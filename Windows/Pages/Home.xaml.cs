using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System;
using System.Collections;
using System.Collections.Generic;
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

namespace Brainstormer.Windows.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {


        public Home()
        {
            UserSettings.loadPreferences();
            InitializeComponent();
            NewIdeaStackPanel.Children.Clear();
            List<Idea> ideas = IdeaOperations.preloadIdeas();
            foreach (Idea value in ideas)
            {
                generateButtons(Convert.ToInt32(value.IdeaID));
            }



        }

        private void IdeaButtonClick(object sender, RoutedEventArgs e)
        {
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            Debug.WriteLine("Button ID: " + buttonID);

            Uri resource = new(@"Windows\Pages\CreateIdeaPage.xaml", System.UriKind.RelativeOrAbsolute);
            HomeMenu.scenario = buttonID.ToString();
            HomeMenu.navFrame.Navigate(resource);
            
        }

        public void generateButtons(int ideaID)
        {
            var button = new Button() { Content = "A button!!", Uid = ideaID.ToString() };
            button.Click += IdeaButtonClick;
            NewIdeaStackPanel.Children.Add(button);
        }
    }
}
