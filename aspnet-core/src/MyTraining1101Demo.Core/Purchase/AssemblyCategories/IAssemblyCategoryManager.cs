namespace MyTraining1101Demo.Purchase.AssemblyCategories
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.AssemblyCategories.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAssemblyCategoryManager : IDomainService
    {
        Task<PagedResultDto<AssemblyCategoryListDto>> GetPaginatedAssemblyCategoriesFromDB(AssemblyCategorySearchDto input);
        Task<Guid> InsertOrUpdateAssemblyCategoryIntoDB(AssemblyCategoryInputDto input);

        Task<bool> DeleteAssemblyCategoryFromDB(Guid assemblyCategoryId);

        Task<AssemblyCategoryDto> GetAssemblyCategoryByIdFromDB(Guid assemblyCategoryId);

        Task<IList<AssemblyCategoryDto>> GetAssemblyCategorylListFromDB();


    }
}
