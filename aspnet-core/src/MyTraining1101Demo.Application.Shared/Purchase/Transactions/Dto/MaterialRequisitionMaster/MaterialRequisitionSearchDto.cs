namespace MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster
{
    using Abp.Runtime.Validation;
    using MyTraining1101Demo.Dto;

    public class MaterialRequisitionSearchDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SearchString { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "MRINumber";
            }
        }
    }
}
