namespace MyTraining1101Demo.Purchase.AssemblyCategories.Dto
{
    using System;
    public class AssemblyCategoryDto
    {
        public Guid Id { get; set; }
        public string SubCategory { get; set; }

        public string SubCategory1 { get; set; }
        public string Comments { get; set; }

        public Guid? ModelId { get; set; }

    }
}
