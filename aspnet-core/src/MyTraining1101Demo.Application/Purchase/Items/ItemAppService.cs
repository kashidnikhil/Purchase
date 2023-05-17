namespace MyTraining1101Demo.Purchase.Items
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Items.CalibrationAgenciesMaster;
    using MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster;
    using MyTraining1101Demo.Purchase.Items.ItemMaster;
    using MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster;
    using MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster;
    using MyTraining1101Demo.Purchase.Items.ItemSupplierMaster;
    using MyTraining1101Demo.Purchase.Items.ProcurementMaster;
    using MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster;
    using System;
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
        public ItemAppService(
          IItemManager itemManager,
          ICalibrationAgencyManager calibrationAgencyManager,
          ICalibrationTypeManager calibrationTypeManager,
          ItemAttachmentManager itemAttachmentManager,
          IItemRateRevisionManager itemRateRevisionManager,
          IItemStorageConditionManager itemStorageConditionManager,
          IItemSupplierManager itemSupplierManager,
          IProcurementManager itemProcurementManager,
          IItemSpareManager itemSpareManager
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

        public async Task<Guid> InsertOrUpdateItem(ItemMasterInputDto input)
        {
            try
            {
                var existingItem = await this._itemManager.GetItemMasterByNameFromDB(input.ItemCategory, input.ItemName);
                if (existingItem == null)
                {
                    var maximumCategoryExistingItem = await this._itemManager.FindItemMasterByCategoryIdFromDB(input.ItemCategory);
                    if (maximumCategoryExistingItem != null) {
                        input.CategoryId = maximumCategoryExistingItem.CategoryId + 1;
                    }
                    else { 
                        input.CategoryId = (int)input.ItemCategory;
                    }
                    input.ItemId = 1;
                }
                else {
                    input.CategoryId = existingItem.CategoryId;
                    input.ItemId = existingItem.ItemId + 1;
                }

                var insertedOrUpdatedItemId = await this._itemManager.InsertOrUpdateItemMasterIntoDB(input);
                if (insertedOrUpdatedItemId != Guid.Empty)
                {
                    if (input.ItemCalibrationAgencies != null && input.ItemCalibrationAgencies.Count > 0)
                    {
                        //Code to be implemented for insertions of item Item Calibration Agencies
                    }

                    if (input.ItemCalibrationTypes != null && input.ItemCalibrationTypes.Count > 0)
                    {
                        //Code to be implemented for insertions of item Item Calibration Types
                    }

                    if (input.ItemAttachments != null && input.ItemAttachments.Count > 0)
                    {
                        //Code to be implemented for insertions of item attachments
                    }

                    if (input.ItemStorageConditions != null && input.ItemStorageConditions.Count > 0)
                    {
                        //Code to be implemented for insertions of Item Storage Conditions
                    }

                    if (input.ItemSuppliers != null && input.ItemSuppliers.Count > 0)
                    {
                        //Code to be implemented for insertions of Item Suppliers
                    }

                    if (input.ItemProcurements != null && input.ItemProcurements.Count > 0)
                    {
                        //Code to be implemented for insertions of Item Procurements
                    }

                    if (input.ItemSpares != null && input.ItemSpares.Count > 0)
                    {
                        //Code to be implemented for insertions of Item Spares
                    }

                }
                return insertedOrUpdatedItemId;
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

    }
}
