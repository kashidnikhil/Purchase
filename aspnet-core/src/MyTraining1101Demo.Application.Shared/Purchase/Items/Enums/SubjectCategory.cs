namespace MyTraining1101Demo.Purchase.Items.Enums
{
    using System.ComponentModel.DataAnnotations;
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
