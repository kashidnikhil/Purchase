using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster;
using System.Threading.Tasks;
using System;
using MyTraining1101Demo.Purchase.AssemblyCategories.Dto;
using MyTraining1101Demo.Purchase.DeliveryTerms;
using MyTraining1101Demo.Purchase.TermsOfPayments.Dto;
using System.Collections.Generic;

namespace MyTraining1101Demo.Purchase.AssemblyCategories
{
    public class AssemblyCategoryAppService : MyTraining1101DemoAppServiceBase, IAssemblyCategoryAppService
    {
        private readonly IAssemblyCategoryManager _assemblyCategoryManager;

        public AssemblyCategoryAppService(
            IAssemblyCategoryManager assemblyCategoryManager
         )
        {
            _assemblyCategoryManager = assemblyCategoryManager;
        }


        public async Task<PagedResultDto<AssemblyCategoryListDto>> GetAssemblyCategories(AssemblyCategorySearchDto input)
        {
            try
            {
                var result = await this._assemblyCategoryManager.GetPaginatedAssemblyCategoriesFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<Guid> InsertOrUpdateAssemblyCategory(AssemblyCategoryInputDto input)
        {
            try
            {
                var insertedOrUpdatedAssemblyCategoryId = await this._assemblyCategoryManager.InsertOrUpdateAssemblyCategoryIntoDB(input);

                return insertedOrUpdatedAssemblyCategoryId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteAssemblyCategory(Guid assemblyCategoryId)
        {
            try
            {
                var isAsseblyCategoryDeleted = await this._assemblyCategoryManager.DeleteAssemblyCategoryFromDB(assemblyCategoryId);
                return isAsseblyCategoryDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<AssemblyCategoryDto> GetAssemblyCategoryById(Guid assemblyCategoryId)
        {
            try
            {
                var assmeblyCategory = await this._assemblyCategoryManager.GetAssemblyCategoryByIdFromDB(assemblyCategoryId);

                return assmeblyCategory;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<AssemblyCategoryDto>> GetAssemblyCategoryList()
        {
            try
            {
                var response = await this._assemblyCategoryManager.GetAssemblyCategorylListFromDB();
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
