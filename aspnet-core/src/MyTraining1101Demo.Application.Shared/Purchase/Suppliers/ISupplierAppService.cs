namespace MyTraining1101Demo.Purchase.Suppliers
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierMaster;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ISupplierAppService
    {
        Task<PagedResultDto<SupplierListDto>> GetSuppliers(SupplierMasterSearchDto input);

        Task<Guid> InsertOrUpdateSupplier(SupplierInputDto input);

        Task<bool> DeleteSupplierMasterData(Guid supplierId);
        Task<SupplierDto> GetSupplierMasterById(Guid supplierId);

        Task<IList<SupplierDto>> GetSupplierList();
    }
}
