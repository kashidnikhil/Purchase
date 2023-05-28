using Abp.Domain.Entities.Auditing;
using MyTraining1101Demo.Purchase.Items.ItemMaster;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster
{
    [Table("ItemSpare")]
    public class ItemSpare : FullAuditedEntity<Guid>
    {
        public virtual Guid? ItemSparesId { get; set; }
        public virtual Item ItemSpares { get; set; }

        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
