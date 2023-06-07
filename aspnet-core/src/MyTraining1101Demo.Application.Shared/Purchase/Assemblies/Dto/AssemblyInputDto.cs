namespace MyTraining1101Demo.Purchase.Assemblies.Dto
{
    using System;
    public class AssemblyInputDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public Guid? ModelId { get; set; }
    }
}
