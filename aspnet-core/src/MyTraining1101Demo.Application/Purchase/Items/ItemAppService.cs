namespace MyTraining1101Demo.Purchase.Items
{
    using Abp.Application.Services.Dto;
    using Microsoft.EntityFrameworkCore.Metadata;
    using MyTraining1101Demo.Purchase.ItemCategories;
    using MyTraining1101Demo.Purchase.Items.CalibrationAgenciesMaster;
    using MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using MyTraining1101Demo.Purchase.Items.ItemAccessoriesMaster;
    using MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster;
    using MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster;
    using MyTraining1101Demo.Purchase.Items.ItemSupplierMaster;
    using MyTraining1101Demo.Purchase.Items.ProcurementMaster;
    using MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ItemAppService : MyTraining1101DemoAppServiceBase, IItemAppService
    {
        private readonly ICalibrationAgencyManager _calibrationAgencyManager;
        private readonly ICalibrationTypeManager _calibrationTypeManager;
        private readonly ItemAttachmentManager _itemAttachmentManager;
        private readonly IItemManager _itemManager;
        private readonly IItemRateRevisionManager _itemRateRevisionManager;
        private readonly IItemStorageConditionManager _itemStorageConditionManager;
        private readonly IItemSupplierManager _itemSupplierManager;
        private readonly IProcurementManager _itemProcurementManager;
        private readonly IItemSpareManager _itemSpareManager;
        private readonly IItemAccessoryManager _itemAccessoryManager;
        private readonly IItemCategoryManager _itemCategoryManager; 
        public ItemAppService(
          IItemManager itemManager,
          ICalibrationAgencyManager calibrationAgencyManager,
          ICalibrationTypeManager calibrationTypeManager,
          ItemAttachmentManager itemAttachmentManager,
          IItemRateRevisionManager itemRateRevisionManager,
          IItemStorageConditionManager itemStorageConditionManager,
          IItemSupplierManager itemSupplierManager,
          IProcurementManager itemProcurementManager,
          IItemAccessoryManager itemAccessoryManager,
          IItemSpareManager itemSpareManager,
          IItemCategoryManager itemCategoryManager
         )
        {
            _itemManager = itemManager;
            _calibrationAgencyManager = calibrationAgencyManager;
            _calibrationTypeManager = calibrationTypeManager;
            _itemAttachmentManager = itemAttachmentManager;
            _itemRateRevisionManager = itemRateRevisionManager;
            _itemStorageConditionManager = itemStorageConditionManager;
            _itemSupplierManager = itemSupplierManager;
            _itemProcurementManager = itemProcurementManager;
            _itemSpareManager = itemSpareManager;
            _itemAccessoryManager = itemAccessoryManager;
            _itemCategoryManager = itemCategoryManager;
        }

        public async Task<PagedResultDto<ItemMasterListDto>> GetItems(ItemMasterSearchDto input)
        {
            try
            {
                var result = await this._itemManager.GetPaginatedItemListFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ResponseDto> InsertOrUpdateItem(ItemMasterInputDto input)
        {
            try
            {

                var existingItem = await this._itemManager.GetItemMasterByNameFromDB(input.ItemCategoryId, input.ItemName);
                if (existingItem == null)
                {
                    var maximumCategoryExistingItem = await this._itemManager.FindItemMasterByCategoryIdFromDB(input.ItemCategoryId);
                    if (maximumCategoryExistingItem != null) {
                        input.CategoryId = maximumCategoryExistingItem.CategoryId + 1;
                    }
                    else { 
                        var exsitingItemCategory = await this._itemCategoryManager.GetItemCategoryByIdFromDB(input.ItemCategoryId);
                        input.CategoryId = exsitingItemCategory.ItemCategoryCode;
                    }
                    input.ItemId = 1;
                }
                else {
                    input.CategoryId = existingItem.CategoryId;
                    input.ItemId = existingItem.Id == input.Id ? existingItem.ItemId : existingItem.ItemId + 1;
                }

                var insertedOrUpdatedItem = await this._itemManager.InsertOrUpdateItemMasterIntoDB(input);
                if (insertedOrUpdatedItem.DataMatchFound) { 
                    return insertedOrUpdatedItem;
                }

                if (insertedOrUpdatedItem.Id != Guid.Empty)
                {
                    await this.InsertOrUpdateSubItemsAndItemRateRevision(input, (Guid)insertedOrUpdatedItem.Id);
                }
                return insertedOrUpdatedItem;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ResponseDto> ForceInsertOrUpdateItem(ItemMasterInputDto input)
        {
            try
            {
                var insertedOrUpdatedItem = await this._itemManager.ForceInsertOrUpdateItemMasterIntoDB(input);
                if (insertedOrUpdatedItem.DataMatchFound)
                {
                    return insertedOrUpdatedItem;
                }

                if (insertedOrUpdatedItem.Id != Guid.Empty)
                {
                    await this.InsertOrUpdateSubItemsAndItemRateRevision(input, (Guid)insertedOrUpdatedItem.Id);
                }
                return insertedOrUpdatedItem;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteItemMasterData(Guid itemId)
        {
            try
            {

                var isCalibrationAgnciesDeleted = await this._calibrationAgencyManager.BulkDeleteItemCalibrationAgencies(itemId);

                var isCalibrationTypesDeleted = await this._calibrationTypeManager.BulkDeleteItemCalibrationType(itemId);

                var isItemAttachmentDeleted = await this._itemAttachmentManager.BulkDeleteItemAttachments(itemId);

                var isItemStorageConditionDeleted = await this._itemStorageConditionManager.BulkDeleteItemStorageConditions(itemId);

                var isItemSupplierDeleted = await this._itemSupplierManager.BulkDeleteItemSuppliers(itemId);

                var isItemProcurementDeleted = await this._itemProcurementManager.BulkDeleteItemProcurements(itemId);

                var isItemSpareDeleted = await this._itemSpareManager.BulkDeleteItemSpares(itemId);

                var isItemMasterDeleted = await this._itemManager.DeleteItemMasterFromDB(itemId);
                
                return isItemMasterDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<ItemMasterDto> GetItemMasterById(Guid itemId)
        {
            try
            {
                var itemMasterData = await this._itemManager.GetItemMasterByIdFromDB(itemId);
                
                if (itemMasterData.Id != Guid.Empty)
                {
                    itemMasterData.ItemCalibrationAgencies = await this._calibrationAgencyManager.GetItemCalibrationAgencyListFromDB(itemMasterData.Id);
                    itemMasterData.ItemCalibrationTypes = await this._calibrationTypeManager.GetItemCalibrationTypeListFromDB(itemMasterData.Id);
                    itemMasterData.ItemAttachments = await this._itemAttachmentManager.GetItemAttachmentListFromDB(itemMasterData.Id);
                    itemMasterData.ItemStorageConditions = await this._itemStorageConditionManager.GetItemStorageConditionListFromDB(itemMasterData.Id);
                    itemMasterData.ItemSuppliers = await this._itemSupplierManager.GetItemSupplierListFromDB(itemMasterData.Id);
                    itemMasterData.ItemProcurements = await this._itemProcurementManager.GetItemProcurementListFromDB(itemMasterData.Id);
                    itemMasterData.ItemSpares = await this._itemSpareManager.GetItemSpareListFromDB(itemMasterData.Id);
                    itemMasterData.ItemRateRevisions = await this._itemRateRevisionManager.GetItemRateRevisionListFromDB(itemMasterData.Id);
                }
                return itemMasterData;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemMasterListDto>> GetItemMasterList()
        {
            try
            {
                var response = await this._itemManager.GetItemMasterListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemListDto>> GetItemMasterListForSubAssemblyPageDropdown()
        {
            try
            {
                var response = await this._itemManager.GetItemListForSubAssemblyPageDropdownFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        private async Task InsertOrUpdateSubItemsAndItemRateRevision(ItemMasterInputDto input, Guid insertedOrUpdatedItemId) {
            try
            {
                if (input.ItemCalibrationAgencies != null && input.ItemCalibrationAgencies.Count > 0)
                {
                    input.ItemCalibrationAgencies.ForEach(itemCalibrationAgency =>
                    {
                        itemCalibrationAgency.ItemId = insertedOrUpdatedItemId;
                    });
                    await this._calibrationAgencyManager.BulkInsertOrUpdateItemCalibrationAgencies(input.ItemCalibrationAgencies);
                }

                if (input.ItemCalibrationTypes != null && input.ItemCalibrationTypes.Count > 0)
                {
                    input.ItemCalibrationTypes.ForEach(ItemCalibrationType =>
                    {
                        ItemCalibrationType.ItemId = insertedOrUpdatedItemId;
                    });
                    await this._calibrationTypeManager.BulkInsertOrUpdateItemCalibrationTypes(input.ItemCalibrationTypes);

                }

                if (input.ItemAttachments != null && input.ItemAttachments.Count > 0)
                {
                    input.ItemAttachments.ForEach(ItemAttachment =>
                    {
                        ItemAttachment.ItemId = insertedOrUpdatedItemId;
                    });
                    await this._itemAttachmentManager.BulkInsertOrUpdateItemAttachments(input.ItemAttachments);
                }

                if (input.ItemStorageConditions != null && input.ItemStorageConditions.Count > 0)
                {
                    input.ItemStorageConditions.ForEach(ItemStorageCondition =>
                    {
                        ItemStorageCondition.ItemId = insertedOrUpdatedItemId;
                    });
                    await this._itemStorageConditionManager.BulkInsertOrUpdateItemStorageConditions(input.ItemStorageConditions);
                }

                if (input.ItemSuppliers != null && input.ItemSuppliers.Count > 0)
                {
                    input.ItemSuppliers.ForEach(ItemSupplier =>
                    {
                        ItemSupplier.ItemId = insertedOrUpdatedItemId;
                    });
                    await this._itemSupplierManager.BulkInsertOrUpdateItemSuppliers(input.ItemSuppliers);
                }

                if (input.ItemProcurements != null && input.ItemProcurements.Count > 0)
                {
                    input.ItemProcurements.ForEach(ItemProcurement =>
                    {
                        ItemProcurement.ItemId = insertedOrUpdatedItemId;
                    });
                    await this._itemProcurementManager.BulkInsertOrUpdateItemProcurements(input.ItemProcurements);
                }

                if (input.ItemSpares != null && input.ItemSpares.Count > 0)
                {
                    input.ItemSpares.ForEach(ItemSpare =>
                    {
                        ItemSpare.ItemId = insertedOrUpdatedItemId;
                    });
                    await this._itemSpareManager.BulkInsertOrUpdateItemSpares(input.ItemSpares);
                }

                if (input.ItemAccessories != null && input.ItemAccessories.Count > 0)
                {
                    input.ItemAccessories.ForEach(ItemAccessory =>
                    {
                        ItemAccessory.ItemId = insertedOrUpdatedItemId;
                    });
                    await this._itemAccessoryManager.BulkInsertOrUpdateItemAccessories(input.ItemAccessories);
                }

                //This is used for inserting item rate. Used for maintaining rate history
                await this._itemRateRevisionManager.InsertItemRateRevision(input, insertedOrUpdatedItemId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
