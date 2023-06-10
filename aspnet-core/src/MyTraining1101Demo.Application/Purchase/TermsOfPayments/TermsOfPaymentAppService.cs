namespace MyTraining1101Demo.Purchase.TermsOfPayments
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.TermsOfPayments.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class TermsOfPaymentAppService : MyTraining1101DemoAppServiceBase, ITermsOfPaymentAppService
    {
        private readonly ITermsOfPaymentManager _termsOfPaymentManager;

        public TermsOfPaymentAppService(
          ITermsOfPaymentManager termsOfPaymentManager
         )
        {
            _termsOfPaymentManager = termsOfPaymentManager;
        }


        public async Task<PagedResultDto<TermsOfPaymentDto>> GetTermsOfPayments(TermsOfPaymentSearchDto input)
        {
            try
            {
                var result = await this._termsOfPaymentManager.GetPaginatedTermsOfPaymentsFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateTermsOfPayment(TermsOfPaymentInputDto input)
        {
            try
            {
                var insertedOrUpdatedTermsOfPaymentId = await this._termsOfPaymentManager.InsertOrUpdateTermsOfPaymentIntoDB(input);

                return insertedOrUpdatedTermsOfPaymentId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteTermsOfPayment(Guid termsOfPaymentId)
        {
            try
            {
                var isTermsOfPaymentDeleted = await this._termsOfPaymentManager.DeleteTermsOfPaymentFromDB(termsOfPaymentId);
                return isTermsOfPaymentDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<TermsOfPaymentDto> GetTermsOfPaymentById(Guid termsOfPaymentId)
        {
            try
            {
                var response = await this._termsOfPaymentManager.GetTermsOfPaymentByIdFromDB(termsOfPaymentId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<TermsOfPaymentDto>> GetTermsOfPaymentList()
        {
            try
            {
                var response = await this._termsOfPaymentManager.GetTermsOfPaymentListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreTermsOfPayment(Guid termsOfPaymentId)
        {
            try
            {
                var response = await this._termsOfPaymentManager.RestoreTermsOfPayment(termsOfPaymentId);
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
