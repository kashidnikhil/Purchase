namespace MyTraining1101Demo.Purchase.AssemblyCategories
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.AssemblyCategories.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    public class AssemblyCategoryManager : MyTraining1101DemoDomainServiceBase,IAssemblyCategoryManager
    {
        private readonly IRepository<AssemblyCategory, Guid> _assemblyCategoryRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public AssemblyCategoryManager(
           IRepository<AssemblyCategory, Guid> assemblyCategoryRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _assemblyCategoryRepository = assemblyCategoryRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<AssemblyCategoryListDto>> GetPaginatedAssemblyCategoriesFromDB(AssemblyCategorySearchDto input)
        {
            try
            {
                var assemblyCategoryQuery = this._assemblyCategoryRepository.GetAllIncluding(x=> x.Model)
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), assemblyCategory => assemblyCategory.Model.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await assemblyCategoryQuery.CountAsync();
                var assemblyCategories = await assemblyCategoryQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<AssemblyCategoryListDto>(
                totalCount,
                ObjectMapper.Map<List<AssemblyCategoryListDto>>(assemblyCategories));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<Guid> InsertOrUpdateAssemblyCategoryIntoDB(AssemblyCategoryInputDto input)
        {
            try
            {
                var mappedAssemblyCategory = ObjectMapper.Map<AssemblyCategory>(input);
                var assemblyCategoryId = await this._assemblyCategoryRepository.InsertOrUpdateAndGetIdAsync(mappedAssemblyCategory);
                await CurrentUnitOfWork.SaveChangesAsync();
                return assemblyCategoryId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<bool> DeleteAssemblyCategoryFromDB(Guid assemblyCategoryId)
        {
            try
            {
                var assemblyCategory = await this._assemblyCategoryRepository.GetAsync(assemblyCategoryId);
                await this._assemblyCategoryRepository.DeleteAsync(assemblyCategory);
                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<AssemblyCategoryDto> GetAssemblyCategoryByIdFromDB(Guid assemblyCategoryId)
        {
            try
            {
                var assemblyCategory = await this._assemblyCategoryRepository.GetAsync(assemblyCategoryId);

                return ObjectMapper.Map<AssemblyCategoryDto>(assemblyCategory);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<AssemblyCategoryDto>> GetAssemblyCategorylListFromDB()
        {
            try
            {
                var assemblyCategoryQuery = this._assemblyCategoryRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<AssemblyCategoryDto>(ObjectMapper.Map<List<AssemblyCategoryDto>>(assemblyCategoryQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }
    }
}
