namespace MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.RequiredItemSparesMaster;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    public interface IItemSpareManager : IDomainService
    {
        Task<IList<ItemSpareDto>> GetItemSpareListFromDB(Guid itemId);

        Task<bool> BulkDeleteItemSpares(Guid itemId);

        Task<Guid> BulkInsertOrUpdateItemSpares(List<ItemSpareInputDto> itemSpareInputList);
    }
}
