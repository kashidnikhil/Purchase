using Abp.Application.Services.Dto;
using Microsoft.Extensions.Logging;
using MyTraining1101Demo.Purchase.AcceptanceCriterias;
using MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.SubAssemblyItems.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    public class SubAssemblyItemAppService : MyTraining1101DemoAppServiceBase, ISubAssemblyItemAppService
    {
        private readonly ISubAssemblyItemManager _subAssemblyItemManager;

        public SubAssemblyItemAppService(
          ISubAssemblyItemManager subAssemblyItemManager
         )
        {
            _subAssemblyItemManager = subAssemblyItemManager;
        }
        public async Task<PagedResultDto<SubAssemblyItemListDto>> GetSubAssemblyItems(SubAssemblyItemSearchDto input)
        {
            try
            {
                var result = await this._subAssemblyItemManager.GetPaginatedSubAssemblyItemsFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateSubAssemblyItem(SubAssemblyItemInputDto input)
        {
            try
            {
                var insertedOrUpdatedSubAssemblyItem = await this._subAssemblyItemManager.InsertOrUpdateSubAssemblyItemIntoDB(input);

                return insertedOrUpdatedSubAssemblyItem;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteSubAssemblyItem(Guid subAssemblyItemId)
        {
            try
            {
                var isSubAssemblyItemDeleted = await this._subAssemblyItemManager.DeleteSubAssemblyItemFromDB(subAssemblyItemId);
                return isSubAssemblyItemDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<SubAssemblyItemDto> GetSubAssemblyItemById(Guid subAssemblyItemId)
        {
            try
            {
                var response = await this._subAssemblyItemManager.GetSubAssemblyItemByIdFromDB(subAssemblyItemId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SubAssemblyItemDto>> GetSubAssemblyItemList()
        {
            try
            {
                var response = await this._subAssemblyItemManager.GetSubAssemblyItemListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreSubAssemblyItem(Guid subAssemblyItemId)
        {
            try
            {
                var response = await this._subAssemblyItemManager.RestoreSubAssemblyItem(subAssemblyItemId);
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
