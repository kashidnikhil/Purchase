using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Items.Enums
{
    public enum ExpiryApplicable
    {
        [Display(Name = "Yes")]
        Yes = 1,

        [Display(Name = "No")]
        No = 2
    }
}
