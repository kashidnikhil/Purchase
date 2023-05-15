using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using System;

namespace MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster
{
    public class ItemStorageConditionManager : MyTraining1101DemoDomainServiceBase, IItemStorageConditionManager
    {
        private readonly IRepository<ItemStorageCondition, Guid> _itemStorageConditionRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemStorageConditionManager(
           IRepository<ItemStorageCondition, Guid> itemStorageConditionRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemStorageConditionRepository = itemStorageConditionRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
