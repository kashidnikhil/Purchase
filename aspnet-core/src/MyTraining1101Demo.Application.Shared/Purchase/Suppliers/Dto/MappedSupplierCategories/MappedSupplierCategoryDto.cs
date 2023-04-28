using System;

namespace MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories
{
    public class MappedSupplierCategoryDto
    {
        public Guid Id { get; set; }
        public Guid SupplierCategoryId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid SupplierId { get; set; }
    }
}
