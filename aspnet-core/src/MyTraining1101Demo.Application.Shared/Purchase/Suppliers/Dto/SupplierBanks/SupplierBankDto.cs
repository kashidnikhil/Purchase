using System;

namespace MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierBanks
{
    public class SupplierBankDto
    {
        public Guid Id { get; set; } 
        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string Address { get; set; }

        public long? AccountNumber { get; set; }

        public string MICRCode { get; set; }

        public string IFSCCode { get; set; }

        public string RTGS { get; set; }

        public int? PaymentMode { get; set; }

        public Guid SupplierId { get; set; }
    }
}
