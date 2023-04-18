using System;

namespace MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierAddresses
{
    public class SupplierAddressInputDto
    {
        public Guid? Id { get; set; }
        public string Address { get; set; }

        public string AddressType { get; set; }

        public Guid SupplierId { get; set; }
    }
}
