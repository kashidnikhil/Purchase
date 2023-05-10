using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Items.Enums
{
    public enum ItemMobility
    {
        [Display(Name = "Fixed")]
        Fixed = 1,

        [Display(Name = "Field")]
        Field
    }
}
