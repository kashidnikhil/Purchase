namespace MyTraining1101Demo.Purchase.Assemblies
{
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto;
    using MyTraining1101Demo.Purchase.Assemblies.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AssemblyAppService : MyTraining1101DemoAppServiceBase, IAssemblyAppService
    {
        private readonly IAssemblyManager _assemblyManager;

        public AssemblyAppService(
          IAssemblyManager assemblyManager
         )
        {
            _assemblyManager = assemblyManager;
        }

        public async Task<PagedResultDto<AssemblyListDto>> GetAssemblies(AssemblySearchDto input)
        {
            try
            {
                var result = await this._assemblyManager.GetPaginatedAssembliesFromDB(input);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public async Task<ResponseDto> InsertOrUpdateAssembly(AssemblyInputDto input)
        {
            try
            {
                var insertedOrUpdatedAssembly = await this._assemblyManager.InsertOrUpdateAssemblyIntoDB(input);

                return insertedOrUpdatedAssembly;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteAssembly(Guid assemblyId)
        {
            try
            {
                var isAssemblyDeleted = await this._assemblyManager.DeleteAssemblyFromDB(assemblyId);
                return isAssemblyDeleted;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<AssemblyDto> GetAssemblyById(Guid acceptanceCriteriaId)
        {
            try
            {
                var response = await this._assemblyManager.GetAssemblyByIdFromDB(acceptanceCriteriaId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<AssemblyDto>> GetAssemblyList(Guid? modelId)
        {
            try
            {
                var response = await this._assemblyManager.GetAssemblyListFromDB(modelId);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> RestoreAssembly(Guid assemblyId)
        {
            try
            {
                var response = await this._assemblyManager.RestoreAssembly(assemblyId);
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
