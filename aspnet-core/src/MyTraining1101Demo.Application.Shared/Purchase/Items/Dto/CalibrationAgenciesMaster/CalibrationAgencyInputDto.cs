namespace MyTraining1101Demo.Purchase.Items.Dto.CalibrationAgenciesMaster
{
    using System;
    public class CalibrationAgencyInputDto
    {
        public Guid? Id { get; set; }
        public Guid SupplierId { get; set; }

        public Guid? ItemId { get; set; }
    }
}
