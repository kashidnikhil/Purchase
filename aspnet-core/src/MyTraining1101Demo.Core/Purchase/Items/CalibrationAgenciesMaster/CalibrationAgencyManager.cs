using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using System;

namespace MyTraining1101Demo.Purchase.Items.CalibrationAgenciesMaster
{
    public class CalibrationAgencyManager : MyTraining1101DemoDomainServiceBase, ICalibrationAgencyManager
    {
        private readonly IRepository<CalibrationAgency, Guid> _calibrationAgencyRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public CalibrationAgencyManager(
           IRepository<CalibrationAgency, Guid> calibrationAgencyRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _calibrationAgencyRepository = calibrationAgencyRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
