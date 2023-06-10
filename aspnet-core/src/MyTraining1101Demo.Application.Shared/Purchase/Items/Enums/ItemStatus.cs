namespace MyTraining1101Demo.Purchase.Items.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum ItemStatus
    {
        [Display(Name = "Active")]
        Active = 1,

        [Display(Name = "Inactive")]
        Inactive = 2
    }
}
