using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.POGeneralTerms.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.POGeneralTerms
{
    public interface IPOGeneralTermManager : IDomainService
    {
        Task<PagedResultDto<POGeneralTermDto>> GetPaginatedPOGeneralTermsFromDB(POGeneralTermSearchDto input);
        Task<ResponseDto> InsertOrUpdatePOGeneralTermIntoDB(POGeneralTermInputDto input);

        Task<bool> DeletePOGeneralTermFromDB(Guid poGeneralTermId);

        Task<POGeneralTermDto> GetPOGeneralTermByIdFromDB(Guid poGeneralTermId);

        Task<IList<POGeneralTermDto>> GetPOGeneralTermListFromDB();

        Task<bool> RestorePOGeneralTerm(Guid poGeneralTermId);
    }
}
