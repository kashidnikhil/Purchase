using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems.Dto;
using System;
using System.Collections.Generic;

namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters.Dto
{
    public class ModelWiseItemMasterDto
    {
        public Guid? Id { get; set; }

        public Guid? ModelId { get; set; }

        public IList<ModelWiseItemDto> ModelWiseItemData { get; set; }
    }
}
