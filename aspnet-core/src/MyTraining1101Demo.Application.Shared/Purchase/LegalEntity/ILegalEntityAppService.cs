using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.LegalEntity.Dto;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.Unit.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.LegalEntity
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
