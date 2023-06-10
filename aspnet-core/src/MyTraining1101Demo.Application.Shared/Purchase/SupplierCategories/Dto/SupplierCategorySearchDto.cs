﻿namespace MyTraining1101Demo.Purchase.SupplierCategories.Dto
{
    using Abp.Runtime.Validation;
    using MyTraining1101Demo.Dto;
    public class SupplierCategorySearchDto : PagedAndSortedInputDto, IShouldNormalize
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
