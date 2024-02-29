using Infrastructure.Models.CommonModels;
using Infrastructure.Models.RequestModels.User;
using Infrastructure.Models.ResponseModels.User;

namespace Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        public Task<(int, List<UserResponse>)> GetMany(UserFilterRequest req);

        public Task<UserResponse> GetEmployeeProfile(string employeeCode);

        public Task<UserResponse> GetProfile(Guid userId);

        public Task<List<PieChartStatisticsEmployeeByDepartmentItem>> PieChartStatisticsEmployeeByDepartment();

        public Task<List<PieChartStatisticsEmployeeByTypeItem>> PieChartStatisticsEmployeeByType();

        public Task<List<LineChartStatisticsPersonnelChanges>> LineChartStatisticsPersonnelChanges(string year);

        public Task<List<BarChartStatisticsTotalEmployeeItem>> BarChartStatisticsEmployee(string year);

        public Task<NumberStatistics> NumberStatisticsTotalEmployee(DateFilterWithPrev req);

        //public Task<(int, List<UserResponse>)> GetLockedAccount(UserFilterRequest req);

        //public Task LockAccount(string employeeCode);

        //public Task UnlockAccount(string employeeCode);

        //public Task UpdateUser(UserUpdateRequest req);

        //public Task DeleteUser(string employeeCode);

        //public Task GetCareerPath(string employeeCode);

        //public Task GetCurrentSalaryDetail(string employeeCode);

        ///public Task GetSalaryHistory(string employeeCode);
    }
}
