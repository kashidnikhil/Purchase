namespace MyTraining1101Demo.Purchase.DeliveryTerms
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.DeliveryTerms.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class DeliveryTermAppService : MyTraining1101DemoAppServiceBase, IDeliveryTermAppService
    {
        private readonly IDeliveryTermManager _deliveryTermManager;

        public DeliveryTermAppService(
          IDeliveryTermManager deliveryTermManager
         )
        {
            _deliveryTermManager = deliveryTermManager;
        }


        public async Task<PagedResultDto<DeliveryTermDto>> GetDeliveryTerms(DeliveryTermSearchDto input)
        {
            try
            {
                var result = await this._deliveryTermManager.GetPaginatedDeliveryTermsFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateDeliveryTerm(DeliveryTermInputDto input)
        {
            try
            {
                var insertedOrUpdatedDeliveryTermId = await this._deliveryTermManager.InsertOrUpdateDeliveryTermIntoDB(input);

                return insertedOrUpdatedDeliveryTermId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteDeliveryTerm(Guid deliveryTermId)
        {
            try
            {
                var isDeliveryTermDeleted = await this._deliveryTermManager.DeleteDeliveryTermFromDB(deliveryTermId);
                return isDeliveryTermDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<DeliveryTermDto> GetDeliveryTermById(Guid deliveryTermId)
        {
            try
            {
                var response = await this._deliveryTermManager.GetDeliveryTermByIdFromDB(deliveryTermId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<DeliveryTermDto>> GetDeliveryTermList()
        {
            try
            {
                var response = await this._deliveryTermManager.GetDeliveryTermListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreDeliveryTerm(Guid deliveryTermId)
        {
            try
            {
                var response = await this._deliveryTermManager.RestoreDeliveryTerm(deliveryTermId);
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
