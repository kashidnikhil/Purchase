﻿using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.Suppliers.Dto.SupplierMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Suppliers
{
    public interface ISupplierAppService
    {
        Task<PagedResultDto<SupplierListDto>> GetSuppliers(SupplierMasterSearchDto input);

        Task<Guid> InsertOrUpdateSupplier(SupplierInputDto input);

        Task<bool> DeleteSupplierMasterData(Guid supplierId);
        Task<SupplierDto> GetSupplierMasterById(Guid supplierId);
    }
}