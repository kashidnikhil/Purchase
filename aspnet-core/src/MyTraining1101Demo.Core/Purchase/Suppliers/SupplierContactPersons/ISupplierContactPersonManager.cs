using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierContactPersons;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MyTraining1101Demo.Purchase.Suppliers.SupplierContactPersons
{
    public interface ISupplierContactPersonManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateSupplierContactPersons(List<SupplierContactPersonInputDto> supplierContactPersonInputList);

        Task<bool> BulkDeleteSupplierContactPersons(Guid supplierId);

        Task<IList<SupplierContactPersonDto>> GetSupplierContactPersonListFromDB(Guid supplierId);
    }
}
