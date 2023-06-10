namespace MyTraining1101Demo.Purchase.Items.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum ItemCategory
    {
        [Display(Name = "Lab Instruments")]
        LabInstruments = 10001,

        [Display(Name = "Office Equipments")]
        OfficeEquipments = 20001,

        [Display(Name = "R&M")]
        RM = 30001,

        [Display(Name = "Books")]
        Books = 40001,

        [Display(Name = "Glassware")]
        Glassware = 50001,

        [Display(Name = "Chemicals")]
        Chemicals = 60001,

        [Display(Name = "Material")]
        Material = 70001,

        [Display(Name = "Furniture & Fixtures")]
        FurnitureAndFixtures = 80001,

        [Display(Name = "Tools & Tackles")]
        ToolsAndTackles = 90001
    }
}
