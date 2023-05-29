namespace MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
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


        [UnitOfWork]
        public async Task InsertItemRateRevision(ItemMasterInputDto input,Guid ItemMasterId)
        {
            try
            {
                if (input.ItemRateRevisions != null && input.ItemRateRevisions.Count > 0)
                {
                    input.ItemRateRevisions.ForEach(itemRateRevision =>
                    {
                        //itemRateRevision.Id = Guid.Empty;
                        itemRateRevision.ItemId = ItemMasterId;
                    });

                    var mappedItemRateRevisionList = ObjectMapper.Map<List<ItemRateRevision>>(input.ItemRateRevisions);
                    var filteredMappedItemRateRevisionList = mappedItemRateRevisionList.Where(x=> x.OrderingQuantity != null).ToList();
                    if (filteredMappedItemRateRevisionList.Count > 0) { 
                        await this.BulkInsertItemRevisionRates(filteredMappedItemRateRevisionList);
                    }
                }

                var mappedItemRateRevision = ObjectMapper.Map<ItemRateRevision>(input);
                mappedItemRateRevision.ItemId = ItemMasterId;
                await this.InsertItemRateRevisionIntoDB(mappedItemRateRevision);
                
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        private async Task<Guid> BulkInsertItemRevisionRates(List<ItemRateRevision> itemRateRevisionList)
        {
            try
            {
                Guid itemId = Guid.Empty;
              
                for (int i = 0; i < itemRateRevisionList.Count; i++)
                {
                    await this.InsertItemRateRevisionIntoDB(itemRateRevisionList[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task InsertItemRateRevisionIntoDB(ItemRateRevision input)
        {
            try
            {
                input.Id = input.Id != Guid.Empty ? input.Id : Guid.Empty;
                input.CreationTime = DateTime.UtcNow;
                var itemRateRevisionId = await this._itemRateRevisionRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemRateRevisionDto>> GetItemRateRevisionListFromDB(Guid itemMasterId)
        {
            try
            {
                var itemRateRevisionQuery = this._itemRateRevisionRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemMasterId);

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
