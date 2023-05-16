namespace MyTraining1101Demo.Purchase.Items.ItemMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using System.Threading.Tasks;
    using System;
    using Abp.Application.Services.Dto;

    public interface IItemManager : IDomainService
    {
        Task<PagedResultDto<ItemMasterListDto>> GetPaginatedItemListFromDB(ItemMasterSearchDto input);
        Task<Guid> InsertOrUpdateItemMasterIntoDB(ItemMasterInputDto input);
        Task<bool> DeleteItemMasterFromDB(Guid itemId);

        Task<ItemMasterDto> GetItemMasterByIdFromDB(Guid itemId);
    }
}
