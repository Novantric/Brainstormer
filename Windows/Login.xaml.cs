using System;
using System.Collections.Generic;
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
using System.Xml.Linq;
using static Brainstormer.Databases.DBBackend.Operations;

namespace Brainstormer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DevMenuOpen(object sender, RoutedEventArgs e)
        {
            DevOptions showDevOptions = new DevOptions();
            showDevOptions.ShowDialog();
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really wanna exit?",
                    "Exit?", //Window Title
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string userType = UserTypeBox.Text;
            string username = UsernameBox.Text;
            string password = PasswordBox.Text;

            //MessageBox.Show(String.Format("Type: {0}\nusername: {1}\npassword: {2}", userType, username, password));
            login(userType, username, password);


        }

        private void UsernameClicked(object sender, TextChangedEventArgs e)
        {
            if (UsernameBox.Text == "Username")
            {
                UsernameBox.Clear();
            }
        }
    }
}
