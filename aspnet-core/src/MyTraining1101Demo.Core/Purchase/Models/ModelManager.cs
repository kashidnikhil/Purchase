namespace MyTraining1101Demo.Purchase.Models
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Models.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class ModelManager
    : MyTraining1101DemoDomainServiceBase, IModelManager
    {
        private readonly IRepository<Model, Guid> _modelRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ModelManager(
           IRepository<Model, Guid> modelRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _modelRepository = modelRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<ModelDto>> GetPaginatedModelsFromDB(ModelSearchDto input)
        {
            try
            {
                var ModelQuery = this._modelRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await ModelQuery.CountAsync();
                var items = await ModelQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<ModelDto>(
                totalCount,
                ObjectMapper.Map<List<ModelDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateModelIntoDB(ModelInputDto input)
        {
            try
            {
                Guid ModelId = Guid.Empty;
                var ModelItem = await this._modelRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (ModelItem != null)
                {
                    if (input.Id != ModelItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = ModelItem.Name,
                            IsExistingDataAlreadyDeleted = ModelItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = ModelItem.Id
                        };
                    }
                    else
                    {
                        ModelItem.Name = input.Name;
                        ModelItem.Description = input.Description;
                        ModelId = await this._modelRepository.InsertOrUpdateAndGetIdAsync(ModelItem);
                        return new ResponseDto
                        {
                            Id = ModelId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedModelItem = ObjectMapper.Map<Model>(input);
                    ModelId = await this._modelRepository.InsertOrUpdateAndGetIdAsync(mappedModelItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = ModelId,
                        DataMatchFound = false
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<bool> DeleteModelFromDB(Guid modelId)
        {
            try
            {
                var ModelItem = await this._modelRepository.GetAsync(modelId);

                await this._modelRepository.DeleteAsync(ModelItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ModelDto> GetModelByIdFromDB(Guid modelId)
        {
            try
            {
                var ModelItem = await this._modelRepository.GetAsync(modelId);

                return ObjectMapper.Map<ModelDto>(ModelItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ModelDto>> GetModelListFromDB()
        {
            try
            {
                var ModelQuery = this._modelRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<ModelDto>(ObjectMapper.Map<List<ModelDto>>(ModelQuery));
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
                var ModelItem = await this._modelRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == modelId);
                ModelItem.IsDeleted = false;
                ModelItem.DeleterUserId = null;
                ModelItem.DeletionTime = null;
                await this._modelRepository.UpdateAsync(ModelItem);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
