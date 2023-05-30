namespace MyTraining1101Demo.Purchase.Items.Dto.ItemAccessoriesMaster
{
    using System;
    public class ItemAccessoryDto
    {
        public Guid Id { get; set; }

        public Guid? AccessoryId { get; set; }
        public Guid? ItemId { get; set; }
    }
}
