using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Suppliers.Enums
{
    public enum SupplierStatus
    {
        [Display(Name = "Inactive")]
        Inactive = 1,

        [Display(Name = "Active")]
        Active
    }
}
