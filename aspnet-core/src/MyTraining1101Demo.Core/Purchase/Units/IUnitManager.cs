using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.Units.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Units
{
    public interface IUnitManager : IDomainService
    {
        Task<PagedResultDto<UnitDto>> GetPaginatedUnitFromDB(UnitSearchDto input);
        Task<ResponseDto> InsertOrUpdateUnitIntoDB(UnitInputDto input);

        Task<bool> DeleteUnitFromDB(Guid unitId);

        Task<UnitDto> GetUnitByIdFromDB(Guid unitId);

        Task<IList<UnitDto>> GetUnitListFromDB();

        Task<bool> RestoreUnit(Guid unitId);
    }
}
