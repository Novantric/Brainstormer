using Brainstormer.Classes;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            UserSettings.loadPreferences();
            refreshData();
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
                User.DeleteAccount(Convert.ToInt32(User.UserID));
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveButton.IsEnabled = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            UserSettings.savePreferences(RegionBox.Text, CurrencyBox.Text, MajorBox.Text, MiniorBox.Text, TypeBox.Text, RiskRatingSliderSettings.Value);
            refreshData();
        }

        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RiskLabel.Content = (RiskRatingSliderSettings.Value / 2) + "/5";
            SaveButton.IsEnabled = true;
        }

        private void refreshData()
        {
            RegionBox.Text = UserSettings.PrefferedRegion;
            CurrencyBox.Text = UserSettings.PrefferedCurrency;
            MajorBox.Text = UserSettings.PrefferedMajorSector;
            MiniorBox.Text = UserSettings.PrefferedMinorSector;
            TypeBox.Text = UserSettings.PrefferedProductType;
            if (UserSettings.PrefferedRiskRating.ToString() == "none")
            {
                RiskRatingSliderSettings.Value = 0;
            }
            else
            {
                RiskRatingSliderSettings.Value = Convert.ToDouble(UserSettings.PrefferedRiskRating.ToString()) * 2;

            }
            UserIDLabel.Content = "User ID: " + User.UserID;
            UserNameLabel.Content = "Email: " + User.UserEmail;
        }
    }
}
