namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.SubAssemblies.Dto;
    using MyTraining1101Demo.Purchase.SubAssemblyItems.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubAssemblyItemManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateSubAssemblyItems(List<SubAssemblyItemInputDto> itemCalibrationAgencyInputList);

        Task<bool> BulkDeleteSubAssemblyItems(Guid subAssemblyId);

        Task<List<SubAssemblyItemDto>> GetSubAssemblyItemListFromDB(Guid subAssemblyId);
    }
}
