﻿using Abp.Configuration;
using Abp.Runtime.Validation;
using MyTraining1101Demo.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTraining1101Demo.Purchase.Models.Dto
{
    public class ModelSearchDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string SearchString { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "name";
            }
        }
    }
}
