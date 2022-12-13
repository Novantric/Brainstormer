using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System.Windows;
using System.Windows.Controls;

namespace Brainstormer.Windows
{
    /// <summary>
    /// Interaction logic for UserInfo.xaml
    /// </summary>
    public partial class UserInfo : Window
    {
        //Tracks which user is currently loaded
        private readonly int userID;
        //Tracks the current scenario
        private readonly string viewScenario;

        public UserInfo(string scenario, int userid, bool isAdded)
        {
            //Sets the current user
            this.userID = userid;
            this.viewScenario = scenario;
            InitializeComponent();

            //Detect the current scenario and changes the UI accordingly
            switch (scenario)
            {
                case "Client":
                    TitleField.Content = "Client Information";
                    if (isAdded)
                    {
                        SaveUserButton.Content = "Remove";
                    }
                    else
                    {
                        SaveUserButton.Content = "Add";
                    }
                    break;
                case "RM":
                    TitleField.Content = "RM Information";
                    SaveUserButton.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }

            //Gets and fills the user data from the ID, only done this once.
            string[] userData = AccountOperations.GetUserData(userid);
            EmailField.Content = userData[0];
            FirstField.Content = userData[1];
            LastField.Content = userData[2];
            PhoneField.Content = userData[3];
        }

        //Allows the 
        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            string buttonContext = ((Button)sender).Content.ToString();
            //If viewing the user as a client
            if (viewScenario == "Client")
            {
                //If the button currently says "add"
                if (buttonContext == "Add")
                {
                    User_RM.addClient(userID);
                }
                else
                {
                    User_RM.removeClient(userID);
                }
            }    
            //Shows comfirmation by closing the window, a toast popup could be used in future
            Close();
        }
    }
}
