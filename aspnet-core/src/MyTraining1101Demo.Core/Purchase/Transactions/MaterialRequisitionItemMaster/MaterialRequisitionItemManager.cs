namespace MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionItemMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionItem;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MaterialRequisitionItemManager : MyTraining1101DemoDomainServiceBase, IMaterialRequisitionItemManager
    {
        private readonly IRepository<MaterialRequisitionItem, Guid> _materialRequisitionItemRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public MaterialRequisitionItemManager(
           IRepository<MaterialRequisitionItem, Guid> materialRequisitionItemRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _materialRequisitionItemRepository = materialRequisitionItemRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<Guid> BulkInsertOrUpdateMaterialRequisitionItems(List<MaterialRequisitionItemInputDto> materialRequisitionItemInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedMaterialRequisitionItems = ObjectMapper.Map<List<MaterialRequisitionItem>>(materialRequisitionItemInputList);
                for (int i = 0; i < mappedMaterialRequisitionItems.Count; i++)
                {
                    itemId = (Guid)mappedMaterialRequisitionItems[i].ItemId;
                    await this.InsertOrUpdateMaterialRequisitionItemIntoDB(mappedMaterialRequisitionItems[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateMaterialRequisitionItemIntoDB(MaterialRequisitionItem input)
        {
            try
            {
                var materialRequisitionItemId = await this._materialRequisitionItemRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteMaterialRequisitionItem(Guid materialRequisitionId)
        {
            try
            {
                var materialRequisitionItems = await this.GetMaterialRequisitionItemListFromDB(materialRequisitionId);

                if (materialRequisitionItems.Count > 0)
                {
                    for (int i = 0; i < materialRequisitionItems.Count; i++)
                    {
                        await this.DeleteMaterialRequisitionItemFromDB(materialRequisitionItems[i].Id);
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
        private async Task DeleteMaterialRequisitionItemFromDB(Guid materialRequsitionItemId)
        {
            try
            {
                var materialRequisitionItem = await this._materialRequisitionItemRepository.GetAsync(materialRequsitionItemId);

                await this._materialRequisitionItemRepository.DeleteAsync(materialRequisitionItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<MaterialRequisitionItemDto>> GetMaterialRequisitionItemListFromDB(Guid materialRequsitionId)
        {
            try
            {
                var materialRequsitionItemQuery = this._materialRequisitionItemRepository.GetAll()
                    .Include(x=> x.SubAssemblyItem).ThenInclude(x=> x.Item)
                    .Include(x => x.SubAssemblyItem).ThenInclude(x=> x.SubAssembly).ThenInclude(x=> x.Assembly)
                    .Include(x=> x.Item).ThenInclude(x=> x.ItemCategory)
                    .Where(x => !x.IsDeleted && x.MaterialRequisitionId == materialRequsitionId);

                return new List<MaterialRequisitionItemDto>(ObjectMapper.Map<List<MaterialRequisitionItemDto>>(materialRequsitionItemQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
