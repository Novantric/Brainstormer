using Brainstormer.Classes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Brainstormer.Windows.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        //load the user's preferences and ID
        public Settings()
        {
            UserSettings.loadPreferences();

            InitializeComponent();
            RefreshData();
            UserIDLabel.Content = "User ID: " + User.UserID;
        }

        //Show the popup for editing the user's account information
        private void EditAccountButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateAccount("Edit").ShowDialog();
            //Refresh the email field as it could've changed
            UserNameLabel.Content = "Email: " + User.UserEmail;
        }

        //Allows the user to delete their preferences and account
        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                UserSettings.DeletePreferences();
                User.DeleteAccount(Convert.ToInt32(User.UserID));
            }
        }

        //Updates the label to show the value of the user's preffered risk rating
        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RiskLabel.Content = (RiskRatingSliderSettings.Value / 2) + "/5";
        }

        //Reloads the user's preferences from memory
        private void RefreshData()
        {
            RegionBox.Text = UserSettings.PrefferedRegion;
            CurrencyBox.Text = UserSettings.PrefferedCurrency;
            MajorBox.Text = UserSettings.PrefferedMajorSector;
            MiniorBox.Text = UserSettings.PrefferedMinorSector;
            TypeBox.Text = UserSettings.PrefferedProductType;
            RiskRatingSliderSettings.Value = UserSettings.PrefferedRiskRating switch
            {
                "none" => 0,
                _ => Convert.ToDouble(UserSettings.PrefferedRiskRating) * 2,
            };
            UserNameLabel.Content = "Email: " + User.UserEmail;
        }

        //Saves the user's preferences to memory, as well as the database
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Save button");
            double risk = RiskRatingSliderSettings.Value / 2;
            UserSettings.savePreferences(RegionBox.Text, CurrencyBox.Text, MajorBox.Text, MiniorBox.Text, TypeBox.Text, risk);
            RefreshData();
        }
    }
}
