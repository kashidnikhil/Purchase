namespace MyTraining1101Demo.Purchase.Items.ItemMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using System.Threading.Tasks;
    using System;
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Items.Enums;
    using System.Collections.Generic;
    using MyTraining1101Demo.Purchase.Shared;

    public interface IItemManager : IDomainService
    {
        Task<PagedResultDto<ItemMasterListDto>> GetPaginatedItemListFromDB(ItemMasterSearchDto input);
        Task<ResponseDto> InsertOrUpdateItemMasterIntoDB(ItemMasterInputDto input);
        Task<bool> DeleteItemMasterFromDB(Guid itemId);

        Task<ItemMasterDto> GetItemMasterByIdFromDB(Guid itemId);

        Task<ItemMasterDto?> GetItemMasterByNameFromDB(Guid itemCategory, string itemName);

        Task<ItemMasterDto?> FindItemMasterByCategoryIdFromDB(Guid itemCategory);

        Task<IList<ItemMasterListDto>> GetItemListByItemCategoryFromDB(Guid itemCategoryId);

        Task<IList<ItemMasterListDto>> GetItemMasterListFromDB();

        Task<IList<ItemListDto>> GetItemListForSubAssemblyPageDropdownFromDB();

        Task<ResponseDto> ForceInsertOrUpdateItemMasterIntoDB(ItemMasterInputDto input);
    }
}
