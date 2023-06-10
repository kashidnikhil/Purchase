namespace MyTraining1101Demo.Purchase.DeliveryTerms
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.DeliveryTerms.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IDeliveryTermAppService
    {
        Task<PagedResultDto<DeliveryTermDto>> GetDeliveryTerms(DeliveryTermSearchDto input);
        Task<ResponseDto> InsertOrUpdateDeliveryTerm(DeliveryTermInputDto input);

        Task<bool> DeleteDeliveryTerm(Guid deliveryTermId);

        Task<DeliveryTermDto> GetDeliveryTermById(Guid deliveryTermId);

        Task<IList<DeliveryTermDto>> GetDeliveryTermList();

        Task<bool> RestoreDeliveryTerm(Guid deliveryTermId);
    }
}
