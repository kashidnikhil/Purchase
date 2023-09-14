namespace MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionItemMaster
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using MyTraining1101Demo.Purchase.SubAssemblyItems;
    using MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MaterialRequisitionItems")]
    public class MaterialRequisitionItem : FullAuditedEntity<Guid>
    {
        //public MaterialRequisitionType MaterialRequisitionType { get; set; }

        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }

        public virtual Guid? MaterialRequisitionId { get; set; }
        public virtual MaterialRequisition MaterialRequisition { get; set; }

        public virtual Guid? SubAssemblyItemId { get; set; }
        public virtual SubAssemblyItem SubAssemblyItem { get; set; }



        public int RequiredQuantity { get; set; }

    }
}
