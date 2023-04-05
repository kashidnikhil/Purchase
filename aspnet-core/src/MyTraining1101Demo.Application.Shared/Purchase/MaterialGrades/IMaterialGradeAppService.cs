using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.MaterialGrades.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.MaterialGrades
{
    public interface IMaterialGradeAppService
    {
        Task<PagedResultDto<MaterialGradeDto>> GetMaterialGrades(MaterialGradeSearchDto input);
        Task<ResponseDto> InsertOrUpdateMaterialGrade(MaterialGradeInputDto input);

        Task<bool> DeleteMaterialGrade(Guid materialGradeId);

        Task<MaterialGradeDto> GetMaterialGradeById(Guid materialGradeId);

        Task<IList<MaterialGradeDto>> GetMaterialGradeList();

        Task<bool> RestoreMaterialGrade(Guid materialGradeId);
    }
}
