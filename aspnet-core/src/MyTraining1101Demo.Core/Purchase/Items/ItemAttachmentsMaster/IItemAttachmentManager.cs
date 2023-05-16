namespace MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemAttachmentsMaster;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IItemAttachmentManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateItemAttachments(List<ItemAttachmentInputDto> itemAttachmentInputList);

        Task<bool> BulkDeleteItemAttachments(Guid itemId);

        Task<IList<ItemAttachmentDto>> GetItemAttachmentListFromDB(Guid itemId);
    }
}
