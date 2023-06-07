namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.SubAssemblyItems.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubAssemblyItemManager: IDomainService
    {
        Task<PagedResultDto<SubAssemblyItemListDto>> GetPaginatedSubAssemblyItemsFromDB(SubAssemblyItemSearchDto input);

        Task<ResponseDto> InsertOrUpdateSubAssemblyItemIntoDB(SubAssemblyItemInputDto input);

        Task<bool> DeleteSubAssemblyItemFromDB(Guid subAssemblyItemId);

        Task<SubAssemblyItemDto> GetSubAssemblyItemByIdFromDB(Guid subAssemblyItemId);

        Task<IList<SubAssemblyItemDto>> GetSubAssemblyItemListFromDB();

        Task<bool> RestoreSubAssemblyItem(Guid subAssemblyItemId);
    }
}
