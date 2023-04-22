namespace MyTraining1101Demo.Purchase.Companies.CompanyContactPersons
{
    using Abp.Domain.Entities.Auditing;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompanyContactPerson")]
    public class CompanyContactPerson : FullAuditedEntity<Guid>
    {
        public string ContactPersonName { get; set; }

        public string Designation { get; set; }

        public string EmailId { get; set; }

        public string MobileNumber { get; set; }
    }
}
