namespace MyTraining1101Demo.Purchase.MaterialGrades
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.MaterialGrades.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;

    using System.Threading.Tasks;
    public interface IMaterialGradeManager : IDomainService
    {
        Task<PagedResultDto<MaterialGradeDto>> GetPaginatedMaterialGradesFromDB(MaterialGradeSearchDto input);
        Task<ResponseDto> InsertOrUpdateMaterialGradeIntoDB(MaterialGradeInputDto input);

        Task<bool> DeleteMaterialGradeFromDB(Guid materialGradeId);

        Task<MaterialGradeDto> GetMaterialGradeByIdFromDB(Guid materialGradeId);

        Task<IList<MaterialGradeDto>> GetMaterialGradeListFromDB();

        Task<bool> RestoreMaterialGrade(Guid materialGradeId);
    }
}
