namespace MyTraining1101Demo.Purchase.MaterialGrades
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.MaterialGrades.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class MaterialGradeManager : MyTraining1101DemoDomainServiceBase, IMaterialGradeManager
    {
        private readonly IRepository<MaterialGrade, Guid> _materialGradeRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public MaterialGradeManager(
           IRepository<MaterialGrade, Guid> materialGradeRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _materialGradeRepository = materialGradeRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<MaterialGradeDto>> GetPaginatedMaterialGradesFromDB(MaterialGradeSearchDto input)
        {
            try
            {
                var MaterialGradeQuery = this._materialGradeRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await MaterialGradeQuery.CountAsync();
                var items = await MaterialGradeQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<MaterialGradeDto>(
                totalCount,
                ObjectMapper.Map<List<MaterialGradeDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateMaterialGradeIntoDB(MaterialGradeInputDto input)
        {
            try
            {
                Guid MaterialGradeId = Guid.Empty;
                var MaterialGradeItem = await this._materialGradeRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (MaterialGradeItem != null)
                {
                    if (input.Id != MaterialGradeItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = MaterialGradeItem.Name,
                            IsExistingDataAlreadyDeleted = MaterialGradeItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = MaterialGradeItem.Id
                        };
                    }
                    else
                    {
                        MaterialGradeItem.Name = input.Name;
                        MaterialGradeItem.Description = input.Description;
                        MaterialGradeId = await this._materialGradeRepository.InsertOrUpdateAndGetIdAsync(MaterialGradeItem);
                        return new ResponseDto
                        {
                            Id = MaterialGradeId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedMaterialGradeItem = ObjectMapper.Map<MaterialGrade>(input);
                    MaterialGradeId = await this._materialGradeRepository.InsertOrUpdateAndGetIdAsync(mappedMaterialGradeItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = MaterialGradeId,
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
        public async Task<bool> DeleteMaterialGradeFromDB(Guid materialGradeId)
        {
            try
            {
                var MaterialGradeItem = await this._materialGradeRepository.GetAsync(materialGradeId);

                await this._materialGradeRepository.DeleteAsync(MaterialGradeItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<MaterialGradeDto> GetMaterialGradeByIdFromDB(Guid materialGradeId)
        {
            try
            {
                var MaterialGradeItem = await this._materialGradeRepository.GetAsync(materialGradeId);

                return ObjectMapper.Map<MaterialGradeDto>(MaterialGradeItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<MaterialGradeDto>> GetMaterialGradeListFromDB()
        {
            try
            {
                var MaterialGradeQuery = this._materialGradeRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<MaterialGradeDto>(ObjectMapper.Map<List<MaterialGradeDto>>(MaterialGradeQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreMaterialGrade(Guid materialGradeId)
        {
            try
            {
                var MaterialGradeItem = await this._materialGradeRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == materialGradeId);
                MaterialGradeItem.IsDeleted = false;
                MaterialGradeItem.DeleterUserId = null;
                MaterialGradeItem.DeletionTime = null;
                await this._materialGradeRepository.UpdateAsync(MaterialGradeItem);

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
