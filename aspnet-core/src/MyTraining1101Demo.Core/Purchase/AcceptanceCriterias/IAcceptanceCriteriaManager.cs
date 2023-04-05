using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.AcceptanceCriterias
{
    public interface IAcceptanceCriteriaManager : IDomainService
    {
        Task<PagedResultDto<AcceptanceCriteriaDto>> GetPaginatedAcceptanceCriteriasFromDB(AcceptanceCriteriaSearchDto input);
        Task<ResponseDto> InsertOrUpdateAcceptanceCriteriaIntoDB(AcceptanceCriteriaInputDto input);

        Task<bool> DeleteAcceptanceCriteriaFromDB(Guid acceptanceCriteriaId);

        Task<AcceptanceCriteriaDto> GetAcceptanceCriteriaByIdFromDB(Guid acceptanceCriteriaId);

        Task<IList<AcceptanceCriteriaDto>> GetAcceptanceCriteriaListFromDB();

        Task<bool> RestoreAcceptanceCriteria(Guid acceptanceCriteriaId);
    }
}
