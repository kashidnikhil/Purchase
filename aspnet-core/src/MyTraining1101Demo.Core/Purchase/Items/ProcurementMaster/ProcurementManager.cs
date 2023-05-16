namespace MyTraining1101Demo.Purchase.Items.ProcurementMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.ProcurementMaster;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProcurementManager : MyTraining1101DemoDomainServiceBase, IProcurementManager
    {
        private readonly IRepository<Procurement, Guid> _procurementRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public ProcurementManager(
           IRepository<Procurement, Guid> procurementRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _procurementRepository = procurementRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<Guid> BulkInsertOrUpdateItemProcurements(List<ProcurementInputDto> itemProcurementInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemProcurements = ObjectMapper.Map<List<Procurement>>(itemProcurementInputList);
                for (int i = 0; i < mappedItemProcurements.Count; i++)
                {
                    itemId = (Guid)mappedItemProcurements[i].ItemId;
                    await this.InsertOrUpdateItemProcurementIntoDB(mappedItemProcurements[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateItemProcurementIntoDB(Procurement input)
        {
            try
            {
                var itemProcurementId = await this._procurementRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteItemProcurements(Guid itemId)
        {

            try
            {
                var itemSpares = await this.GetItemProcurementListFromDB(itemId);

                if (itemSpares.Count > 0)
                {
                    for (int i = 0; i < itemSpares.Count; i++)
                    {
                        await this.DeleteItemProcurementFromDB(itemSpares[i].Id);
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
        private async Task DeleteItemProcurementFromDB(Guid itemProcurementId)
        {
            try
            {
                var itemProcurementItem = await this._procurementRepository.GetAsync(itemProcurementId);

                await this._procurementRepository.DeleteAsync(itemProcurementItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<ProcurementDto>> GetItemProcurementListFromDB(Guid itemId)
        {
            try
            {
                var itemProcurementQuery = this._procurementRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId);

                return new List<ProcurementDto>(ObjectMapper.Map<List<ProcurementDto>>(itemProcurementQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }
    
    }
}
