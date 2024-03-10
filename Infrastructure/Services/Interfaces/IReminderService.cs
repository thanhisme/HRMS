using Infrastructure.Models.RequestModels.Common;
using Infrastructure.Models.ResponseModels.Reminder;

namespace Infrastructure.Services.Interfaces
{
    public interface IReminderService
    {
        public Task<(int, List<ContractAboutToExpireItem>)> GetListContractsAboutToExpire(PaginationFilterRequest req);

        public Task<(int, List<ViolatingEmployeeItem>)> GetListViolatingEmployees(PaginationFilterRequest req);

        public Task<(int, List<EmployeeWithBirthdateThisMonthItem>)> GetListEmployeeWithBirthdateThisMonth(PaginationFilterRequest req);

        public Task<(int, List<EmployeeHaveNotSignedAContractItem>)> GetListEmployeeHaveNotSignedAContract(PaginationFilterRequest req);
    }
}
