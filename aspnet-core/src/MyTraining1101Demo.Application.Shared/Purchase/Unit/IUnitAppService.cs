﻿using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.Unit.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Unit
{
    public interface IUnitAppService
    {
        Task<PagedResultDto<UnitDto>> GetUnits(UnitSearchDto input);
        Task<ResponseDto> InsertOrUpdateUnit(UnitInputDto input);

        Task<bool> DeleteUnit(Guid unitId);

        Task<UnitDto> GetUnitById(Guid unitId);

        Task<IList<UnitDto>> GetUnitList();

        Task<bool> RestoreUnit(Guid unitId);
    }
}
