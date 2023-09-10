namespace MyTraining1101Demo.Purchase.SubAssemblyItems.Dto
{
    using System;
    public class SubAssemblyItemDto
    {
        public Guid Id { get; set; }
        public Guid? ItemId { get; set; }
        public string ItemName { get; set; }

        public string SubAssemblyName { get; set; }

        public string AssemblyName { get; set; }

        public Guid? SubAssemblyId { get; set; }

    }
}
