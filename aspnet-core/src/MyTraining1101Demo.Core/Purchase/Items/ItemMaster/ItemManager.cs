using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using System;

namespace MyTraining1101Demo.Purchase.Items.ItemMaster
{
    public class ItemManager : MyTraining1101DemoDomainServiceBase, IItemManager
    {
        private readonly IRepository<Item, Guid> _itemMasterRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemManager(
           IRepository<Item, Guid> itemMasterRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemMasterRepository = itemMasterRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
