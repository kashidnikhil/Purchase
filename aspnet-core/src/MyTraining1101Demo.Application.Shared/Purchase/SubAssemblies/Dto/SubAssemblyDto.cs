namespace MyTraining1101Demo.Purchase.SubAssemblies.Dto
{
    using MyTraining1101Demo.Purchase.SubAssemblyItems.Dto;
    using System;
    using System.Collections.Generic;

    public class SubAssemblyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? ModelId { get; set; }

        public Guid? AssemblyId { get; set; }

        public List<SubAssemblyItemDto> SubAssemblyItems { get; set; }
    }
}
