using System;
using System.Collections.Generic;
using System.Linq;
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

        double scalingFactor;
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
                recipeListBuilder.AppendLine("\nIngredients: ");
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

        public string GetFormattedRecipeDisplay(int displayRecChoice)
        {
            StringBuilder recipeBuilder = new StringBuilder();
            recipeBuilder.AppendLine("Recipe Name: ");

            var selectedRecipe = recipes[displayRecChoice - 1];
            int count = 1;
            recipeBuilder.AppendLine(selectedRecipe.RecipeName);
            recipeBuilder.AppendLine("\nIngredients: ");
            double totalCalories = 0;
            count = 1;

            foreach (var ingredient in selectedRecipe.Ingredients)
            {
                recipeBuilder.AppendLine($"{count}) {ingredient.textboxQuantity.Text} {ingredient.textboxUnit.Text} of {ingredient.textboxIngredName.Text}(Calories: {ingredient.textboxCalories.Text}) (Food Group: {ingredient.comboFoodGroup.Text})");
                count++; 
                totalCalories = totalCalories + int.Parse(ingredient.textboxCalories.Text);
                recipeBuilder.Append($"");
            }

            recipeBuilder.AppendLine($"Total Calories: {totalCalories}");
            recipeBuilder.AppendLine();
            recipeBuilder.AppendLine("Steps: ");

            count = 1;
            foreach (var step in selectedRecipe.Steps)
            {
                recipeBuilder.AppendLine($"{count}) {step.textboxSteps.Text}");
                count++;
            }

            if(totalCalories > 300)
            {
                recipeBuilder.AppendLine($"YOU HAVE EXCEEDED YOUR CALORIE INTAKE OF 300!!!\nConsuming large amounts of calories can be harmful to your health\nPlease try scaling the recipe by 0,5");
            }
            else
            {
                recipeBuilder.AppendLine($"\nThis recipe is under 300 calories, which is a healthy calorie limit for a meal. Eating meals under 300 calories can aid in weight loss, and can also improve mental health.");
            }

            return recipeBuilder.ToString();
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
        }

        private void SearchIngredient_Click(object sender, RoutedEventArgs e)
        {
            string searchIngredient = textboxsearchIngred.Text;
            textBlockDisplay.Text = string.Empty;
            List<Recipe> filteredRecipes = recipes.Where(recipe =>recipe.Ingredients.Any(ingredient =>ingredient.textboxIngredName.Text.Equals(searchIngredient, StringComparison.OrdinalIgnoreCase))).ToList();

            if (filteredRecipes.Count > 0)
            {
                StringBuilder recipeListBuilder = new StringBuilder();
                for (int i = 0; i < filteredRecipes.Count; i++)
                {
                    recipeListBuilder.AppendLine
                        ($"{i + 1}.{filteredRecipes[i].RecipeName}");
                }
                textBlockDisplay.Text = recipeListBuilder.ToString();
            }
            else
            {
                MessageBox.Show("Recipe not found");
            }
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
            textBlockDisplay.Text = DisplayRecipeNameMethod();
        }
        public string DisplayRecipeNameMethod()
        {
            StringBuilder recipeBuilder = new StringBuilder();
            recipeBuilder.AppendLine("Recipe Names: \n");
            int nameCount = 1;
            foreach (var item in recipes)
            {
                recipeBuilder.AppendLine($"{nameCount}{" :"}{item.RecipeName}");
                nameCount++;
            }
            return recipeBuilder.ToString();
        }

        private void btnScale_Click(object sender, RoutedEventArgs e)
        {
            gridRecipeNumber.Visibility = Visibility.Visible;
            gridScaleValue.Visibility = Visibility.Visible;
            btnScaleNext.Visibility = Visibility.Visible;
        }

        public void ScaleRecipe(int recipeIndex, double scalingFactor)
        {
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                Recipe recipe = recipes[recipeIndex];
                foreach (var ingredient in recipe.Ingredients)
                {
                    if (double.TryParse(ingredient.textboxQuantity.Text, out double originalQuantity))
                    {
                        string unit = ingredient.textboxUnit.Text.ToLower();
                        double scaledQuantity = originalQuantity * scalingFactor; // Correctly apply scaling factor here
                        string newUnit = unit;

                        // Conversion logic for tablespoons and cups
                        if (unit.Equals("tablespoons", StringComparison.OrdinalIgnoreCase) || unit.Equals("tablespoon", StringComparison.OrdinalIgnoreCase))
                        {
                            // Convert to cups if applicable
                            if (scaledQuantity >= 16)
                            {
                                newUnit = "cups";
                                scaledQuantity /= 16; // Convert tablespoons to cups
                            }
                        }
                        else if (unit.Equals("cups", StringComparison.OrdinalIgnoreCase) || unit.Equals("cup", StringComparison.OrdinalIgnoreCase))
                        {
                            // Convert to tablespoons if scaled quantity is less than 1 cup, this part remains unchanged as the issue was not here
                            if (scaledQuantity < 1)
                            {
                                newUnit = "tablespoons";
                                scaledQuantity *= 16; // Convert cups to tablespoons
                            }
                        }
                        // Conversion logic for teaspoons to tablespoons
                        else if (unit.Equals("teaspoons", StringComparison.OrdinalIgnoreCase) || unit.Equals("teaspoon", StringComparison.OrdinalIgnoreCase))
                        {
                            // Convert to tablespoons if applicable
                            if (scaledQuantity >= 3)
                            {
                                newUnit = "tablespoons";
                                scaledQuantity /= 3; // Convert teaspoons to tablespoons
                            }
                        }

                        // Update the ingredient with the new quantity and unit
                        ingredient.textboxQuantity.Text = scaledQuantity.ToString("0.##");
                        ingredient.textboxUnit.Text = newUnit;

                        // Assuming calories scale linearly with quantity
                        if (double.TryParse(ingredient.textboxCalories.Text, out double calories))
                        {
                            ingredient.textboxCalories.Text = (calories * scalingFactor).ToString("0.##");
                        }
                    }
                }
                textBlockDisplay.Text = GetFormattedRecipeDisplay(recipeIndex + 1);
            }
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
            if (int.TryParse(textboxDisplayrecChoice.Text, out int displayRecChoice))
            {
                if(displayRecChoice >= 1 && displayRecChoice <= recipes.Count)
                {
                    Recipe selectedRecipe = recipes[displayRecChoice - 1];
                    textBlockDisplay.Text = GetFormattedRecipeDisplay(displayRecChoice);
                }
                else
                {
                    MessageBox.Show("Invalid recipe number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    textBlockDisplay.Text = "Invalid recipe number";
                }
            }
            else
            {
                MessageBox.Show("Invalid recipe number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            gridRecipeNumber.Visibility = Visibility.Hidden;
            gridScaleValue.Visibility = Visibility.Hidden;
            btnDisplayRecCont.Visibility = Visibility.Hidden;
        }

        private void btnScaleNext_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(textboxDisplayrecChoice.Text, out int displayRecChoice))
            {
                if (displayRecChoice > 0 && displayRecChoice <= recipes.Count)
                {
                    if (double.TryParse(textboxScaleNumber.Text, out double scaleNumber))
                    {
                        // Validate the scaling factor to be 0.5, 2, or 3
                        if (scaleNumber == 0.5 || scaleNumber == 2 || scaleNumber == 3)
                        {
                            ScaleRecipe(displayRecChoice - 1, scaleNumber);
                        }
                        else
                        {
                            MessageBox.Show("Invalid scaling factor. Please enter 0.5, 2, or 3.", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid scaling factor", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid recipe number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid recipe number", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            gridRecipeNumber.Visibility = Visibility.Hidden;
            gridScaleValue.Visibility = Visibility.Hidden;
            btnScaleNext.Visibility = Visibility.Hidden;
        }
    }
}
