namespace MyTraining1101Demo.Purchase.MaterialGrades
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.MaterialGrades.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MaterialGradeAppService : MyTraining1101DemoAppServiceBase, IMaterialGradeAppService
    {
        private readonly IMaterialGradeManager _materialGradeManager;

        public MaterialGradeAppService(
          IMaterialGradeManager materialGradeManager
         )
        {
            _materialGradeManager = materialGradeManager;
        }


        public async Task<PagedResultDto<MaterialGradeDto>> GetMaterialGrades(MaterialGradeSearchDto input)
        {
            try
            {
                var result = await this._materialGradeManager.GetPaginatedMaterialGradesFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateMaterialGrade(MaterialGradeInputDto input)
        {
            try
            {
                var insertedOrUpdatedMaterialGradeId = await this._materialGradeManager.InsertOrUpdateMaterialGradeIntoDB(input);

                return insertedOrUpdatedMaterialGradeId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteMaterialGrade(Guid materialGradeId)
        {
            try
            {
                var isMaterialGradeDeleted = await this._materialGradeManager.DeleteMaterialGradeFromDB(materialGradeId);
                return isMaterialGradeDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<MaterialGradeDto> GetMaterialGradeById(Guid materialGradeId)
        {
            try
            {
                var response = await this._materialGradeManager.GetMaterialGradeByIdFromDB(materialGradeId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<MaterialGradeDto>> GetMaterialGradeList()
        {
            try
            {
                var response = await this._materialGradeManager.GetMaterialGradeListFromDB();
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreMaterialGrade(Guid materialGradeId)
        {
            try
            {
                var response = await this._materialGradeManager.RestoreMaterialGrade(materialGradeId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
