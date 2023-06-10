namespace MyTraining1101Demo.Purchase.Items
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IItemAppService
    {
        Task<PagedResultDto<ItemMasterListDto>> GetItems(ItemMasterSearchDto input);

        Task<ResponseDto> InsertOrUpdateItem(ItemMasterInputDto input);

        Task<bool> DeleteItemMasterData(Guid itemId);

        Task<ItemMasterDto> GetItemMasterById(Guid itemId);

        Task<IList<ItemMasterListDto>> GetItemMasterList();

        Task<IList<ItemListDto>> GetItemMasterListForSubAssemblyPageDropdown();
    }
}
