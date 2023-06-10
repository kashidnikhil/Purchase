namespace MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories
{
    using System;
    public class MappedSupplierCategoryInputDto
    {
        public Guid? Id { get; set; }
        public Guid SupplierCategoryId { get; set; }

        public Guid SupplierId { get; set; }
    }
}
