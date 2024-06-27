using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10372065_PROG6221_PART3POE
{
    public class Recipe
    {
        //recipename , ingredient , step 
        public string RecipeName { get; set; }
        public string OriginalUnit { get; set; }
        public int OriginalQuantity { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public List<IngredientDetails> Ingredients { get; set; }
        public List<StepDetails> Steps { get; set; }
        
        
        public Recipe()
        {
            Quantity = OriginalQuantity;
            Unit = OriginalUnit;
            Ingredients = new List<IngredientDetails>();
            Steps = new List<StepDetails>();
        }

        public void ResetIngredientsToOriginal()
        {
            foreach (var ingredient in Ingredients)
            {
                // Assuming IngredientDetails has methods or properties to reset to original values
                // You would need to adjust this based on your actual implementation
                ingredient.Unit = ingredient.OriginalUnit;
                ingredient.Quantity = ingredient.OriginalQuantity;
                
            }
        }
    }
}
