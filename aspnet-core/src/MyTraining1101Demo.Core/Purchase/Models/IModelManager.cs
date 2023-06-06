namespace MyTraining1101Demo.Purchase.Models
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Models.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IModelManager : IDomainService
    {
        Task<PagedResultDto<ModelDto>> GetPaginatedModelsFromDB(ModelSearchDto input);
        Task<ResponseDto> InsertOrUpdateModelIntoDB(ModelInputDto input);

        Task<bool> DeleteModelFromDB(Guid modelId);

        Task<ModelDto> GetModelByIdFromDB(Guid modelId);

        Task<IList<ModelDto>> GetModelListFromDB();

        Task<bool> RestoreModel(Guid modelId);
    }
}
