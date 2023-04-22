namespace MyTraining1101Demo.Purchase.Companies.CompanyAddresses
{
    using Abp.Domain.Services;
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses;

    public interface ICompanyAddressManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateCompanyAddresses(List<CompanyAddressInputDto> companyAddressInputList);

        Task<bool> BulkDeleteCompanyAddresses(Guid companyId);

        Task<IList<CompanyAddressDto>> GetCompanyAddressListFromDB(Guid companyId);
    }
}
