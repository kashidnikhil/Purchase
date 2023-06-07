using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.SubAssemblyItems.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    public interface ISubAssemblyItemAppService
    {
        Task<PagedResultDto<SubAssemblyItemListDto>> GetSubAssemblyItems(SubAssemblyItemSearchDto input);
        Task<ResponseDto> InsertOrUpdateSubAssemblyItem(SubAssemblyItemInputDto input);

        Task<bool> DeleteSubAssemblyItem(Guid subAssemblyItemId);

        Task<SubAssemblyItemDto> GetSubAssemblyItemById(Guid subAssemblyItemId);

        Task<IList<SubAssemblyItemDto>> GetSubAssemblyItemList();

        Task<bool> RestoreSubAssemblyItem(Guid subAssemblyItemId);

    }
}
