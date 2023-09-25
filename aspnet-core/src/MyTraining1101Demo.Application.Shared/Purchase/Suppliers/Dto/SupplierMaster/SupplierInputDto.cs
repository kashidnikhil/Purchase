namespace MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierMaster
{
    using MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierAddresses;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierBanks;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierContactPersons;
    using MyTraining1101Demo.Purchase.Suppliers.Enums;
    using System;
    using System.Collections.Generic;

    public class SupplierInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public string TelephoneNumber { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Website { get; set; }

        public int? YearOfEstablishment { get; set; }

        public string PanNumber { get; set; }

        public string VatNumber { get; set; }

        public string GSTNumber { get; set; }

        public string Certifications { get; set; }

        public int? DeliveryBy { get; set; }

        public int? PaymentMode { get; set; }

        public int? Category { get; set; }

        public string ServicesProvided { get; set; }

        public string CommentsOrObservations { get; set; }

        public string CommentsOrApprovalByCeo { get; set; }

        public string POComments { get; set; }

        public string LicenseDetails { get; set; }

        public SupplierStatus? Status { get; set; }

        public MSMEStatus? MSMEStatus { get; set; }

        public long? MSMENumber { get; set; }

        public string PAN { get; set; }

        public string StatusChangeReason { get; set; }

        public DateTime? StatusChangeDate { get; set; }

        public string ReferredBy { get; set; }

        public string PositiveObservation { get; set; }

        public string NegativeObservation { get; set; }

        public List<MappedSupplierCategoryInputDto> SupplierCategories { get; set; }
        public List<SupplierAddressInputDto> SupplierAddresses { get; set; }

        public List<SupplierContactPersonInputDto> SupplierContactPersons { get; set; }

        public List<SupplierBankInputDto> SupplierBanks { get; set; }

        public Guid? LegalEntityId { get; set; }


    }
}
