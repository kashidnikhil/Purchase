namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Assemblies;
    using MyTraining1101Demo.Purchase.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SubAssemblyItems")]
    public class SubAssemblyItem : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }

        public virtual Guid? AssemblyId { get; set; }
        public virtual Assembly Assembly { get; set; }

        public virtual Guid? ModelId { get; set; }
        public virtual Model Model { get; set; }
    }
}
