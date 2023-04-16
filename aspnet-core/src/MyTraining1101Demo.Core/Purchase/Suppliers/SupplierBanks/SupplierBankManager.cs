using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierBanks;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierContactPersons;
using MyTraining1101Demo.Purchase.Suppliers.SupplierContactPersons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Suppliers.SupplierBanks
{
    public class SupplierBankManager : MyTraining1101DemoDomainServiceBase, ISupplierBankManager
    {
        private readonly IRepository<SupplierBank, Guid> _supplierBankRepository;

        public SupplierBankManager(
          IRepository<SupplierBank, Guid> supplierBankRepository)
        {
            _supplierBankRepository = supplierBankRepository;
        }
        public async Task<Guid> BulkInsertOrUpdateSupplierBanks(List<SupplierBankInputDto> supplierBankInputList)
        {
            try
            {
                Guid supplierId = Guid.Empty;
                var mappedSupplierBanks = ObjectMapper.Map<List<SupplierBank>>(supplierBankInputList);
                for (int i = 0; i < mappedSupplierBanks.Count; i++)
                {
                    supplierId = (Guid)mappedSupplierBanks[i].SupplierId;
                    await this.InsertOrUpdateSupplierBankIntoDB(mappedSupplierBanks[i]);
                }
                return supplierId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateSupplierBankIntoDB(SupplierBank input)
        {
            try
            {
                var supplierBankId = await this._supplierBankRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteSupplierBanks(Guid supplierId)
        {

            try
            {
                var supplierBanks = await this.GetSupplierBankListFromDB(supplierId);

                if (supplierBanks.Count > 0)
                {
                    for (int i = 0; i < supplierBanks.Count; i++)
                    {
                        await this.DeleteSupplierBankFromDB(supplierBanks[i].Id);
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
        private async Task DeleteSupplierBankFromDB(Guid supplierBankId)
        {
            try
            {
                var supplierContactPersonItem = await this._supplierBankRepository.GetAsync(supplierBankId);

                await this._supplierBankRepository.DeleteAsync(supplierContactPersonItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SupplierBankDto>> GetSupplierBankListFromDB(Guid supplierId)
        {
            try
            {
                var supplierBankQuery = this._supplierBankRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.SupplierId == supplierId);

                return new List<SupplierBankDto>(ObjectMapper.Map<List<SupplierBankDto>>(supplierBankQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
