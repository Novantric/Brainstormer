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
        public static string? operation;
        public static string? IdeaID;
        public static Frame? navFrame;
        public HomeMenu()
        {
            InitializeComponent();
            navFrame = PageFrame;

        }
        private void loadPage(string PageName)
        {
            Uri resource = new(@"Windows\Pages\" + PageName + ".xaml", System.UriKind.RelativeOrAbsolute);
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
            operation = "none";
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
            loadPage("Clients");
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            string[] temp = { "none" };
            MainWindow showLogin = new(temp);
            showLogin.Show();
            Close();
            UserSettings.ClearPreferences();
            User.Logout();
        }
    }
}
