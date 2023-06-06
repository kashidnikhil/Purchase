namespace MyTraining1101Demo.Purchase.AssemblyCategories
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AssemblyCategories")]
    public class AssemblyCategory : FullAuditedEntity<Guid>
    {
        public string SubCategory { get; set; }

        public string SubCategory1 { get; set; }

        public string Comments { get; set; }
        public virtual Guid? ModelId { get; set; }
        public virtual Model Model { get; set; }
    }
}
