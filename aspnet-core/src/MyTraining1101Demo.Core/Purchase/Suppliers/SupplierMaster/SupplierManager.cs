﻿using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Suppliers.SupplierMaster
{
    public class SupplierManager : MyTraining1101DemoDomainServiceBase, ISupplierManager
    {
        private readonly IRepository<Supplier, Guid> _supplierRepository;

        public SupplierManager(
           IRepository<Supplier, Guid> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<PagedResultDto<SupplierListDto>> GetPaginatedSupplierListListFromDB(SupplierMasterSearchDto input)
        {
            try
            {
                var supplierMasterQuery = this._supplierRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));


                var totalCount = await supplierMasterQuery.CountAsync();
                var items = await supplierMasterQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<SupplierListDto>(
                totalCount,
                ObjectMapper.Map<List<SupplierListDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<Guid> InsertOrUpdateSupplierMasterIntoDB(SupplierInputDto input)
        {
            try
            {
                var mappedSupplierMasterItem = ObjectMapper.Map<Supplier>(input);
                var supplierId = await this._supplierRepository.InsertOrUpdateAndGetIdAsync(mappedSupplierMasterItem);
                await CurrentUnitOfWork.SaveChangesAsync();
                return supplierId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<bool> DeleteSupplierMasterFromDB(Guid supplierId)
        {
            try
            {
                var supplierMasterItem = await this._supplierRepository.GetAsync(supplierId);

                await this._supplierRepository.DeleteAsync(supplierMasterItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<SupplierDto> GetSupplierMasterByIdFromDB(Guid supplierId)
        {
            try
            {
                var supplierMasterItem = await this._supplierRepository.GetAsync(supplierId);

                return ObjectMapper.Map<SupplierDto>(supplierMasterItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

    }
}
