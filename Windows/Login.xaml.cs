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
        //Automatically login if the launch parameters are a username and password
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

                    //Virtually click the login buttton
                    ((IInvokeProvider)new ButtonAutomationPeer(LoginButton).GetPattern(PatternInterface.Invoke)).Invoke();
                }
            }
        }

        //Open the developer menu, if the button is visible.
        private void DevMenuOpen(object sender, RoutedEventArgs e)
        {
            new DevOptions().Show();
        }

        //Ask the user if they wanna exit the program
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

        //Validate the input fields then login
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            //Prevent the value being changed while the validation is happening
            UsernameBox.IsEnabled = false;
            PasswordBox.IsEnabled = false;

            //MessageBox.Show(String.Format("Type: {0}\nusername: {1}\npassword: {2}", userType, username, password));
            if (Login(UsernameBox.Text, PasswordBox.Password))
            {
                Debug.WriteLine("Logged in!");
                AccountOperations.CheckUserData(UsernameBox.Text, PasswordBox.Password);

                new HomeMenu().Show();
                Close();
            }
            UsernameBox.IsEnabled = true;
            PasswordBox.IsEnabled = true;
        }

        //Show the popup window to create a new account
        private void CreateAccountClicked(object sender, RoutedEventArgs e)
        {
            new CreateAccount("Create").ShowDialog();
        }

        //Allows for the password field to be shown as pure text.
        //Could be improved with textChanged events to mirror every update.
        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowPasswordButton.Content.ToString().Equals("Hide"))
            {
                ShowPasswordButton.Content = "Show";
                PasswordUnmask.Visibility = Visibility.Hidden;
                PasswordBox.Visibility = Visibility.Visible;

                //Sets the password box as the plaintext
                PasswordBox.Password = PasswordUnmask.Text;

            }
            else if (ShowPasswordButton.Content.ToString().Equals("Show"))
            {
                ShowPasswordButton.Content = "Hide";
                PasswordUnmask.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Hidden;

                //Sets the plaintext box as the password text
                PasswordUnmask.Text = PasswordBox.Password;
            }

        }
    }
}
