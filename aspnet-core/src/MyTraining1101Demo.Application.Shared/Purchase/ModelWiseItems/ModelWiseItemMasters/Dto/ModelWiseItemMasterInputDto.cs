namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters.Dto
{
    using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems.Dto;
    using System;
    using System.Collections.Generic;
    public class ModelWiseItemMasterInputDto
    {
        public Guid Id { get; set; }

        public Guid? ModelId { get; set; }

        public List<ModelWiseItemInputDto> ModelWiseItemData { get; set; }
    }
}
