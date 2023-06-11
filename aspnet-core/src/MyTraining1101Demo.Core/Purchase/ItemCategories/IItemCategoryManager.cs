namespace MyTraining1101Demo.Purchase.ItemCategories
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.ItemCategories.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IItemCategoryManager: IDomainService
    {
        Task<PagedResultDto<ItemCategoryDto>> GetPaginatedItemCategoriesFromDB(ItemCategorySearchDto input);

        Task<ResponseDto> InsertOrUpdateItemCategoryIntoDB(ItemCategoryInputDto input);

        Task<bool> DeleteItemCategoryFromDB(Guid itemCategoryId);

        Task<ItemCategoryDto> GetItemCategoryByIdFromDB(Guid itemCategoryId);

        Task<List<ItemCategoryDto>> GetItemCategoryListFromDB();

        Task<ItemCategoryDto?> FindRecentlyAddedItemCategoryFromDB();

        Task<bool> RestoreItemCategory(Guid itemCategoryId);
    }
}
