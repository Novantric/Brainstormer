using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System.Windows;
using static Brainstormer.Databases.DBBackend.Checks;
using static Brainstormer.Classes.User;
using System;
using System.Diagnostics;

namespace Brainstormer.Windows
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        protected string scenario;
        public CreateAccount(string inScenario)
        {
            InitializeComponent();


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
                    PasswordBox.Text = EncryptDecrypt.Decrypt(UserPassword);
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

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            string email = UsernameBox.Text;
            string password = PasswordBox.Text;
            string passwordConfirm = PasswordConfirmBox.Text;
            string accountType = AccountTypeBox.Text;
            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;
            string phoneNum = MobNumBox.Text;

            if (password == passwordConfirm)
            {
                if (checkIsBlank(email) == false && checkIsBlank(password) == false)
                {
                    if (checkHasSpace(email) == false && checkHasSpace(password) == false && checkHasSpace(firstName) == false && checkHasSpace(lastName) == false && checkHasSpace(phoneNum) == false)
                    {
                        if (checkIsAllInt(phoneNum) == true && checkIsBlank(phoneNum) == false)
                        {
                            Debug.WriteLine("Success + Phone Number!");
                            switch (scenario)
                            {
                                case "Create":
                                    AccountOperations.CreateAccount(accountType, firstName, lastName, email, EncryptDecrypt.Encrypt(password), phoneNum);
                                    break;
                                case "Edit":
                                    User.UpdateData(Convert.ToInt32(UserID), accountType, firstName, lastName, email, EncryptDecrypt.Encrypt(password), phoneNum);
                                    break;
                                default:
                                    break;
                            }
                            Close();

                        }
                        else if (checkIsBlank(phoneNum) == true)
                        {
                            Debug.WriteLine("Success!");
                            switch (scenario)
                            {
                                case "Create":
                                    AccountOperations.CreateAccount(accountType, firstName, lastName, email, EncryptDecrypt.Encrypt(password), "none");
                                    break;
                                case "Edit":
                                    User.UpdateData(Convert.ToInt32(UserID), accountType, firstName, lastName, email, EncryptDecrypt.Encrypt(password), "none");
                                    break;
                                default:
                                    break;
                            }
                            Close();

                        }
                        else if (checkIsAllInt(phoneNum) == false)
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

        private void FirstNameBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
