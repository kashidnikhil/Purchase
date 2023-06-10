namespace MyTraining1101Demo.Purchase.Items.Enums
{
    using System.ComponentModel.DataAnnotations;
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
