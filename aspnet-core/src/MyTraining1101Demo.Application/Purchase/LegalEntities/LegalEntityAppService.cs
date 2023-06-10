namespace MyTraining1101Demo.Purchase.LegalEntities
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.LegalEntities.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class LegalEntityAppService : MyTraining1101DemoAppServiceBase, ILegalEntityAppService
    {
        private readonly ILegalEntityManager _LegalEntityManager;

        public LegalEntityAppService(
          ILegalEntityManager LegalEntityManager
         )
        {
            _LegalEntityManager = LegalEntityManager;
        }


        public async Task<PagedResultDto<LegalEntityDto>> GetLegalEntities(LegalEntitySearchDto input)
        {
            try
            {
                var result = await this._LegalEntityManager.GetPaginatedLegalEntitiesFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateLegalEntity(LegalEntityInputDto input)
        {
            try
            {
                var insertedOrUpdatedLegalEntityId = await this._LegalEntityManager.InsertOrUpdateLegalEntityIntoDB(input);

                return insertedOrUpdatedLegalEntityId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteLegalEntity(Guid LegalEntityId)
        {
            try
            {
                var isLegalEntityDeleted = await this._LegalEntityManager.DeleteLegalEntityFromDB(LegalEntityId);
                return isLegalEntityDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<LegalEntityDto> GetLegalEntityById(Guid LegalEntityId)
        {
            try
            {
                var response = await this._LegalEntityManager.GetLegalEntityByIdFromDB(LegalEntityId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<LegalEntityDto>> GetLegalEntityList()
        {
            try
            {
                var response = await this._LegalEntityManager.GetLegalEntityListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreLegalEntity(Guid legalEntityId)
        {
            try
            {
                var response = await this._LegalEntityManager.RestoreLegalEntity(legalEntityId);
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
