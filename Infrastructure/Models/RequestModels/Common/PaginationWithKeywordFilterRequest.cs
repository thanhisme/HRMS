﻿namespace Infrastructure.Models.RequestModels.Common
{
    public class PaginationWithKeywordFilterRequest
    {
        public int Page { get; set; } = 1;


        public int PageSize { get; set; } = 10;


        public string Keyword { get; set; } = string.Empty;
    }
}
