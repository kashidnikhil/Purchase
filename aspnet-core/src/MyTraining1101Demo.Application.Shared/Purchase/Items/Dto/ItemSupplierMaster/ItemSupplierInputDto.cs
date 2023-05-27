using System;

namespace MyTraining1101Demo.Purchase.Items.Dto.ItemSupplierMaster
{
    public class ItemSupplierInputDto
    {
        public Guid? Id { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? ItemId { get; set; }
    }
}
