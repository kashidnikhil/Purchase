using System;

namespace MyTraining1101Demo.Purchase.ItemCategories.Dto
{
    public class ItemCategoryInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public long ItemCategoryCode { get; set; }

        public string Description { get; set; }
    }
}
