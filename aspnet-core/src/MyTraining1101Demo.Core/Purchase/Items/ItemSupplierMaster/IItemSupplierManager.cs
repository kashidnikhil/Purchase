namespace MyTraining1101Demo.Purchase.Items.ItemSupplierMaster
{
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemSupplierMaster;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    public interface IItemSupplierManager : IDomainService
    {
        Task<Guid> BulkInsertOrUpdateItemSuppliers(List<ItemSupplierInputDto> itemSupplierInputList);

        Task<bool> BulkDeleteItemSuppliers(Guid itemId);

        Task<IList<ItemSupplierDto>> GetItemSupplierListFromDB(Guid itemId);
    }
}
