namespace MyTraining1101Demo.Purchase.SubAssemblyItems.Dto
{
    using System;
    public class SubAssemblyItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? ModelId { get; set; }

        public Guid? AssemblyId { get; set; }
    }
}
