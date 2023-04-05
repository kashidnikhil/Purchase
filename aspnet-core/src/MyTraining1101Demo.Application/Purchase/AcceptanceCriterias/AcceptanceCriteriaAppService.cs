using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.AcceptanceCriterias
{
    public class AcceptanceCriteriaAppService : MyTraining1101DemoAppServiceBase, IAcceptanceCriteriaAppService
    {
        private readonly IAcceptanceCriteriaManager _acceptanceCriteriaManager;

        public AcceptanceCriteriaAppService(
          IAcceptanceCriteriaManager acceptanceCriteriaManager
         )
        {
            _acceptanceCriteriaManager = acceptanceCriteriaManager;
        }


        public async Task<PagedResultDto<AcceptanceCriteriaDto>> GetAcceptanceCriterias(AcceptanceCriteriaSearchDto input)
        {
            try
            {
                var result = await this._acceptanceCriteriaManager.GetPaginatedAcceptanceCriteriasFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateAcceptanceCriteria(AcceptanceCriteriaInputDto input)
        {
            try
            {
                var insertedOrUpdatedAcceptanceCriteriaId = await this._acceptanceCriteriaManager.InsertOrUpdateAcceptanceCriteriaIntoDB(input);

                return insertedOrUpdatedAcceptanceCriteriaId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteAcceptanceCriteria(Guid acceptanceCriteriaId)
        {
            try
            {
                var isAcceptanceCriteriaDeleted = await this._acceptanceCriteriaManager.DeleteAcceptanceCriteriaFromDB(acceptanceCriteriaId);
                return isAcceptanceCriteriaDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<AcceptanceCriteriaDto> GetAcceptanceCriteriaById(Guid acceptanceCriteriaId)
        {
            try
            {
                var response = await this._acceptanceCriteriaManager.GetAcceptanceCriteriaByIdFromDB(acceptanceCriteriaId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<AcceptanceCriteriaDto>> GetAcceptanceCriteriaList()
        {
            try
            {
                var response = await this._acceptanceCriteriaManager.GetAcceptanceCriteriaListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreAcceptanceCriteria(Guid acceptanceCriteriaId)
        {
            try
            {
                var response = await this._acceptanceCriteriaManager.RestoreAcceptanceCriteria(acceptanceCriteriaId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
