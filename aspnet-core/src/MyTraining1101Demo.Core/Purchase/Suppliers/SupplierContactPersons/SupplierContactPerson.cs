namespace MyTraining1101Demo.Purchase.Suppliers.SupplierContactPersons
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SupplierContactPersons")]
    public class SupplierContactPerson : FullAuditedEntity<Guid>
    {
        public string ContactPersonName { get; set; }

        public string Designation { get; set; }

        public string EmailId { get; set; }

        public string MobileNumber { get; set; }

        public virtual Guid? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
