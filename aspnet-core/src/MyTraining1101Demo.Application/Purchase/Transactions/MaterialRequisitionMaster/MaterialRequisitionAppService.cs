namespace MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionMaster
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster;
    using System;
    using System.Threading.Tasks;

    public class MaterialRequisitionAppService : MyTraining1101DemoAppServiceBase, IMaterialRequisitionAppService
    {
        private readonly IMaterialRequisitionManager _materialRequisitionManager;

        public MaterialRequisitionAppService(
          IMaterialRequisitionManager materialRequisitionManager
         )
        {
            _materialRequisitionManager = materialRequisitionManager;
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

        public async Task<MaterialRequisitionDto> GetSupplierCategoryById(Guid materialRequisitionId)
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
    }
}
