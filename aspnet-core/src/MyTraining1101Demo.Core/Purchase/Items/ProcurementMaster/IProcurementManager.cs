namespace MyTraining1101Demo.Purchase.Items.ProcurementMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.ProcurementMaster;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProcurementManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateItemProcurements(List<ProcurementInputDto> itemProcurementInputList);
        Task<bool> BulkDeleteItemProcurements(Guid itemId);

        Task<IList<ProcurementDto>> GetItemProcurementListFromDB(Guid itemId);
    }
}
