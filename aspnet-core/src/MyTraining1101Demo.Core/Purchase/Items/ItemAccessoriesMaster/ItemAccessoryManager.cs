namespace MyTraining1101Demo.Purchase.Items.ItemAccesoriesMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemAccessoriesMaster;
    using MyTraining1101Demo.Purchase.Items.ItemAccessoriesMaster;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ItemAccessoryManager : MyTraining1101DemoDomainServiceBase, IItemAccessoryManager
    {
        private readonly IRepository<ItemAccessory, Guid> _itemAccessoryRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemAccessoryManager(
           IRepository<ItemAccessory, Guid> itemAccessoryRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemAccessoryRepository = itemAccessoryRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<Guid> BulkInsertOrUpdateItemAccessories(List<ItemAccessoryInputDto> itemAccessoryInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemAccessories = ObjectMapper.Map<List<ItemAccessory>>(itemAccessoryInputList);
                //var filteredMappedItemSpares = mappedItemAccessories.Where(x => x.ItemSparesId!= null && x.ItemSparesId != Guid.Empty).ToList();

                for (int i = 0; i < mappedItemAccessories.Count; i++)
                {
                    itemId = (Guid)mappedItemAccessories[i].ItemId;
                    await this.InsertOrUpdateItemAccessoryIntoDB(mappedItemAccessories[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateItemAccessoryIntoDB(ItemAccessory input)
        {
            try
            {
                var itemAccessoryId = await this._itemAccessoryRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteItemAccessories(Guid itemMasterId)
        {

            try
            {
                var itemAccessories = await this.GetItemAccessoryListFromDB(itemMasterId);

                if (itemAccessories.Count > 0)
                {
                    for (int i = 0; i < itemAccessories.Count; i++)
                    {
                        await this.DeleteItemAcessoryFromDB(itemAccessories[i].Id);
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
        private async Task DeleteItemAcessoryFromDB(Guid itemAccessoryId)
        {
            try
            {
                var itemAccessory = await this._itemAccessoryRepository.GetAsync(itemAccessoryId);

                await this._itemAccessoryRepository.DeleteAsync(itemAccessory);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemAccessoryDto>> GetItemAccessoryListFromDB(Guid itemId)
        {
            try
            {
                var itemAccessoryQuery = await this._itemAccessoryRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId).ToListAsync();

                return new List<ItemAccessoryDto>(ObjectMapper.Map<List<ItemAccessoryDto>>(itemAccessoryQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
