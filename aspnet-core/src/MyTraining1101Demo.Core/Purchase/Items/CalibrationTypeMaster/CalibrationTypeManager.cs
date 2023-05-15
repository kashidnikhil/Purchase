using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using System;

namespace MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster
{
    public class CalibrationTypeManager : MyTraining1101DemoDomainServiceBase, ICalibrationTypeManager
    {
        private readonly IRepository<CalibrationType, Guid> _calibrationTypeRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public CalibrationTypeManager(
           IRepository<CalibrationType, Guid> calibrationTypeRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _calibrationTypeRepository = calibrationTypeRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
