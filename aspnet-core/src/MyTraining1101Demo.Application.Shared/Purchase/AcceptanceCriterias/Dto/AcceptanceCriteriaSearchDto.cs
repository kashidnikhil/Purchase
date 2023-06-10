namespace MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto
{
    using Abp.Runtime.Validation;
    using MyTraining1101Demo.Dto;
    public class AcceptanceCriteriaSearchDto : PagedAndSortedInputDto, IShouldNormalize
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
