using System;

namespace MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionItem
{
    public class MaterialRequisitionItemDto
    {
        public Guid Id { get; set; }

        public Guid? ItemId { get; set; }
        
        public Guid? MaterialRequisitionId { get; set; }
    }
}
