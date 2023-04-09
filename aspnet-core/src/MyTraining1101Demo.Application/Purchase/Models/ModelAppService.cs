using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.Models.Dto;
using MyTraining1101Demo.Purchase.Models;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Models
{
    public class ModelAppService : MyTraining1101DemoAppServiceBase, IModelAppService
    {
        private readonly IModelManager _modelManager;

        public ModelAppService(
          IModelManager modelManager
         )
        {
            _modelManager = modelManager;
        }


        public async Task<PagedResultDto<ModelDto>> GetModels(ModelSearchDto input)
        {
            try
            {
                var result = await this._modelManager.GetPaginatedModelsFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateModel(ModelInputDto input)
        {
            try
            {
                var insertedOrUpdatedModelId = await this._modelManager.InsertOrUpdateModelIntoDB(input);

                return insertedOrUpdatedModelId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteModel(Guid modelId)
        {
            try
            {
                var isModelDeleted = await this._modelManager.DeleteModelFromDB(modelId);
                return isModelDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<ModelDto> GetModelById(Guid modelId)
        {
            try
            {
                var response = await this._modelManager.GetModelByIdFromDB(modelId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ModelDto>> GetModelList()
        {
            try
            {
                var response = await this._modelManager.GetModelListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreModel(Guid modelId)
        {
            try
            {
                var response = await this._modelManager.RestoreModel(modelId);
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
