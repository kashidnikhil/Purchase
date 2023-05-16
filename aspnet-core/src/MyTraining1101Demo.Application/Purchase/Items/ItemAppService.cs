using MyTraining1101Demo.Purchase.Items.CalibrationAgenciesMaster;
using MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster;
using MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster;
using MyTraining1101Demo.Purchase.Items.ItemMaster;
using MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster;
using MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster;
using MyTraining1101Demo.Purchase.Items.ItemSupplierMaster;
using MyTraining1101Demo.Purchase.Items.ProcurementMaster;
using MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster;

namespace MyTraining1101Demo.Purchase.Items
{
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
    }
}
