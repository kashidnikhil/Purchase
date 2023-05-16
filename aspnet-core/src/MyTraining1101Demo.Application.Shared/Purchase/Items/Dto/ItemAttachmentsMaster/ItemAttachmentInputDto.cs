namespace MyTraining1101Demo.Purchase.Items.Dto.ItemAttachmentsMaster
{
    using System;
    public class ItemAttachmentInputDto
    {
        public Guid? Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public Guid ItemId { get; set; }
    }
}
