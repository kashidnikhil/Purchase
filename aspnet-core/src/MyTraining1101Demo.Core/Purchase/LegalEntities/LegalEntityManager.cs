using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using MyTraining1101Demo.Purchase.LegalEntity.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.LegalEntities
{


    public class LegalEntityManager : MyTraining1101DemoDomainServiceBase, ILegalEntityManager
    {
        private readonly IRepository<LegalEntity, Guid> _legalEntityRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public LegalEntityManager(
           IRepository<LegalEntity, Guid> legalEntityRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _legalEntityRepository = legalEntityRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<LegalEntityDto>> GetPaginatedLegalEntitiesFromDB(LegalEntitySearchDto input)
        {
            try
            {
                var legalEntityQuery = this._legalEntityRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await legalEntityQuery.CountAsync();
                var items = await legalEntityQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<LegalEntityDto>(
                totalCount,
                ObjectMapper.Map<List<LegalEntityDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateLegalEntityIntoDB(LegalEntityInputDto input)
        {
            try
            {
                Guid LegalEntityId = Guid.Empty;
                var LegalEntityItem = await this._legalEntityRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (LegalEntityItem != null)
                {
                    if (input.Id != LegalEntityItem.Id) {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = LegalEntityItem.Name,
                            IsExistingDataAlreadyDeleted = LegalEntityItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = LegalEntityItem.Id
                        };
                    }
                    else
                    {
                        LegalEntityItem.Name = input.Name;
                        LegalEntityItem.Description = input.Description;
                        LegalEntityId = await this._legalEntityRepository.InsertOrUpdateAndGetIdAsync(LegalEntityItem);
                        return new ResponseDto
                        {
                            Id = LegalEntityId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedLegalEntityItem = ObjectMapper.Map<LegalEntity>(input);
                    LegalEntityId = await this._legalEntityRepository.InsertOrUpdateAndGetIdAsync(mappedLegalEntityItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = LegalEntityId,
                        DataMatchFound = false
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        [UnitOfWork]
        public async Task<bool> DeleteLegalEntityFromDB(Guid LegalEntityId)
        {
            try
            {
                var LegalEntityItem = await this._legalEntityRepository.GetAsync(LegalEntityId);

                await this._legalEntityRepository.DeleteAsync(LegalEntityItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<LegalEntityDto> GetLegalEntityByIdFromDB(Guid LegalEntityId)
        {
            try
            {
                var LegalEntityItem = await this._legalEntityRepository.GetAsync(LegalEntityId);

                return ObjectMapper.Map<LegalEntityDto>(LegalEntityItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<LegalEntityDto>> GetLegalEntityListFromDB()
        {
            try
            {
                var LegalEntityQuery = this._legalEntityRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<LegalEntityDto>(ObjectMapper.Map<List<LegalEntityDto>>(LegalEntityQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreLegalEntity(Guid LegalEntityId)
        {
            try
            {
                var LegalEntityItem = await this._legalEntityRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == LegalEntityId);
                LegalEntityItem.IsDeleted = false;
                LegalEntityItem.DeleterUserId = null;
                LegalEntityItem.DeletionTime = null;
                await this._legalEntityRepository.UpdateAsync(LegalEntityItem);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
    }
}
