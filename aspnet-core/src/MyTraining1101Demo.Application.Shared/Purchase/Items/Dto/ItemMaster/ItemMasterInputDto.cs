using MyTraining1101Demo.Purchase.Items.Enums;
using System;

namespace MyTraining1101Demo.Purchase.Items.Dto.ItemMaster
{
    public class ItemMasterInputDto
    {
        public Guid? Id { get; set; }
        public ItemCategory ItemCategory { get; set; }

        public int CategoryId { get; set; }

        public int ItemId { get; set; }

        public string GenericName { get; set; }

        public string ItemName { get; set; }

        public ItemType? ItemType { get; set; }

        public ItemAMCRequirement? AMCRequired { get; set; }

        public ItemMobility? ItemMobility { get; set; }

        public CalibrationRequirement? CalibrationRequirement { get; set; }

        public string Alias { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Specifications { get; set; }

        public string StorageConditions { get; set; }

        public Guid? SupplierId { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal? PurchaseValue { get; set; }

        public decimal GST { get; set; }

        public string HSNCode { get; set; }

        public decimal OrderingRate { get; set; }

        public decimal RatePerQuantity { get; set; }

        public decimal? RateAsOnDate { get; set; }

        public long? Quantity { get; set; }

        public int? LeadTime { get; set; }

        public string SupplierItemName { get; set; }

        public ItemStatus? Status { get; set; }

        public string Attachments { get; set; }

        public long RecordedBy { get; set; }

        public long ApprovedBy { get; set; }

        public DateTime? DiscardedOn { get; set; }

        public long DiscardApprovedBy { get; set; }

        public string DiscardedReason { get; set; }

        public Guid? MaterialGradeId { get; set; }

        public string Comment { get; set; }

        public string MSL { get; set; }

        public Guid? UnitOrderId { get; set; }

        public Guid? UnitStockId { get; set; }

        public Guid? OrderingUOMId { get; set; }

        public Guid? StockUOMId { get; set; }

        public long QuantityPerOrderingUOM { get; set; }

        public CTQRequirement? CTQRequirement { get; set; }
        public string CTQSpecifications { get; set; }

        public ExpiryApplicable? ExpiryApplicable { get; set; }

        public int? MinimumOrderQuantity { get; set; }

        public string Author { get; set; }

        public string Publication { get; set; }

        public int PublicationYear { get; set; }

        public SubjectCategory? SubjectCategory { get; set; }

        public long? PurchasedBy { get; set; }

        public decimal? WeightPerUOM { get; set; }

        public decimal? SellingPrice { get; set; }
    }
}
