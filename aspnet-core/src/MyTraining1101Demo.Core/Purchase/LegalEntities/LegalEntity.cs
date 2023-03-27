using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.LegalEntities
{

    [Table("LegalEntity")]
    public class LegalEntity : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter Legal Entity Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
