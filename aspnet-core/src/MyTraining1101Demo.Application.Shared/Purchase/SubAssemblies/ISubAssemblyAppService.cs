namespace MyTraining1101Demo.Purchase.SubAssemblyItems
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.SubAssemblies.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ISubAssemblyAppService
    {
        Task<PagedResultDto<SubAssemblyListDto>> GetSubAssemblies(SubAssemblySearchDto input);
        Task<ResponseDto> InsertOrUpdateSubAssembly(SubAssemblyInputDto input);

        Task<bool> DeleteSubAssembly(Guid subAssemblyItemId);

        Task<SubAssemblyDto> GetSubAssemblyById(Guid subAssemblyId);

        Task<IList<SubAssemblyDto>> GetSubAssemblyList();

        Task<bool> RestoreSubAssembly(Guid subAssemblyId);

    }
}
