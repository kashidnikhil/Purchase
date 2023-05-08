using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Items.Enums
{
    public enum CalibrationFrequency
    {
        [Display(Name = "Monthly")]
        Monthly = 1,

        [Display(Name = "Yearly")]
        Yearly
    }
}
