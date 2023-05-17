namespace MyTraining1101Demo.Purchase.Items.ItemMaster
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using MyTraining1101Demo.Purchase.Items.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    public class ItemManager : MyTraining1101DemoDomainServiceBase, IItemManager
    {
        private readonly IRepository<Item, Guid> _itemMasterRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemManager(
           IRepository<Item, Guid> itemMasterRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemMasterRepository = itemMasterRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }


        public async Task<PagedResultDto<ItemMasterListDto>> GetPaginatedItemListFromDB(ItemMasterSearchDto input)
        {
            try
            {
                var itemMasterQuery = this._itemMasterRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.ItemName.ToLower().Contains(input.SearchString.ToLower()) || item.GenericName.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await itemMasterQuery.CountAsync();
                var items = await itemMasterQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<ItemMasterListDto>(
                totalCount,
                ObjectMapper.Map<List<ItemMasterListDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<Guid> InsertOrUpdateItemMasterIntoDB(ItemMasterInputDto input)
        {
            try
            {
                var mappedItemMaster = ObjectMapper.Map<Item>(input);
                var itemMasterId = await this._itemMasterRepository.InsertOrUpdateAndGetIdAsync(mappedItemMaster);
                await CurrentUnitOfWork.SaveChangesAsync();
                return itemMasterId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<bool> DeleteItemMasterFromDB(Guid itemId)
        {
            try
            {
                var itemMasterItem = await this._itemMasterRepository.GetAsync(itemId);

                await this._itemMasterRepository.DeleteAsync(itemMasterItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ItemMasterDto> GetItemMasterByIdFromDB(Guid itemId)
        {
            try
            {
                var itemMasterItem = await this._itemMasterRepository.GetAsync(itemId);

                return ObjectMapper.Map<ItemMasterDto>(itemMasterItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ItemMasterDto?> GetItemMasterByNameFromDB(ItemCategory itemCategory, string itemName)
        {
            try
            {
                var itemMaster = await this._itemMasterRepository.GetAll()
                    .Where(x => x.ItemCategory == itemCategory && x.ItemName.ToLower().Trim() == itemName.Trim().ToLower())
                    .OrderByDescending(x=> x.ItemId)
                    .FirstOrDefaultAsync();

                return ObjectMapper.Map<ItemMasterDto?>(itemMaster);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ItemMasterDto?> FindItemMasterByCategoryIdFromDB(ItemCategory itemCategory)
        {
            try
            {
                var itemMaster = await this._itemMasterRepository.GetAll()
                    .Where(x => x.ItemCategory == itemCategory)
                    .OrderByDescending(x=> x.CategoryId)
                    .FirstOrDefaultAsync();

                return ObjectMapper.Map<ItemMasterDto?>(itemMaster);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
