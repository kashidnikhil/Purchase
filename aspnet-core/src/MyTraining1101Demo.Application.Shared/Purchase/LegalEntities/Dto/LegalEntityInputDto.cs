using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Purchase.LegalEntities.Dto
{
    public class LegalEntityInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
