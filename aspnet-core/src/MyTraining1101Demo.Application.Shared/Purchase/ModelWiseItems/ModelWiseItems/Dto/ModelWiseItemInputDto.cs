using System;

namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems.Dto
{
    public class ModelWiseItemInputDto
    {
        public Guid Id { get; set; }
        public string Comments { get; set; }

        public Guid? ItemId { get; set; }

        public Guid? ModelWiseItemMasterId { get; set; }
    }
}
