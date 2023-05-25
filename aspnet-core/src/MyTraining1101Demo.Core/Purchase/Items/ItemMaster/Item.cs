using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using MyTraining1101Demo.Purchase.Items.Enums;
using MyTraining1101Demo.Purchase.MaterialGrades;
using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
using MyTraining1101Demo.Purchase.Units;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.Items.ItemMaster
{
    [Table("Items")]
    public class Item : FullAuditedEntity<Guid>
    {
        public ItemCategory ItemCategory { get; set; }

        public int? CategoryId { get; set; }

        public int? ItemId { get; set; }


        public string GenericName { get; set; }

        public string ItemName { get; set; }

        public ItemType? ItemType { get; set; }

        public ItemAMCRequirement? AMCRequired {get;set;}

        public ItemMobility? ItemMobility { get; set; }
        
        public CalibrationRequirement? CalibrationRequirement { get; set; }
        
        public string Alias { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Specifications { get; set; }

        public string StorageConditions { get; set; }

        public virtual Guid? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public DateTime? PurchaseDate { get; set; }
       
        [Precision(18, 2)]
        public decimal? PurchaseValue { get; set; }

        [Precision(18, 2)]
        public decimal GST { get; set; }

        public string HSNCode { get; set; }

        [Precision(18, 2)]
        public decimal? OrderingRate { get; set; }

        [Precision(18, 2)]
        public decimal? RatePerQuantity { get; set; }


        [Precision(18, 2)]
        public decimal? RateAsOnDate { get; set; }

        public long? Quantity { get; set; }

        public int? LeadTime { get; set; }

        public string SupplierItemName { get; set; }

        public ItemStatus? Status { get; set; }
        
        public string Attachments { get; set; }

        public long? RecordedBy { get; set; }

        public long? ApprovedBy { get; set; }

        public DateTime? DiscardedOn { get; set; }

        public long? DiscardApprovedBy { get; set; }

        public string DiscardedReason { get; set; }

        public virtual Guid? MaterialGradeId { get; set; }
        public virtual MaterialGrade MaterialGrade { get; set; }

        public string Comment { get; set; }

        public string MSL { get; set; }

        public virtual Guid? UnitOrderId { get; set; }
        public virtual Unit UnitOrder { get; set; }

        public virtual Guid? UnitStockId { get; set; }
        public virtual Unit UnitStock { get; set; }

        public virtual Guid? OrderingUOMId { get; set; }
        public virtual Unit OrderingUOM { get; set; }

        public virtual Guid? StockUOMId { get; set; }
        public virtual Unit StockUOM { get; set; }

        [Precision(18, 2)]
        public decimal? QuantityPerOrderingUOM { get; set; }

        public CTQRequirement? CTQRequirement { get; set; }
        public string CTQSpecifications { get; set; }

        public ExpiryApplicable? ExpiryApplicable { get; set; }
        
        [Precision(18, 2)]
        public decimal? MinimumOrderQuantity { get; set; }

        public string Author { get; set; }

        public string Publication { get; set; }

        public int? PublicationYear { get; set; }

        public SubjectCategory? SubjectCategory { get; set; }

        public long? PurchasedBy { get; set; }

        [Precision(18, 2)]
        public decimal? WeightPerUOM { get; set; }

        [Precision(18, 2)]
        public decimal? SellingPrice { get; set; }

    }
}
