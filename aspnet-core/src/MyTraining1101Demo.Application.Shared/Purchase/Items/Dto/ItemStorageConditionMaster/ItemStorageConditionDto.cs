using MyTraining1101Demo.Purchase.Items.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Purchase.Items.Dto.ItemStorageConditionMaster
{
    public class ItemStorageConditionDto
    {
        public Guid Id { get; set; }
        public HazardousEnum Hazardous { get; set; }
        public long ThresholdQuantity { get; set; }

        public string Location { get; set; }
        public Guid ItemId { get; set; }
    }
}
