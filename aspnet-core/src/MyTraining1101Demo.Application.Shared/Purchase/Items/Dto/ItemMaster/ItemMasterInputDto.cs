namespace MyTraining1101Demo.Purchase.Items.Dto.ItemMaster
{
    using MyTraining1101Demo.Purchase.Items.Dto.CalibrationAgenciesMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.CalibrationTypeMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemAccessoriesMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemAttachmentsMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemRateRevisionMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemStorageConditionMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemSupplierMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ProcurementMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.RequiredItemSparesMaster;
    using MyTraining1101Demo.Purchase.Items.Enums;
    using System;
    using System.Collections.Generic;
    public class ItemMasterInputDto
    {
        public Guid? Id { get; set; }
        public Guid ItemCategoryId { get; set; }

        public long? CategoryId { get; set; }

        public int? ItemId { get; set; }

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

        public decimal? OrderingRate { get; set; }

        public decimal? RatePerQuantity { get; set; }

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

        public Guid? MaterialGradeId { get; set; }

        public string Comment { get; set; }

        public string MSL { get; set; }

        public Guid? UnitOrderId { get; set; }

        public Guid? UnitStockId { get; set; }

        public Guid? OrderingUOMId { get; set; }

        public Guid? StockUOMId { get; set; }

        public decimal? QuantityPerOrderingUOM { get; set; }

        public CTQRequirement? CTQRequirement { get; set; }
        public string CTQSpecifications { get; set; }

        public ExpiryApplicable? ExpiryApplicable { get; set; }

        public decimal? MinimumOrderQuantity { get; set; }

        public string Author { get; set; }

        public string Publication { get; set; }

        public int? PublicationYear { get; set; }

        public SubjectCategory? SubjectCategory { get; set; }

        public long? PurchasedBy { get; set; }

        public decimal? WeightPerUOM { get; set; }

        public decimal? SellingPrice { get; set; }

        public List<CalibrationAgencyInputDto> ItemCalibrationAgencies { get; set; }

        public List<CalibrationTypeInputDto> ItemCalibrationTypes { get; set; }

        public List<ItemAttachmentInputDto> ItemAttachments { get; set; }

        public List<ItemStorageConditionInputDto> ItemStorageConditions { get; set; }

        public List<ItemSupplierInputDto> ItemSuppliers { get; set; }

        public List<ProcurementInputDto> ItemProcurements { get; set; }

        public List<ItemAccessoryInputDto> ItemAccessories { get; set; }

        public List<ItemSpareInputDto> ItemSpares { get; set; }

        public List<ItemRateRevisionInputDto> ItemRateRevisions { get; set; }

    }
}
