using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using MyTraining1101Demo.Purchase.Items.Dto.ItemSupplierMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Guid> BulkInsertOrUpdateItemSuppliers(List<ItemSupplierInputDto> itemSupplierInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemSuppliers = ObjectMapper.Map<List<ItemSupplier>>(itemSupplierInputList);
                var filteredMappedItemSuppliers = mappedItemSuppliers.Where(x => x.SupplierId != null && x.SupplierId != Guid.Empty).ToList();

                for (int i = 0; i < filteredMappedItemSuppliers.Count; i++)
                {
                    itemId = (Guid)filteredMappedItemSuppliers[i].ItemId;
                    await this.InsertOrUpdateItemSupplierIntoDB(filteredMappedItemSuppliers[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateItemSupplierIntoDB(ItemSupplier input)
        {
            try
            {
                var itemSupplierId = await this._itemSupplierRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteItemSuppliers(Guid itemId)
        {

            try
            {
                var itemSuppliers = await this.GetItemSupplierListFromDB(itemId);

                if (itemSuppliers.Count > 0)
                {
                    for (int i = 0; i < itemSuppliers.Count; i++)
                    {
                        await this.DeleteItemSupplierFromDB(itemSuppliers[i].Id);
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
        private async Task DeleteItemSupplierFromDB(Guid itemSupplierId)
        {
            try
            {
                var itemSupplierItem = await this._itemSupplierRepository.GetAsync(itemSupplierId);

                await this._itemSupplierRepository.DeleteAsync(itemSupplierItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemSupplierDto>> GetItemSupplierListFromDB(Guid itemId)
        {
            try
            {
                var itemSupplierQuery = this._itemSupplierRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId);

                return new List<ItemSupplierDto>(ObjectMapper.Map<List<ItemSupplierDto>>(itemSupplierQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
