namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.SubAssemblies.Dto;
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

        public async Task<Guid> BulkInsertOrUpdateSubAssemblyItems(List<SubAssemblyInputDto> itemCalibrationAgencyInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedSubAssemblyItems = ObjectMapper.Map<List<SubAssemblyItem>>(itemCalibrationAgencyInputList);

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
                var subAssemblyItems = await this.GetSubAssemblyItemListFromDB(subAssemblyId);

                if (subAssemblyItems.Count > 0)
                {
                    for (int i = 0; i < subAssemblyItems.Count; i++)
                    {
                        await this.DeleteSubAssemblyItemFromDB(subAssemblyItems[i].Id);
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

        public async Task<IList<SubAssemblyItemDto>> GetSubAssemblyItemListFromDB(Guid subAssemblyId)
        {
            try
            {
                var subAssemblyItemQuery = this._subAssemblyItemRepository.GetAllIncluding(x => x.SubAssembly)
                    .Include(x=> x.Item).ThenInclude(x=> x.OrderingUOM)
                    .Where(x => !x.IsDeleted && x.SubAssemblyId == subAssemblyId)
                    .Select(x=> new SubAssemblyItemDto {
                    Id = x.Id,
                    ItemId = x.ItemId,
                    CategoryId = x.Item.CategoryId,
                    ItemName = x.Item.ItemName,
                    Make = x.Item.Make,
                    UnitName = x.Item.OrderingUOM.Name,
                    GenericName = x.Item.GenericName,
                    ExistingItemId = x.Item.ItemId,
                    SubAssemblyId = x.SubAssemblyId,
                    SubAssemblyName = x.SubAssembly.Name
                    });

                var response =  ObjectMapper.Map<List<SubAssemblyItemDto>>(subAssemblyItemQuery);

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
