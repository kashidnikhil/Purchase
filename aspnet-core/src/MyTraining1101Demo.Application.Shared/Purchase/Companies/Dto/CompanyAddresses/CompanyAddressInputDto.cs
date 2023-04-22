using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Purchase.Companies.Dto.CompanyAddresses
{
    public class CompanyAddressInputDto
    {
        public Guid? Id { get; set; }
        public string Address { get; set; }

        public Guid CompanyId { get; set; }
    }
}
