using MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses;
using MyTraining1101Demo.Purchase.Companies.Dto.CompanyContactPersons;
using MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories;
using System;
using System.Collections.Generic;

namespace MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string GSTNumber { get; set; }

        public IList<MappedSupplierCategoryDto> SupplierCategories { get; set; }

        public IList<CompanyContactPersonDto> CompanyContactPersons { get; set; }

        public IList<CompanyAddressDto> CompanyAddresses { get; set; }

    }
}
