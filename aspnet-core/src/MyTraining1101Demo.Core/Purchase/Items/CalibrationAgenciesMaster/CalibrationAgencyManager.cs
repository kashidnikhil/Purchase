namespace MyTraining1101Demo.Purchase.Items.CalibrationAgenciesMaster
{
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.Items.Dto.CalibrationAgenciesMaster;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CalibrationAgencyManager : MyTraining1101DemoDomainServiceBase, ICalibrationAgencyManager
    {
        private readonly IRepository<CalibrationAgency, Guid> _calibrationAgencyRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public CalibrationAgencyManager(
           IRepository<CalibrationAgency, Guid> calibrationAgencyRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _calibrationAgencyRepository = calibrationAgencyRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<Guid> BulkInsertOrUpdateItemCalibrationAgencies(List<CalibrationAgencyInputDto> itemCalibrationAgencyInputList)
        {
            try
            {
                Guid itemId = Guid.Empty;
                var mappedItemCalibrationAgencies = ObjectMapper.Map<List<CalibrationAgency>>(itemCalibrationAgencyInputList);

                var filteredMappedItemCalibrationAgencies = mappedItemCalibrationAgencies.Where(x => x.SupplierId != null && x.SupplierId != Guid.Empty).ToList();
                    for (int i = 0; i < filteredMappedItemCalibrationAgencies.Count; i++)
                {
                    itemId = (Guid)filteredMappedItemCalibrationAgencies[i].ItemId;
                    await this.InsertOrUpdateItemCalibrationAgencyIntoDB(filteredMappedItemCalibrationAgencies[i]);
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [UnitOfWork]
        private async Task InsertOrUpdateItemCalibrationAgencyIntoDB(CalibrationAgency input)
        {
            try
            {
                var itemCalibrationAgencyId = await this._calibrationAgencyRepository.InsertOrUpdateAndGetIdAsync(input);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<bool> BulkDeleteItemCalibrationAgencies(Guid itemId)
        {
            try
            {
                var itemCalibrationAgencies = await this.GetItemCalibrationAgencyListFromDB(itemId);

                if (itemCalibrationAgencies.Count > 0)
                {
                    for (int i = 0; i < itemCalibrationAgencies.Count; i++)
                    {
                        await this.DeleteItemCalibrationAgencyFromDB(itemCalibrationAgencies[i].Id);
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
        private async Task DeleteItemCalibrationAgencyFromDB(Guid itemCalibrationAgencyId)
        {
            try
            {
                var itemCalibrationAgencyItem = await this._calibrationAgencyRepository.GetAsync(itemCalibrationAgencyId);

                await this._calibrationAgencyRepository.DeleteAsync(itemCalibrationAgencyItem);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<CalibrationAgencyDto>> GetItemCalibrationAgencyListFromDB(Guid itemId)
        {
            try
            {
                var itemCalibrationAgencyQuery = this._calibrationAgencyRepository.GetAll()
                    .Where(x => !x.IsDeleted && x.ItemId == itemId);

                return new List<CalibrationAgencyDto>(ObjectMapper.Map<List<CalibrationAgencyDto>>(itemCalibrationAgencyQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
