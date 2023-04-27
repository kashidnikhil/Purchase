namespace MyTraining1101Demo.Purchase.Suppliers.MappedSupplierCategories
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using MyTraining1101Demo.Purchase.SupplierCategories.Dto;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MappedSupplierCategoryManager : MyTraining1101DemoDomainServiceBase, IMappedSupplierCategoryManager
    {
        private readonly IRepository<MappedSupplierCategory, Guid> _supplierCategoryRepository;

        public MappedSupplierCategoryManager(IRepository<MappedSupplierCategory, Guid> supplierCategoryRepository)
        {
            _supplierCategoryRepository = supplierCategoryRepository;
        }

        public async Task<Guid> BulkInsertOrUpdateMappedSupplierCategories(List<MappedSupplierCategoryInputDto> supplierCategoriesInputList)
        {
            try
            {
                Guid mappedSupplierId = Guid.Empty;

                // Using the approach of (soft) delete and insert for updating the records. (soft) delete is used because it should not hamper the other functionalities at any point of time.
                await this.BulkDeleteSupplierCategories(supplierCategoriesInputList[0].SupplierId);

                var mappedSupplierCategories = ObjectMapper.Map<List<MappedSupplierCategory>>(supplierCategoriesInputList);
                
                for (int i = 0; i < mappedSupplierCategories.Count; i++)
                {
                    mappedSupplierId = (Guid)mappedSupplierCategories[i].SupplierId;
                    mappedSupplierCategories[i].Id = Guid.Empty;
                    await this.InsertMappedSupplierCategoryIntoDB(mappedSupplierCategories[i]);
                }
                return mappedSupplierId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertMappedSupplierCategoryIntoDB(MappedSupplierCategory input)
        {
            try
            {
                var supplierCategoryId = await this._supplierCategoryRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteSupplierCategories(Guid supplierId)
        {

            try
            {
                var supplierCategories = await this.GetSupplierCategoryListFromDB(supplierId);

                if (supplierCategories.Count > 0)
                {
                    for (int i = 0; i < supplierCategories.Count; i++)
                    {
                        await this.DeleteSupplierCategoryFromDB(supplierCategories[i].Id);
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
        private async Task DeleteSupplierCategoryFromDB(Guid supplierCategoryId)
        {
            try
            {
                var supplierCategoryItem = await this._supplierCategoryRepository.GetAsync(supplierCategoryId);

                await this._supplierCategoryRepository.DeleteAsync(supplierCategoryItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SupplierCategoryDto>> GetSupplierCategoryListFromDB(Guid supplierId)
        {
            try
            {
                var supplierCategoryQuery = this._supplierCategoryRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.SupplierId == supplierId);

                return new List<SupplierCategoryDto>(ObjectMapper.Map<List<SupplierCategoryDto>>(supplierCategoryQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }


    }
}
