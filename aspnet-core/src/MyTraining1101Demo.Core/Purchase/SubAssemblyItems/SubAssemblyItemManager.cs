namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.SubAssemblyItems.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
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

        public async Task<PagedResultDto<SubAssemblyItemListDto>> GetPaginatedSubAssemblyItemsFromDB(SubAssemblyItemSearchDto input)
        {
            try
            {
                var subAssemblyItemQuery = this._subAssemblyItemRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await subAssemblyItemQuery.CountAsync();
                var subAssemblyItems = await subAssemblyItemQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<SubAssemblyItemListDto>(
                totalCount,
                ObjectMapper.Map<List<SubAssemblyItemListDto>>(subAssemblyItems));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateSubAssemblyItemIntoDB(SubAssemblyItemInputDto input)
        {
            try
            {
                Guid subAssemblyItemId = Guid.Empty;
                var subAssemblyItem = await this._subAssemblyItemRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.AssemblyId == input.AssemblyId && x.ModelId == input.ModelId && x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (subAssemblyItem != null)
                {
                    if (input.Id != subAssemblyItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = subAssemblyItem.Name,
                            IsExistingDataAlreadyDeleted = subAssemblyItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = subAssemblyItem.Id
                        };
                    }
                    else
                    {
                        ObjectMapper.Map(input, subAssemblyItem);
                        subAssemblyItemId = await this._subAssemblyItemRepository.InsertOrUpdateAndGetIdAsync(subAssemblyItem);
                        return new ResponseDto
                        {
                            Id = subAssemblyItemId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedSubAssemblyItem = ObjectMapper.Map<SubAssemblyItem>(input);
                    subAssemblyItemId = await this._subAssemblyItemRepository.InsertOrUpdateAndGetIdAsync(mappedSubAssemblyItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = subAssemblyItemId,
                        DataMatchFound = false
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<bool> DeleteSubAssemblyItemFromDB(Guid subAssemblyItemId)
        {
            try
            {
                var subAssemblyItem = await this._subAssemblyItemRepository.GetAsync(subAssemblyItemId);

                await this._subAssemblyItemRepository.DeleteAsync(subAssemblyItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<SubAssemblyItemDto> GetSubAssemblyItemByIdFromDB(Guid subAssemblyItemId)
        {
            try
            {
                var subAssemblyItem = await this._subAssemblyItemRepository.GetAsync(subAssemblyItemId);

                return ObjectMapper.Map<SubAssemblyItemDto>(subAssemblyItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SubAssemblyItemDto>> GetSubAssemblyItemListFromDB()
        {
            try
            {
                var subAssemblyItemQuery = this._subAssemblyItemRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<SubAssemblyItemDto>(ObjectMapper.Map<List<SubAssemblyItemDto>>(subAssemblyItemQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreSubAssemblyItem(Guid subAssemblyItemId)
        {
            try
            {
                var subAssemblyItem = await this._subAssemblyItemRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == subAssemblyItemId);
                subAssemblyItem.IsDeleted = false;
                subAssemblyItem.DeleterUserId = null;
                subAssemblyItem.DeletionTime = null;
                await this._subAssemblyItemRepository.UpdateAsync(subAssemblyItem);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
