namespace MyTraining1101Demo.Purchase.AcceptanceCriterias
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.AcceptanceCriterias.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class AcceptanceCriteriaManager : MyTraining1101DemoDomainServiceBase, IAcceptanceCriteriaManager
    {
        private readonly IRepository<AcceptanceCriteria, Guid> _acceptanceCriteriaRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public AcceptanceCriteriaManager(
           IRepository<AcceptanceCriteria, Guid> acceptanceCriteriaRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _acceptanceCriteriaRepository = acceptanceCriteriaRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<AcceptanceCriteriaDto>> GetPaginatedAcceptanceCriteriasFromDB(AcceptanceCriteriaSearchDto input)
        {
            try
            {
                var AcceptanceCriteriaQuery = this._acceptanceCriteriaRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await AcceptanceCriteriaQuery.CountAsync();
                var items = await AcceptanceCriteriaQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<AcceptanceCriteriaDto>(
                totalCount,
                ObjectMapper.Map<List<AcceptanceCriteriaDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateAcceptanceCriteriaIntoDB(AcceptanceCriteriaInputDto input)
        {
            try
            {
                Guid AcceptanceCriteriaId = Guid.Empty;
                var AcceptanceCriteriaItem = await this._acceptanceCriteriaRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (AcceptanceCriteriaItem != null)
                {
                    if (input.Id != AcceptanceCriteriaItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = AcceptanceCriteriaItem.Name,
                            IsExistingDataAlreadyDeleted = AcceptanceCriteriaItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = AcceptanceCriteriaItem.Id
                        };
                    }
                    else
                    {
                        AcceptanceCriteriaItem.Name = input.Name;
                        AcceptanceCriteriaItem.Description = input.Description;
                        AcceptanceCriteriaId = await this._acceptanceCriteriaRepository.InsertOrUpdateAndGetIdAsync(AcceptanceCriteriaItem);
                        return new ResponseDto
                        {
                            Id = AcceptanceCriteriaId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedAcceptanceCriteriaItem = ObjectMapper.Map<AcceptanceCriteria>(input);
                    AcceptanceCriteriaId = await this._acceptanceCriteriaRepository.InsertOrUpdateAndGetIdAsync(mappedAcceptanceCriteriaItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = AcceptanceCriteriaId,
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
        public async Task<bool> DeleteAcceptanceCriteriaFromDB(Guid acceptanceCriteriaId)
        {
            try
            {
                var AcceptanceCriteriaItem = await this._acceptanceCriteriaRepository.GetAsync(acceptanceCriteriaId);

                await this._acceptanceCriteriaRepository.DeleteAsync(AcceptanceCriteriaItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<AcceptanceCriteriaDto> GetAcceptanceCriteriaByIdFromDB(Guid acceptanceCriteriaId)
        {
            try
            {
                var AcceptanceCriteriaItem = await this._acceptanceCriteriaRepository.GetAsync(acceptanceCriteriaId);

                return ObjectMapper.Map<AcceptanceCriteriaDto>(AcceptanceCriteriaItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<AcceptanceCriteriaDto>> GetAcceptanceCriteriaListFromDB()
        {
            try
            {
                var AcceptanceCriteriaQuery = this._acceptanceCriteriaRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<AcceptanceCriteriaDto>(ObjectMapper.Map<List<AcceptanceCriteriaDto>>(AcceptanceCriteriaQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreAcceptanceCriteria(Guid acceptanceCriteriaId)
        {
            try
            {
                var AcceptanceCriteriaItem = await this._acceptanceCriteriaRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == acceptanceCriteriaId);
                AcceptanceCriteriaItem.IsDeleted = false;
                AcceptanceCriteriaItem.DeleterUserId = null;
                AcceptanceCriteriaItem.DeletionTime = null;
                await this._acceptanceCriteriaRepository.UpdateAsync(AcceptanceCriteriaItem);

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
