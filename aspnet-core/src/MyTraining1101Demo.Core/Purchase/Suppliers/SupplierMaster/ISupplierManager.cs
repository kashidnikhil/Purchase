using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierMaster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Suppliers.SupplierMaster
{
    public interface ISupplierManager : IDomainService
    {
        Task<PagedResultDto<SupplierListDto>> GetPaginatedSupplierListListFromDB(SupplierMasterSearchDto input);

        Task<Guid> InsertOrUpdateSupplierMasterIntoDB(SupplierInputDto input);

        Task<bool> DeleteSupplierMasterFromDB(Guid supplierId);

        Task<SupplierDto> GetSupplierMasterByIdFromDB(Guid supplierId);

        Task<IList<SupplierDto>> GetSupplierListFromDB();
    }
}
