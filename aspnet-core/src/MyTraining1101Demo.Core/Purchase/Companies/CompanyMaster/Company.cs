namespace MyTraining1101Demo.Purchase.Companies.CompanyMaster
{
    using Abp.Domain.Entities.Auditing;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Companies")]
    public class Company : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
    }
}
