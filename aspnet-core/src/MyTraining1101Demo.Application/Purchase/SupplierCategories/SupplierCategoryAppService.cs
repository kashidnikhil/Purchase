namespace MyTraining1101Demo.Purchase.SupplierCategories
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.SupplierCategories.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class SupplierCategoryAppService : MyTraining1101DemoAppServiceBase, ISupplierCategoryAppService
    {
        private readonly ISupplierCategoryManager _supplierCategoryManager;

        public SupplierCategoryAppService(
          ISupplierCategoryManager supplierCategoryManager
         )
        {
            _supplierCategoryManager = supplierCategoryManager;
        }


        public async Task<PagedResultDto<SupplierCategoryDto>> GetSupplierCategories(SupplierCategorySearchDto input)
        {
            try
            {
                var result = await this._supplierCategoryManager.GetPaginatedSupplierCategoriesFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateSupplierCategory(SupplierCategoryInputDto input)
        {
            try
            {
                var insertedOrUpdatedSupplierCategoryId = await this._supplierCategoryManager.InsertOrUpdateSupplierCategoryIntoDB(input);

                return insertedOrUpdatedSupplierCategoryId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteSupplierCategory(Guid supplierCategoryId)
        {
            try
            {
                var isSupplierCategoryDeleted = await this._supplierCategoryManager.DeleteSupplierCategoryFromDB(supplierCategoryId);
                return isSupplierCategoryDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<SupplierCategoryDto> GetSupplierCategoryById(Guid supplierCategoryId)
        {
            try
            {
                var response = await this._supplierCategoryManager.GetSupplierCategoryByIdFromDB(supplierCategoryId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SupplierCategoryDto>> GetSupplierCategoryList()
        {
            try
            {
                var response = await this._supplierCategoryManager.GetSupplierCategoryListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreSupplierCategory(Guid supplierCategoryId)
        {
            try
            {
                var response = await this._supplierCategoryManager.RestoreSupplierCategory(supplierCategoryId);
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
