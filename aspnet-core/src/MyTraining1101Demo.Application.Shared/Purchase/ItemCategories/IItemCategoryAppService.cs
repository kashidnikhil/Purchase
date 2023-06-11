namespace MyTraining1101Demo.Purchase.ItemCategories
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.ItemCategories.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IItemCategoryAppService
    {
        Task<PagedResultDto<ItemCategoryDto>> GetItemCategories(ItemCategorySearchDto input);

        Task<ResponseDto> InsertOrUpdateItemCategory(ItemCategoryInputDto input);

        Task<bool> DeleteItemCategory(Guid itemCategoryId);

        Task<ItemCategoryDto> GetItemCategoryById(Guid itemCategoryId);

        Task<IList<ItemCategoryDto>> GetItemCategoryList();

        Task<bool> RestoreItemCategory(Guid itemCategoryId);
    }
}
