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
        private readonly int userID;
        public UserInfo(string scenario, int userid, bool isAdded)
        {
            this.userID = userid;
            InitializeComponent();
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
            string[] userData = AccountOperations.GetUserData(userid);

            EmailField.Content = userData[0];
            FirstField.Content = userData[1];
            LastField.Content = userData[2];
            PhoneField.Content = userData[3];


        }


        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            string buttonContext = ((Button)sender).Content.ToString();
            if (buttonContext == "Add")
            {
                User_RM.addClient(userID);
            }
            else
            {
                User_RM.removeClient(userID);
            }
            Close();
        }
    }
}
