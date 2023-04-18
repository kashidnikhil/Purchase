namespace MyTraining1101Demo.Purchase.Suppliers.SupplierAddresses
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SupplierAddresses")]
    public class SupplierAddress : FullAuditedEntity<Guid>
    {
        public string Address { get; set; }

        public string AddressType { get; set; }

        public virtual Guid? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
