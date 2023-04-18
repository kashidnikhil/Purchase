using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierAddresses;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierBanks;
using MyTraining1101Demo.Purchase.Suppliers.SupplierBanks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Suppliers.SupplierAddresses
{
    public class SupplierAddressManager : MyTraining1101DemoDomainServiceBase, ISupplierAddressManager
    {
        private readonly IRepository<SupplierAddress, Guid> _supplierAddressRepository;

        public SupplierAddressManager(IRepository<SupplierAddress, Guid> supplierAddressRepository)
        {
            _supplierAddressRepository = supplierAddressRepository;
        }

        public async Task<Guid> BulkInsertOrUpdateSupplierAddresses(List<SupplierAddressInputDto> supplierAddressInputList)
        {
            try
            {
                Guid supplierId = Guid.Empty;
                var mappedSupplierAddresses = ObjectMapper.Map<List<SupplierAddress>>(supplierAddressInputList);
                for (int i = 0; i < mappedSupplierAddresses.Count; i++)
                {
                    supplierId = (Guid)mappedSupplierAddresses[i].SupplierId;
                    await this.InsertOrUpdateSupplierAddressIntoDB(mappedSupplierAddresses[i]);
                }
                return supplierId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateSupplierAddressIntoDB(SupplierAddress input)
        {
            try
            {
                var supplierBankId = await this._supplierAddressRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteSupplierAddresses(Guid supplierId)
        {

            try
            {
                var supplierAddresses = await this.GetSupplierAddressListFromDB(supplierId);

                if (supplierAddresses.Count > 0)
                {
                    for (int i = 0; i < supplierAddresses.Count; i++)
                    {
                        await this.DeleteSupplierAddressFromDB(supplierAddresses[i].Id);
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
        private async Task DeleteSupplierAddressFromDB(Guid supplierAddressId)
        {
            try
            {
                var supplierAddressItem = await this._supplierAddressRepository.GetAsync(supplierAddressId);

                await this._supplierAddressRepository.DeleteAsync(supplierAddressItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SupplierAddressDto>> GetSupplierAddressListFromDB(Guid supplierId)
        {
            try
            {
                var supplierAddressQuery = this._supplierAddressRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.SupplierId == supplierId);

                return new List<SupplierAddressDto>(ObjectMapper.Map<List<SupplierAddressDto>>(supplierAddressQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
