﻿using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.Models.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Models
{
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