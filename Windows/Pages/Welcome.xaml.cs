using System;
using System.Windows.Controls;

namespace Brainstormer.Windows.Pages
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
        }

        //Allows the user to immediately navigate to the home page
        private void HomeButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            HomeMenu.navFrame.Navigate(new Uri(@"Windows\Pages\Home.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
