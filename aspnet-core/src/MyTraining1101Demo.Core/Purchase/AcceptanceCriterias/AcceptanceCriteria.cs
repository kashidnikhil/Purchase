using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.AcceptanceCriterias
{

    [Table("AcceptanceCriteria")]
    public class AcceptanceCriteria : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter Acceptance Criteria Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
