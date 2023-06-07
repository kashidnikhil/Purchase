namespace MyTraining1101Demo.Purchase.Assemblies
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Assemblies.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IAssemblyAppService
    {
        Task<PagedResultDto<AssemblyListDto>> GetAssemblies(AssemblySearchDto input);
        Task<bool> DeleteAssembly(Guid assemblyId);

        Task<AssemblyDto> GetAssemblyById(Guid acceptanceCriteriaId);

        Task<IList<AssemblyDto>> GetAssemblyList(Guid? modelId);

        Task<bool> RestoreAcceptanceCriteria(Guid assemblyId);
    }
}
