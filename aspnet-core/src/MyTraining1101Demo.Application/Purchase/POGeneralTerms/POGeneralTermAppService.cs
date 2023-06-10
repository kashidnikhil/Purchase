namespace MyTraining1101Demo.Purchase.POGeneralTerms
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.POGeneralTerms.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class POGeneralTermAppService : MyTraining1101DemoAppServiceBase, IPOGeneralTermAppService
    {
        private readonly IPOGeneralTermManager _poGeneralTermManager;

        public POGeneralTermAppService(
          IPOGeneralTermManager poGeneralTermManager
         )
        {
            _poGeneralTermManager = poGeneralTermManager;
        }


        public async Task<PagedResultDto<POGeneralTermDto>> GetPOGeneralTerms(POGeneralTermSearchDto input)
        {
            try
            {
                var result = await this._poGeneralTermManager.GetPaginatedPOGeneralTermsFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdatePOGeneralTerm(POGeneralTermInputDto input)
        {
            try
            {
                var insertedOrUpdatedPOGeneralTermId = await this._poGeneralTermManager.InsertOrUpdatePOGeneralTermIntoDB(input);

                return insertedOrUpdatedPOGeneralTermId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeletePOGeneralTerm(Guid poGeneralTermId)
        {
            try
            {
                var isPOGeneralTermDeleted = await this._poGeneralTermManager.DeletePOGeneralTermFromDB(poGeneralTermId);
                return isPOGeneralTermDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<POGeneralTermDto> GetPOGeneralTermById(Guid poGeneralTermId)
        {
            try
            {
                var response = await this._poGeneralTermManager.GetPOGeneralTermByIdFromDB(poGeneralTermId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<POGeneralTermDto>> GetPOGeneralTermList()
        {
            try
            {
                var response = await this._poGeneralTermManager.GetPOGeneralTermListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestorePOGeneralTerm(Guid poGeneralTermId)
        {
            try
            {
                var response = await this._poGeneralTermManager.RestorePOGeneralTerm(poGeneralTermId);
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
