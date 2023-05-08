using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Items.Enums
{
    public enum ItemStatus
    {
        [Display(Name = "Active")]
        Active = 1,

        [Display(Name = "Inactive")]
        Inactive = 2
    }
}
