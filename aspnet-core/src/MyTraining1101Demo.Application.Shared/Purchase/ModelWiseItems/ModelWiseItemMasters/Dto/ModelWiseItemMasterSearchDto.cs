namespace MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters.Dto
{
    using Abp.Runtime.Validation;
    using MyTraining1101Demo.Dto;
    public class ModelWiseItemMasterSearchDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SearchString { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "ModelName";
            }
        }
    }
}
