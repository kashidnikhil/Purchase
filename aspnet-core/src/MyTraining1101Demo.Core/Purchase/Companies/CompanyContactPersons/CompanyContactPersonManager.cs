namespace MyTraining1101Demo.Purchase.Companies.CompanyContactPersons
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyContactPersons;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanyContactPersonManager : MyTraining1101DemoDomainServiceBase, ICompanyContactPersonManager
    {
        private readonly IRepository<CompanyContactPerson, Guid> _companyContactPersonRepository;

        public CompanyContactPersonManager(
          IRepository<CompanyContactPerson, Guid> companyContactPersonRepository)
        {
            _companyContactPersonRepository = companyContactPersonRepository;
        }
        public async Task<Guid> BulkInsertOrUpdateCompanyContactPersons(List<CompanyContactPersonInputDto> companyContactPersonInputList)
        {
            try
            {
                Guid companyId = Guid.Empty;
                var mappedCompanyContactPersons = ObjectMapper.Map<List<CompanyContactPerson>>(companyContactPersonInputList);
                for (int i = 0; i < mappedCompanyContactPersons.Count; i++)
                {
                    companyId = (Guid)mappedCompanyContactPersons[i].CompanyId;
                    await this.InsertOrUpdateCompanyContactPersonIntoDB(mappedCompanyContactPersons[i]);
                }
                return companyId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateCompanyContactPersonIntoDB(CompanyContactPerson input)
        {
            try
            {
                var companyContactPersonId = await this._companyContactPersonRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteCompanyContactPersons(Guid companyId)
        {

            try
            {
                var companyContactPersons = await this.GetCompanyContactPersonListFromDB(companyId);

                if (companyContactPersons.Count > 0)
                {
                    for (int i = 0; i < companyContactPersons.Count; i++)
                    {
                        await this.DeleteCompanyContactPersonFromDB(companyContactPersons[i].Id);
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
        private async Task DeleteCompanyContactPersonFromDB(Guid companyContactPersonId)
        {
            try
            {
                var companyContactPersonItem = await this._companyContactPersonRepository.GetAsync(companyContactPersonId);

                await this._companyContactPersonRepository.DeleteAsync(companyContactPersonItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<CompanyContactPersonDto>> GetCompanyContactPersonListFromDB(Guid companyId)
        {
            try
            {
                var companyContactPersonQuery = this._companyContactPersonRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.CompanyId == companyId);

                return new List<CompanyContactPersonDto>(ObjectMapper.Map<List<CompanyContactPersonDto>>(companyContactPersonQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
