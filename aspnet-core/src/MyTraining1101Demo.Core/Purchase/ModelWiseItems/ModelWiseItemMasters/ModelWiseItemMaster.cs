namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters
{
    using Abp.Domain.Entities.Auditing;
    using MyTraining1101Demo.Purchase.Models;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ModelWiseItemMaster")]
    public class ModelWiseItemMaster : FullAuditedEntity<Guid>
    {
        public virtual Guid? ModelId { get; set; }
        public virtual Model Model { get; set; }
    }
}
