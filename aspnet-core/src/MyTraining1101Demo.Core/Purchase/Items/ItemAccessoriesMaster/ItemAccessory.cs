namespace MyTraining1101Demo.Purchase.Items.ItemAccesoriesMaster
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ItemAccessory")]
    public class ItemAccessory : FullAuditedEntity<Guid>
    {
        public virtual Guid? AccessoryId { get; set; }
        public virtual Item Accessory { get; set; }

        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
