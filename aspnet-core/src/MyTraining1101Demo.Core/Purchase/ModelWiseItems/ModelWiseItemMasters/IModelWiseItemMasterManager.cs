namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Threading.Tasks;
    public interface IModelWiseItemMasterManager: IDomainService
    {
        Task<PagedResultDto<ModelWiseItemMasterListDto>> GetPaginatedModelWiseItemMastersFromDB(ModelWiseItemMasterSearchDto input);

        Task<ResponseDto> InsertOrUpdateModelWiseItemMasterIntoDB(ModelWiseItemMasterInputDto input);

        Task<bool> DeleteModelWiseItemMasterFromDB(Guid modelWiseItemMasterId);

        Task<ModelWiseItemMasterDto> GetModelWiseItemMasterByIdFromDB(Guid modelWiseItemMasterId);
    }
}
