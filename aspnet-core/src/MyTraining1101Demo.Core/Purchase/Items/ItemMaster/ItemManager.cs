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
    using MyTraining1101Demo.Purchase.Shared;
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

        //[UnitOfWork]
        //public async Task<Guid> InsertOrUpdateItemMasterIntoDB(ItemMasterInputDto input)
        //{
        //    try
        //    {
        //        var mappedItemMaster = ObjectMapper.Map<Item>(input);
        //        var itemMasterId = await this._itemMasterRepository.InsertOrUpdateAndGetIdAsync(mappedItemMaster);
        //        await CurrentUnitOfWork.SaveChangesAsync();
        //        return itemMasterId;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //}

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateItemMasterIntoDB(ItemMasterInputDto input)
        {
            try
            {
                Guid itemMasterId = Guid.Empty;
                var existingItemMaster = await this._itemMasterRepository.GetAll()
                    .Include(x => x.ItemCategory)
                    .Where(x => !x.IsDeleted && x.ItemCategoryId == input.ItemCategoryId && x.ItemName.ToLower().Trim() == input.ItemName.Trim().ToLower())
                    .FirstOrDefaultAsync();

                if (existingItemMaster != null)
                {
                    if (input.Id != existingItemMaster.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = existingItemMaster.ItemName,
                            RecentlyAddedItem = input,
                            DataMatchFound = true
                        };
                    }
                    else
                    {
                        ObjectMapper.Map(input, existingItemMaster);
                        itemMasterId = await this._itemMasterRepository.InsertOrUpdateAndGetIdAsync(existingItemMaster);
                        await CurrentUnitOfWork.SaveChangesAsync();
                        return new ResponseDto
                        {
                            Id = itemMasterId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {

                    var mappedItemMaster = ObjectMapper.Map<Item>(input);
                    itemMasterId = await this._itemMasterRepository.InsertOrUpdateAndGetIdAsync(mappedItemMaster);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = itemMasterId,
                        DataMatchFound = false
                    };
                }
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

        public async Task<ItemMasterDto?> GetItemMasterByNameFromDB(Guid itemCategory, string itemName)
        {
            try
            {
                var itemMaster = await this._itemMasterRepository.GetAll()
                    .Include(x => x.ItemCategory)
                    .Where(x => x.ItemCategoryId == itemCategory && x.ItemName.ToLower().Trim() == itemName.Trim().ToLower())
                    .OrderByDescending(x => x.ItemId)
                    .FirstOrDefaultAsync();

                return ObjectMapper.Map<ItemMasterDto?>(itemMaster);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ItemMasterDto?> FindItemMasterByCategoryIdFromDB(Guid itemCategory)
        {
            try
            {
                var itemMaster = await this._itemMasterRepository.GetAll()
                    .Where(x => x.ItemCategoryId == itemCategory)
                    .OrderByDescending(x => x.CategoryId)
                    .FirstOrDefaultAsync();

                return ObjectMapper.Map<ItemMasterDto?>(itemMaster);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        public async Task<IList<ItemMasterListDto>> GetItemMasterListFromDB()
        {
            try
            {
                var itemMasterQuery = this._itemMasterRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<ItemMasterListDto>(ObjectMapper.Map<List<ItemMasterListDto>>(itemMasterQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<IList<ItemMasterListDto>> GetItemListByItemCategoryFromDB(Guid itemCategoryId)
        {
            try
            {
                var itemMasterQuery = this._itemMasterRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemCategoryId == itemCategoryId);

                return new List<ItemMasterListDto>(ObjectMapper.Map<List<ItemMasterListDto>>(itemMasterQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<IList<ItemListDto>> GetItemListForSubAssemblyPageDropdownFromDB()
        {
            try
            {
                var itemMasterQuery = this._itemMasterRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<ItemListDto>(ObjectMapper.Map<List<ItemListDto>>(itemMasterQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<ResponseDto> ForceInsertOrUpdateItemMasterIntoDB(ItemMasterInputDto input) {
            try
            {
                Guid itemMasterId = Guid.Empty;
                var mappedItemMaster = ObjectMapper.Map<Item>(input);
                itemMasterId = await this._itemMasterRepository.InsertOrUpdateAndGetIdAsync(mappedItemMaster);
                await CurrentUnitOfWork.SaveChangesAsync();
                return new ResponseDto
                {
                    Id = itemMasterId,
                    DataMatchFound = false
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
