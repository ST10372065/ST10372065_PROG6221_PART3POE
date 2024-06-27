using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        /// <summary>
        /// add steps to the recipe
        /// </summary>
        public void AddSteps()
        {
            // Dynamically add steps to the stack panel
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

        /// <summary>
        /// add ingredients to the recipe and dynamically add the ingredients to the stack panel
        /// </summary>
        public void AddIngreds()
        {
            // Dynamically add ingredients to the stack panel
            StackPanel ingredientPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            // Add the ingredient details to the stack panel
            TextBox textboxIngredName = new TextBox() { Width = 100 };
            TextBox textboxQuantity = new TextBox() { Width = 100 };
            TextBox textboxUnit = new TextBox() { Width = 100 };
            TextBox textboxCalories = new TextBox() { Width = 100 };
            ComboBox comboFoodGroup = new ComboBox() { Width = 100 };
            // Add the food groups to the combo box
            comboFoodGroup.Items.Add("Vegetables");
            comboFoodGroup.Items.Add("Fruits");
            comboFoodGroup.Items.Add("Grains");
            comboFoodGroup.Items.Add("Protein Foods");
            comboFoodGroup.Items.Add("Dairy");
            comboFoodGroup.Items.Add("Oils and Solid Fats");
            comboFoodGroup.Items.Add("Added Sugars");
            comboFoodGroup.Items.Add("Beverages");
            // Add the ingredients to the stack panel
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
            // Add the ingredient details to the list
            ingredients.Add(new IngredientDetails(textboxIngredName, textboxQuantity, textboxUnit, textboxCalories, comboFoodGroup));
            // Add the ingredient panel to the display panel
            pnlDisplay.Children.Add(ingredientPanel);
            
        }

        /// <summary>
        /// format the recipe list to display the recipe name, ingredients, total calories, steps and calorie limit
        /// </summary>
        public void UpdateRecipeListTextBlock()
        {
            // Clear the text block
            StringBuilder recipeListBuilder = new StringBuilder();
            // Loop through the recipes and display the recipe name, ingredients, total calories, steps and calorie limit
            for (int i = 0; i < recipes.Count; i++)
            {
                recipeListBuilder.AppendLine($"{i + 1}. {recipes[i].RecipeName}");
                double totalCalories = 0;
                recipeListBuilder.AppendLine("\nIngredients: ");
                int count = 1;
                // Loop through the ingredients and display the ingredient details
                foreach (var ingredient in recipes[i].Ingredients)
                {
                    // Append the ingredient details to the recipe list
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
                // Display the calorie limit
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
            // Display the recipe list
            textBlockDisplay.Text = recipeListBuilder.ToString();
        }

        /// <summary>
        /// get the formatted recipe display
        /// </summary>
        /// <param name="displayRecChoice"></param>
        /// <returns></returns>
        public string GetFormattedRecipeDisplay(int displayRecChoice)
        {
            // Display the recipe details
            StringBuilder recipeBuilder = new StringBuilder();
            recipeBuilder.AppendLine("Recipe Name: ");
            // Get the selected recipe
            var selectedRecipe = recipes[displayRecChoice - 1];
            int count = 1;
            recipeBuilder.AppendLine(selectedRecipe.RecipeName);
            recipeBuilder.AppendLine("\nIngredients: ");
            double totalCalories = 0;
            count = 1;
            // Loop through the ingredients and display the ingredient details
            foreach (var ingredient in selectedRecipe.Ingredients)
            {
                recipeBuilder.AppendLine($"{count}) {ingredient.textboxQuantity.Text} {ingredient.textboxUnit.Text} of {ingredient.textboxIngredName.Text}(Calories: {ingredient.textboxCalories.Text}) (Food Group: {ingredient.comboFoodGroup.Text})");
                count++; 
                totalCalories = totalCalories + int.Parse(ingredient.textboxCalories.Text);
                recipeBuilder.Append($"");
            }
            // Display the total calories
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
            // Return the formatted recipe display
            return recipeBuilder.ToString();
        }

        /// <summary>
        /// tells the program to add ingredients and steps to the recipe as well as how many ingredients and steps to add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            // Enable the buttons
            btnsaveanddisplay.IsEnabled = true;
            int numIngreds;
            // Add the ingredients to the recipe
            if (int.TryParse(textboxNumIngreds.Text, out numIngreds))
            {
                for (int i = 0; i < numIngreds; i++)
                {
                    AddIngreds();
                }
            }
            textboxNumIngreds.Text = string.Empty;
            // Add the steps to the recipe
            int numSteps;
            if (int.TryParse(textBoxnumSteps.Text, out numSteps))
            {
                for (int i = 0; i < numSteps; i++)
                {
                    AddSteps();
                }
            }
            textBoxnumSteps.Text = string.Empty;
            
            textBlockDisplay.Text = "";
        }

        /// <summary>
        /// search for the ingredient in the recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Search for the ingredient in the recipe
            string searchIngredient = textboxsearchIngred.Text;
            textBlockDisplay.Text = string.Empty;
            // Filter the recipes based on the ingredient
            List<Recipe> filteredRecipes = recipes.Where(recipe =>recipe.Ingredients.Any(ingredient =>ingredient.textboxIngredName.Text.Equals(searchIngredient, StringComparison.OrdinalIgnoreCase))).ToList();
            // Display the filtered recipes
            if (filteredRecipes.Count > 0)
            {
                StringBuilder recipeListBuilder = new StringBuilder();
                for (int i = 0; i < filteredRecipes.Count; i++)
                {
                    recipeListBuilder.AppendLine($"{i + 1}.{filteredRecipes[i].RecipeName}");
                }
                textBlockDisplay.Text = recipeListBuilder.ToString();
            }
            else
            {
                MessageBox.Show("Recipe not found");
            }
        }

        /// <summary>
        /// search for the food group in the recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchFoodGroup_Click(object sender, RoutedEventArgs e)
        {
            // Search for the food group in the recipe
            if (comboboxsearchFoodGroup.SelectedItem == null)
            {
                MessageBox.Show("Please select a food group first.");
                return;
            }
            // Get the selected food group
            ComboBoxItem selectedFoodGroupItem = comboboxsearchFoodGroup.SelectedItem as ComboBoxItem;
            string selectedFoodGroup = selectedFoodGroupItem.Content.ToString();
            // Filter the recipes based on the food group
            List<Recipe> matchingRecipes = new List<Recipe>();
            // Loop through the recipes and filter the recipes based on the food group
            for (int i = 0; i < recipes.Count; i++)
            {
                // Loop through the ingredients and check if the food group matches
                for (int j = 0; j < recipes[i].Ingredients.Count; j++)
                {
                    // Check if the food group matches
                    if (recipes[i].Ingredients[j].comboFoodGroup.Text == selectedFoodGroup)
                    {
                        // Add the recipe to the matching recipes list
                        matchingRecipes.Add(recipes[i]);
                        break;
                    }
                }
            }
            // Display the matching recipes
            if (matchingRecipes.Any())
            {
                // Display the matching recipes
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Recipes containing {selectedFoodGroup}:");
                // Loop through the matching recipes and display the recipe names
                foreach (var recipe in matchingRecipes)
                {
                    // Display the recipe name
                    sb.AppendLine(recipe.RecipeName);
                }

                textBlockDisplay.Text = sb.ToString();
            }
            else
            {
                textBlockDisplay.Text = $"No recipes found containing {selectedFoodGroup}.";
            }
        }

        /// <summary>
        /// close the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// display the recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            gridRecipeNumber.Visibility = Visibility.Visible;
            textboxDisplayrecChoice.Visibility = Visibility.Visible;
            btnDisplayRecCont.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// display all the recipe names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplayAllNames_Click(object sender, RoutedEventArgs e)
        {
            textBlockDisplay.Text = DisplayRecipeNameMethod();
        }

        /// <summary>
        /// display the recipe names
        /// </summary>
        /// <returns></returns>
        public string DisplayRecipeNameMethod()
        {
            // Display the recipe names
            StringBuilder recipeBuilder = new StringBuilder();
            recipeBuilder.AppendLine("Recipe Names: \n");
            int nameCount = 1;
            // Loop through the recipes and display the recipe names
            foreach (var item in recipes)
            {
                recipeBuilder.AppendLine($"{nameCount}{" :"}{item.RecipeName}");
                nameCount++;
            }
            return recipeBuilder.ToString();
        }

        /// <summary>
        /// scale the recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScale_Click(object sender, RoutedEventArgs e)
        {
            gridRecipeNumber.Visibility = Visibility.Visible;
            gridScaleValue.Visibility = Visibility.Visible;
            btnScaleNext.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// scale the recipe as well as does the conversion for tablespoons, cups and teaspoons both ways
        /// </summary>
        /// <param name="recipeIndex"></param>
        /// <param name="scalingFactor"></param>
        public void ScaleRecipe(int recipeIndex, double scalingFactor)
        {
            // Validate the recipe index
            if (recipeIndex >= 0 && recipeIndex < recipes.Count)
            {
                // Get the recipe to scale
                Recipe recipe = recipes[recipeIndex];
                // Loop through the ingredients and scale the quantities
                foreach (var ingredient in recipe.Ingredients)
                {
                    // Validate the quantity
                    if (double.TryParse(ingredient.textboxQuantity.Text, out double originalQuantity))
                    {
                        // Get the unit and scaled quantity
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

        /// <summary>
        /// add the recipe name, ingredients and steps to the recipe list as well as display the recipe list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsaveanddisplay_Click(object sender, RoutedEventArgs e)
        {
            // Making the elements inside the Grid visible
            SearchIngred.IsEnabled = true;
            SearchFoodGroup.IsEnabled = true;
            SearchMaxCal.IsEnabled = true;
            // Add the recipe name, ingredients and steps to the recipe list
            Recipe newRecipe = new Recipe();
            newRecipe.RecipeName = textboxRecipeName.Text;
            newRecipe.Ingredients.AddRange(ingredients);
            newRecipe.Steps.AddRange(listDescription);
            recipes.Add(newRecipe);


            // Display the recipe list
            textboxRecipeName.Text = string.Empty;
            pnlDisplay.Children.Clear();
            // Display the recipe list
            UpdateRecipeListTextBlock();
            // Enable the buttons
            btnDisplayRecipe.IsEnabled = true;
            btnDisplayAllNames.IsEnabled = true;
            btnScale.IsEnabled = true;
            btnReset.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnsaveanddisplay.IsEnabled = true;
            btnEnterIngredsandSteps.IsEnabled = true;
            // Sort the recipes by name
            recipes.Sort((r1, r2) => r1.RecipeName.CompareTo(r2.RecipeName));
            // Clear the ingredients and steps
            ingredients.Clear();
            listDescription.Clear();
        }

        /// <summary>
        /// display the recipe name with the number of the recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplayRecCont_Click(object sender, RoutedEventArgs e)
        {
            // Display the recipe with the specified number
            if (int.TryParse(textboxDisplayrecChoice.Text, out int displayRecChoice))
            {
                // Validate the recipe number
                if (displayRecChoice >= 1 && displayRecChoice <= recipes.Count)
                {
                    // Get the selected recipe
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

        /// <summary>
        /// determine the scaling factor for the recipe and if it is 0.5, 2 or 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScaleNext_Click(object sender, RoutedEventArgs e)
        {
            // Scale the recipe
            if (int.TryParse(textboxDisplayrecChoice.Text, out int displayRecChoice))
            {
                // Validate the recipe number
                if (displayRecChoice > 0 && displayRecChoice <= recipes.Count)
                {
                    // Validate the scaling factor
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

        /// <summary>
        /// doesnt work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            foreach (var recipe in recipes)
            {
                recipe.ResetIngredientsToOriginal();
            }

            UpdateRecipeListTextBlock();
        }

        /// <summary>
        /// search for the maximum calories in the recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchMaxCalories_Click(object sender, RoutedEventArgs e)
        {
            // Search for the maximum calories in the recipe
            if (int.TryParse(textboxMaxCal.Text, out int maxCalories))
            {
                // Filter the recipes based on the maximum calories
                textBlockDisplay.Text = string.Empty;
                // Filter the recipes based on the maximum calories
                List<Recipe> filteredRecipes = recipes.Where(recipe =>recipe.Ingredients.Sum(ingredient =>int.Parse(ingredient.textboxCalories.Text)) <= maxCalories).ToList();
                // Display the filtered recipes
                if (filteredRecipes.Count > 0)
                {
                    // Display the filtered recipes
                    StringBuilder recipeListBuilder = new StringBuilder();
                    for (int i = 0; i < filteredRecipes.Count; i++)
                    {
                        recipeListBuilder.AppendLine($"{i + 1}. {filteredRecipes[i].RecipeName}");
                    }
                    textBlockDisplay.Text = recipeListBuilder.ToString();
                }
                else
                {
                    textBlockDisplay.Text = "No recipes found within the specified calorie limit";
                }
            }
            else
            {
                textBlockDisplay.Text = "Please enter a valid number for maximum calories";
            }
        }

        /// <summary>
        /// delete the recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            gridDelete.Visibility = Visibility.Visible;
            textboxDeleteRecipe.Visibility = Visibility.Visible;
            btnDeleteRecipe.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// delete the selected recipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Delete the selected recipe
            if (int.TryParse(textboxDeleteRecipe.Text, out int recipeNumber) && recipeNumber >= 1 && recipeNumber <= recipes.Count)
            {
                int recipeIndex = recipeNumber - 1;
                // Remove the recipe from the list
                recipes.RemoveAt(recipeIndex);

                textBlockDisplay.Text = "Recipe deleted successfully.";

                textboxDeleteRecipe.Clear();

                UpdateRecipeListTextBlock();
            }
            else
            {
                MessageBox.Show("Please enter a valid recipe number.");
            }
        }

    }
}
