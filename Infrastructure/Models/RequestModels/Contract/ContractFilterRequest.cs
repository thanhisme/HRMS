using Infrastructure.Models.RequestModels.Common;

namespace Infrastructure.Models.RequestModels.Contract
{
    public class ContractFilterRequest : PaginationWithKeywordFilterRequest
    {
        public string StatusCode { get; set; }

        public string DepartmentCode { get; set; }
    }
}
