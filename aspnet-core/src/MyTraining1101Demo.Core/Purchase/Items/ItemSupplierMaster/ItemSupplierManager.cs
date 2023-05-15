using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using System;

namespace MyTraining1101Demo.Purchase.Items.ItemSupplierMaster
{
    public class ItemSupplierManager : MyTraining1101DemoDomainServiceBase, IItemSupplierManager
    {
        private readonly IRepository<ItemSupplier, Guid> _itemSupplierRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemSupplierManager(
           IRepository<ItemSupplier, Guid> itemSupplierRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemSupplierRepository = itemSupplierRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
