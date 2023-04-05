using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using MyTraining1101Demo.Purchase.Shared;
using MyTraining1101Demo.Purchase.TermsOfPayments.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MyTraining1101Demo.Purchase.TermsOfPayments
{
    public class TermsOfPaymentManager : MyTraining1101DemoDomainServiceBase, ITermsOfPaymentManager
    {
        private readonly IRepository<TermsOfPayment, Guid> _termsOfPaymentRepository;
        private readonly IConfigurationRoot _appConfiguration;


        public TermsOfPaymentManager(
           IRepository<TermsOfPayment, Guid> termsOfPaymentRepository,
           IAppConfigurationAccessor configurationAccessor
            )
        {
            _termsOfPaymentRepository = termsOfPaymentRepository;
            _appConfiguration = configurationAccessor.Configuration;
        }

        public async Task<PagedResultDto<TermsOfPaymentDto>> GetPaginatedTermsOfPaymentsFromDB(TermsOfPaymentSearchDto input)
        {
            try
            {
                var TermsOfPaymentQuery = this._termsOfPaymentRepository.GetAll()
                    .Where(x => !x.IsDeleted)
                    .WhereIf(!input.SearchString.IsNullOrWhiteSpace(), item => item.Name.ToLower().Contains(input.SearchString.ToLower()));

                var totalCount = await TermsOfPaymentQuery.CountAsync();
                var items = await TermsOfPaymentQuery.OrderBy(input.Sorting).PageBy(input).ToListAsync();

                return new PagedResultDto<TermsOfPaymentDto>(
                totalCount,
                ObjectMapper.Map<List<TermsOfPaymentDto>>(items));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        [UnitOfWork]
        public async Task<ResponseDto> InsertOrUpdateTermsOfPaymentIntoDB(TermsOfPaymentInputDto input)
        {
            try
            {
                Guid TermsOfPaymentId = Guid.Empty;
                var TermsOfPaymentItem = await this._termsOfPaymentRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == input.Name.ToLower().Trim());
                if (TermsOfPaymentItem != null)
                {
                    if (input.Id != TermsOfPaymentItem.Id)
                    {
                        return new ResponseDto
                        {
                            Id = input.Id == Guid.Empty ? null : input.Id,
                            Name = TermsOfPaymentItem.Name,
                            IsExistingDataAlreadyDeleted = TermsOfPaymentItem.IsDeleted,
                            DataMatchFound = true,
                            RestoringItemId = TermsOfPaymentItem.Id
                        };
                    }
                    else
                    {
                        TermsOfPaymentItem.Name = input.Name;
                        TermsOfPaymentItem.Description = input.Description;
                        TermsOfPaymentId = await this._termsOfPaymentRepository.InsertOrUpdateAndGetIdAsync(TermsOfPaymentItem);
                        return new ResponseDto
                        {
                            Id = TermsOfPaymentId,
                            DataMatchFound = false
                        };
                    }

                }
                else
                {
                    var mappedTermsOfPaymentItem = ObjectMapper.Map<TermsOfPayment>(input);
                    TermsOfPaymentId = await this._termsOfPaymentRepository.InsertOrUpdateAndGetIdAsync(mappedTermsOfPaymentItem);
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return new ResponseDto
                    {
                        Id = TermsOfPaymentId,
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
        public async Task<bool> DeleteTermsOfPaymentFromDB(Guid termsOfPaymentId)
        {
            try
            {
                var TermsOfPaymentItem = await this._termsOfPaymentRepository.GetAsync(termsOfPaymentId);

                await this._termsOfPaymentRepository.DeleteAsync(TermsOfPaymentItem);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<TermsOfPaymentDto> GetTermsOfPaymentByIdFromDB(Guid termsOfPaymentId)
        {
            try
            {
                var TermsOfPaymentItem = await this._termsOfPaymentRepository.GetAsync(termsOfPaymentId);

                return ObjectMapper.Map<TermsOfPaymentDto>(TermsOfPaymentItem);

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public async Task<IList<TermsOfPaymentDto>> GetTermsOfPaymentListFromDB()
        {
            try
            {
                var TermsOfPaymentQuery = this._termsOfPaymentRepository.GetAll()
                    .Where(x => !x.IsDeleted);

                return new List<TermsOfPaymentDto>(ObjectMapper.Map<List<TermsOfPaymentDto>>(TermsOfPaymentQuery));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw ex;
            }

        }

        public async Task<bool> RestoreTermsOfPayment(Guid termsOfPaymentId)
        {
            try
            {
                var TermsOfPaymentItem = await this._termsOfPaymentRepository.GetAll().IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == termsOfPaymentId);
                TermsOfPaymentItem.IsDeleted = false;
                TermsOfPaymentItem.DeleterUserId = null;
                TermsOfPaymentItem.DeletionTime = null;
                await this._termsOfPaymentRepository.UpdateAsync(TermsOfPaymentItem);

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
