namespace MyTraining1101Demo.Purchase.Assemblies
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Assemblies.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class AssemblyManager : MyTraining1101DemoDomainServiceBase, IAssemblyManager
    {
        private readonly IRepository<Assembly, Guid> _assemblyRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public AssemblyManager(
           IRepository<Assembly, Guid> assemblyRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _assemblyRepository = assemblyRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<AssemblyListDto>> GetPaginatedAssembliesFromDB(AssemblySearchDto input)
        {
            try
            {
                var assemblyQuery = this._assemblyRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await assemblyQuery.CountAsync();
                var assemblyItems = await assemblyQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<AssemblyListDto>(
                totalCount,
                ObjectMapper.Map<List<AssemblyListDto>>(assemblyItems));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateAssemblyIntoDB(AssemblyInputDto input)
        {
            try
            {
                Guid assemblyId = Guid.Empty;
                var assemblyItem = await this._assemblyRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (assemblyItem != null)
                {
                    if (input.Id != assemblyItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = assemblyItem.Name,
                            IsExistingDataAlreadyDeleted = assemblyItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = assemblyItem.Id
                        };
                    }
                    else
                    {
                        ObjectMapper.Map(input, assemblyItem);
                        assemblyId = await this._assemblyRepository.InsertOrUpdateAndGetIdAsync(assemblyItem);
                        return new ResponseDto
                        {
                            Id = assemblyId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedAssemblyItem = ObjectMapper.Map<Assembly>(input);
                    assemblyId = await this._assemblyRepository.InsertOrUpdateAndGetIdAsync(mappedAssemblyItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = assemblyId,
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
        public async Task<bool> DeleteAssemblyFromDB(Guid assemblyId)
        {
            try
            {
                var assemblyItem = await this._assemblyRepository.GetAsync(assemblyId);

                await this._assemblyRepository.DeleteAsync(assemblyItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<AssemblyDto> GetAssemblyByIdFromDB(Guid assemblyId)
        {
            try
            {
                var assemblyItem = await this._assemblyRepository.GetAsync(assemblyId);

                return ObjectMapper.Map<AssemblyDto>(assemblyItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<AssemblyDto>> GetAssemblyListFromDB(Guid? modelId)
        {
            try
            {
                var assemblyQuery = this._assemblyRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(modelId != null && modelId != Guid.Empty, x=> x.ModelId == modelId);

                return new List<AssemblyDto>(ObjectMapper.Map<List<AssemblyDto>>(assemblyQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreAssembly(Guid assemblyId)
        {
            try
            {
                var assemblyItem = await this._assemblyRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == assemblyId);
                assemblyItem.IsDeleted = false;
                assemblyItem.DeleterUserId = null;
                assemblyItem.DeletionTime = null;
                await this._assemblyRepository.UpdateAsync(assemblyItem);

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
