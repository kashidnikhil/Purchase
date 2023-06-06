namespace MyTraining1101Demo.Purchase.AssemblyCategories.Dto
{
    using Abp.Runtime.Validation;
    using MyTraining1101Demo.Dto;
    public class AssemblyCategorySearchDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SearchString { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "";
            }
        }
    }
}
