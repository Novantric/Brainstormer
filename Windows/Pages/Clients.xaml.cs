using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            string QUERY = "";
            string TABLE = "[dbo].[User_RM]";

            InitializeComponent();
            ClientGrid.Children.Clear();
            if (User_RM.clientScenario.Equals("Client View"))
            {
                TitleLabel.Content = "View Relationship Managers";
                TipLabel.Content = "Click to view!";

                //Select the RMs linked to the client
                QUERY = "SELECT RMID FROM [dbo].[User_RM] WHERE ClientID = " + User.UserID + "GROUP BY ClientID";
                DataTable rmInfo = (Connection.getInstanceOfDBConnection().getDataSet(QUERY, TABLE)).Tables[0];

                //Alternative way of doing the below
                //for (int i = 0; i < rmInfo.Rows.Count; i++)
                //{
                //    string QUERYUSER = "SELECT * FROM [dbo].[User] WHERE Id = " + rmInfo.Rows[i]["RMID"];
                //    DataTable rmUserInfo = (Connection.getInstanceOfDBConnection().getDataSet(QUERYUSER, TABLE)).Tables[0];

                //    generateRMButtons((int)rmUserInfo.Rows[i]["RMID"], rmUserInfo.Rows[i]["FirstName"].ToString(), rmUserInfo.Rows[i]["LastName"].ToString(), rmUserInfo.Rows[i]["Email"].ToString(), columnn, row);

                //}

                for (int i = 0; i < rmInfo.Rows.Count; i++)
                {
                    if (rmInfo.Rows[i]["RMID"].ToString().Equals(User.UserID))
                    {
                        generateRMButtons((int)rmInfo.Rows[i]["RMID"], rmInfo.Rows[i]["FirstName"].ToString(), rmInfo.Rows[i]["LastName"].ToString(), rmInfo.Rows[i]["Email"].ToString(), columnn, row);

                    }
                }
            }
            else if (User_RM.clientScenario.Equals("RM View"))
            {
                List<Client> clients = AccountOperations.getClients();
                foreach (Client value in clients)
                {

                    generateClientButtons(value, columnn, row);
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
        private void generateRMButtons(int RMID, string FirstName, string LastName, string Email, int columnn, int row)
        {
            Button button = new() { Uid = RMID.ToString(), Background = Brushes.Black, Foreground = Brushes.White };
            button.Content = FirstName + " " + LastName;

            Grid.SetColumn(button, columnn);
            Grid.SetRow(button, row);
            button.Click += RMViewButtonClick;

            Thickness margin = button.Margin;
            margin.Left = 5;
            button.Margin = margin;

            ClientGrid.Children.Add(button);
        }

        private void generateClientButtons(Client client, int column, int row)
        {
            Button button = new Button() { Uid = client.UserID, Background = Brushes.Black, Foreground = Brushes.White };
            button.Content = client.UserFirstName + " " + client.UserLastName;



            if (User_RM.getClientIDs().Contains(Convert.ToInt32(client.UserID)))
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



        private void RMViewButtonClick(object sender, RoutedEventArgs e)
        {
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            Debug.WriteLine(buttonID);
            UserInfo showInfo = new("RM", buttonID, false);
            showInfo.ShowDialog();
        }

        private void ClientViewButtonClick(object sender, RoutedEventArgs e)
        {
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            Brush colourBrush = ((sender as Button).Foreground);
            string colour = (colourBrush).ToString();
            Debug.WriteLine(buttonID);

            if (colour == "#FFFF0000")
            {
                UserInfo showInfo = new("Client", buttonID, true);
                showInfo.ShowDialog();
            }
            else
            {
                UserInfo showInfo = new("Client", buttonID, false);
                showInfo.ShowDialog();
            }

            //refresh page
        }

        private void increaseRows()
        {
            RowDefinition row = new();
            row.Height = new GridLength(80, GridUnitType.Pixel);
            ClientGrid.RowDefinitions.Add(row);
        }
    }
}
