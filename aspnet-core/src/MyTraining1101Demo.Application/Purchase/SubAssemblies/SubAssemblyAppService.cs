using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.SubAssemblies;
using MyTraining1101Demo.Purchase.SubAssemblies.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    public class SubAssemblyAppService : MyTraining1101DemoAppServiceBase, ISubAssemblyAppService
    {
        private readonly ISubAssemblyManager _subAssemblyManager;
        private readonly ISubAssemblyItemManager _subAssemblyItemManager;

        public SubAssemblyAppService(
          ISubAssemblyManager subAssemblyManager,
          ISubAssemblyItemManager subAssemblyItemManager
         )
        {
            _subAssemblyManager = subAssemblyManager;
            _subAssemblyItemManager = subAssemblyItemManager;
        }
        public async Task<PagedResultDto<SubAssemblyListDto>> GetSubAssemblies(SubAssemblySearchDto input)
        {
            try
            {
                var result = await this._subAssemblyManager.GetPaginatedSubAssembliesFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateSubAssembly(SubAssemblyInputDto input)
        {
            try
            {
                var insertedOrUpdatedSubAssembly = await this._subAssemblyManager.InsertOrUpdateSubAssemblyIntoDB(input);

                if (input.SubAssemblyItems != null && input.SubAssemblyItems.Count > 0)
                {
                    input.SubAssemblyItems.ForEach(subAssemblyItem =>
                    {
                        subAssemblyItem.SubAssemblyId = insertedOrUpdatedSubAssembly.Id;
                    });

                    await this._subAssemblyItemManager.BulkInsertOrUpdateSubAssemblyItems(input.SubAssemblyItems);
                }

                return insertedOrUpdatedSubAssembly;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteSubAssembly(Guid subAssemblyId)
        {
            try
            {
                var isSubAssemblyItemsDeleted = await this._subAssemblyItemManager.BulkDeleteSubAssemblyItems(subAssemblyId);
                
                var isSubAssemblyDeleted = await this._subAssemblyManager.DeleteSubAssemblyFromDB(subAssemblyId);
                return isSubAssemblyDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<SubAssemblyDto> GetSubAssemblyById(Guid subAssemblyItemId)
        {
            try
            {
                var subAssembly = await this._subAssemblyManager.GetSubAssemblyByIdFromDB(subAssemblyItemId);

                if (subAssembly.Id != Guid.Empty)
                {
                    subAssembly.SubAssemblyItems = await this._subAssemblyItemManager.GetSubAssemblyItemListFromDB(subAssembly.Id);
                }

                return subAssembly;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SubAssemblyDto>> GetSubAssemblyList()
        {
            try
            {
                var response = await this._subAssemblyManager.GetSubAssemblyListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreSubAssembly(Guid subAssemblyId)
        {
            try
            {
                var response = await this._subAssemblyManager.RestoreSubAssembly(subAssemblyId);
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
