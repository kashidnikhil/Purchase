using Abp.Domain.Entities.Auditing;
using MyTraining1101Demo.Purchase.Items.ItemMaster;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster
{
    [Table("ItemAttachment")]
    public class ItemAttachment : FullAuditedEntity<Guid>
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
