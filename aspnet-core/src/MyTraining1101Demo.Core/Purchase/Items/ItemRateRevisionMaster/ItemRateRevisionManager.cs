using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using System;

namespace MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster
{
    public class ItemRateRevisionManager : MyTraining1101DemoDomainServiceBase, IItemRateRevisionManager
    {
        private readonly IRepository<ItemRateRevision, Guid> _itemRateRevisionRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemRateRevisionManager(
           IRepository<ItemRateRevision, Guid> itemRateRevisionRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemRateRevisionRepository = itemRateRevisionRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
