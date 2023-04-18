using Abp.Domain.Entities.Auditing;
namespace MyTraining1101Demo.Purchase.Suppliers.SupplierMaster
{
    using MyTraining1101Demo.Purchase.LegalEntities;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Suppliers")]
    public class Supplier : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }

        public string TelephoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Website { get; set; }

        public int? YearOfEstablishment { get; set; }

        public string PanNumber { get; set; }

        public string VatNumber { get; set; }

        public string GSTNumber { get; set; }

        public string Certifications { get; set; }

        public DeliveryType? DeliveryBy { get; set; }

        public PaymentModeType? PaymentMode { get; set; }

        public CategoryType? Category { get; set; }

        public virtual Guid? LegalEntityId { get; set; }
        public virtual LegalEntity LegalEntity { get; set; }

    }

    public enum DeliveryType
    {
        Supplier = 1,

    }

    public enum PaymentModeType
    {
        Advance = 1,

    }

    public enum CategoryType
    {
        Manufacturer = 1,
        Trader,
        ServiceProvider

    }
}
