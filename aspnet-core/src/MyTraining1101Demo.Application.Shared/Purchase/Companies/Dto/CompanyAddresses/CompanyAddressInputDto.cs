namespace MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses
{
    using System;
    public class CompanyAddressInputDto
    {
        public Guid? Id { get; set; }
        public string Address { get; set; }

        public Guid CompanyId { get; set; }
    }
}
