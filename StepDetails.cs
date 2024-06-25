using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ST10372065_PROG6221_PART3POE
{
    public class StepDetails
    {
        //constructor
        public StepDetails(TextBox textboxSteps)
        {
            this.textboxSteps = textboxSteps;
        }
        //property
        public TextBox textboxSteps { get; set; }
    }
}
