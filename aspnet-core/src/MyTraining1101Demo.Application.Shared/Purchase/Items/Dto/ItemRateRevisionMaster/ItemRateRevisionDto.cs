namespace MyTraining1101Demo.Purchase.Items.Dto.ItemRateRevisionMaster
{
    using System;

    public class ItemRateRevisionDto
    {
        public Guid Id { get; set; }
        public DateTime? DateOfEntry { get; set; }
        public string Make { get; set; }

        public string CatalogueNumber { get; set; }

        public decimal? OrderingQuantity { get; set; }

        public decimal? RatePerOrderingQuantity { get; set; }

        public decimal? StockQuantityPerOrderingUOM { get; set; }

        public decimal? RatePerStockUOM { get; set; }

        public Guid? StockUOMId { get; set; }
        
        public Guid? OrderingUOMId { get; set; }

        public Guid? ItemId { get; set; }
    }
}
