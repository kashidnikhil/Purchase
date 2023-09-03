namespace MyTraining1101Demo.Purchase.Transactions.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum MaterialRequisitionType
    {
        [Display(Name = "Single Item")]
        SingleItem = 1,

        [Display(Name = "Assembly Wise Item")]
        AssemblyWiseItem = 2,
            
        [Display(Name = "Model Wise Item")]
        ModelWiseItem = 3
    }
}
