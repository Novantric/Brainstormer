using Brainstormer.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Brainstormer.Windows
{
    /// <summary>
    /// Interaction logic for HomeMenu.xaml
    /// </summary>
    public partial class HomeMenu : Window
    {
        //Tracks the current page
        public static string currentPage = "Welcome";
        //Allows other classes to navigate to other pages
        public static Frame? navFrame;

        public HomeMenu()
        {
            InitializeComponent();
            //Links the public and private frames
            navFrame = PageFrame;

            //Changes the visibility of buttons depending on the account type
            switch (User.UserType)
            {
                case "Client":
                    ClientsButton.Visibility = Visibility.Collapsed;
                    CreateIdeaButton.Visibility = Visibility.Collapsed;
                    AdminOptionsButton.Visibility = Visibility.Collapsed;
                    DevOptionsButton.Visibility = Visibility.Collapsed;
                    SavedIdeasButton.Visibility = Visibility.Collapsed;
                    break;
                case "RM":
                    RelationshipManagersButton.Visibility = Visibility.Collapsed;
                    AdminOptionsButton.Visibility = Visibility.Collapsed;
                    DevOptionsButton.Visibility = Visibility.Collapsed;
                    break;
                case "Admin":
                    RelationshipManagersButton.Visibility = Visibility.Collapsed;
                    DevOptionsButton.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
            if (User.UserFirstName.Equals("Developer"))
            {
                DevOptionsButton.Visibility = Visibility.Visible;
            }
        }

        //Logic for changing the displayed page in navframe
        public static void LoadPage(string PageName)
        {
            currentPage = PageName;
            //Creates a new URI to the page and navigates to it
            navFrame.Navigate(new Uri(@"Windows\Pages\" + PageName + ".xaml", System.UriKind.RelativeOrAbsolute));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Settings");
        }

        //Shows the developer options window
        private void DevOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            DevOptions showDevOptions = new();
            showDevOptions.Show();
        }

        private void CreateIdeaButton_Click(object sender, RoutedEventArgs e)
        {
            Idea.loadedIdeaOperation = "none";
            LoadPage("CreateIdeaPage");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Search");
        }

        private void HomeMenuButton_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Home");
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            User_RM.clientScenario = "RM View";
            LoadPage("Clients");
        }

        //Logs the user out, clears local data and shows the login menu
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            //To satisfy the requirement for launch params
            new MainWindow(new string[] { "none" }).Show();
            User.Logout();
            Close();
        }

        private void RelationshipManagersButton_Click(object sender, RoutedEventArgs e)
        {
            User_RM.clientScenario = "Client View";
            LoadPage("Clients");

        }

        private void SavedIdeasButton_Click(object sender, RoutedEventArgs e)
        {
            User_RM.clientScenario = "Saved Ideas";
            LoadPage("SavedIdeas");
        }
    }
}
