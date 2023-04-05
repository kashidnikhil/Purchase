using Abp.Runtime.Validation;
using MyTraining1101Demo.Dto;

namespace MyTraining1101Demo.Purchase.MaterialGrades.Dto
{
    public class MaterialGradeSearchDto : PagedAndSortedInputDto, IShouldNormalize
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
