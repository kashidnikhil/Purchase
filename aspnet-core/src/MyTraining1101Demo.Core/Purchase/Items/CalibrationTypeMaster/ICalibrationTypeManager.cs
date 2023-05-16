namespace MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.CalibrationTypeMaster;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICalibrationTypeManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateItemCalibrationTypes(List<CalibrationTypeInputDto> itemCalibrationTypeInputList);
        
        Task<bool> BulkDeleteItemCalibrationType(Guid itemId);

        Task<IList<CalibrationTypeDto>> GetItemCalibrationTypeListFromDB(Guid itemId);
    }
}
