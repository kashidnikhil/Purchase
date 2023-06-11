namespace MyTraining1101Demo.Purchase.ItemCategories.Dto
{
    using System;
    public class ItemCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public long ItemCategoryCode { get; set; }

        public string Description { get; set; }
    }
}
