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
        //public async Task<Guid> InsertOrUpdateItem(ItemMasterInputDto input)
        //{
        //    try
        //    {
        //        var insertedOrUpdatedSupplierId = await this._supplierManager.InsertOrUpdateSupplierMasterIntoDB(input);

        //        if (insertedOrUpdatedSupplierId != Guid.Empty)
        //        {
        //            if (input.SupplierAddresses != null && input.SupplierAddresses.Count > 0)
        //            {
        //                input.SupplierAddresses.ForEach(supplierAddressItem =>
        //                {
        //                    supplierAddressItem.SupplierId = insertedOrUpdatedSupplierId;
        //                });
        //                await this._supplierAddressManager.BulkInsertOrUpdateSupplierAddresses(input.SupplierAddresses);
        //            }

        //            if (input.SupplierContactPersons != null && input.SupplierContactPersons.Count > 0)
        //            {
        //                input.SupplierContactPersons.ForEach(supplierContactPersonItem =>
        //                {
        //                    supplierContactPersonItem.SupplierId = insertedOrUpdatedSupplierId;
        //                });
        //                await this._supplierContactPersonManager.BulkInsertOrUpdateSupplierContactPersons(input.SupplierContactPersons);
        //            }

        //            if (input.SupplierBanks != null && input.SupplierBanks.Count > 0)
        //            {
        //                input.SupplierBanks.ForEach(supplierBankItem =>
        //                {
        //                    supplierBankItem.SupplierId = insertedOrUpdatedSupplierId;
        //                });
        //                await this._supplierBankManager.BulkInsertOrUpdateSupplierBanks(input.SupplierBanks);
        //            }

        //            if (input.SupplierCategories != null && input.SupplierCategories.Count > 0)
        //            {
        //                input.SupplierCategories.ForEach(supplierCategoryItem =>
        //                {
        //                    supplierCategoryItem.SupplierId = insertedOrUpdatedSupplierId;
        //                });

        //                await this._supplierCategoryManager.BulkInsertOrUpdateMappedSupplierCategories(input.SupplierCategories);
        //            }

        //        }
        //        return insertedOrUpdatedSupplierId;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //        throw ex;
        //    }
        //}

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
