namespace MyTraining1101Demo.Purchase.Items.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum ItemType
    {
        [Display(Name = "Analogue")]
        Analogue = 1,

        [Display(Name = "Digital")]
        Digital = 2
    }
}
