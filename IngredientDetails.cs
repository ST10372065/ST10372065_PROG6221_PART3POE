using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ST10372065_PROG6221_PART3POE
{
    public class IngredientDetails
    {
        //constructor
        public IngredientDetails(TextBox textboxIngredName, TextBox textboxQuantity, TextBox textboxUnit, TextBox textboxCalories, ComboBox comboFoodGroup)
        {
            this.textboxIngredName = textboxIngredName;
            this.textboxQuantity = textboxQuantity;
            this.textboxUnit = textboxUnit;
            this.textboxCalories = textboxCalories;
            this.comboFoodGroup = comboFoodGroup;
            this.FoodGroup = comboFoodGroup.SelectedItem as ComboBoxItem;
        }


        //properties
        public TextBox textboxIngredName { get; set; }
        public TextBox textboxQuantity { get; set; }
        public TextBox textboxUnit { get; set; }
        public TextBox textboxCalories { get; set; }
        public ComboBox comboFoodGroup { get; set; }
        public ComboBoxItem FoodGroup { get; set; }
    }
}
