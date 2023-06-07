namespace MyTraining1101Demo.Purchase.Assemblies
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Assemblies.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAssemblyManager : IDomainService
    {
        Task<PagedResultDto<AssemblyListDto>> GetPaginatedAssembliesFromDB(AssemblySearchDto input);

        Task<ResponseDto> InsertOrUpdateAssemblyIntoDB(AssemblyInputDto input);

        Task<bool> DeleteAssemblyFromDB(Guid assemblyId);

        Task<IList<AssemblyDto>> GetAssemblyListFromDB(Guid? modelId);

        Task<bool> RestoreAssembly(Guid assemblyId);

        Task<AssemblyDto> GetAssemblyByIdFromDB(Guid assemblyId);
    }
}
