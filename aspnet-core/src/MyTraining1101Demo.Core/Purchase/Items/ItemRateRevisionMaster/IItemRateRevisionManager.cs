namespace MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemRateRevisionMaster;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IItemRateRevisionManager : IDomainService
    {
        //Task<Guid> BulkInsertOrUpdateItemRateRevisions(List<ItemRateRevisionInputDto> itemRateRevisionInputList);

        Task InsertItemRateRevisionIntoDB(ItemMasterInputDto input,Guid ItemMasterId);

    }
}
