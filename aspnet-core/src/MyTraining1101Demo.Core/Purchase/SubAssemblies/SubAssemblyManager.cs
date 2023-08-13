namespace MyTraining1101Demo.Purchase.SubAssemblies
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
    using MyTraining1101Demo.Purchase.SubAssemblies.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class SubAssemblyManager : MyTraining1101DemoDomainServiceBase, ISubAssemblyManager
    {
        private readonly IRepository<SubAssembly, Guid> _subAssemblyRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public SubAssemblyManager(
           IRepository<SubAssembly, Guid> subAssemblyRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _subAssemblyRepository = subAssemblyRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<SubAssemblyListDto>> GetPaginatedSubAssembliesFromDB(SubAssemblySearchDto input)
        {
            try
            {
                var subAssemblyQuery = this._subAssemblyRepository.GetAllIncluding(x=> x.Assembly)
                    .Include(x=> x.Model)
                    .Where(x => !x.IsDeleted)
                    .Select(x=> new SubAssemblyListDto {
                        Id = x.Id,
                        Name = x.Name,
                        ModelName = x.Model.Name,
                        AssemblyName = x.Assembly.Name
                    })
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()) || item.ModelName.ToLower().Contains(input.SearchString.ToLower()) || item.AssemblyName.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await subAssemblyQuery.CountAsync();
                var subAssemblies = await subAssemblyQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<SubAssemblyListDto>(totalCount, subAssemblies);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateSubAssemblyIntoDB(SubAssemblyInputDto input)
        {
            try
            {
                Guid subAssemblyId = Guid.Empty;
                var subAssembly = await this._subAssemblyRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.AssemblyId == input.AssemblyId && x.ModelId == input.ModelId && x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (subAssembly != null)
                {
                    if (input.Id != subAssembly.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = subAssembly.Name,
                            IsExistingDataAlreadyDeleted = subAssembly.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = subAssembly.Id
                        };
                    }
                    else
                    {
                        ObjectMapper.Map(input, subAssembly);
                        subAssemblyId = await this._subAssemblyRepository.InsertOrUpdateAndGetIdAsync(subAssembly);
                        return new ResponseDto
                        {
                            Id = subAssemblyId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedSubAssemblyItem = ObjectMapper.Map<SubAssembly>(input);
                    subAssemblyId = await this._subAssemblyRepository.InsertOrUpdateAndGetIdAsync(mappedSubAssemblyItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = subAssemblyId,
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
        public async Task<bool> DeleteSubAssemblyFromDB(Guid subAssemblyItemId)
        {
            try
            {
                var subAssembly = await this._subAssemblyRepository.GetAsync(subAssemblyItemId);

                await this._subAssemblyRepository.DeleteAsync(subAssembly);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<SubAssemblyDto> GetSubAssemblyByIdFromDB(Guid subAssemblyItemId)
        {
            try
            {
                var subAssembly = await this._subAssemblyRepository.GetAsync(subAssemblyItemId);

                return ObjectMapper.Map<SubAssemblyDto>(subAssembly);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SubAssemblyDto>> GetSubAssemblyListFromDB(Guid? assemblyId)
        {
            try
            {
                var subAssemblyQuery = this._subAssemblyRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(assemblyId != null && assemblyId != Guid.Empty, x => x.AssemblyId == assemblyId);

                return new List<SubAssemblyDto>(ObjectMapper.Map<List<SubAssemblyDto>>(subAssemblyQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreSubAssembly(Guid subAssemblyId)
        {
            try
            {
                var subAssembly = await this._subAssemblyRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == subAssemblyId);
                subAssembly.IsDeleted = false;
                subAssembly.DeleterUserId = null;
                subAssembly.DeletionTime = null;
                await this._subAssemblyRepository.UpdateAsync(subAssembly);

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
