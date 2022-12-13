using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System.Diagnostics;
using System.Windows;
using static Brainstormer.Classes.User;
using static Brainstormer.Databases.DBBackend.Checks;

namespace Brainstormer.Windows
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        //Tracks what the window should do, be that create or edit an account.
        protected string scenario;
        //Parses all the data from UI elements, checks them then creates the new account.
        public CreateAccount(string inScenario)
        {
            InitializeComponent();

            //Changes UI elements depending on the sscenario
            switch (inScenario)
            {
                case "Create":
                    this.Title = "Create Account";

                    titleText.Content = "Create Account";
                    scenario = "Create";
                    break;
                case "Edit":
                    this.Title = "Edit Account";

                    titleText.Content = "Edit Account";
                    UsernameBox.Text = UserEmail;
                    PasswordBox.Password = UserPassword;
                    AccountTypeBox.Text = UserType;
                    FirstNameBox.Text = UserFirstName;
                    LastNameBox.Text = UserLastName;
                    MobNumBox.Text = UserPhoneNum;

                    scenario = "Edit";
                    break;
                default:
                    break;
            }
            scenario = inScenario;
        }

        //Loads and validates data from UI elements
        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            //If the password boxes match
            if (PasswordBox.Password == PasswordConfirmBox.Password)
            {
                //If the username and password fields aren't blank
                if (CheckIsBlank(UsernameBox.Text) == false && CheckIsBlank(PasswordBox.Password) == false)
                {
                    //If the boxes don't have spaces
                    if (CheckHasSpace(UsernameBox.Text) == false && CheckHasSpace(PasswordBox.Password) == false && CheckHasSpace(FirstNameBox.Text) == false && CheckHasSpace(LastNameBox.Text) == false && CheckHasSpace(MobNumBox.Text) == false)
                    {
                        //If the phone number field is all numbers and not blank
                        if (CheckIsAllInt(MobNumBox.Text) == true && CheckIsBlank(MobNumBox.Text) == false)
                        {
                            Debug.WriteLine("Success + Phone Number!");
                            //Eiether adds the information or edits the matching database entry
                            switch (scenario)
                            {
                                case "Create":
                                    AccountOperations.CreateAccount(AccountTypeBox.Text, FirstNameBox.Text, LastNameBox.Text, UsernameBox.Text, EncryptDecrypt.Encrypt(PasswordBox.Password), MobNumBox.Text);
                                    break;
                                case "Edit":
                                    User.UpdateData(UserID, AccountTypeBox.Text, FirstNameBox.Text, LastNameBox.Text, UsernameBox.Text, EncryptDecrypt.Encrypt(PasswordBox.Password), MobNumBox.Text);
                                    break;
                                default:
                                    break;
                            }
                            //Exists the window
                            Close();
                        }
                        //If the phone number box is blank
                        else if (CheckIsBlank(MobNumBox.Text) == true)
                        {
                            Debug.WriteLine("Success!");
                            //Eiether adds the information or edits the matching database entry
                            switch (scenario)
                            {
                                case "Create":
                                    AccountOperations.CreateAccount(AccountTypeBox.Text, FirstNameBox.Text, LastNameBox.Text, UsernameBox.Text, EncryptDecrypt.Encrypt(PasswordBox.Password), "none");
                                    break;
                                case "Edit":
                                    User.UpdateData(UserID, AccountTypeBox.Text, FirstNameBox.Text, LastNameBox.Text, UsernameBox.Text, EncryptDecrypt.Encrypt(PasswordBox.Password), "none");
                                    break;
                                default:
                                    break;
                            }
                            //Exists the window
                            Close();
                        }
                        else if (CheckIsAllInt(MobNumBox.Text) == false)
                        {
                            MessageBox.Show("The phone number you entered contains non-number elemends. Get rid of them!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("One or more fields have spaces in them. Get rid of em!");
                    }
                }
                else
                {
                    MessageBox.Show("You left some entries blank!");
                }
            }
            else
            {
                MessageBox.Show("Please enter the correct password!");
            }
        }
    }
}
