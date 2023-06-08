namespace MyTraining1101Demo.Purchase.SubAssemblies.Dto
{
    using System;
    public class SubAssemblyInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public Guid? ModelId { get; set; }

        public Guid? AssemblyId { get; set; }
    }
}
