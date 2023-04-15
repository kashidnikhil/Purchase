using System;

namespace MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierMaster
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string TelephoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Website { get; set; }

        public int YearOfEstablishment { get; set; }

        public string PanNumber { get; set; }

        public string VatNumber { get; set; }

        public string GSTNumber { get; set; }

        public string Certifications { get; set; }

        public int DeliveryBy { get; set; }

        public int PaymentMode { get; set; }

        public int Category { get; set; }

        public Guid LegalEntityId { get; set; }
    }
}
