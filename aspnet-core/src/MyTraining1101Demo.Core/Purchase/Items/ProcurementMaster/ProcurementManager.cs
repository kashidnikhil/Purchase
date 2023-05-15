using Abp.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using System;

namespace MyTraining1101Demo.Purchase.Items.ProcurementMaster
{
    public class ProcurementManager : MyTraining1101DemoDomainServiceBase, IProcurementManager
    {
        private readonly IRepository<Procurement, Guid> _procurementRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ProcurementManager(
           IRepository<Procurement, Guid> procurementRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _procurementRepository = procurementRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }
    }
}
