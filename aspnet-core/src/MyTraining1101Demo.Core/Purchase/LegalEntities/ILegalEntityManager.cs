using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyTraining1101Demo.Purchase.LegalEntities.Dto;

namespace MyTraining1101Demo.Purchase.LegalEntities
{
    public interface ILegalEntityManager : IDomainService
    {
        Task<PagedResultDto<LegalEntityDto>> GetPaginatedLegalEntitiesFromDB(LegalEntitySearchDto input);
        Task<ResponseDto> InsertOrUpdateLegalEntityIntoDB(LegalEntityInputDto input);

        Task<bool> DeleteLegalEntityFromDB(Guid LegalEntityId);

        Task<LegalEntityDto> GetLegalEntityByIdFromDB(Guid LegalEntityId);

        Task<IList<LegalEntityDto>> GetLegalEntityListFromDB();

        Task<bool> RestoreLegalEntity(Guid LegalEntityId);
    }
}
