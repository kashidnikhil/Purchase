namespace MyTraining1101Demo.Purchase.ModelWiseItems
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters.Dto;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IModelWiseItemAppService
    {
        Task<PagedResultDto<ModelWiseItemMasterListDto>> GetModelWiseItems(ModelWiseItemMasterSearchDto input);

        Task<ResponseDto> InsertOrUpdateModelWiseItem(ModelWiseItemMasterInputDto input);

        Task<bool> DeleteModelWiseItemMasterData(Guid modelWiseItemMasterId);

        Task<bool> DeleteModelWiseItemData(Guid modelWiseItemId);

        Task<ModelWiseItemMasterDto> GetModelWiseItemMasterById(Guid modelWiseItemMasterId);
        Task<IList<ModelWiseItemDto>> GetModelWiseItemList(Guid modelId);

    }
}
