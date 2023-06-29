using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Transactions.Enums
{
    public enum MaterialRequisitionLocationType
    {
        [Display(Name = "Head Office")]
        HeadOffice = 1,

        [Display(Name = "Factory")]
        Factory = 2
    }
}
