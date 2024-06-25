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

namespace ST10372065_PROG6221_PART3POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Recipe> recipes = new List<Recipe>();
        List<RecipeName> listRecipeName = new List<RecipeName>();
        List<IngredientDetails> ingredients = new List<IngredientDetails>();
        List<StepDetails> listDescription = new List<StepDetails>();


        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddSteps()
        {
            StackPanel stepPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBox textboxSteps = new TextBox(){ Width = 150, Height = 40 };
            stepPanel.Children.Add(new TextBlock(){Text = "Step: ",VerticalAlignment = VerticalAlignment.Center});
            stepPanel.Children.Add(textboxSteps);
            listDescription.Add(new StepDetails(textboxSteps));
            pnlDisplay.Children.Add(stepPanel);

        }
        public void AddIngreds()
        {
            StackPanel ingredientPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            TextBox textboxIngredName = new TextBox() { Width = 100 };
            TextBox textboxQuantity = new TextBox() { Width = 100 };
            TextBox textboxUnit = new TextBox() { Width = 100 };
            TextBox textboxCalories = new TextBox() { Width = 100 };
            ComboBox comboFoodGroup = new ComboBox() { Width = 100 };
            comboFoodGroup.Items.Add("Vegetables");
            comboFoodGroup.Items.Add("Fruits");
            comboFoodGroup.Items.Add("Grains");
            comboFoodGroup.Items.Add("Protein Foods");
            comboFoodGroup.Items.Add("Dairy");
            comboFoodGroup.Items.Add("Oils and Solid Fats");
            comboFoodGroup.Items.Add("Added Sugars");
            comboFoodGroup.Items.Add("Beverages");
            ingredientPanel.Children.Add(new TextBlock() { Text = "Ingredient Name: ", VerticalAlignment = VerticalAlignment.Center });
            ingredientPanel.Children.Add(textboxIngredName);
            ingredientPanel.Children.Add(new TextBlock() { Text = "Quantity: ", VerticalAlignment = VerticalAlignment.Center });
            ingredientPanel.Children.Add(textboxQuantity);
            ingredientPanel.Children.Add(new TextBlock() { Text = "Unit: ", VerticalAlignment = VerticalAlignment.Center });
            ingredientPanel.Children.Add(textboxUnit);
            ingredientPanel.Children.Add(new TextBlock() { Text = "Calories: ", VerticalAlignment = VerticalAlignment.Center });
            ingredientPanel.Children.Add(textboxCalories);
            ingredientPanel.Children.Add(new TextBlock() { Text = "Food Group: ", VerticalAlignment = VerticalAlignment.Center });
            ingredientPanel.Children.Add(comboFoodGroup);
            ingredients.Add(new IngredientDetails(textboxIngredName, textboxQuantity, textboxUnit, textboxCalories, comboFoodGroup));
            pnlDisplay.Children.Add(ingredientPanel);
        }
        private void btnEnterIngredsandSteps_Click(object sender, RoutedEventArgs e)
        {
            // Making the elements inside the Grid visible
            LabelIngredient.Visibility = System.Windows.Visibility.Visible;
            LabelFoodGroup.Visibility = System.Windows.Visibility.Visible;
            LabelMaxCalories.Visibility = System.Windows.Visibility.Visible;
            textboxMaxCal.Visibility = System.Windows.Visibility.Visible;
            SearchIngred.Visibility = System.Windows.Visibility.Visible;
            textboxsearchIngred.Visibility = System.Windows.Visibility.Visible;
            SearchFoodGroup.Visibility = System.Windows.Visibility.Visible;
            SearchMaxCal.Visibility = System.Windows.Visibility.Visible;
            comboboxsearchFoodGroup.Visibility = System.Windows.Visibility.Visible;
            LabelFilters.Visibility = System.Windows.Visibility.Visible;

            // Enable the Search buttons
            SearchIngred.IsEnabled = true;
            SearchFoodGroup.IsEnabled = true;
            SearchMaxCal.IsEnabled = true;

            //btnsaveanddisplay.IsEnabled = true;
            int numIngreds;
            if (int.TryParse(textboxNumIngreds.Text, out numIngreds))
            {
                for (int i = 0; i < numIngreds; i++)
                {
                    AddIngreds();
                }
            }
            textboxNumIngreds.Text = string.Empty;

            int numSteps;
            if (int.TryParse(textBoxnumSteps.Text, out numSteps))
            {
                for (int i = 0; i < numSteps; i++)
                {
                    AddSteps();
                }
            }
            //TextBoxnumSteps.Text= string.Empty; 
        }

        private void SearchIngred_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchFoodGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
