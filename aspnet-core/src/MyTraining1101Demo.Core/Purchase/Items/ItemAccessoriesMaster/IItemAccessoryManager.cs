namespace MyTraining1101Demo.Purchase.Items.ItemAccessoriesMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemAccessoriesMaster;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IItemAccessoryManager : IDomainService
    {
        Task<IList<ItemAccessoryDto>> GetItemAccessoryListFromDB(Guid itemId);

        Task<bool> BulkDeleteItemAccessories(Guid itemMasterId);

        Task<Guid> BulkInsertOrUpdateItemAccessories(List<ItemAccessoryInputDto> itemAccessoryInputList);
    }
}
