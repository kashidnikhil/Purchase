using Abp.Domain.Entities.Auditing;
using MyTraining1101Demo.Purchase.Items.ItemMaster;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.Items.ProcurementMaster
{
    [Table("Procurements")]
    public class Procurement : FullAuditedEntity<Guid>
    {
        public string Make { get; set; }
        public string CatalogueNumber { get; set; }
        
        public double RatePerPack { get; set; }

        public long QuantityPerOrderingUOM { get; set; }

        public double RatePerStockUOM { get; set; }

        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
