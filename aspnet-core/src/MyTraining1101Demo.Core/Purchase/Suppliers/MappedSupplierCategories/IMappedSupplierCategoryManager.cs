namespace MyTraining1101Demo.Purchase.Suppliers.MappedSupplierCategories
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.SupplierCategories.Dto;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMappedSupplierCategoryManager: IDomainService
    {
        Task<Guid> BulkInsertOrUpdateMappedSupplierCategories(List<MappedSupplierCategoryInputDto> supplierCategoriesInputList);

        Task<bool> BulkDeleteSupplierCategories(Guid supplierId);

        Task<IList<SupplierCategoryDto>> GetSupplierCategoryListFromDB(Guid supplierId);
    }
}
