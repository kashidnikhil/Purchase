namespace MyTraining1101Demo.Purchase.ItemCategories.Dto
{
    using Abp.Runtime.Validation;
    using MyTraining1101Demo.Dto;
    public class ItemCategorySearchDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SearchString { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "ItemCategoryCode";
            }
        }
    }
}
