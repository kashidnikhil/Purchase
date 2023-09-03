namespace MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster
{
    using MyTraining1101Demo.Purchase.Items.Dto.CalibrationTypeMaster;
    using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionItem;
    using MyTraining1101Demo.Purchase.Transactions.Enums;
    using System;
    using System.Collections.Generic;

    public class MaterialRequisitionInputDto
    {
        public Guid? Id { get; set; }
        public DateTime MRIDate { get; set; }

        public string MRINumber { get; set; }

        public MaterialRequisitionLocationType Location { get; set; }

        public int MaterialRequisitionType { get; set; }

        public long? UserId { get; set; }

        public string UsedFor { get; set; }

        public int ItemType { get; set; }

        public string ProjectNumber { get; set; }

        public string Comments { get; set; }

        public DateTime RequireByDate { get; set; }

        public string AdditionalComments { get; set; }

        public List<MaterialRequisitionItemInputDto> MaterialRequisitionItems { get; set; }
    }
}
