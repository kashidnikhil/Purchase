using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.MaterialGrades
{

    [Table("MaterialGrade")]
    public class MaterialGrade : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter Material Grade Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
