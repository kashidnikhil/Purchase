using Abp.Domain.Entities.Auditing;
using MyTraining1101Demo.Purchase.SupplierCategories;
using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Suppliers.MappedSupplierCategories
{
    [Table("MappedSupplierCategories")]
    public class MappedSupplierCategory : FullAuditedEntity<Guid>
    {
        public virtual Guid? SupplierCategoryId { get; set; }
        public virtual SupplierCategory SupplierCategory { get; set; }

        public virtual Guid? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
