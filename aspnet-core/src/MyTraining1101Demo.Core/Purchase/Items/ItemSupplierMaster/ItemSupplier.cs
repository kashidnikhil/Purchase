namespace MyTraining1101Demo.Purchase.Items.ItemSupplierMaster
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ItemSupplier")]
    public class ItemSupplier : FullAuditedEntity<Guid>
    {
        public virtual Guid? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
