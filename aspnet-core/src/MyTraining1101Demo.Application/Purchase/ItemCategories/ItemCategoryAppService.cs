namespace MyTraining1101Demo.Purchase.ItemCategories
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.ItemCategories.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ItemCategoryAppService : MyTraining1101DemoAppServiceBase, IItemCategoryAppService
    {
        private readonly IItemCategoryManager _itemCategoryManager;

        public ItemCategoryAppService(
          IItemCategoryManager itemcategoryManager
         )
        {
            _itemCategoryManager = itemcategoryManager;
        }


        public async Task<PagedResultDto<ItemCategoryDto>> GetItemCategories(ItemCategorySearchDto input)
        {
            try
            {
                var result = await this._itemCategoryManager.GetPaginatedItemCategoriesFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ResponseDto> InsertOrUpdateItemCategory(ItemCategoryInputDto input)
        {
            try
            {
                var existingItemCategory = await this._itemCategoryManager.FindRecentlyAddedItemCategoryFromDB();
                if (existingItemCategory == null)
                {
                    input.ItemCategoryCode = 10001;
                }
                else
                {
                    input.ItemCategoryCode = existingItemCategory.ItemCategoryCode + 10000;
                }


                var insertedOrUpdatedItemCategory = await this._itemCategoryManager.InsertOrUpdateItemCategoryIntoDB(input);

                return insertedOrUpdatedItemCategory;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteItemCategory(Guid itemCategoryId)
        {
            try
            {
                var isItemCategoryDeleted = await this._itemCategoryManager.DeleteItemCategoryFromDB(itemCategoryId);
                return isItemCategoryDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<ItemCategoryDto> GetItemCategoryById(Guid itemCategoryId)
        {
            try
            {
                var response = await this._itemCategoryManager.GetItemCategoryByIdFromDB(itemCategoryId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemCategoryDto>> GetItemCategoryList()
        {
            try
            {
                var response = await this._itemCategoryManager.GetItemCategoryListFromDB();
                return response;
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
                var response = await this._itemCategoryManager.RestoreItemCategory(itemCategoryId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
