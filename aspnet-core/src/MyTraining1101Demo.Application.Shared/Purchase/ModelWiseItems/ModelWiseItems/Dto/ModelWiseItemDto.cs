namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems.Dto
{
    using System;
    public class ModelWiseItemDto
    {
        public Guid Id { get; set; }

        public string Comments { get; set; }

        public string ModelName { get; set; }

        public string ItemName { get; set; }

        public Guid? ModelId { get; set; }

        public Guid? ItemId { get; set; }

        public Guid? ModelWiseItemMasterId { get; set; }
    }
}
