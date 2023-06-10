namespace MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierAddresses
{
    using System;
    public class SupplierAddressDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }

        public string AddressType { get; set; }

        public Guid SupplierId { get; set; }
    }
}
