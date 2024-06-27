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
        public TextBox textboxIngredName { get; set; }
        public TextBox textboxQuantity { get; set; }
        public TextBox textboxUnit { get; set; }
        public TextBox textboxCalories { get; set; }
        public ComboBox comboFoodGroup { get; set; }
        public ComboBoxItem FoodGroup { get; set; }
        public string OriginalUnit { get; set; }
        public int OriginalQuantity { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }

        public IngredientDetails(TextBox textboxIngredName, TextBox textboxQuantity, TextBox textboxUnit, TextBox textboxCalories, ComboBox comboFoodGroup)
        {
            this.textboxIngredName = textboxIngredName;
            this.textboxQuantity = textboxQuantity;
            this.textboxUnit = textboxUnit;
            this.textboxCalories = textboxCalories;
            this.comboFoodGroup = comboFoodGroup;
            this.FoodGroup = comboFoodGroup.SelectedItem as ComboBoxItem;

            if (int.TryParse(textboxQuantity.Text, out int quantity))
            {
                this.OriginalQuantity = quantity;
                this.Quantity = quantity;
            }
            this.OriginalUnit = textboxUnit.Text;
            this.Unit = textboxUnit.Text;
        }
    }
}
