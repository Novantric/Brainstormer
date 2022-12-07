using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

            NewIdeaStackPanel.Children.Clear();
            IdeaNameStackPanel.Children.Clear();
            List<Idea> ideas = IdeaOperations.preloadIdeas();
            foreach (Idea value in ideas)
            {
                generateButtons(value);
            }
        }

        private void IdeaViewButtonClick(object sender, RoutedEventArgs e)
        {
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            Debug.WriteLine("Button ID: " + buttonID);

            Uri resource = new(@"Windows\Pages\CreateIdeaPage.xaml", System.UriKind.RelativeOrAbsolute);
            HomeMenu.operation = "View";
            HomeMenu.IdeaID = buttonID.ToString();
            HomeMenu.navFrame.Navigate(resource);
            
        }
        private void IdeaEditButtonClick(object sender, RoutedEventArgs e)
        {
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            Debug.WriteLine("Button ID: " + buttonID);

            Uri resource = new(@"Windows\Pages\CreateIdeaPage.xaml", System.UriKind.RelativeOrAbsolute);
            HomeMenu.operation = "Edit";
            HomeMenu.IdeaID = buttonID.ToString();
            HomeMenu.navFrame.Navigate(resource);

        }


        private void generateButtons(Idea ideaObject)
        {
            Button buttonView = new() { Content = "View", Uid = ideaObject.IdeaID.ToString(), Background = Brushes.Black, Foreground = Brushes.White};
            buttonView.Click += IdeaViewButtonClick;
            Thickness margin = buttonView.Margin;
            margin.Left = 5;
            buttonView.Margin = margin;

            if (ideaObject.CreatorID.Equals(User.UserID))
            {
                buttonView.Width = 70;
                NewIdeaStackPanel.Children.Add(buttonView);

                Button buttonEdit = new() { Content = "Edit", Uid = ideaObject.IdeaID.ToString(), Background = Brushes.Black, Foreground = Brushes.White };
                buttonEdit.Click += IdeaEditButtonClick;
                buttonEdit.Width = 70;

                NewIdeaStackPanel.Children.Add(buttonEdit);
            }
            else
            {
                buttonView.Width = 140;
                NewIdeaStackPanel.Children.Add(buttonView);
            }



            TextBlock buttonLabel = new() { Text = ideaObject.IdeaTitle, Background = Brushes.DarkGray, Width = 140, FontSize = 12, TextWrapping = TextWrapping.Wrap};

            SolidColorBrush customColour = (SolidColorBrush)new BrushConverter().ConvertFromString(ideaObject.Colour);
            buttonLabel.Background = customColour;
            switch (ideaObject.Colour)
            {
                case "Red": case "Black": case "Blue": case "Purple": case "Green":
                    Foreground = Brushes.White;
                    break;
                default:
                    Foreground = Brushes.Black;
                    break;
            }


            Thickness margin2 = buttonLabel.Margin;
            margin2.Left = 5;
            buttonLabel.Margin = margin2;

            IdeaNameStackPanel.Children.Add(buttonLabel);
        }
    }
}
