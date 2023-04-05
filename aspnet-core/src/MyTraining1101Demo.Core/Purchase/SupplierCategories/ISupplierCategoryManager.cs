using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.SupplierCategories.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MyTraining1101Demo.Purchase.SupplierCategories
{
    public interface ISupplierCategoryManager : IDomainService
    {
        Task<PagedResultDto<SupplierCategoryDto>> GetPaginatedSupplierCategoriesFromDB(SupplierCategorySearchDto input);
        Task<ResponseDto> InsertOrUpdateSupplierCategoryIntoDB(SupplierCategoryInputDto input);

        Task<bool> DeleteSupplierCategoryFromDB(Guid supplierCategoryId);

        Task<SupplierCategoryDto> GetSupplierCategoryByIdFromDB(Guid supplierCategoryId);

        Task<IList<SupplierCategoryDto>> GetSupplierCategoryListFromDB();

        Task<bool> RestoreSupplierCategory(Guid supplierCategoryId);
    }
}
