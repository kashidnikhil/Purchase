namespace MyTraining1101Demo.Purchase.POGeneralTerms
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Repositories;
    using Abp.Domain.Uow;
    using Abp.Extensions;
    using Abp.Linq.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using MyTraining1101Demo.Configuration;
    using MyTraining1101Demo.Purchase.POGeneralTerms.Dto;
    using MyTraining1101Demo.Purchase.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    public class POGeneralTermManager
    : MyTraining1101DemoDomainServiceBase, IPOGeneralTermManager
    {
        private readonly IRepository<POGeneralTerm, Guid> _poGeneralTermRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public POGeneralTermManager(
           IRepository<POGeneralTerm, Guid> poGeneralTermRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _poGeneralTermRepository = poGeneralTermRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<POGeneralTermDto>> GetPaginatedPOGeneralTermsFromDB(POGeneralTermSearchDto input)
        {
            try
            {
                var POGeneralTermQuery = this._poGeneralTermRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await POGeneralTermQuery.CountAsync();
                var items = await POGeneralTermQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<POGeneralTermDto>(
                totalCount,
                ObjectMapper.Map<List<POGeneralTermDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdatePOGeneralTermIntoDB(POGeneralTermInputDto input)
        {
            try
            {
                Guid POGeneralTermId = Guid.Empty;
                var POGeneralTermItem = await this._poGeneralTermRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (POGeneralTermItem != null)
                {
                    if (input.Id != POGeneralTermItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = POGeneralTermItem.Name,
                            IsExistingDataAlreadyDeleted = POGeneralTermItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = POGeneralTermItem.Id
                        };
                    }
                    else
                    {
                        POGeneralTermItem.Name = input.Name;
                        POGeneralTermItem.Description = input.Description;
                        POGeneralTermId = await this._poGeneralTermRepository.InsertOrUpdateAndGetIdAsync(POGeneralTermItem);
                        return new ResponseDto
                        {
                            Id = POGeneralTermId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedPOGeneralTermItem = ObjectMapper.Map<POGeneralTerm>(input);
                    POGeneralTermId = await this._poGeneralTermRepository.InsertOrUpdateAndGetIdAsync(mappedPOGeneralTermItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = POGeneralTermId,
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
        public async Task<bool> DeletePOGeneralTermFromDB(Guid poGeneralTermId)
        {
            try
            {
                var POGeneralTermItem = await this._poGeneralTermRepository.GetAsync(poGeneralTermId);

                await this._poGeneralTermRepository.DeleteAsync(POGeneralTermItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<POGeneralTermDto> GetPOGeneralTermByIdFromDB(Guid poGeneralTermId)
        {
            try
            {
                var POGeneralTermItem = await this._poGeneralTermRepository.GetAsync(poGeneralTermId);

                return ObjectMapper.Map<POGeneralTermDto>(POGeneralTermItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<POGeneralTermDto>> GetPOGeneralTermListFromDB()
        {
            try
            {
                var POGeneralTermQuery = this._poGeneralTermRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<POGeneralTermDto>(ObjectMapper.Map<List<POGeneralTermDto>>(POGeneralTermQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestorePOGeneralTerm(Guid poGeneralTermId)
        {
            try
            {
                var POGeneralTermItem = await this._poGeneralTermRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == poGeneralTermId);
                POGeneralTermItem.IsDeleted = false;
                POGeneralTermItem.DeleterUserId = null;
                POGeneralTermItem.DeletionTime = null;
                await this._poGeneralTermRepository.UpdateAsync(POGeneralTermItem);

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
