namespace MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemStorageConditionMaster;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    public interface IItemStorageConditionManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateItemStorageConditions(List<ItemStorageConditionInputDto> itemStorageConditionInputList);

        Task<bool> BulkDeleteItemStorageConditions(Guid itemId);

        Task<IList<ItemStorageConditionDto>> GetItemStorageConditionListFromDB(Guid itemId);
    }
}
