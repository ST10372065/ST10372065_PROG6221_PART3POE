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
        public List<IngredientDetails> Ingredients { get; set; }
        public List<StepDetails> Steps { get; set; }
        public Recipe()
        {
            Ingredients = new List<IngredientDetails>();
            Steps = new List<StepDetails>();
        }
    }
}
