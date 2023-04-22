namespace MyTraining1101Demo.Purchase.Companies.CompanyAddresses
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanyAddressManager : MyTraining1101DemoDomainServiceBase, ICompanyAddressManager
    {
        private readonly IRepository<CompanyAddress, Guid> _companyAddressRepository;

        public CompanyAddressManager(
          IRepository<CompanyAddress, Guid> companyAddressRepository)
        {
            _companyAddressRepository = companyAddressRepository;
        }
        public async Task<Guid> BulkInsertOrUpdateCompanyAddresses(List<CompanyAddressInputDto> companyAddressInputList)
        {
            try
            {
                Guid companyId = Guid.Empty;
                var mappedCompanyAddresses = ObjectMapper.Map<List<CompanyAddress>>(companyAddressInputList);
                for (int i = 0; i < mappedCompanyAddresses.Count; i++)
                {
                    companyId = (Guid)mappedCompanyAddresses[i].CompanyId;
                    await this.InsertOrUpdateCompanyAddressIntoDB(mappedCompanyAddresses[i]);
                }
                return companyId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateCompanyAddressIntoDB(CompanyAddress input)
        {
            try
            {
                var companyAddressId = await this._companyAddressRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteCompanyAddresses(Guid companyId)
        {

            try
            {
                var companyAddresses = await this.GetCompanyAddressListFromDB(companyId);

                if (companyAddresses.Count > 0)
                {
                    for (int i = 0; i < companyAddresses.Count; i++)
                    {
                        await this.DeleteCompanyAddressFromDB(companyAddresses[i].Id);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task DeleteCompanyAddressFromDB(Guid companyAddressId)
        {
            try
            {
                var companyAddressItem = await this._companyAddressRepository.GetAsync(companyAddressId);

                await this._companyAddressRepository.DeleteAsync(companyAddressItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<CompanyAddressDto>> GetCompanyAddressListFromDB(Guid companyId)
        {
            try
            {
                var companyAddressQuery = this._companyAddressRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.CompanyId == companyId);

                return new List<CompanyAddressDto>(ObjectMapper.Map<List<CompanyAddressDto>>(companyAddressQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }
    }
}
