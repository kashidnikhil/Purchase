namespace MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemRateRevisionMaster;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using System.Threading.Tasks;
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

        public async Task<Guid> BulkInsertOrUpdateItemRateRevisions(List<ItemRateRevisionInputDto> itemRateRevisionInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemRateRevisions = ObjectMapper.Map<List<ItemRateRevision>>(itemRateRevisionInputList);
                for (int i = 0; i < mappedItemRateRevisions.Count; i++)
                {
                    itemId = (Guid)mappedItemRateRevisions[i].ItemId;
                    await this.InsertOrUpdateItemRateRevisionIntoDB(mappedItemRateRevisions[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task InsertOrUpdateItemRateRevisionIntoDB(ItemRateRevision input)
        {
            try
            {
                var itemRateRevisionId = await this._itemRateRevisionRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        //public async Task<bool> BulkDeleteItemStorageConditions(Guid itemId)
        //{
        //    try
        //    {
        //        var itemStorageConditions = await this.GetItemStorageConditionListFromDB(itemId);

        //        if (itemStorageConditions.Count > 0)
        //        {
        //            for (int i = 0; i < itemStorageConditions.Count; i++)
        //            {
        //                await this.DeleteItemStorageConditionFromDB(itemStorageConditions[i].Id);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[UnitOfWork]
        //private async Task DeleteItemStorageConditionFromDB(Guid itemRateRevisionId)
        //{
        //    try
        //    {
        //        var itemRateRevisionItem = await this._itemRateRevisionRepository.GetAsync(itemRateRevisionId);

        //        await this._itemRateRevisionRepository.DeleteAsync(itemRateRevisionItem);

        //        await CurrentUnitOfWork.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //}

        public async Task<IList<ItemRateRevisionDto>> GetItemRateRevisionListFromDB(Guid itemId)
        {
            try
            {
                var itemRateRevisionQuery = this._itemRateRevisionRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId);

                return new List<ItemRateRevisionDto>(ObjectMapper.Map<List<ItemRateRevisionDto>>(itemRateRevisionQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
