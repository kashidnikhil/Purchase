﻿using Abp.Application.Services.Dto;
using MyTraining1101Demo.Purchase.Items.Dto.ItemMaster;
using System;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Purchase.Items
{
    public interface IItemAppService
    {
        Task<PagedResultDto<ItemMasterListDto>> GetItems(ItemMasterSearchDto input);

        //Task<Guid> InsertOrUpdateItem(ItemMasterInputDto input);

        Task<bool> DeleteItemMasterData(Guid itemId);
    }
}