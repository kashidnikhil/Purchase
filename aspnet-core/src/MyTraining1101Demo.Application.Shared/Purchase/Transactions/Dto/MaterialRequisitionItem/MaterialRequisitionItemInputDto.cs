using System;

namespace MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionItem
{
    public class MaterialRequisitionItemInputDto
    {
        public Guid? Id { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? SubAssemblyItemId { get; set; }
        
        public Guid? MaterialRequisitionId { get; set; }

        public int RequiredQuantity { get; set; }
        public string SubAssemblyName { get; set; }

        public string ItemName { get; set; }
        public string ItemCategoryName { get; set; }
        public string UnitName { get; set; }
    }
}
