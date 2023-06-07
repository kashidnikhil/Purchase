namespace MyTraining1101Demo.Purchase.Assemblies
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Assemblies")]
    public class Assembly : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }

        public virtual Guid? ModelId { get; set; }
        public virtual Model Model { get; set; }
    }
}
