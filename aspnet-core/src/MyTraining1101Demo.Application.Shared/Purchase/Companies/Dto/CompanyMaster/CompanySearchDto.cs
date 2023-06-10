namespace MyTraining1101Demo.Purchase.Companies.Dto.CompanyMaster
{
    using Abp.Runtime.Validation;
    using MyTraining1101Demo.Dto;
    public class CompanySearchDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SearchString { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "name";
            }
        }
    }
}
