using System.ComponentModel.DataAnnotations;

namespace MyTraining1101Demo.Purchase.Items.Enums
{
    public enum ItemCategory
    {
        [Display(Name = "Lab Instruments")]
        LabInstruments = 1,

        [Display(Name = "Office Equipments")]
        OfficeEquipments,

        [Display(Name = "R&M")]
        RM,

        [Display(Name = "Books")]
        Books,

        [Display(Name = "Glassware")]
        Glassware,

        [Display(Name = "Chemicals")]
        Chemicals,

        [Display(Name = "Material")]
        Material,

        [Display(Name = "Furniture & Fixtures")]
        FurnitureAndFixtures,

        [Display(Name = "Tools & Tackles")]
        ToolsAndTackles
    }
}
