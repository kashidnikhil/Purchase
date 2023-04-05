namespace MyTraining1101Demo.Purchase.TermsOfPayments
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.TermsOfPayments.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITermsOfPaymentAppService
    {
        Task<PagedResultDto<TermsOfPaymentDto>> GetTermsOfPayments(TermsOfPaymentSearchDto input);
        Task<ResponseDto> InsertOrUpdateTermsOfPayment(TermsOfPaymentInputDto input);

        Task<bool> DeleteTermsOfPayment(Guid termsOfPaymentId);

        Task<TermsOfPaymentDto> GetTermsOfPaymentById(Guid termsOfPaymentId);

        Task<IList<TermsOfPaymentDto>> GetTermsOfPaymentList();

        Task<bool> RestoreTermsOfPayment(Guid termsOfPaymentId);
    }
}
