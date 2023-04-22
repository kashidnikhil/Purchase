namespace MyTraining1101Demo.Purchase.Companies.CompanyMaster
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class CompanyManager : MyTraining1101DemoDomainServiceBase, ICompanyManager
    {
        private readonly IRepository<Company, Guid> _companyRepository;

        public CompanyManager(
           IRepository<Company, Guid> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<PagedResultDto<CompanyListDto>> GetPaginatedCompanyListFromDB(CompanySearchDto input)
        {
            try
            {
                var companyMasterQuery = this._companyRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));


                var totalCount = await companyMasterQuery.CountAsync();
                var items = await companyMasterQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<CompanyListDto>(
                totalCount,
                ObjectMapper.Map<List<CompanyListDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<Guid> InsertOrUpdateCompanyMasterIntoDB(CompanyInputDto input)
        {
            try
            {
                var mappedCompanyMasterItem = ObjectMapper.Map<Company>(input);
                var companyId = await this._companyRepository.InsertOrUpdateAndGetIdAsync(mappedCompanyMasterItem);
                await CurrentUnitOfWork.SaveChangesAsync();
                return companyId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<bool> DeleteCompanyMasterFromDB(Guid companyId)
        {
            try
            {
                var companyMasterItem = await this._companyRepository.GetAsync(companyId);

                await this._companyRepository.DeleteAsync(companyMasterItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<CompanyDto> GetCompanyMasterByIdFromDB(Guid companyId)
        {
            try
            {
                var companyMasterItem = await this._companyRepository.GetAsync(companyId);

                return ObjectMapper.Map<CompanyDto>(companyMasterItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
