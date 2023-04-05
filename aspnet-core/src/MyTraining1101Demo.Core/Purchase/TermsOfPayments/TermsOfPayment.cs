using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.TermsOfPayments
{
    [Table("TermsOfPayment")]
    public class TermsOfPayment : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter Terms Of Payment Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
