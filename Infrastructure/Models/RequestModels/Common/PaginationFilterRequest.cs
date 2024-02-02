﻿namespace Infrastructure.Models.RequestModels.Common
{
    public class PaginationFilterRequest
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
