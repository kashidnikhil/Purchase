namespace MyTraining1101Demo.Purchase.DeliveryTerms
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.DeliveryTerms.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;
    using Abp.Domain.Services;

    public interface IDeliveryTermManager : IDomainService
    {
        Task<PagedResultDto<DeliveryTermDto>> GetPaginatedDeliveryTermsFromDB(DeliveryTermSearchDto input);
        Task<ResponseDto> InsertOrUpdateDeliveryTermIntoDB(DeliveryTermInputDto input);

        Task<bool> DeleteDeliveryTermFromDB(Guid deliveryTermId);

        Task<DeliveryTermDto> GetDeliveryTermByIdFromDB(Guid deliveryTermId);

        Task<IList<DeliveryTermDto>> GetDeliveryTermListFromDB();

        Task<bool> RestoreDeliveryTerm(Guid deliveryTermId);
    }
}
