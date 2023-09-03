namespace MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionItemMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionItem;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    public interface IMaterialRequisitionItemManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateMaterialRequisitionItems(List<MaterialRequisitionItemInputDto> materialRequuuisitionItemInputList);

        Task<bool> BulkDeleteMaterialRequisitionItem(Guid materialRequisitionId);

        Task<IList<MaterialRequisitionItemDto>> GetMaterialRequisitionItemListFromDB(Guid materialRequsitionId);
    }
}
