namespace MyTraining1101Demo.Purchase.ItemCategories
{
    using Abp.Domain.Entities.Auditing;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ItemCategories")]
    public class ItemCategory: FullAuditedEntity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public long ItemCategoryCode { get; set; } 
    }
}
