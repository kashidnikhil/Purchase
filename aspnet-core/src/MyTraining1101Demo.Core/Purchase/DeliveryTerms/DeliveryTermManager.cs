using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using MyTraining1101Demo.Purchase.DeliveryTerms.Dto;
using MyTraining1101Demo.Purchase.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.DeliveryTerms
{
    public class DeliveryTermManager : MyTraining1101DemoDomainServiceBase, IDeliveryTermManager
    {
        private readonly IRepository<DeliveryTerm, Guid> _deliveryTermRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public DeliveryTermManager(
           IRepository<DeliveryTerm, Guid> deliveryTermRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _deliveryTermRepository = deliveryTermRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<DeliveryTermDto>> GetPaginatedDeliveryTermsFromDB(DeliveryTermSearchDto input)
        {
            try
            {
                var DeliveryTermQuery = this._deliveryTermRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await DeliveryTermQuery.CountAsync();
                var items = await DeliveryTermQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<DeliveryTermDto>(
                totalCount,
                ObjectMapper.Map<List<DeliveryTermDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateDeliveryTermIntoDB(DeliveryTermInputDto input)
        {
            try
            {
                Guid DeliveryTermId = Guid.Empty;
                var DeliveryTermItem = await this._deliveryTermRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (DeliveryTermItem != null)
                {
                    if (input.Id != DeliveryTermItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = DeliveryTermItem.Name,
                            IsExistingDataAlreadyDeleted = DeliveryTermItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = DeliveryTermItem.Id
                        };
                    }
                    else
                    {
                        DeliveryTermItem.Name = input.Name;
                        DeliveryTermItem.Description = input.Description;
                        DeliveryTermId = await this._deliveryTermRepository.InsertOrUpdateAndGetIdAsync(DeliveryTermItem);
                        return new ResponseDto
                        {
                            Id = DeliveryTermId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedDeliveryTermItem = ObjectMapper.Map<DeliveryTerm>(input);
                    DeliveryTermId = await this._deliveryTermRepository.InsertOrUpdateAndGetIdAsync(mappedDeliveryTermItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = DeliveryTermId,
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
        public async Task<bool> DeleteDeliveryTermFromDB(Guid deliveryTermId)
        {
            try
            {
                var DeliveryTermItem = await this._deliveryTermRepository.GetAsync(deliveryTermId);

                await this._deliveryTermRepository.DeleteAsync(DeliveryTermItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<DeliveryTermDto> GetDeliveryTermByIdFromDB(Guid deliveryTermId)
        {
            try
            {
                var DeliveryTermItem = await this._deliveryTermRepository.GetAsync(deliveryTermId);

                return ObjectMapper.Map<DeliveryTermDto>(DeliveryTermItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<DeliveryTermDto>> GetDeliveryTermListFromDB()
        {
            try
            {
                var deliveryTermQuery = this._deliveryTermRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<DeliveryTermDto>(ObjectMapper.Map<List<DeliveryTermDto>>(deliveryTermQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreDeliveryTerm(Guid deliveryTermId)
        {
            try
            {
                var DeliveryTermItem = await this._deliveryTermRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == deliveryTermId);
                DeliveryTermItem.IsDeleted = false;
                DeliveryTermItem.DeleterUserId = null;
                DeliveryTermItem.DeletionTime = null;
                await this._deliveryTermRepository.UpdateAsync(DeliveryTermItem);

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
