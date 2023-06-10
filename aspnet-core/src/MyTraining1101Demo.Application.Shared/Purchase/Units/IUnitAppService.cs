namespace MyTraining1101Demo.Purchase.Units
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.Units.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
