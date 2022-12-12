using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Brainstormer.Windows.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            List<Idea> ideas = IdeaOperations.preloadIdeas();

            InitializeComponent();

            //Removes all elements, including the template data in Home.xaml
            IdeaPanel.Children.Clear();

            //load all ideas to a category
            loadNewButtons(ideas);
            loadTagsButtons(ideas);

        }

        private void IdeaViewButtonClick(object sender, RoutedEventArgs e)
        {
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            ideaButtonClick("View", buttonID);
        }

        private void IdeaEditButtonClick(object sender, RoutedEventArgs e)
        {
            int buttonID = Convert.ToInt32((sender as Button).Uid);
            ideaButtonClick("Edit", buttonID);
        }

        private void ideaButtonClick(string scenario, int ID)
        {
            Debug.WriteLine("Button ID: " + ID);

            Uri resource = new(@"Windows\Pages\CreateIdeaPage.xaml", System.UriKind.RelativeOrAbsolute);
            Idea.loadedIdeaOperation = scenario;
            Idea.loadedIdeaID = ID;
            HomeMenu.navFrame.Navigate(resource);
        }


        private void loadNewButtons(List<Idea> ideas)
        {
            bool isIdeas = false;

            foreach (Idea value in ideas)
            {
                isIdeas = true;
                StackPanel[] generatedPanels = generateUIElements("Fresh Ideas!", false);
                generateButtons(value, generatedPanels[1], generatedPanels[0]);
            }

            if (isIdeas == false)
            {
                StackPanel[] generatedPanels = generateUIElements("No Ideas! Go make one!", false);
            }
        }

        //Loads data into precreated UI elements
        private void loadTagsButtons(List<Idea> ideas)
        {
            //Load the tags
            Tags.loadTags();

            //repeat for the amount of tags, limited to a maximum of 3
            for (int i = 0; i < Tags.tagslist.Count || i == 3; i++)
            {
                //Get the ideas that have the current tag
                List<Idea> ideasWithTag = Tags.getIdeasWithTag(Tags.tagslist[i]);

                StackPanel[] generatedPanels = generateUIElements(Tags.tagslist[i], true);

                //Limit each category to 20 buttons
                for (int y = 0; y < ideasWithTag.Count || y == 20; y++)
                {
                    generateButtons(ideasWithTag[y], generatedPanels[1], generatedPanels[0]);
                }

            }
        }

        //Generates UI elements in order to display idea buttons and data
        private StackPanel[] generateUIElements(string input, bool isTag)
        {

            //Create a horizontal scrollviewer and add it
            ScrollViewer tempSV = new ScrollViewer { HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, VerticalScrollBarVisibility = ScrollBarVisibility.Hidden };
            IdeaPanel.Children.Add(tempSV);

            //create a stackpanel for the elements and add it
            StackPanel tempParentStackPanel = new() { };
            tempSV.Content = tempParentStackPanel;

            //create a label and add it
            Label tempTagLabel = new();
            if (isTag)
            {
                tempTagLabel = new() { Content = "Ideas with the tag '" + input + "'", Foreground = Brushes.White, FontSize = 20 };
            }
            else
            {
                tempTagLabel = new() { Content = input, Foreground = Brushes.White, FontSize = 20 };
            }
            tempParentStackPanel.Children.Add(tempTagLabel);

            //Create the other two stackpanels
            StackPanel tempNameStackPanel = new() { Height = 60, Orientation = Orientation.Horizontal };
            StackPanel tempButtonStackPanel = new() { Height = 30, Orientation = Orientation.Horizontal };

            tempParentStackPanel.Children.Add(tempNameStackPanel);
            tempParentStackPanel.Children.Add(tempButtonStackPanel);

            StackPanel[] result = { tempNameStackPanel, tempButtonStackPanel };
            return result;
        }

        private void generateButtons(Idea ideaObject, StackPanel buttonPanel, StackPanel namePanel)
        {
            if (DateTime.Compare(ideaObject.ExpiryDate, DateTime.Parse(DateTime.Today.ToString("d"))) <= 0)
            {
                Button buttonView = new() { Content = "View", Uid = ideaObject.IdeaID.ToString(), Background = Brushes.Black, Foreground = Brushes.White };
                buttonView.Click += IdeaViewButtonClick;
                Thickness margin = buttonView.Margin;
                margin.Left = 5;
                buttonView.Margin = margin;

                if (ideaObject.CreatorID.Equals(User.UserID))
                {
                    buttonView.Width = 70;
                    buttonPanel.Children.Add(buttonView);

                    Button buttonEdit = new() { Content = "Edit", Uid = ideaObject.IdeaID.ToString(), Background = Brushes.Black, Foreground = Brushes.White };
                    buttonEdit.Click += IdeaEditButtonClick;
                    buttonEdit.Width = 70;

                    buttonPanel.Children.Add(buttonEdit);
                }
                else
                {
                    buttonView.Width = 140;
                    buttonPanel.Children.Add(buttonView);
                }

                TextBlock buttonLabel = new() { Text = ideaObject.IdeaTitle, Background = Brushes.DarkGray, Width = 140, FontSize = 12, TextWrapping = TextWrapping.Wrap };

                SolidColorBrush? customColour = new BrushConverter().ConvertFromString(ideaObject.Colour) as SolidColorBrush;
                buttonLabel.Background = customColour;
                switch (ideaObject.Colour)
                {
                    case "Red":
                    case "Black":
                    case "Blue":
                    case "Purple":
                    case "Green":
                        Foreground = Brushes.White;
                        break;
                    default:
                        Foreground = Brushes.Black;
                        break;
                }


                Thickness margin2 = buttonLabel.Margin;
                margin2.Left = 5;
                buttonLabel.Margin = margin2;

                namePanel.Children.Add(buttonLabel);
            }
        }


    }
}
