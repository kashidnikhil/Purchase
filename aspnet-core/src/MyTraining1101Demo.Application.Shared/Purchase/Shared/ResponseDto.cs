namespace MyTraining1101Demo.Purchase.Shared
{
    using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
    using System;
    public class ResponseDto
    {
        public Guid? Id { get; set; }

        public Guid RestoringItemId { get; set; }

        public string Name { get; set; }


        public bool IsExistingDataAlreadyDeleted { get; set; }

        public bool DataMatchFound { get; set; }

        public ItemMasterInputDto RecentlyAddedItem { get; set; }

    }
}
