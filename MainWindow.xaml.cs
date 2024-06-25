using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

        public void UpdateRecipeListTextBlock()
        {
            StringBuilder recipeListBuilder = new StringBuilder();
            for (int i = 0; i < recipes.Count; i++)
            {
                recipeListBuilder.AppendLine($"{i + 1}. {recipes[i].RecipeName}");
                double totalCalories = 0;
                recipeListBuilder.AppendLine("\nIngredients:");
                int count = 1;
                foreach (var ingredient in recipes[i].Ingredients)
                {
                    recipeListBuilder.AppendLine($"{count}) {ingredient.textboxQuantity.Text} {ingredient.textboxUnit.Text} of {ingredient.textboxIngredName.Text}(Calories: {ingredient.textboxCalories.Text}) (Food Group: {ingredient.comboFoodGroup.Text})");
                    totalCalories = totalCalories + int.Parse(ingredient.textboxCalories.Text);
                    count++;
                }
                recipeListBuilder.AppendLine($"Total Calories: {totalCalories}");
                recipeListBuilder.AppendLine("\nSteps:");
                count = 1;
                foreach (var step in recipes[i].Steps)
                {
                    recipeListBuilder.AppendLine($"{count}) {step.textboxSteps.Text}");
                    count++;
                }
                recipeListBuilder.AppendLine();
                if (totalCalories > 300)
                {
                    recipeListBuilder.AppendLine($"YOU HAVE EXCEEDED YOUR CALORIE INTAKE OF 300!!!\nConsuming large amounts of calories can be harmful to your health\nPlease try scaling the recipe by 0,5");
                }
                else
                {
                    recipeListBuilder.AppendLine($"\nThis recipe is under 300 calories, which is a healthy calorie limit for a meal. Eating meals under 300 calories can aid in weight loss, and can also improve mental health.");
                }
            }
            textBlockDisplay.Text = recipeListBuilder.ToString();
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
            btnsaveanddisplay.Visibility = System.Windows.Visibility.Visible;

            // Enable the Search buttons
            SearchIngred.IsEnabled = true;
            SearchFoodGroup.IsEnabled = true;
            SearchMaxCal.IsEnabled = true;

            btnsaveanddisplay.IsEnabled = true;
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

        private void SearchIngredient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchFoodGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            gridRecipeNumber.Visibility = Visibility.Visible;
            textboxDisplayrecChoice.Visibility = Visibility.Visible;
            btnDisplayRecCont.Visibility = Visibility.Visible;
        }

        private void btnDisplayAllNames_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnScale_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnsaveanddisplay_Click(object sender, RoutedEventArgs e)
        {
            SearchIngred.IsEnabled = true;
            SearchFoodGroup.IsEnabled = true;
            SearchMaxCal.IsEnabled = true;

            Recipe newRecipe = new Recipe();
            newRecipe.RecipeName = textboxRecipeName.Text;
            newRecipe.Ingredients.AddRange(ingredients);
            newRecipe.Steps.AddRange(listDescription);
            recipes.Add(newRecipe);

            textboxRecipeName.Text = string.Empty;
            pnlDisplay.Children.Clear();

            UpdateRecipeListTextBlock();

            btnDisplayRecipe.IsEnabled = true;
            btnDisplayAllNames.IsEnabled = true;
            btnScale.IsEnabled = true;
            btnReset.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnsaveanddisplay.IsEnabled = true;
            btnEnterIngredsandSteps.IsEnabled = true;

            recipes.Sort((r1, r2) => r1.RecipeName.CompareTo(r2.RecipeName));
        }

        private void btnDisplayRecCont_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnScaleNext_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
