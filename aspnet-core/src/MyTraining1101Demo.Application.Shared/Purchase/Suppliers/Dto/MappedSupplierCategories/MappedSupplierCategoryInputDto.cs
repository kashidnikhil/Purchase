using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories
{
    public class MappedSupplierCategoryInputDto
    {
        public Guid? Id { get; set; }
        public Guid SupplierCategoryId { get; set; }

        public Guid SupplierId { get; set; }
    }
}
