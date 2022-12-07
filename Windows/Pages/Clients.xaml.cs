using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
            int row = 0, columnn = 0;
            bool isClicked = false;
            string QUERY = "";
            string TABLE = "[dbo].[User_RM]";

            InitializeComponent();
            ClientGrid.Children.Clear();
            if (User_RM.clientScenario.Equals("Client View"))
            {
                TitleLabel.Content = "View Relationship Managers";
                //Request feature in future?
                TipLabel.Content = "Click to view!";

                QUERY = "SELECT RMID FROM [dbo].[User_RM] WHERE ClientID = " + User.UserID;
                DataTable rmInfo = (Connection.getInstanceOfDBConnection().getDataSet(QUERY, TABLE)).Tables[0];
                string QUERYUSER = "SELECT * FROM [dbo].[User]";

                for (int i = 0; i < rmInfo.Rows.Count; i++)
                {
                    if (rmInfo.Rows[i]["RMID"].ToString().Equals(User.UserID))
                    {
                        string RMID = rmInfo.Rows[i]["RMID"].ToString();
                        //CONTINUE HERE LATER
                    }


                }


            }
            else if (User_RM.clientScenario.Equals("RM View"))
            {
                List<Client> clients = AccountOperations.getClients();
                foreach (Client value in clients)
                {

                    generateButtons(value, columnn, row, isClicked);
                    columnn++;

                    if (columnn >= 5)
                    {
                        columnn = 0;
                        row++;
                    }
                    if (row >= 5)
                    {
                        increaseRows();
                    }
                }

            }




        }

        private void generateButtons(Client client, int column, int row, bool isClicked)
        {
            // button.Click += addToUserRM;

            Button button = new Button() { Uid = client.UserID, Background = Brushes.Black, Foreground = Brushes.White };
            button.Content = client.UserEmail + "\n" + client.UserFirstName + " " + client.UserLastName;

            if (isClicked)
            {
                button.Foreground = Brushes.Red;
            }

            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
            button.Click += ClientViewButtonClick;

            Thickness margin = button.Margin;
            margin.Left = 5;
            button.Margin = margin;

            ClientGrid.Children.Add(button);
        }

        private void ClientViewButtonClick(object sender, RoutedEventArgs e)
        {
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            Debug.WriteLine(buttonID);
            //Open user info
            //check if wanted to be saved
            //save to db
        }

        private void increaseRows()
        {
            RowDefinition row = new();
            row.Height = new GridLength(80, GridUnitType.Pixel);
            ClientGrid.RowDefinitions.Add(row);
        }
    }
}
