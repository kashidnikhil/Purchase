using System;

namespace MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories
{
    public class MappedSupplierCategoryDto
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
