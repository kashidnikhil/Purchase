namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using MyTraining1101Demo.Purchase.SubAssemblies;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SubAssemblyItems")]
    public class SubAssemblyItem : FullAuditedEntity<Guid>
    {
        public virtual Guid? SubAssemblyId { get; set; }
        public virtual SubAssembly SubAssembly { get; set; }

        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
