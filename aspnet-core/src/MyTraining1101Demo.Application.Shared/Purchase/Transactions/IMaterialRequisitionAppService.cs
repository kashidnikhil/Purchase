namespace MyTraining1101Demo.Purchase.Transactions
{
    using MyTraining1101Demo.Purchase.Transactions.Dto.MaterialRequisitionMaster;
    using System.Threading.Tasks;
    using System;
    using Abp.Application.Services.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    public interface IMaterialRequisitionAppService
    {
        Task<string> GetLatestMaterialRequisitionNumber();
        Task<PagedResultDto<MaterialRequisitionMasterListDto>> GetMaterialRequisitions(MaterialRequisitionSearchDto input);
        Task<MaterialRequisitionDto> GetMaterialRequisitionById(Guid materialRequisitionId);

        Task<ResponseDto> InsertOrUpdateMaterialRequisition(MaterialRequisitionInputDto input);

        Task<bool> DeleteMaterialRequisition(Guid materialRequisitionId);
    }
}
