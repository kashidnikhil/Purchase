namespace MyTraining1101Demo.Purchase.Companies.CompanyAddresses
{
    using Abp.Domain.Entities.Auditing;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompanyAddress")]
    public class CompanyAddress : FullAuditedEntity<Guid>
    {
        public string Address { get; set; }
    }
}
