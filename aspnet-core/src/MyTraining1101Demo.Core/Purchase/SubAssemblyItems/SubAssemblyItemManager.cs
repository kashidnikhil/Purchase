namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.SubAssemblyItems.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubAssemblyItemManager : MyTraining1101DemoDomainServiceBase, ISubAssemblyItemManager
    {
        private readonly IRepository<SubAssemblyItem, Guid> _subAssemblyItemRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public SubAssemblyItemManager(
           IRepository<SubAssemblyItem, Guid> subAssemblyItemRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _subAssemblyItemRepository = subAssemblyItemRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<Guid> BulkInsertOrUpdateSubAssemblyItems(List<SubAssemblyItemInputDto> subAssemblyItemInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;

                // Using the approach of (soft) delete and insert for updating the records. (soft) delete is used because it should not hamper the other functionalities at any point of time.
                await this.BulkDeleteSubAssemblyItems((Guid)subAssemblyItemInputList[0].SubAssemblyId);

                var mappedSubAssemblyItems = ObjectMapper.Map<List<SubAssemblyItem>>(subAssemblyItemInputList);

                //var filteredMappedItemCalibrationAgencies = mappedSubAssemblyItems.Where(x => x.SupplierId != null && x.SupplierId != Guid.Empty).ToList();
                for (int i = 0; i < mappedSubAssemblyItems.Count; i++)
                {
                    itemId = (Guid)mappedSubAssemblyItems[i].SubAssemblyId;
                    await this.InsertOrUpdateSubAssemblyItemIntoDB(mappedSubAssemblyItems[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateSubAssemblyItemIntoDB(SubAssemblyItem input)
        {
            try
            {
                var subAssemblyItemId = await this._subAssemblyItemRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteSubAssemblyItems(Guid subAssemblyId)
        {
            try
            {
                var subAssemblyItemList = await this.GetSubAssemblyItemListFromDB(subAssemblyId);

                if (subAssemblyItemList.Count > 0)
                {
                    for (int i = 0; i < subAssemblyItemList.Count; i++)
                    {
                        await this.DeleteSubAssemblyItemFromDB(subAssemblyItemList[i].Id);
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
        private async Task DeleteSubAssemblyItemFromDB(Guid subAssemblyItemId)
        {
            try
            {
                var subAssemblyItem = await this._subAssemblyItemRepository.GetAsync(subAssemblyItemId);

                await this._subAssemblyItemRepository.DeleteAsync(subAssemblyItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<List<SubAssemblyItemDto>> GetSubAssemblyItemListFromDB(Guid subAssemblyId)
        {
            try
            {
                var subAssemblyItemQuery = this._subAssemblyItemRepository.GetAllIncluding(x=> x.Item)
                    .Include(x=> x.SubAssembly)
                    .Where(x => !x.IsDeleted && x.SubAssemblyId == subAssemblyId).ToList();

                var response = new List<SubAssemblyItemDto>(ObjectMapper.Map<List<SubAssemblyItemDto>>(subAssemblyItemQuery));
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<List<SubAssemblyItemDto>> GetSubAssemblyItemsByAssemblyIdFromDB(Guid assemblyId)
        {
            try
            {
                var subAssemblyItemQuery = this._subAssemblyItemRepository.GetAllIncluding(x => x.Item)
                    .Include(x => x.SubAssembly)
                    .Where(x => !x.IsDeleted && x.SubAssembly.AssemblyId == assemblyId).ToList();

                var response = new List<SubAssemblyItemDto>(ObjectMapper.Map<List<SubAssemblyItemDto>>(subAssemblyItemQuery));
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
