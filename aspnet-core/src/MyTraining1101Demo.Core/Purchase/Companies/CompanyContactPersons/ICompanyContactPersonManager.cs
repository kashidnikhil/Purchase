namespace MyTraining1101Demo.Purchase.Companies.CompanyContactPersons
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyContactPersons;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanyContactPersonManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateCompanyContactPersons(List<CompanyContactPersonInputDto> companyContactPersonInputList);

        Task<bool> BulkDeleteCompanyContactPersons(Guid companyId);

        Task<IList<CompanyContactPersonDto>> GetCompanyContactPersonListFromDB(Guid companyId);
    }
}
