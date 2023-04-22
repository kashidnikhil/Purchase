using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Purchase.Companies.Dto.CompanyContactPersons
{
    public class CompanyContactPersonDto
    {
        public Guid Id { get; set; }
        public string ContactPersonName { get; set; }

        public string Designation { get; set; }

        public string EmailId { get; set; }

        public string MobileNumber { get; set; }

        public Guid CompanyId { get; set; }
    }
}
