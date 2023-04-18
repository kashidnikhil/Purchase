namespace MyTraining1101Demo.Purchase.Suppliers.SupplierBanks
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SupplierBanks")]
    public class SupplierBank : FullAuditedEntity<Guid>
    {
        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string Address { get; set; }

        public long AccountNumber { get; set; }

        public string MICRCode { get; set; }

        public string IFSCCode { get; set; }

        public string RTGS { get; set; }

        public string PaymentMode { get; set; }

        public virtual Guid? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
