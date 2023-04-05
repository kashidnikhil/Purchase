using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTraining1101Demo.Purchase.SupplierCategories
{
    [Table("SupplierCategory")]
    public class SupplierCategory : FullAuditedEntity<Guid>
    {

        [Required(ErrorMessage = "Enter Supplier Category Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
