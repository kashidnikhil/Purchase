namespace MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster
{
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses;
    using MyTraining1101Demo.Purchase.Companies.Dto.CompanyContactPersons;
    using MyTraining1101Demo.Purchase.Suppliers.Dto.MappedSupplierCategories;
    using System.Collections.Generic;

    public class CompanyInputDto
    {
        public string Name { get; set; }

        public string GSTNumber { get; set; }

        public List<MappedSupplierCategoryInputDto> SupplierCategories { get; set; }

        public List<CompanyContactPersonInputDto> CompanyContactPersons { get; set; }

        public List<CompanyAddressInputDto> CompanyAddresses { get; set; }    
    }
}
