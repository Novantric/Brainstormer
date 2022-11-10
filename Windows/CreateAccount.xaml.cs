using Brainstormer.Databases.DBBackend;
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
using System.Windows.Shapes;
using static Brainstormer.Databases.DBBackend.Checks;

namespace Brainstormer.Windows
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
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
                            MessageBox.Show("Success + Phone Number!");
                            AccountOperations.createAccount(accountType, firstName, lastName, email, password, phoneNum);

                        }
                        else if (checkIsBlank(phoneNum) == true)
                        {
                            MessageBox.Show("Success!");
                            AccountOperations.createAccount(accountType, firstName, lastName, email, password, "none");

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
    }
}
