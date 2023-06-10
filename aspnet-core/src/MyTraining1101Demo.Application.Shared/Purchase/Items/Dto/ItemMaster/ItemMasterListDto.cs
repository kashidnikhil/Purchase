namespace MyTraining1101Demo.Purchase.Items.Dto.ItemMaster
{
    using System;
    public class ItemMasterListDto
    {
        public Guid Id { get; set; }

        public int CategoryId { get; set; }

        public int ItemId { get; set; }

        public string GenericName { get; set; }

        public string ItemName { get; set; }

        public string Make { get; set; }

        public string UnitName { get; set; }
    }
}
