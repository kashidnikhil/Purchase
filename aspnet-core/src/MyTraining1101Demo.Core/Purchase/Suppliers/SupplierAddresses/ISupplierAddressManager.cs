namespace MyTraining1101Demo.Purchase.Suppliers.SupplierAddresses
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierAddresses;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISupplierAddressManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateSupplierAddresses(List<SupplierAddressInputDto> supplierAddressInputList);

        Task<bool> BulkDeleteSupplierAddresses(Guid supplierId);

        Task<IList<SupplierAddressDto>> GetSupplierAddressListFromDB(Guid supplierId);
    }
}
