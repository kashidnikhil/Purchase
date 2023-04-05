using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.SupplierCategories.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace MyTraining1101Demo.Purchase.SupplierCategories
{
    public class SupplierCategoryManager : MyTraining1101DemoDomainServiceBase, ISupplierCategoryManager
    {
        private readonly IRepository<SupplierCategory, Guid> _supplierCategoryRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public SupplierCategoryManager(
           IRepository<SupplierCategory, Guid> supplierCategoryRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _supplierCategoryRepository = supplierCategoryRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<SupplierCategoryDto>> GetPaginatedSupplierCategoriesFromDB(SupplierCategorySearchDto input)
        {
            try
            {
                var taxQuery = this._supplierCategoryRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await taxQuery.CountAsync();
                var items = await taxQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<SupplierCategoryDto>(
                totalCount,
                ObjectMapper.Map<List<SupplierCategoryDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateSupplierCategoryIntoDB(SupplierCategoryInputDto input)
        {
            try
            {
                Guid SupplierCategoryId = Guid.Empty;
                var SupplierCategoryItem = await this._supplierCategoryRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (SupplierCategoryItem != null)
                {
                    if (input.Id != SupplierCategoryItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = SupplierCategoryItem.Name,
                            IsExistingDataAlreadyDeleted = SupplierCategoryItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = SupplierCategoryItem.Id
                        };
                    }
                    else
                    {
                        SupplierCategoryItem.Name = input.Name;
                        SupplierCategoryItem.Description = input.Description;
                        SupplierCategoryId = await this._supplierCategoryRepository.InsertOrUpdateAndGetIdAsync(SupplierCategoryItem);
                        return new ResponseDto
                        {
                            Id = SupplierCategoryId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedSupplierCategoryItem = ObjectMapper.Map<SupplierCategory>(input);
                    SupplierCategoryId = await this._supplierCategoryRepository.InsertOrUpdateAndGetIdAsync(mappedSupplierCategoryItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = SupplierCategoryId,
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
        public async Task<bool> DeleteSupplierCategoryFromDB(Guid supplierCategoryId)
        {
            try
            {
                var SupplierCategoryItem = await this._supplierCategoryRepository.GetAsync(supplierCategoryId);

                await this._supplierCategoryRepository.DeleteAsync(SupplierCategoryItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<SupplierCategoryDto> GetSupplierCategoryByIdFromDB(Guid supplierCategoryId)
        {
            try
            {
                var SupplierCategoryItem = await this._supplierCategoryRepository.GetAsync(supplierCategoryId);

                return ObjectMapper.Map<SupplierCategoryDto>(SupplierCategoryItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<SupplierCategoryDto>> GetSupplierCategoryListFromDB()
        {
            try
            {
                var SupplierCategoryQuery = this._supplierCategoryRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<SupplierCategoryDto>(ObjectMapper.Map<List<SupplierCategoryDto>>(SupplierCategoryQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreSupplierCategory(Guid supplierCategoryId)
        {
            try
            {
                var SupplierCategoryItem = await this._supplierCategoryRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == supplierCategoryId);
                SupplierCategoryItem.IsDeleted = false;
                SupplierCategoryItem.DeleterUserId = null;
                SupplierCategoryItem.DeletionTime = null;
                await this._supplierCategoryRepository.UpdateAsync(SupplierCategoryItem);

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
