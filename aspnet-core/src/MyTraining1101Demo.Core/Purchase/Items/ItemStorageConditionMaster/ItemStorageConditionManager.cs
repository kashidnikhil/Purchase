namespace MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemStorageConditionMaster;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<Guid> BulkInsertOrUpdateItemStorageConditions(List<ItemStorageConditionInputDto> itemStorageConditionInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemStorageConditions = ObjectMapper.Map<List<ItemStorageCondition>>(itemStorageConditionInputList);
                for (int i = 0; i < mappedItemStorageConditions.Count; i++)
                {
                    itemId = (Guid)mappedItemStorageConditions[i].ItemId;
                    await this.InsertOrUpdateItemStorageConditionIntoDB(mappedItemStorageConditions[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateItemStorageConditionIntoDB(ItemStorageCondition input)
        {
            try
            {
                var itemStorageConditionId = await this._itemStorageConditionRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteItemStorageConditions(Guid itemId)
        {
            try
            {
                var itemStorageConditions = await this.GetItemStorageConditionListFromDB(itemId);

                if (itemStorageConditions.Count > 0)
                {
                    for (int i = 0; i < itemStorageConditions.Count; i++)
                    {
                        await this.DeleteItemStorageConditionFromDB(itemStorageConditions[i].Id);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task DeleteItemStorageConditionFromDB(Guid itemStorageConditionId)
        {
            try
            {
                var itemStorageConditionItem = await this._itemStorageConditionRepository.GetAsync(itemStorageConditionId);

                await this._itemStorageConditionRepository.DeleteAsync(itemStorageConditionItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemStorageConditionDto>> GetItemStorageConditionListFromDB(Guid itemId)
        {
            try
            {
                var itemStorageConditionQuery = this._itemStorageConditionRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId);

                return new List<ItemStorageConditionDto>(ObjectMapper.Map<List<ItemStorageConditionDto>>(itemStorageConditionQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
