using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.Models.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Models
{
    public interface IModelAppService
    {
        Task<PagedResultDto<ModelDto>> GetModels(ModelSearchDto input);
        Task<ResponseDto> InsertOrUpdateModel(ModelInputDto input);

        Task<bool> DeleteModel(Guid ModelId);

        Task<ModelDto> GetModelById(Guid ModelId);

        Task<IList<ModelDto>> GetModelList();

        Task<bool> RestoreModel(Guid ModelId);
    }
}
