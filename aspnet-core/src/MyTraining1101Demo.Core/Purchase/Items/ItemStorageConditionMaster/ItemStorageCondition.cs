using Abp.Domain.Entities.Auditing;
using MyTraining1101Demo.Purchase.Items.Enums;
using MyTraining1101Demo.Purchase.Items.ItemMaster;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster
{
    [Table("ItemStorageCondition")]
    public class ItemStorageCondition : FullAuditedEntity<Guid>
    {
        public HazardousEnum Hazardous { get; set; }
        public long ThresholdQuantity { get; set; }
     
        public string Location { get; set; }
        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
