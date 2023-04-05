using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.TermsOfPayments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.TermsOfPayments
{
    public interface ITermsOfPaymentManager : IDomainService
    {
        Task<PagedResultDto<TermsOfPaymentDto>> GetPaginatedTermsOfPaymentsFromDB(TermsOfPaymentSearchDto input);
        Task<ResponseDto> InsertOrUpdateTermsOfPaymentIntoDB(TermsOfPaymentInputDto input);

        Task<bool> DeleteTermsOfPaymentFromDB(Guid termsOfPaymentId);

        Task<TermsOfPaymentDto> GetTermsOfPaymentByIdFromDB(Guid termsOfPaymentId);

        Task<IList<TermsOfPaymentDto>> GetTermsOfPaymentListFromDB();

        Task<bool> RestoreTermsOfPayment(Guid termsOfPaymentId);
    }
}
