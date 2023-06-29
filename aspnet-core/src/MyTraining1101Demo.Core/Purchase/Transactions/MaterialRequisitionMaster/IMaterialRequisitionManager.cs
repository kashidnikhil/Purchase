namespace MyTraining1101Demo.Purchase.Transactions.MaterialRequisitionMaster
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Services;
    using MyTraining1101Demo.Purchase.Shared;
    using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster;
    using System;
    using System.Threading.Tasks;

    public interface IMaterialRequisitionManager : IDomainService
    {
        Task<string> GetLatestMRINumberFromDb();

        Task<PagedResultDto<MaterialRequisitionMasterListDto>> GetPaginatedMaterialRequisitionListFromDB(MaterialRequisitionSearchDto input);

        Task<ResponseDto> InsertOrUpdateMaterialRequisitionIntoDB(MaterialRequisitionInputDto input);
        
        Task<MaterialRequisitionDto> GetMaterialRequisitionByIdFromDB(Guid materialRequisitionId);

        Task<bool> DeleteMaterialRequisitionFromDB(Guid materialRequisitionId);
    }
}
