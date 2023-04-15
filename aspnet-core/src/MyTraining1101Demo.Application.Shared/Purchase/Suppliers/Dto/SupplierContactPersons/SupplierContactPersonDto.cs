using System;

namespace MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierContactPersons
{
    public class SupplierContactPersonDto
    {
        public Guid Id { get; set; }
        public string ContactPersonName { get; set; }

        public string Designation { get; set; }

        public string EmailId { get; set; }

        public string MobileNumber { get; set; }

        public Guid SupplierId { get; set; }

    }
}
