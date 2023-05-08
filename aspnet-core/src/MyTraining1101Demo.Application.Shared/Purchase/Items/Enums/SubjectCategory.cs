using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Items.Enums
{
    public enum SubjectCategory
    {
        [Display(Name = "Chemistry")]
        Chemistry = 1,

        [Display(Name = "Marketing")]
        Marketing,

        [Display(Name = "Drafting")]
        Drafting,

        [Display(Name = "Management")]
        Management,

        [Display(Name = "Other")]
        Other
    }
}
