using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using System;

namespace MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster
{
    public class ItemSpareManager : MyTraining1101DemoDomainServiceBase, IItemSpareManager
    {
        private readonly IRepository<ItemSpare, Guid> _itemSpareRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemSpareManager(
           IRepository<ItemSpare, Guid> itemSpareRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemSpareRepository = itemSpareRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
