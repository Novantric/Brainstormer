using System.Windows;
using System.Windows.Navigation;

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
