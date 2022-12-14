using Brainstormer.Classes;
using Brainstormer.Databases.DBBackend;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        //Load ideas
        public Home()
        {
            List<Idea> ideas = IdeaOperations.PreloadIdeas();

            InitializeComponent();
            //Removes all elements, including the template data in Home.xaml
            IdeaPanel.Children.Clear();
            IdeaPanel.UpdateLayout();
            //load all ideas to a category
            LoadNewButtons(ideas);
            //Load the ideas associated with a tag
            LoadTagsButtons();
        }

        //Called when a generated view button is clicked
        private void IdeaViewButtonClick(object sender, RoutedEventArgs e)
        {
            IdeaButtonClick("View", Convert.ToInt32((sender as Button).Uid));
        }

        //Called when a generated edit button is clicked
        private void IdeaEditButtonClick(object sender, RoutedEventArgs e)
        {
            IdeaButtonClick("Edit", Convert.ToInt32((sender as Button).Uid));
        }

        //Tells CreateIdea how to display the current idea
        private static void IdeaButtonClick(string scenario, int ID)
        {
            Debug.WriteLine("Button ID: " + ID);

            //Update the scenario and idea ID, then launch CreateIdeaPage
            Idea.loadedIdeaOperation = scenario;
            Idea.loadedIdeaID = ID;

            HomeMenu.navFrame.Navigate(new Uri(@"Windows\Pages\CreateIdeaPage.xaml", System.UriKind.RelativeOrAbsolute));
        }

        //Responsible for filling the "Whats new" section with ideas
        private void LoadNewButtons(List<Idea> ideas)
        {
            //Sorts the list so the ideas that expire the soonest are first
            ideas.Sort((x, y) => DateOnly.FromDateTime(DateTime.Today.Date).CompareTo(x.ExpiryDate));

            //Tracks if there are 0 ideas stored
            if (ideas.Count == 0)
            {
                GenerateUIElements("No Ideas! Go make one!", false);
                return;
            }

            StackPanel[] generatedPanels = GenerateUIElements("Fresh Ideas!", false);
            foreach (Idea value in ideas)
            {
                //generate buttons for the ideas
                GenerateButtons(value, generatedPanels[1], generatedPanels[0]);
            }
        }

        //Loads tag data and generates relevant idea buttons
        private void LoadTagsButtons()
        {
            //Load the tags
            Tags.LoadTags();

            //repeat for the amount of tags, limited to a maximum of 3
            for (int i = 0; i < Tags.tagslist.Count; i++)
            {
                //Get the ideas that have the current tag
                List<Idea> ideasWithTag = Tags.GetIdeasWithTag(Tags.tagslist[i]);
                //Sort by name alphabetically
                ideasWithTag = ideasWithTag.OrderBy(o => o.IdeaTitle).ToList();
                //Generate stackpanels for the ideas
                StackPanel[] generatedPanels = GenerateUIElements(Tags.tagslist[i], true);

                //Limit each category to 20 buttons
                for (int y = 0; y < ideasWithTag.Count || y == 20; y++)
                {
                    GenerateButtons(ideasWithTag[y], generatedPanels[1], generatedPanels[0]);
                }

                //Break the loop whem 3 tags have been generated
                if (i == 2)
                {
                    break;
                }
            }
        }

        //Generates UI elements in order to display idea buttons and data
        private StackPanel[] GenerateUIElements(string input, bool isTag)
        {

            //Create a horizontal scrollviewer and add it
            ScrollViewer tempSV = new() { HorizontalScrollBarVisibility = ScrollBarVisibility.Auto, VerticalScrollBarVisibility = ScrollBarVisibility.Hidden };
            IdeaPanel.Children.Add(tempSV);

            //create a stackpanel for the elements and add it
            StackPanel tempParentStackPanel = new() { };
            tempSV.Content = tempParentStackPanel;

            //create a label and add it
            Label tempTagLabel = new();
            if (isTag)
            {
                tempTagLabel = new() { Content = "Ideas with the tag '" + input.Trim() + "'", Foreground = Brushes.White, FontSize = 20 };
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

        //generates buttons and text boxes into the proviced panels
        private void GenerateButtons(Idea ideaObject, StackPanel buttonPanel, StackPanel namePanel)
        {
            //Doesn't show expired values
            if (ideaObject.ExpiryDate.CompareTo(DateOnly.FromDateTime(DateTime.Today.Date)) >= 0)
            {
                //Create the button
                Button buttonView = new() { Content = "View", Uid = ideaObject.IdeaID.ToString(), Background = Brushes.Black, Foreground = Brushes.White };
                buttonView.Click += IdeaViewButtonClick;
                Thickness margin = buttonView.Margin;
                margin.Left = 5;
                buttonView.Margin = margin;

                //If the current user created the idea, add an edit button
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

                //Add the label
                TextBlock buttonLabel = new() { Text = ideaObject.IdeaTitle, Background = Brushes.DarkGray, Width = 140, FontSize = 12, TextWrapping = TextWrapping.Wrap };

                //Set the label colours depending on the user's prefernece
                SolidColorBrush? customColour = new BrushConverter().ConvertFromString(ideaObject.Colour) as SolidColorBrush;
                buttonLabel.Background = customColour;
                Foreground = ideaObject.Colour switch
                {
                    "Red" or "Black" or "Blue" or "Purple" or "Green" => Brushes.White,
                    _ => Brushes.Black,
                };
                Thickness margin2 = buttonLabel.Margin;
                margin2.Left = 5;
                buttonLabel.Margin = margin2;

                namePanel.Children.Add(buttonLabel);
            }
        }
    }
}
