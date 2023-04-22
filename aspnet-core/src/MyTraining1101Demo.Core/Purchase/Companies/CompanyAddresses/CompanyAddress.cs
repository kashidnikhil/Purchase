namespace MyTraining1101Demo.Purchase.Companies.CompanyAddresses
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Companies.CompanyMaster;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompanyAddress")]
    public class CompanyAddress : FullAuditedEntity<Guid>
    {
        public string Address { get; set; }

        public virtual Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
