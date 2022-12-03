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
            RefreshData();
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
            if (RegionBox.Text.Equals("none") || CurrencyBox.Text.Equals("none") || MajorBox.Text.Equals("none") || MiniorBox.Text.Equals("none") || TypeBox.Text.Equals("None"))
            {
                SaveButton.IsEnabled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            UserSettings.savePreferences(RegionBox.Text, CurrencyBox.Text, MajorBox.Text, MiniorBox.Text, TypeBox.Text, RiskRatingSliderSettings.Value);
            RefreshData();
        }

        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RiskLabel.Content = (RiskRatingSliderSettings.Value / 2) + "/5";
            SaveButton.IsEnabled = true;
        }

        private void RefreshData()
        {
            RegionBox.Text = UserSettings.PrefferedRegion;
            CurrencyBox.Text = UserSettings.PrefferedCurrency;
            MajorBox.Text = UserSettings.PrefferedMajorSector;
            MiniorBox.Text = UserSettings.PrefferedMinorSector;
            TypeBox.Text = UserSettings.PrefferedProductType;
            switch (UserSettings.PrefferedRiskRating.ToString())
            {
                case "none":
                    RiskRatingSliderSettings.Value = 0;
                    break;
                default:
                    RiskRatingSliderSettings.Value = Convert.ToDouble(UserSettings.PrefferedRiskRating.ToString()) * 2;
                    break;
            }
            UserIDLabel.Content = "User ID: " + User.UserID;
            UserNameLabel.Content = "Email: " + User.UserEmail;
        }
    }
}
