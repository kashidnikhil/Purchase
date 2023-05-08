using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using MyTraining1101Demo.Purchase.Items.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.Items.ItemMaster
{
    [Table("Items")]
    public class Item : FullAuditedEntity<Guid>
    {
        public ItemCategory ItemCategory { get; set; }
        public string GenericName { get; set; }

        public string ItemName { get; set; }

        public ItemType? ItemType { get; set; }

        public ItemAMCRequirement? AMCRequired {get;set;}

        public CalibrationRequirement? CalibrationRequirement { get; set; }
        
        public string Alias { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Specifications { get; set; }

        public string StorageConditions { get; set; }

        public DateTime? PurchaseDate { get; set; }
        public long? PurchaseValue { get; set; }

        [Precision(18, 2)]
        public decimal GST { get; set; }

        public string HSNCode { get; set; }

        public double OrderingRate { get; set; }

        public double RatePerQuantity { get; set; }

        public DateTime? DateOfRate { get; set; }

        public long? Quantity { get; set; }

        public DateTime? LeadTime { get; set; }

        public ItemStatus? Status { get; set; }
        
        public string Attachments { get; set; }

        public long RecordedBy { get; set; }

        public long ApprovedBy { get; set; }

        public DateTime? DiscardedOn { get; set; }

        public long DiscardApprovedBy { get; set; }

        public string DiscardedReason { get; set; }

        public long? ClassId { get; set; }

        public string StorageCondition { get; set; }

        public string Comment { get; set; }

        public string MSL { get; set; }

        public CTQRequirement? CTQRequirement { get; set; }
        public string CTQSpecifications { get; set; }

        public ExpiryApplicable? ExpiryApplicable { get; set; }
        public int? MinimumOrderQuantity { get; set; }

        public string Author { get; set; }

        public string Publication { get; set; }

        public int PublicationYear { get; set; }

        public SubjectCategory? SubjectCategory { get; set; }

        public long? PurchasedBy { get; set; }

    }
}
