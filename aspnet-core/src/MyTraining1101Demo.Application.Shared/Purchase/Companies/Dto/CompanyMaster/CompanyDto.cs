using MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses;
using MyTraining1101Demo.Purchase.Companies.Dto.CompanyContactPersons;
using System;
using System.Collections.Generic;

namespace MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IList<CompanyContactPersonDto> CompanyContactPersons { get; set; }

        public IList<CompanyAddressDto> CompanyAddresses { get; set; }

    }
}
