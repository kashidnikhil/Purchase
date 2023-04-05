using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.AcceptanceCriterias
{
    public interface IAcceptanceCriteriaAppService
    {
        Task<PagedResultDto<AcceptanceCriteriaDto>> GetAcceptanceCriterias(AcceptanceCriteriaSearchDto input);
        Task<ResponseDto> InsertOrUpdateAcceptanceCriteria(AcceptanceCriteriaInputDto input);

        Task<bool> DeleteAcceptanceCriteria(Guid acceptanceCriteriaId);

        Task<AcceptanceCriteriaDto> GetAcceptanceCriteriaById(Guid acceptanceCriteriaId);

        Task<IList<AcceptanceCriteriaDto>> GetAcceptanceCriteriaList();

        Task<bool> RestoreAcceptanceCriteria(Guid acceptanceCriteriaId);
    }
}
