namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ModelWiseItems")]
    public class ModelWiseItem : FullAuditedEntity<Guid>
    {
        public string Comments { get; set; }
        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }

        public virtual Guid? ModelWiseItemMasterId { get; set; }
        public virtual ModelWiseItemMaster ModelWiseItemMaster { get; set; }
        
    }
}
