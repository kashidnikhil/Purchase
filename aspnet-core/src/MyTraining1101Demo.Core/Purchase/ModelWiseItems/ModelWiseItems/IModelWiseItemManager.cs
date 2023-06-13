using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems
{
    public interface IModelWiseItemManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateModelWiseItems(List<ModelWiseItemInputDto> itemCalibrationTypeInputList);

        Task<bool> BulkDeleteModelWiseItems(Guid modelWiseItemMasterId);

        Task<bool> DeleteModelWiseItemFromDB(Guid modelWiseItemId);

        Task<IList<ModelWiseItemDto>> GetModelWiseItemListFromDB(Guid modelWiseItemMasterId);
    }
}
