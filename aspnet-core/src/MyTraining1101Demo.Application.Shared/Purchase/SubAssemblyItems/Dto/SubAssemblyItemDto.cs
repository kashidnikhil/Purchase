using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Purchase.SubAssemblyItems.Dto
{
    public class SubAssemblyItemDto
    {
        public Guid Id { get; set; }
        public Guid? ItemId { get; set; }
        public string ItemName { get; set; }

        public string SubAssemblyName { get; set; }

        public Guid? SubAssemblyId { get; set; }

    }
}
