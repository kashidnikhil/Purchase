namespace MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster
{
    using Abp.Domain.Repositories;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using System;

    public class ItemAttachmentManager : MyTraining1101DemoDomainServiceBase, IItemAttachmentManager
    {
        private readonly IRepository<ItemAttachment, Guid> _itemAttachmentRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemAttachmentManager(
           IRepository<ItemAttachment, Guid> itemAttachmentRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemAttachmentRepository = itemAttachmentRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
