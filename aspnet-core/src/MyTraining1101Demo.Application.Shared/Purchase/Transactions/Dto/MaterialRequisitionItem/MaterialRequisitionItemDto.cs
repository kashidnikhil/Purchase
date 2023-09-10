using System;

namespace MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionItem
{
    public class MaterialRequisitionItemDto
    {
        public Guid Id { get; set; }

        public Guid? ItemId { get; set; }

        public Guid? SubAssemblyItemId { get; set; }
        public Guid? SubAssemblyWiseItemId { get; set; }


        public int RequiredQuantity { get; set; }
        public Guid? MaterialRequisitionId { get; set; }

        public string AssemblyName { get; set; }

        public string ModelName { get; set; }

        public string ItemName { get; set; }
        public string ItemCategoryName { get; set; }
        public string UnitName { get; set; }

    }
}
