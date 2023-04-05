using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.POGeneralTerms.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.POGeneralTerms
{
    public interface IPOGeneralTermAppService
    {
        Task<PagedResultDto<POGeneralTermDto>> GetPOGeneralTerms(POGeneralTermSearchDto input);
        Task<ResponseDto> InsertOrUpdatePOGeneralTerm(POGeneralTermInputDto input);

        Task<bool> DeletePOGeneralTerm(Guid poGeneralTermId);

        Task<POGeneralTermDto> GetPOGeneralTermById(Guid poGeneralTermId);

        Task<IList<POGeneralTermDto>> GetPOGeneralTermList();

        Task<bool> RestorePOGeneralTerm(Guid poGeneralTermId);
    }
}
