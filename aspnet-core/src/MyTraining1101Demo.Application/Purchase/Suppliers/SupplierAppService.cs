namespace MyTraining1101Demo.Purchase.Suppliers
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierMaster;
    using MyTraining1101Demo.Purchase.Suppliers.MappedSupplierCategories;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierAddresses;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierBanks;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierContactPersons;
    using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SupplierAppService : MyTraining1101DemoAppServiceBase, ISupplierAppService
    {
        private readonly ISupplierManager _supplierManager;
        private readonly ISupplierBankManager _supplierBankManager;
        private readonly ISupplierContactPersonManager _supplierContactPersonManager;
        private readonly ISupplierAddressManager _supplierAddressManager;
        private readonly IMappedSupplierCategoryManager _supplierCategoryManager;
        public SupplierAppService(
            ISupplierManager supplierManager,
            ISupplierBankManager supplierBankManager,
            ISupplierContactPersonManager supplierContactPersonManager,
            ISupplierAddressManager supplierAddressManager,
            IMappedSupplierCategoryManager supplierCategoryManager
         )
        {
            _supplierManager = supplierManager;
            _supplierAddressManager = supplierAddressManager;
            _supplierContactPersonManager = supplierContactPersonManager;
            _supplierBankManager = supplierBankManager;
            _supplierCategoryManager = supplierCategoryManager;
        }

        public async Task<PagedResultDto<SupplierListDto>> GetSuppliers(SupplierMasterSearchDto input)
        {
            try
            {
                var result = await this._supplierManager.GetPaginatedSupplierListListFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<Guid> InsertOrUpdateSupplier(SupplierInputDto input)
        {
            try
            {
                var insertedOrUpdatedSupplierId = await this._supplierManager.InsertOrUpdateSupplierMasterIntoDB(input);

                if (insertedOrUpdatedSupplierId != Guid.Empty)
                {
                    if (input.SupplierAddresses != null && input.SupplierAddresses.Count > 0)
                    {
                        input.SupplierAddresses.ForEach(supplierAddressItem =>
                        {
                            supplierAddressItem.SupplierId = insertedOrUpdatedSupplierId;
                        });
                        await this._supplierAddressManager.BulkInsertOrUpdateSupplierAddresses(input.SupplierAddresses);
                    }

                    if (input.SupplierContactPersons != null && input.SupplierContactPersons.Count > 0)
                    {
                        input.SupplierContactPersons.ForEach(supplierContactPersonItem =>
                        {
                            supplierContactPersonItem.SupplierId = insertedOrUpdatedSupplierId;
                        });
                        await this._supplierContactPersonManager.BulkInsertOrUpdateSupplierContactPersons(input.SupplierContactPersons);
                    }

                    if (input.SupplierBanks != null && input.SupplierBanks.Count > 0)
                    {
                        input.SupplierBanks.ForEach(supplierBankItem =>
                        {
                            supplierBankItem.SupplierId = insertedOrUpdatedSupplierId;
                        });
                        await this._supplierBankManager.BulkInsertOrUpdateSupplierBanks(input.SupplierBanks);
                    }

                    if (input.SupplierCategories != null && input.SupplierCategories.Count > 0)
                    {
                        input.SupplierCategories.ForEach(supplierCategoryItem =>
                        {
                            supplierCategoryItem.SupplierId = insertedOrUpdatedSupplierId;
                        });

                        await this._supplierCategoryManager.BulkInsertOrUpdateMappedSupplierCategories(input.SupplierCategories);
                    }

                }
                return insertedOrUpdatedSupplierId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteSupplierMasterData(Guid supplierId)
        {
            try
            {
                var isSupplierMasterDeleted = await this._supplierManager.DeleteSupplierMasterFromDB(supplierId);

                var isSupplierAddressDeleted = await this._supplierAddressManager.BulkDeleteSupplierAddresses(supplierId);

                var isSupplierCategoryDeleted = await this._supplierCategoryManager.BulkDeleteSupplierCategories(supplierId);

                var isSupplierBanksDelete = await this._supplierBankManager.BulkDeleteSupplierBanks(supplierId);

                var isSupplierContactPersonDeleted = await this._supplierContactPersonManager.BulkDeleteSupplierContactPersons(supplierId);

                //var isCustomerDataDeleted = await this._customerMasterManager.DeleteCustomerFromDB(customerId);

                return isSupplierMasterDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<SupplierDto> GetSupplierMasterById(Guid supplierId)
        {
            try
            {
                var supplierMasterItem = await this._supplierManager.GetSupplierMasterByIdFromDB(supplierId);

                if (supplierMasterItem.Id != Guid.Empty)
                {
                    supplierMasterItem.SupplierAddresses = await this._supplierAddressManager.GetSupplierAddressListFromDB(supplierMasterItem.Id);
                    supplierMasterItem.SupplierContactPersons = await this._supplierContactPersonManager.GetSupplierContactPersonListFromDB(supplierMasterItem.Id);
                    supplierMasterItem.SupplierBanks = await this._supplierBankManager.GetSupplierBankListFromDB(supplierMasterItem.Id);
                    supplierMasterItem.SupplierCategories = await this._supplierCategoryManager.GetSupplierCategoryListFromDB(supplierMasterItem.Id);
                }

                return supplierMasterItem;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SupplierDto>> GetSupplierList()
        {
            try
            {
                var response = await this._supplierManager.GetSupplierListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
