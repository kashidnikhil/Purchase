namespace MyTraining1101Demo.Purchase.Companies.CompanyContactPersons
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Companies.CompanyMaster;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompanyContactPerson")]
    public class CompanyContactPerson : FullAuditedEntity<Guid>
    {
        public string ContactPersonName { get; set; }

        public string Designation { get; set; }

        public string EmailId { get; set; }

        public string MobileNumber { get; set; }

        public virtual Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
