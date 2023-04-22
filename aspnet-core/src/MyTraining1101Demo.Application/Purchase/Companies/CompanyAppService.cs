namespace MyTraining1101Demo.Purchase.Companies
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Companies.CompanyAddresses;
    using MyTraining1101Demo.Purchase.Companies.CompanyContactPersons;
    using MyTraining1101Demo.Purchase.Companies.CompanyMaster;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster;
    using System;
    using System.Threading.Tasks;

    public class CompanyAppService : MyTraining1101DemoAppServiceBase, ICompanyAppService
    {
        private readonly ICompanyManager _companyManager;
        private readonly ICompanyContactPersonManager _companyContactPersonManager;
        private readonly ICompanyAddressManager _companyAddressManager;
        
        public CompanyAppService(
            ICompanyManager companyManager,
            ICompanyContactPersonManager companyContactPersonManager,
            ICompanyAddressManager companyAddressManager
         )
        {
            _companyManager = companyManager;
            _companyContactPersonManager = companyContactPersonManager;
            _companyAddressManager = companyAddressManager;
        }

        public async Task<PagedResultDto<CompanyListDto>> GetCompanies(CompanySearchDto input)
        {
            try
            {
                var result = await this._companyManager.GetPaginatedCompanyListFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<Guid> InsertOrUpdateCompany(CompanyInputDto input)
        {
            try
            {
                var insertedOrUpdatedCompanyId = await this._companyManager.InsertOrUpdateCompanyMasterIntoDB(input);

                if (insertedOrUpdatedCompanyId != Guid.Empty)
                {
                    if (input.CompanyAddresses != null && input.CompanyAddresses.Count > 0)
                    {
                        input.CompanyAddresses.ForEach(companyAddressItem =>
                        {
                            companyAddressItem.CompanyId = insertedOrUpdatedCompanyId;
                        });
                        await this._companyAddressManager.BulkInsertOrUpdateCompanyAddresses(input.CompanyAddresses);
                    }

                    if (input.CompanyContactPersons != null && input.CompanyContactPersons.Count > 0)
                    {
                        input.CompanyContactPersons.ForEach(companyContactPersonItem =>
                        {
                            companyContactPersonItem.CompanyId = insertedOrUpdatedCompanyId;
                        });
                        await this._companyContactPersonManager.BulkInsertOrUpdateCompanyContactPersons(input.CompanyContactPersons);
                    }
                }
                return insertedOrUpdatedCompanyId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteCompanyMasterData(Guid companyId)
        {
            try
            {
                var isCompanyMasterDeleted = await this._companyManager.DeleteCompanyMasterFromDB(companyId);

                var isCompanyAddressesDeleted = await this._companyAddressManager.BulkDeleteCompanyAddresses(companyId);

                var isCompanyContactPersonDeleted = await this._companyContactPersonManager.BulkDeleteCompanyContactPersons(companyId);

                return isCompanyMasterDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<CompanyDto> GetCompanyMasterById(Guid companyId)
        {
            try
            {
                var companyMasterItem = await this._companyManager.GetCompanyMasterByIdFromDB(companyId);

                if (companyMasterItem.Id != Guid.Empty)
                {
                    companyMasterItem.CompanyAddresses = await this._companyAddressManager.GetCompanyAddressListFromDB(companyMasterItem.Id);
                    companyMasterItem.CompanyContactPersons = await this._companyContactPersonManager.GetCompanyContactPersonListFromDB(companyMasterItem.Id);
                }

                return companyMasterItem;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

    }

}
