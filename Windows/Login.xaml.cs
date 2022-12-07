using Brainstormer.Databases.DBBackend;
using Brainstormer.Windows;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using static Brainstormer.Databases.DBBackend.AccountOperations;

namespace Brainstormer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string[] launchParams)
        {
            InitializeComponent();
            if (launchParams.Length > 0 && launchParams.Length < 3)
            {
                Debug.WriteLine("Login Parameters Detected!");
                if (launchParams[0] != "none")
                {
                    UsernameBox.Text = launchParams[0];
                    PasswordBox.Password = launchParams[1];

                    ButtonAutomationPeer peer = new ButtonAutomationPeer(LoginButton);
                    IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    invokeProv.Invoke();
                }                
            }
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
            if (Login(email, password))
            {
                Debug.WriteLine("Logged in!");
                string type = "", firstName = "", lastName = "", phoneNum = "";
                AccountOperations.GetUserData(type, firstName, lastName, email, password, phoneNum);

                HomeMenu showHomeMenu = new HomeMenu();
                showHomeMenu.Show();
                Close();
            }
        }

        private void CreateAccountClicked(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new CreateAccount("Create");
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
