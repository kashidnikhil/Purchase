using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Items.Enums
{
    public enum CalibrationTypeEnum
    {
        [Display(Name = "External")]
        External = 1,

        [Display(Name = "Internal")]
        Internal,

        [Display(Name = "Intermediate")]
        Intermediate,
    }
}
