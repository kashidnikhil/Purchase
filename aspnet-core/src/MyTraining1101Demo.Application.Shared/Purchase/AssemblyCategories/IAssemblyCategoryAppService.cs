namespace MyTraining1101Demo.Purchase.AssemblyCategories
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.AssemblyCategories.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IAssemblyCategoryAppService
    {
        Task<PagedResultDto<AssemblyCategoryListDto>> GetAssemblyCategories(AssemblyCategorySearchDto input);

        Task<Guid> InsertOrUpdateAssemblyCategory(AssemblyCategoryInputDto input);

        Task<bool> DeleteAssemblyCategory(Guid assemblyCategoryId);

        Task<AssemblyCategoryDto> GetAssemblyCategoryById(Guid assemblyCategoryId);

        Task<IList<AssemblyCategoryDto>> GetAssemblyCategoryList();
    }
}
