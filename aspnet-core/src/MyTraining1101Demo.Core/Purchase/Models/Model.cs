using Abp.Domain.Entities.Auditing;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Models
{

    [Table("Model")]
    public class Model : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter Model Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
