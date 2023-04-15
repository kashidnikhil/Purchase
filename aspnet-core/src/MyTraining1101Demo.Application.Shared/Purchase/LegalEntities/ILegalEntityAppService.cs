using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.LegalEntities.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.LegalEntities
{
    public interface ILegalEntityAppService
    {
        Task<PagedResultDto<LegalEntityDto>> GetLegalEntities(LegalEntitySearchDto input);
        Task<ResponseDto> InsertOrUpdateLegalEntity(LegalEntityInputDto input);

        Task<bool> DeleteLegalEntity(Guid LegalEntityId);

        Task<LegalEntityDto> GetLegalEntityById(Guid LegalEntityId);

        Task<IList<LegalEntityDto>> GetLegalEntityList();

        Task<bool> RestoreLegalEntity(Guid legalEntityId);
    }
}
