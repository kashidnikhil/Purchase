namespace MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionMaster
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.SupplierCategories.Dto;
    using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class MaterialRequisitionManager : MyTraining1101DemoDomainServiceBase, IMaterialRequisitionManager
    {
        private readonly IRepository<MaterialRequisition, Guid> _materialRequisitionRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public MaterialRequisitionManager(
           IRepository<MaterialRequisition, Guid> materialRequisitionRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _materialRequisitionRepository = materialRequisitionRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<string> GetLatestMRINumberFromDb() {
            var materialRequisitionItem = await this._materialRequisitionRepository.GetAll().IgnoreQueryFilters().OrderByDescending(x => x.CreationTime).FirstAsync();
            return materialRequisitionItem.MRINumber;
        }

        public async Task<PagedResultDto<MaterialRequisitionMasterListDto>> GetPaginatedMaterialRequisitionListFromDB(MaterialRequisitionSearchDto input)
        {
            try
            {
                var itemMasterQuery = this._materialRequisitionRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.MRINumber.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await itemMasterQuery.CountAsync();
                var items = await itemMasterQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<MaterialRequisitionMasterListDto>(
                totalCount,
                ObjectMapper.Map<List<MaterialRequisitionMasterListDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateMaterialRequisitionIntoDB(MaterialRequisitionInputDto input)
        {
            try
            {
                Guid materialRequisitionId = Guid.Empty;
                
                var mappedMaterialRequisition = ObjectMapper.Map<MaterialRequisition>(input);
                materialRequisitionId = await this._materialRequisitionRepository.InsertOrUpdateAndGetIdAsync(mappedMaterialRequisition);
                await CurrentUnitOfWork.SaveChangesAsync();
                return new ResponseDto
                {
                     Id = materialRequisitionId,
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
        public async Task<bool> DeleteMaterialRequisitionFromDB(Guid materialRequisitionId)
        {
            try
            {
                var materialRequisition = await this._materialRequisitionRepository.GetAsync(materialRequisitionId);

                await this._materialRequisitionRepository.DeleteAsync(materialRequisition);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        public async Task<MaterialRequisitionDto> GetMaterialRequisitionByIdFromDB(Guid materialRequisitionId)
        {
            try
            {
                var materialRequisitionItem = await this._materialRequisitionRepository.GetAsync(materialRequisitionId);

                return ObjectMapper.Map<MaterialRequisitionDto>(materialRequisitionItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
