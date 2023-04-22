namespace MyTraining1101Demo.Purchase.Companies
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster;
    using System;
    using System.Threading.Tasks;

    public interface ICompanyAppService
    {
        Task<PagedResultDto<CompanyListDto>> GetCompanies(CompanySearchDto input);

        Task<Guid> InsertOrUpdateCompany(CompanyInputDto input);

        Task<bool> DeleteCompanyMasterData(Guid companyId);

        Task<CompanyDto> GetCompanyMasterById(Guid companyId);
    }
}
