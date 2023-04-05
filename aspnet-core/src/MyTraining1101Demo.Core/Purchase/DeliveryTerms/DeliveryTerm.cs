using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.DeliveryTerms
{

    [Table("DeliveryTerm")]
    public class DeliveryTerm : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter Delivery Term Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }


}
