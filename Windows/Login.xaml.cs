using Brainstormer.Databases.DBBackend;
using Brainstormer.Windows;
using System.Windows;
using System.Windows.Controls;
using static Brainstormer.Databases.DBBackend.AccountOperations;

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
            showDevOptions.Show();
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
            string email = UsernameBox.Text;
            string password = PasswordBox.Password;

            //MessageBox.Show(String.Format("Type: {0}\nusername: {1}\npassword: {2}", userType, username, password));
            if (login(email, password))
            {
                MessageBox.Show("Success");
                string type = "", firstName = "", lastName = "", phoneNum = "";
                AccountOperations.getUserData(type, firstName, lastName, email, password, phoneNum);
            }
        }

        private void CreateAccountClicked(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new CreateAccount();
            createAccount.ShowDialog();
        }

        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowPasswordButton.Content.ToString().Equals("Hide"))
            {
                ShowPasswordButton.Content = "Show";
                PasswordUnmask.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;

                PasswordBox.Password = PasswordUnmask.Text;

            }
            else if (ShowPasswordButton.Content.ToString().Equals("Show"))
            {
                ShowPasswordButton.Content = "Hide";
                PasswordUnmask.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Hidden;

                PasswordUnmask.Text = PasswordBox.Password;
            }



        }
    }
}
