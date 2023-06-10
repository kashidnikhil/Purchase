namespace MyTraining1101Demo.Purchase.Shared
{
    using System;
    public class ResponseDto
    {
        public Guid? Id { get; set; }

        public Guid RestoringItemId { get; set; }

        public string Name { get; set; }


        public bool IsExistingDataAlreadyDeleted { get; set; }

        public bool DataMatchFound { get; set; }

    }
}
