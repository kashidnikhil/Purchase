using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using MyTraining1101Demo.Purchase.Items.Dto.CalibrationTypeMaster;
using MyTraining1101Demo.Purchase.Items.Dto.ItemAttachmentsMaster;
using MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster
{
    public class CalibrationTypeManager : MyTraining1101DemoDomainServiceBase, ICalibrationTypeManager
    {
        private readonly IRepository<CalibrationType, Guid> _calibrationTypeRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public CalibrationTypeManager(
           IRepository<CalibrationType, Guid> calibrationTypeRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _calibrationTypeRepository = calibrationTypeRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<Guid> BulkInsertOrUpdateItemCalibrationTypes(List<CalibrationTypeInputDto> itemCalibrationTypeInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemCalibrationTypes = ObjectMapper.Map<List<CalibrationType>>(itemCalibrationTypeInputList);
                for (int i = 0; i < mappedItemCalibrationTypes.Count; i++)
                {
                    itemId = (Guid)mappedItemCalibrationTypes[i].ItemId;
                    await this.InsertOrUpdateItemCalibrationTypeIntoDB(mappedItemCalibrationTypes[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateItemCalibrationTypeIntoDB(CalibrationType input)
        {
            try
            {
                var itemCalibrationTypeId = await this._calibrationTypeRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteItemCalibrationType(Guid itemId)
        {
            try
            {
                var itemCalibrationTypes = await this.GetItemCalibrationTypeListFromDB(itemId);

                if (itemCalibrationTypes.Count > 0)
                {
                    for (int i = 0; i < itemCalibrationTypes.Count; i++)
                    {
                        await this.DeleteItemCalibrationTypeFromDB(itemCalibrationTypes[i].Id);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task DeleteItemCalibrationTypeFromDB(Guid itemCalibrationTypeId)
        {
            try
            {
                var itemCalibrationTypeItem = await this._calibrationTypeRepository.GetAsync(itemCalibrationTypeId);

                await this._calibrationTypeRepository.DeleteAsync(itemCalibrationTypeItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<CalibrationTypeDto>> GetItemCalibrationTypeListFromDB(Guid itemId)
        {
            try
            {
                var itemCalibrationTypeQuery = this._calibrationTypeRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId);

                return new List<CalibrationTypeDto>(ObjectMapper.Map<List<CalibrationTypeDto>>(itemCalibrationTypeQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

    }
}
