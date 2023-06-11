namespace MyTraining1101Demo.Purchase.ItemCategories
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.ItemCategories.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    public class ItemCategoryManager : MyTraining1101DemoDomainServiceBase, IItemCategoryManager
    {
        private readonly IRepository<ItemCategory, Guid> _itemCategoryRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemCategoryManager(
           IRepository<ItemCategory, Guid> itemCategoryRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemCategoryRepository = itemCategoryRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<ItemCategoryDto>> GetPaginatedItemCategoriesFromDB(ItemCategorySearchDto input)
        {
            try
            {
                var itemCategoryQuery = this._itemCategoryRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await itemCategoryQuery.CountAsync();
                var items = await itemCategoryQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<ItemCategoryDto>(
                totalCount,
                ObjectMapper.Map<List<ItemCategoryDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateItemCategoryIntoDB(ItemCategoryInputDto input)
        {
            try
            {
                Guid itemCategoryId = Guid.Empty;
                var itemCategory = await this._itemCategoryRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (itemCategory != null)
                {
                    if (input.Id != itemCategory.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = itemCategory.Name,
                            IsExistingDataAlreadyDeleted = itemCategory.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = itemCategory.Id
                        };
                    }
                    else
                    {
                        ObjectMapper.Map(input, itemCategory);
                        itemCategoryId = await this._itemCategoryRepository.InsertOrUpdateAndGetIdAsync(itemCategory);
                        return new ResponseDto
                        {
                            Id = itemCategoryId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedItemCategory = ObjectMapper.Map<ItemCategory>(input);
                    itemCategoryId = await this._itemCategoryRepository.InsertOrUpdateAndGetIdAsync(mappedItemCategory);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = itemCategoryId,
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
        public async Task<bool> DeleteItemCategoryFromDB(Guid itemCategoryId)
        {
            try
            {
                var itemCategory = await this._itemCategoryRepository.GetAsync(itemCategoryId);

                await this._itemCategoryRepository.DeleteAsync(itemCategory);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ItemCategoryDto> GetItemCategoryByIdFromDB(Guid itemCategoryId)
        {
            try
            {
                var itemCategory = await this._itemCategoryRepository.GetAsync(itemCategoryId);

                return ObjectMapper.Map<ItemCategoryDto>(itemCategory);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<List<ItemCategoryDto>> GetItemCategoryListFromDB()
        {
            try
            {
                var itemCategory = this._itemCategoryRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<ItemCategoryDto>(ObjectMapper.Map<List<ItemCategoryDto>>(itemCategory));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }


        public async Task<bool> RestoreItemCategory(Guid itemCategoryId)
        {
            try
            {
                var ItemCategory = await this._itemCategoryRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == itemCategoryId);
                ItemCategory.IsDeleted = false;
                ItemCategory.DeleterUserId = null;
                ItemCategory.DeletionTime = null;
                await this._itemCategoryRepository.UpdateAsync(ItemCategory);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        public async Task<ItemCategoryDto?> FindRecentlyAddedItemCategoryFromDB()
        {
            try
            {
                var itemMaster = await this._itemCategoryRepository.GetAll().IgnoreQueryFilters()
                    .OrderByDescending(x => x.ItemCategoryCode)
                    .FirstOrDefaultAsync();

                return ObjectMapper.Map<ItemCategoryDto?>(itemMaster);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


    }
}
