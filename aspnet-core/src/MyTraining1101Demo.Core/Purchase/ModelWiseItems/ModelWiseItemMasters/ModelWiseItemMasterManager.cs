namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.SubAssemblies.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class ModelWiseItemMasterManager : MyTraining1101DemoDomainServiceBase, IModelWiseItemMasterManager
    {
        private readonly IRepository<ModelWiseItemMaster, Guid> _modelWiseItemMasterRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ModelWiseItemMasterManager(
           IRepository<ModelWiseItemMaster, Guid> modelWiseItemMasterRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _modelWiseItemMasterRepository = modelWiseItemMasterRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<ModelWiseItemMasterListDto>> GetPaginatedModelWiseItemMastersFromDB(ModelWiseItemMasterSearchDto input)
        {
            try
            {
                var modelWiseItemMasterQuery = this._modelWiseItemMasterRepository.GetAllIncluding(x=> x.Model)
                    .Where(x => !x.IsDeleted)
                    .Select(x => new ModelWiseItemMasterListDto
                    {
                        Id = x.Id,
                        ModelName = x.Model.Name
                    })
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.ModelName.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await modelWiseItemMasterQuery.CountAsync();
                var items = await modelWiseItemMasterQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<ModelWiseItemMasterListDto>(totalCount,items);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateModelWiseItemMasterIntoDB(ModelWiseItemMasterInputDto input)
        {
            try
            {
                Guid modelWiseItemMasterId = Guid.Empty;
                var mappedModelWiseItemMaster = ObjectMapper.Map<ModelWiseItemMaster>(input);
                modelWiseItemMasterId = await this._modelWiseItemMasterRepository.InsertOrUpdateAndGetIdAsync(mappedModelWiseItemMaster);
                await CurrentUnitOfWork.SaveChangesAsync();
                return new ResponseDto
                {
                        Id = modelWiseItemMasterId,
                        DataMatchFound = false
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        [UnitOfWork]
        public async Task<bool> DeleteModelWiseItemMasterFromDB(Guid modelWiseItemMasterId)
        {
            try
            {
                var ModelWiseItemMaster = await this._modelWiseItemMasterRepository.GetAsync(modelWiseItemMasterId);

                await this._modelWiseItemMasterRepository.DeleteAsync(ModelWiseItemMaster);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<ModelWiseItemMasterDto> GetModelWiseItemMasterByIdFromDB(Guid modelWiseItemMasterId)
        {
            try
            {
                var modelWiseItemMaster = await this._modelWiseItemMasterRepository.GetAsync(modelWiseItemMasterId);

                return ObjectMapper.Map<ModelWiseItemMasterDto>(modelWiseItemMaster);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        //public async Task<IList<LegalEntityDto>> GetLegalEntityListFromDB()
        //{
        //    try
        //    {
        //        var LegalEntityQuery = this._legalEntityRepository.GetAll()
        //            .Where(x => !x.IsDeleted);

        //        return new List<LegalEntityDto>(ObjectMapper.Map<List<LegalEntityDto>>(LegalEntityQuery));
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex.Message, ex);
        //        throw ex;
        //    }

        //}
    }
}
