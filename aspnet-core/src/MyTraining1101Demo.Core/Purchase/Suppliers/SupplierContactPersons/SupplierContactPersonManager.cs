

namespace MyTraining1101Demo.Purchase.Suppliers.SupplierContactPersons
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierContactPersons;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SupplierContactPersonManager : MyTraining1101DemoDomainServiceBase, ISupplierContactPersonManager
    {
        private readonly IRepository<SupplierContactPerson, Guid> _supplierContactPersonRepository;

        public SupplierContactPersonManager(
          IRepository<SupplierContactPerson, Guid> supplierContactPersonRepository)
        {
            _supplierContactPersonRepository = supplierContactPersonRepository;
        }
        public async Task<Guid> BulkInsertOrUpdateSupplierContactPersons(List<SupplierContactPersonInputDto> supplierContactPersonInputList)
        {
            try
            {
                Guid supplierId = Guid.Empty;
                var mappedSupplierContactPersons = ObjectMapper.Map<List<SupplierContactPerson>>(supplierContactPersonInputList);
                for (int i = 0; i < mappedSupplierContactPersons.Count; i++)
                {
                    supplierId = (Guid)mappedSupplierContactPersons[i].SupplierId;
                    await this.InsertOrUpdateSupplierContactPersonIntoDB(mappedSupplierContactPersons[i]);
                }
                return supplierId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateSupplierContactPersonIntoDB(SupplierContactPerson input)
        {
            try
            {
                var supplierContactPersonId = await this._supplierContactPersonRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteSupplierContactPersons(Guid supplierId)
        {

            try
            {
                var supplierContactPersons = await this.GetSupplierContactPersonListFromDB(supplierId);

                if (supplierContactPersons.Count > 0)
                {
                    for (int i = 0; i < supplierContactPersons.Count; i++)
                    {
                        await this.DeleteSupplierContactPersonFromDB(supplierContactPersons[i].Id);
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
        private async Task DeleteSupplierContactPersonFromDB(Guid supplierContactPersonId)
        {
            try
            {
                var supplierContactPersonItem = await this._supplierContactPersonRepository.GetAsync(supplierContactPersonId);

                await this._supplierContactPersonRepository.DeleteAsync(supplierContactPersonItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SupplierContactPersonDto>> GetSupplierContactPersonListFromDB(Guid supplierId)
        {
            try
            {
                var supplierContactPersonQuery = this._supplierContactPersonRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.SupplierId == supplierId);

                return new List<SupplierContactPersonDto>(ObjectMapper.Map<List<SupplierContactPersonDto>>(supplierContactPersonQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
