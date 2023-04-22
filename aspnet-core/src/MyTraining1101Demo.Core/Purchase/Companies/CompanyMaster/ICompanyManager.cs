namespace MyTraining1101Demo.Purchase.Companies.CompanyMaster
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster;
    using System;
    using System.Threading.Tasks;

    public interface ICompanyManager : IDomainService
    {
        Task<PagedResultDto<CompanyListDto>> GetPaginatedCompanyListFromDB(CompanySearchDto input);

        Task<Guid> InsertOrUpdateCompanyMasterIntoDB(CompanyInputDto input);

        Task<bool> DeleteCompanyMasterFromDB(Guid companyId);

        Task<CompanyDto> GetCompanyMasterByIdFromDB(Guid companyId);
    }
}
