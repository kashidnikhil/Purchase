namespace MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Items.Enums;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CalibrationType")]
    public class CalibrationType : FullAuditedEntity<Guid>
    {
        public CalibrationTypeEnum Type { get; set; }

        public CalibrationFrequency Frequency { get; set; }

        public virtual Guid? ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
