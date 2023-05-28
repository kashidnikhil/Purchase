namespace MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.RequiredItemSparesMaster;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierBanks;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierBanks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<Guid> BulkInsertOrUpdateItemSpares(List<ItemSpareInputDto> itemSpareInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemSpares = ObjectMapper.Map<List<ItemSpare>>(itemSpareInputList);
                var filteredMappedItemSpares = mappedItemSpares.Where(x => x.ItemSparesId!= null && x.ItemSparesId != Guid.Empty).ToList();

                for (int i = 0; i < filteredMappedItemSpares.Count; i++)
                {
                    itemId = (Guid)filteredMappedItemSpares[i].ItemId;
                    await this.InsertOrUpdateItemSpareIntoDB(filteredMappedItemSpares[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateItemSpareIntoDB(ItemSpare input)
        {
            try
            {
                var itemSpareId = await this._itemSpareRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteItemSpares(Guid itemId)
        {

            try
            {
                var itemSpares = await this.GetItemSpareListFromDB(itemId);

                if (itemSpares.Count > 0)
                {
                    for (int i = 0; i < itemSpares.Count; i++)
                    {
                        await this.DeleteItemSpareFromDB(itemSpares[i].Id);
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
        private async Task DeleteItemSpareFromDB(Guid supplierBankId)
        {
            try
            {
                var itemSpareItem = await this._itemSpareRepository.GetAsync(supplierBankId);

                await this._itemSpareRepository.DeleteAsync(itemSpareItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemSpareDto>> GetItemSpareListFromDB(Guid itemId)
        {
            try
            {
                var itemSpareQuery = this._itemSpareRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId);

                return new List<ItemSpareDto>(ObjectMapper.Map<List<ItemSpareDto>>(itemSpareQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
