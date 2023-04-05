namespace MyTraining1101Demo.Purchase.SupplierCategories
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.SupplierCategories.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISupplierCategoryAppService
    {
        Task<PagedResultDto<SupplierCategoryDto>> GetSupplierCategories(SupplierCategorySearchDto input);
        Task<ResponseDto> InsertOrUpdateSupplierCategory(SupplierCategoryInputDto input);

        Task<bool> DeleteSupplierCategory(Guid supplierCategoryId);

        Task<SupplierCategoryDto> GetSupplierCategoryById(Guid supplierCategoryId);

        Task<IList<SupplierCategoryDto>> GetSupplierCategoryList();

        Task<bool> RestoreSupplierCategory(Guid supplierCategoryId);
    }
}
