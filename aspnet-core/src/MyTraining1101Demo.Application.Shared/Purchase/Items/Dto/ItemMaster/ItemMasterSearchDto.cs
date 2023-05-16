namespace MyTraining1101Demo.Purchase.Items.Dto.ItemMaster
{
    using Abp.Runtime.Validation;
    using MyTraining1101Demo.Dto;
    public class ItemMasterSearchDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SearchString { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "ItemName";
            }
        }
    }
}
