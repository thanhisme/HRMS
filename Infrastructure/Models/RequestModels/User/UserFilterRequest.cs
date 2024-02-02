using Infrastructure.Models.RequestModels.Common;

namespace Infrastructure.Models.RequestModels.User
{
    public class UserFilterRequest : PaginationWithKeywordFilterRequest
    {
        public string? DepartmentCode { get; set; } = null;

        public string? PositionCode { get; set; } = null;
    }
}
