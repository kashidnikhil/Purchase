namespace MyTraining1101Demo.Purchase.Models
{
    using Abp.Domain.Entities.Auditing;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Model")]
    public class Model : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter Model Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
