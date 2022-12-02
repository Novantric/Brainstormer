using Brainstormer.Windows.Pages;
using System;
using System.Collections.Generic;
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

namespace Brainstormer.Windows
{
    /// <summary>
    /// Interaction logic for CreateIdea.xaml
    /// </summary>
    public partial class CreateIdea : Window
    {
        public CreateIdea()
        {
            InitializeComponent();
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {


            // do whatever with str, like assign to a view model field, etc.
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void RiskRatingChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RiskLabel.Content = RiskRatingSlider.Value / 2 + "/5"; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
