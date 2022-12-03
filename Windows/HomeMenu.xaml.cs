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
        public static string? scenario;
        public static Frame? navFrame;
        public HomeMenu()
        {
            InitializeComponent();
            navFrame = PageFrame;


        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Uri resource = new(@"Windows\Pages\Settings.xaml", System.UriKind.RelativeOrAbsolute);
            navFrame.Navigate(resource);
        }

        private void DevOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            DevOptions showDevOptions = new DevOptions();
            showDevOptions.Show();
        }

        private void CreateIdeaButton_Click(object sender, RoutedEventArgs e)
        {
            Uri resource = new(@"Windows\Pages\CreateIdeaPage.xaml", System.UriKind.RelativeOrAbsolute);
            scenario = "none";
            navFrame.Navigate(resource);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            System.Uri resource = new(@"Windows\Pages\Search.xaml", System.UriKind.RelativeOrAbsolute);
            navFrame.Navigate(resource);
        }

        private void HomeMenuButton_Click(object sender, RoutedEventArgs e)
        {
            System.Uri resource = new(@"Windows\Pages\Home.xaml", System.UriKind.RelativeOrAbsolute);
            navFrame.Navigate(resource);
            InfoBar.Text = "Home Menu";
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            System.Uri resource = new(@"Windows\Pages\Clients.xaml", System.UriKind.RelativeOrAbsolute);
            navFrame.Navigate(resource);
            InfoBar.Text = "Clients";
        }
    }
}
