﻿namespace MyTraining1101Demo.Purchase.ModelWiseItems
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters.Dto;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Threading.Tasks;

    public class ModelWiseItemAppService : MyTraining1101DemoAppServiceBase
    {
        private readonly IModelWiseItemMasterManager _modelWiseItemMasterManager;
        private readonly IModelWiseItemManager _modelWiseItemManager;

        public ModelWiseItemAppService(
          IModelWiseItemMasterManager modelWiseItemMasterManager,
          IModelWiseItemManager modelWiseItemManager
         )
        {
            _modelWiseItemMasterManager = modelWiseItemMasterManager;
            _modelWiseItemManager = modelWiseItemManager;
        }


        public async Task<PagedResultDto<ModelWiseItemMasterListDto>> GetModelWiseItems(ModelWiseItemMasterSearchDto input)
        {
            try
            {
                var result = await this._modelWiseItemMasterManager.GetPaginatedModelWiseItemMastersFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ResponseDto> InsertOrUpdateModelWiseItem(ModelWiseItemMasterInputDto input)
        {
            try
            {
                var insertedOrUpdatedModelWiseItem = await this._modelWiseItemMasterManager.InsertOrUpdateModelWiseItemMasterIntoDB(input);
                if (insertedOrUpdatedModelWiseItem.DataMatchFound)
                {
                    return insertedOrUpdatedModelWiseItem;
                }

                if (insertedOrUpdatedModelWiseItem.Id != Guid.Empty)
                {
                    if (input.ModelWiseItemData != null && input.ModelWiseItemData.Count > 0)
                    {
                        input.ModelWiseItemData.ForEach(modelWiseItem =>
                        {
                            modelWiseItem.ModelWiseItemMasterId = insertedOrUpdatedModelWiseItem.Id;
                        });
                        await this._modelWiseItemManager.BulkInsertOrUpdateModelWiseItems(input.ModelWiseItemData);
                    }
                }
                return insertedOrUpdatedModelWiseItem;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteModelWiseItemMasterData(Guid modelWiseItemMasterId)
        {
            try
            {

                var isCalibrationAgnciesDeleted = await this._modelWiseItemManager.BulkDeleteModelWiseItems(modelWiseItemMasterId);

               
                var isModelWiseItemMasterDeleted = await this._modelWiseItemMasterManager.DeleteModelWiseItemMasterFromDB(modelWiseItemMasterId);

                return isModelWiseItemMasterDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<ModelWiseItemMasterDto> GetItemMasterById(Guid modelWiseItemMasterId)
        {
            try
            {
                var modelWiseItemMasterData = await this._modelWiseItemMasterManager.GetModelWiseItemMasterByIdFromDB(modelWiseItemMasterId);

                if (modelWiseItemMasterData.Id != Guid.Empty)
                {
                    modelWiseItemMasterData.ModelWiseItemData = await this._modelWiseItemManager.GetModelWiseItemListFromDB(modelWiseItemMasterId);
                }
                return modelWiseItemMasterData;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
