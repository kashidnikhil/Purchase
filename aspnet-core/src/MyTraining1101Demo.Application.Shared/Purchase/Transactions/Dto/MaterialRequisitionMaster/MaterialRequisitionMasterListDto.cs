namespace MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster
{
    using MyTraining1101Demo.Purchase.Transactions.Enums;
    using System;
    public class MaterialRequisitionMasterListDto
    {
        public Guid Id { get; set; }
        public DateTime MRIDate { get; set; }

        public string MRINumber { get; set; }

        public string ProjectNumber { get; set; }

        public MaterialRequisitionLocationType Location { get; set; }
    }
}
