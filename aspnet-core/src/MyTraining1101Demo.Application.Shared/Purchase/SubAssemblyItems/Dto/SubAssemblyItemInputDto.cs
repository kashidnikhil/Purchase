using System;

namespace MyTraining1101Demo.Purchase.SubAssemblyItems.Dto
{
    public class SubAssemblyItemInputDto
    {
        public Guid? Id { get; set; }
        public Guid? SubAssemblyId { get; set; }

        public Guid? ItemId { get; set; }
    }
}
