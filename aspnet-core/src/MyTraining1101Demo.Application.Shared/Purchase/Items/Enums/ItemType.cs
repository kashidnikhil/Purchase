using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Items.Enums
{
    public enum ItemType
    {
        [Display(Name = "Analogue")]
        Analogue = 1,

        [Display(Name = "Digital")]
        Digital = 2
    }
}
