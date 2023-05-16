namespace MyTraining1101Demo.Purchase.Items.CalibrationAgenciesMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.CalibrationAgenciesMaster;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICalibrationAgencyManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateItemCalibrationAgencies(List<CalibrationAgencyInputDto> itemCalibrationAgencyInputList);

        Task<bool> BulkDeleteItemCalibrationAgencies(Guid itemId);

        Task<IList<CalibrationAgencyDto>> GetItemCalibrationAgencyListFromDB(Guid itemId);
    }
}
