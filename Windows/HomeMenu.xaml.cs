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
        public static string currentPage = "Welcome";
        public static Frame? navFrame;
        public HomeMenu()
        {
            InitializeComponent();
            navFrame = PageFrame;

            switch (User.UserType)
            {
                case "Client":
                    ClientsButton.Visibility = Visibility.Collapsed;
                    CreateIdeaButton.Visibility = Visibility.Collapsed;
                    AdminOptionsButton.Visibility = Visibility.Collapsed;
                    DevOptionsButton.Visibility = Visibility.Collapsed;                    
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

        private void loadPage(string PageName)
        {
            Uri resource = new(@"Windows\Pages\" + PageName + ".xaml", System.UriKind.RelativeOrAbsolute);
            currentPage = PageName;
            navFrame.Navigate(resource);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            loadPage("Settings");

        }

        private void DevOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            DevOptions showDevOptions = new();
            showDevOptions.Show();
        }

        private void CreateIdeaButton_Click(object sender, RoutedEventArgs e)
        {
            Idea.loadedIdeaOperation = "none";
            loadPage("CreateIdeaPage");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            loadPage("Search");
        }

        private void HomeMenuButton_Click(object sender, RoutedEventArgs e)
        {
            loadPage("Home");
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            User_RM.clientScenario = "RM View";
            loadPage("Clients");
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            //To satisfy the need for launch params
            string[] temp = { "none" };
            MainWindow showLogin = new(temp);
            showLogin.Show();
            UserSettings.ClearPreferences();
            User.Logout();
            Close();
        }

        private void RelationshipManagersButton_Click(object sender, RoutedEventArgs e)
        {
            User_RM.clientScenario = "Client View";
            loadPage("Clients");

        }
    }
}
