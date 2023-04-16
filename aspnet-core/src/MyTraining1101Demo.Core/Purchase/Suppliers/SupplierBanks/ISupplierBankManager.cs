using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierBanks;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MyTraining1101Demo.Purchase.Suppliers.SupplierBanks
{
    public interface ISupplierBankManager: IDomainService
    {
        Task<Guid> BulkInsertOrUpdateSupplierBanks(List<SupplierBankInputDto> supplierBankInputList);

        Task<bool> BulkDeleteSupplierBanks(Guid supplierId);

        Task<IList<SupplierBankDto>> GetSupplierBankListFromDB(Guid supplierId);
    }
}
