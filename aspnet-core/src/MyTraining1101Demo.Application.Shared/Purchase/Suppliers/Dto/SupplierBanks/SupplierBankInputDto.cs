using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierBanks
{
    public class SupplierBankInputDto
    {
        public Guid? Id { get; set; }
        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string Address { get; set; }

        public long? AccountNumber { get; set; }

        public string MICRCode { get; set; }

        public string IFSCCode { get; set; }

        public string RTGS { get; set; }

        public string PaymentMode { get; set; }

        public Guid? SupplierId { get; set; }
    }
}
