namespace MyTraining1101Demo.Purchase.Items.Dto.RequiredItemSparesMaster
{
    using System;
    public class ItemSpareDto
    {
        public Guid Id { get; set; }

        public Guid? ItemSparesId { get; set; }
        public Guid? ItemId { get; set; }
    }
}
