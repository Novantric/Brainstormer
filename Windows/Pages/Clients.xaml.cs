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
        //Loads all the clients as previews when opened
        public Clients()
        {
            InitializeComponent();

            //Reset the grid
            int row = 0, columnn = 0;
            ClientGrid.Children.Clear();

            //If the page is being viewed by a client
            if (User_RM.clientScenario.Equals("Client View"))
            {
                TitleLabel.Content = "View Relationship Managers";
                TipLabel.Content = "Click to view!";

                //Get the RMs linked to the client
                string QUERY = "SELECT RMID FROM [dbo].[User_RM] WHERE ClientID = " + User.UserID + "GROUP BY ClientID";
                DataTable rmInfo = (Connection.GetInstanceOfDBConnection().GetDataSet(QUERY, "[dbo].[User_RM]"));

                //Alternative way of doing the below
                //for (int i = 0; i < rmInfo.Rows.Count; i++)
                //{
                //    string QUERYUSER = "SELECT * FROM [dbo].[User] WHERE Id = " + rmInfo.Rows[i]["RMID"];
                //    DataTable rmUserInfo = (Connection.getInstanceOfDBConnection().getDataSet(QUERYUSER, TABLE)).Tables[0];
                //    generateRMButtons((int)rmUserInfo.Rows[i]["RMID"], rmUserInfo.Rows[i]["FirstName"].ToString(), rmUserInfo.Rows[i]["LastName"].ToString(), rmUserInfo.Rows[i]["Email"].ToString(), columnn, row);
                //}

                //Generate buttons for each RM
                for (int i = 0; i < rmInfo.Rows.Count; i++)
                {
                    //But only if the RM 'owns' the client
                    if (rmInfo.Rows[i]["RMID"].ToString().Equals(User.UserID))
                    {
                        GenerateRMButtons((int)rmInfo.Rows[i]["RMID"], rmInfo.Rows[i]["FirstName"].ToString(), rmInfo.Rows[i]["LastName"].ToString(), columnn, row);

                        //Allows for correct button placement and for the grid to be expanded
                        columnn++;
                        if (columnn >= 5)
                        {
                            columnn = 0;
                            row++;
                        }
                        if (row >= 5)
                        {
                            IncreaseRows();
                        }
                    }
                }
            }
            //If the page is being viewed by a RM
            else if (User_RM.clientScenario.Equals("RM View"))
            {
                //Gets the list of clients
                List<Client> clients = AccountOperations.GetClients();


                foreach (Client value in clients)
                {
                    GenerateClientButtons(value, columnn, row);

                    //Allows for correct button placement and for the grid to be expanded
                    columnn++;
                    if (columnn >= 5)
                    {
                        columnn = 0;
                        row++;
                    }
                    if (row >= 5)
                    {
                        IncreaseRows();
                    }
                }
            }
        }

        //Generates buttons for the RMs, viewed by a client
        private void GenerateRMButtons(int RMID, string FirstName, string LastName, int columnn, int row)
        {
            //Create a button and assign it a click event
            Button button = new()
            {
                Uid = RMID.ToString(),
                Background = Brushes.Black,
                Foreground = Brushes.White,
                Content = FirstName + " " + LastName                
            };
            button.Click += RMViewButtonClick;

            //Change the properties and add the button to the grid
            Grid.SetColumn(button, columnn);
            Grid.SetRow(button, row);

            Thickness margin = button.Margin;
            margin.Left = 5;
            button.Margin = margin;

            ClientGrid.Children.Add(button);
        }

        //Generates buttons for the Clients, viewed by an RM
        private void GenerateClientButtons(Client client, int column, int row)
        {
            //Generate a button
            Button button = new()
            {
                Uid = client.UserID,
                Background = Brushes.Black,
                Foreground = Brushes.White,
                Content = client.UserFirstName + " " + client.UserLastName
            };
            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
            button.Click += ClientViewButtonClick;

            Thickness margin = button.Margin;
            margin.Left = 5;
            button.Margin = margin;

            //If the client is associated with with the user, display the text in red,
            if (User_RM.getClientIDs().Contains(Convert.ToInt32(client.UserID)))
            {
                button.Foreground = Brushes.Red;
            }

            ClientGrid.Children.Add(button);
        }

        //Called when an autogenerated button is clicked, used to open a window to show RM data
        private void RMViewButtonClick(object sender, RoutedEventArgs e)
        {
            //Get the Uid of the button, where the user ID is stored.
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            Debug.WriteLine("RM User ID: " + buttonID);
            //Show the userinfo window, passing in the uid
            new UserInfo("RM", buttonID, false).ShowDialog();
        }

        //Called when an autogenerated button is clicked, used to open a window to show Client data
        private void ClientViewButtonClick(object sender, RoutedEventArgs e)
        {
            //Gets the button/user ID and the colour of the button text
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            Brush colourBrush = ((sender as Button).Foreground);
            string colour = (colourBrush).ToString();

            Debug.WriteLine("Client User ID: " + buttonID);

            //If the button text colour is Red
            if (colour == "#FFFF0000")
            {
                //Tell UserInfo that the client is added to the RM
                new UserInfo("Client", buttonID, true).ShowDialog();
            }
            else
            {
                new UserInfo("Client", buttonID, false).ShowDialog();
            }

            //refresh page, use similar method to Connection.cs?
        }

        //Dynamically adds rows the the grid where users are displayed.
        private void IncreaseRows()
        {
            //Create a row 80 pixels tall
            RowDefinition row = new()
            {
                Height = new GridLength(80, GridUnitType.Pixel)
            };
            ClientGrid.RowDefinitions.Add(row);
        }
    }
}
