namespace MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.ItemAttachmentsMaster;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ItemAttachmentManager : MyTraining1101DemoDomainServiceBase, IItemAttachmentManager
    {
        private readonly IRepository<ItemAttachment, Guid> _itemAttachmentRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ItemAttachmentManager(
           IRepository<ItemAttachment, Guid> itemAttachmentRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _itemAttachmentRepository = itemAttachmentRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<Guid> BulkInsertOrUpdateItemAttachments(List<ItemAttachmentInputDto> itemAttachmentInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemStorageConditions = ObjectMapper.Map<List<ItemAttachment>>(itemAttachmentInputList);
                for (int i = 0; i < mappedItemStorageConditions.Count; i++)
                {
                    itemId = (Guid)mappedItemStorageConditions[i].ItemId;
                    await this.InsertOrUpdateItemAttachmentIntoDB(mappedItemStorageConditions[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateItemAttachmentIntoDB(ItemAttachment input)
        {
            try
            {
                var itemAttachmentId = await this._itemAttachmentRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteItemAttachments(Guid itemId)
        {
            try
            {
                var itemAttachments = await this.GetItemAttachmentListFromDB(itemId);

                if (itemAttachments.Count > 0)
                {
                    for (int i = 0; i < itemAttachments.Count; i++)
                    {
                        await this.DeleteItemAttachmentFromDB(itemAttachments[i].Id);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task DeleteItemAttachmentFromDB(Guid itemAttachmentId)
        {
            try
            {
                var itemAttachmentItem = await this._itemAttachmentRepository.GetAsync(itemAttachmentId);

                await this._itemAttachmentRepository.DeleteAsync(itemAttachmentItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ItemAttachmentDto>> GetItemAttachmentListFromDB(Guid itemId)
        {
            try
            {
                var itemAttachmentQuery = this._itemAttachmentRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId);

                return new List<ItemAttachmentDto>(ObjectMapper.Map<List<ItemAttachmentDto>>(itemAttachmentQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
