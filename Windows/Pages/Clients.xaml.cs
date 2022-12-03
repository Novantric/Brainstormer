using Brainstormer.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Brainstormer.Windows.Pages
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
        public Clients()
        {
            InitializeComponent();

            List<Client> clients = AccountOperations.getClients();
            foreach (Client value in clients)
            {
                generateButtons(value.UserID, value.UserFirstName, value.UserLastName, value.UserEmail);
            }
        }

        private void generateButtons(string ID, string FirstName, string LastName, string Email)
        {


            Button button = new Button() { Content = "Add", Uid = ID, Background = Brushes.Black, Foreground = Brushes.White };
            button.Click += addToUserRM;

        }
    }
}
