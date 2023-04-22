using MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses;
using MyTraining1101Demo.Purchase.Companies.Dto.CompanyContactPersons;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierAddresses;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierContactPersons;
using System.Collections.Generic;

namespace MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster
{
    public class CompanyInputDto
    {
        public string Name { get; set; }

        public List<CompanyContactPersonInputDto> CompanyContactPersons { get; set; }

        public List<CompanyAddressInputDto> CompanyAddresses { get; set; }    
    }
}
