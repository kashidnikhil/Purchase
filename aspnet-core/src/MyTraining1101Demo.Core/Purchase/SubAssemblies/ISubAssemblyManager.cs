namespace MyTraining1101Demo.Purchase.SubAssemblies
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.SubAssemblies.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubAssemblyManager: IDomainService
    {
        Task<PagedResultDto<SubAssemblyListDto>> GetPaginatedSubAssembliesFromDB(SubAssemblySearchDto input);

        Task<ResponseDto> InsertOrUpdateSubAssemblyIntoDB(SubAssemblyInputDto input);

        Task<bool> DeleteSubAssemblyFromDB(Guid subAssemblyItemId);

        Task<SubAssemblyDto> GetSubAssemblyByIdFromDB(Guid subAssemblyItemId);

        Task<IList<SubAssemblyDto>> GetSubAssemblyListFromDB(Guid? assemblyId);

        Task<bool> RestoreSubAssembly(Guid subAssemblyItemId);
    }
}
