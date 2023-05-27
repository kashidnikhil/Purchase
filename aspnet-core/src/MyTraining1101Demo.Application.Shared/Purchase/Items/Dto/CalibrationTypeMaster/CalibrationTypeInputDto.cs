namespace MyTraining1101Demo.Purchase.Items.Dto.CalibrationTypeMaster
{
    using MyTraining1101Demo.Purchase.Items.Enums;
    using System;

    public class CalibrationTypeInputDto
    {
        public Guid? Id { get; set; }
        public CalibrationTypeEnum? Type { get; set; }

        public CalibrationFrequency? Frequency { get; set; }

        public Guid? ItemId { get; set; }
    }
}
