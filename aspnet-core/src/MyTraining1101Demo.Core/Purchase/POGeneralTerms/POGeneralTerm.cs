using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.POGeneralTerms
{
    [Table("POGeneralTerm")]
    public class POGeneralTerm : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter PO General Term Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
