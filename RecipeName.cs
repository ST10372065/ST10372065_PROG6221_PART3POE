using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ST10372065_PROG6221_PART3POE
{
    public class RecipeName
    {
        //constructor
        public RecipeName(TextBox textboxRecipeName)
        {
            this.textboxRecipeName = textboxRecipeName;
        }
        //property
        public TextBox textboxRecipeName { get; set; }
    }
}
