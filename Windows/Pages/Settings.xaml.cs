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
        public Settings()
        {
            UserSettings.loadPreferences();

            InitializeComponent();
            RefreshData();
            UserIDLabel.Content = "User ID: " + User.UserID;
        }

        private void EditAccountButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount createAccount = new("Edit");
            createAccount.ShowDialog();
            UserNameLabel.Content = "Email: " + User.UserEmail;
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                UserSettings.DeletePreferences();
                User.DeleteAccount(Convert.ToInt32(User.UserID));

            }
        }


        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RiskLabel.Content = (RiskRatingSliderSettings.Value / 2) + "/5";
        }

        private void RefreshData()
        {
            RegionBox.Text = UserSettings.PrefferedRegion;
            CurrencyBox.Text = UserSettings.PrefferedCurrency;
            MajorBox.Text = UserSettings.PrefferedMajorSector;
            MiniorBox.Text = UserSettings.PrefferedMinorSector;
            TypeBox.Text = UserSettings.PrefferedProductType;
            switch (UserSettings.PrefferedRiskRating)
            {
                case "none":
                    RiskRatingSliderSettings.Value = 0;
                    break;
                default:
                    RiskRatingSliderSettings.Value = Convert.ToDouble(UserSettings.PrefferedRiskRating) * 2;
                    break;
            }
            UserNameLabel.Content = "Email: " + User.UserEmail;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Save button");
            double risk = RiskRatingSliderSettings.Value / 2;
            UserSettings.savePreferences(RegionBox.Text, CurrencyBox.Text, MajorBox.Text, MiniorBox.Text, TypeBox.Text, risk);
            RefreshData();
        }
    }
}
