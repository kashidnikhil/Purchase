namespace MyTraining1101Demo.Purchase.Items.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum CalibrationFrequency
    {
        [Display(Name = "Monthly")]
        Monthly = 1,

        [Display(Name = "Yearly")]
        Yearly
    }
}
