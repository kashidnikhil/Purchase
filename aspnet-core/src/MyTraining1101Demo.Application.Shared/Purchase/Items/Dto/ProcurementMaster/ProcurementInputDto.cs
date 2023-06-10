namespace MyTraining1101Demo.Purchase.Items.Dto.ProcurementMaster
{
    using System;
    public class ProcurementInputDto
    {
        public Guid? Id { get; set; }
        public string Make { get; set; }
        public string CatalogueNumber { get; set; }

        public double? RatePerPack { get; set; }

        public double? QuantityPerOrderingUOM { get; set; }

        public double? RatePerStockUOM { get; set; }

        public Guid? ItemId { get; set; }
    }
}
