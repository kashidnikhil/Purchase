namespace MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionMaster
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster;
    using MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionItemMaster;
    using System;
    using System.Threading.Tasks;

    public class MaterialRequisitionAppService : MyTraining1101DemoAppServiceBase, IMaterialRequisitionAppService
    {
        private readonly IMaterialRequisitionManager _materialRequisitionManager;
        private readonly IMaterialRequisitionItemManager _materialRequisitionItemManager;

        public MaterialRequisitionAppService(
          IMaterialRequisitionManager materialRequisitionManager,
          IMaterialRequisitionItemManager materialRequisitionItemManager
         )
        {
            _materialRequisitionManager = materialRequisitionManager;
            _materialRequisitionItemManager = materialRequisitionItemManager;
        }

        public async Task<string> GetLatestMaterialRequisitionNumber() {
            try
            {
                var result = await this._materialRequisitionManager.GetLatestMRINumberFromDb();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<PagedResultDto<MaterialRequisitionMasterListDto>> GetMaterialRequisitions(MaterialRequisitionSearchDto input)
        {
            try
            {
                var result = await this._materialRequisitionManager.GetPaginatedMaterialRequisitionListFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateMaterialRequisition(MaterialRequisitionInputDto input)
        {
            try
            {
                var insertedOrUpdatedMaterialRequisition = await this._materialRequisitionManager.InsertOrUpdateMaterialRequisitionIntoDB(input);

                if (insertedOrUpdatedMaterialRequisition.Id != Guid.Empty)
                {
                    await this.InsertOrUpdateMaterialRequisitionItem(input, (Guid)insertedOrUpdatedMaterialRequisition.Id);
                }

                return insertedOrUpdatedMaterialRequisition;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteMaterialRequisition(Guid materialRequisitionId)
        {
            try
            {
                var isMaterialRequisitionDeleted = await this._materialRequisitionManager.DeleteMaterialRequisitionFromDB(materialRequisitionId);
                return isMaterialRequisitionDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<MaterialRequisitionDto> GetMaterialRequisitionById(Guid materialRequisitionId)
        {
            try
            {
                var response = await this._materialRequisitionManager.GetMaterialRequisitionByIdFromDB(materialRequisitionId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        private async Task InsertOrUpdateMaterialRequisitionItem(MaterialRequisitionInputDto input, Guid insertedOrUpdatedMaterialRequisitionId)
        {
            try
            {
                if (input.MaterialRequisitionItems != null && input.MaterialRequisitionItems.Count > 0)
                {
                    input.MaterialRequisitionItems.ForEach(MaterialRequisitionItem =>
                    {
                        MaterialRequisitionItem.MaterialRequisitionId = insertedOrUpdatedMaterialRequisitionId;
                    });
                    await this._materialRequisitionItemManager.BulkInsertOrUpdateMaterialRequisitionItems(input.MaterialRequisitionItems);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
