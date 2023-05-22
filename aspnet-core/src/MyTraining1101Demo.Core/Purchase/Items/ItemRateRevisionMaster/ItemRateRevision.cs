namespace MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster
{
    using Abp.Domain.Entities.Auditing;
    using Microsoft.EntityFrameworkCore;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using MyTraining1101Demo.Purchase.Units;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ItemRateRevisions")]
    public class ItemRateRevision : FullAuditedEntity<Guid>
    {
        public DateTime DateOfEntry { get; set; }
        public string Make { get; set; }

        public string CatalogueNumber { get; set; }

        public int OrderingQuantity { get; set; }
       
        [Precision(18, 2)]
        public decimal? RatePerOrderingQuantity { get; set; }

        public int StockQuantityPerOrderingUOM { get; set; }

        [Precision(18, 2)]
        public decimal RatePerStockUOM { get; set; }

        public virtual Guid? StockUOMId { get; set; }
        public virtual Unit StockUOM { get; set; }

        public virtual Guid? OrderingUOMId { get; set; }
        public virtual Unit OrderingUOM { get; set; }

        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
