namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class ModelWiseItemManager : MyTraining1101DemoDomainServiceBase,IModelWiseItemManager
    {
        private readonly IRepository<ModelWiseItem, Guid> _modelWiseItemRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ModelWiseItemManager(
           IRepository<ModelWiseItem, Guid> modelWiseItemRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _modelWiseItemRepository = modelWiseItemRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }


        public async Task<Guid> BulkInsertOrUpdateModelWiseItems(List<ModelWiseItemInputDto> itemCalibrationTypeInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedModelWiseItems = ObjectMapper.Map<List<ModelWiseItem>>(itemCalibrationTypeInputList);
                for (int i = 0; i < mappedModelWiseItems.Count; i++)
                {
                    itemId = (Guid)mappedModelWiseItems[i].ItemId;
                    await this.InsertOrUpdateModelWiseItemIntoDB(mappedModelWiseItems[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateModelWiseItemIntoDB(ModelWiseItem input)
        {
            try
            {
                var modelWiseItemId = await this._modelWiseItemRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteModelWiseItems(Guid modelWiseItemMasterId)
        {
            try
            {
                bool isModelWiseItemsDeleted = false;
                var modelWiseItems = await this.GetModelWiseItemListFromDB(modelWiseItemMasterId);

                if (modelWiseItems.Count > 0)
                {
                    for (int i = 0; i < modelWiseItems.Count; i++)
                    {
                        isModelWiseItemsDeleted = await this.DeleteModelWiseItemFromDB(modelWiseItems[i].Id);
                    }
                }

                return isModelWiseItemsDeleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<bool> DeleteModelWiseItemFromDB(Guid modelWiseItemId)
        {
            try
            {
                var modelWiseItem = await this._modelWiseItemRepository.GetAsync(modelWiseItemId);

                await this._modelWiseItemRepository.DeleteAsync(modelWiseItem);

                await CurrentUnitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ModelWiseItemDto>> GetModelWiseItemListFromDB(Guid modelWiseItemMasterId)
        {
            try
            {
                var modelWiseItemQuery = this._modelWiseItemRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ModelWiseItemMasterId == modelWiseItemMasterId);

                return new List<ModelWiseItemDto>(ObjectMapper.Map<List<ModelWiseItemDto>>(modelWiseItemQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<IList<ModelWiseItemDto>> GetModelWiseItemListByModelIdFromDB(Guid modelId)
        {
            try
            {
                var modelWiseItemQuery = this._modelWiseItemRepository.GetAllIncluding(x=> x.Item)
                    .Include(x=> x.ModelWiseItemMaster).ThenInclude(x=> x.Model)
                    .Where(x => !x.IsDeleted && x.ModelWiseItemMaster.ModelId == modelId);

                return new List<ModelWiseItemDto>(ObjectMapper.Map<List<ModelWiseItemDto>>(modelWiseItemQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }
    }
}
