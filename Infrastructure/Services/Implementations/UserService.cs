using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.User;
using Infrastructure.Models.ResponseModels.User;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using Utils.Constants.Strings;
using Utils.HelperFuncs;
using Utils.HttpResponseModels;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly DbSet<User> _userRepository;

        public UserService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _userRepository = unitOfWork.Repository<User>();
        }

        #region Get list employee
        public async Task<(int, List<UserResponse>)> GetMany(UserFilterRequest req)
        {
            var workingInfos = await _unitOfWork
               .Repository<WorkingInfo>()
               .Include(w => w.Department)
               .Include(w => w.Position)
               .Include(w => w.Employee)
               .Where(w => !w.IsDeleted &&
                   w.Status == WorkingInfo.WorkingInfoStatus.COMFIRMED &&
                   (req.DepartmentCode == null || w.Department.Code == req.DepartmentCode) &&
                   (req.PositionCode == null || w.Position.Code == req.PositionCode)
               )
               .Join(
                    _unitOfWork.Repository<Account>(),
                         w => w.EmployeeId,
                         a => a.User.Id,
                         (w, a) => new { WorkingInfo = w, Account = a }
                )
               .Where(i => !i.Account.IsDeleted && i.Account.State == Account.ACTIVATED)
               .Select(i => i.WorkingInfo)
               .GroupBy(w => w.EmployeeId)
               .Select(w => w.OrderByDescending(w => w.ValidFrom).FirstOrDefault())
               .ToListAsync();

            var result = workingInfos
                .Where(w => w != null &&
                            (StringHelper.DoesStringContainKeyword(w.Employee.FullName, req.Keyword) || w.Employee.EmployeeCode.Contains(req.Keyword))
                )
                .Select(w => new UserResponse
                {
                    Id = w.Employee.Id,
                    FullName = w.Employee.FullName,
                    EmployeeCode = w.Employee.EmployeeCode,
                    Department = w.Department.Name,
                    Position = w.Position.Name,
                    DepartmentId = w.Department.Id
                })
                .ToList();

            return (result.Count, result.Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToList());
        }
        #endregion

        #region Get employee profile
        public async Task<UserResponse> GetEmployeeProfile(string employeeCode)
        {
            var employee = await _unitOfWork
                .Repository<User>()
                .Include(e => e.WorkingInfos)
                .ThenInclude(w => w.Department)
                .Include(e => e.WorkingInfos)
                .ThenInclude(w => w.Position)
                .Where(e => e.EmployeeCode == employeeCode)
                .FirstOrDefaultAsync()
                ?? throw new AppException(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND);

            return new UserResponse
            {
                Id = employee.Id,
                FullName = employee.FullName,
                EmployeeCode = employee.EmployeeCode,
            };
        }
        #endregion

        #region Get current user profile
        public async Task<UserResponse> GetProfile(Guid userId)
        {
            var profile = await _userRepository
                .Include(u => u.WorkingInfos)
                .ThenInclude(w => w.Department)
                .Include(u => u.WorkingInfos)
                .ThenInclude(w => w.Position)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync()
                ?? throw new AppException(HttpStatusCode.NotFound, HttpExceptionMessages.NOT_FOUND);

            return new UserResponse
            {
                Id = profile.Id,
                FullName = profile.FullName,
                EmployeeCode = profile.EmployeeCode,
                Avatar = profile.Avatar
            };
        }
        #endregion

        #region Get statistics employee (pie chart - group by department)
        public async Task<List<PieChartStatisticsEmployeeByDepartmentItem>> PieChartStatisticsEmployeeByDepartment()
        {
            Random random = new();
            int min = 0;
            int max = 100;
            await _userRepository.FirstOrDefaultAsync();

            return new List<PieChartStatisticsEmployeeByDepartmentItem>()
            {
                new PieChartStatisticsEmployeeByDepartmentItem()
                {
                    DepartmentName = "Bộ phận quản lí",
                    TotalCount = random.Next(min, max)
                },
                new PieChartStatisticsEmployeeByDepartmentItem()
                {
                    DepartmentName = "Bộ phận Backend",
                    TotalCount = random.Next(min, max)
                },
                new PieChartStatisticsEmployeeByDepartmentItem()
                {
                    DepartmentName = "Bộ phận Frontend",
                    TotalCount = random.Next(min, max)
                },
                new PieChartStatisticsEmployeeByDepartmentItem()
                {
                    DepartmentName = "Bộ phận Tester",
                    TotalCount = random.Next(min, max)
                },
                new PieChartStatisticsEmployeeByDepartmentItem()
                {
                    DepartmentName = "Bộ phận Mobile",
                    TotalCount = random.Next(min, max)
                },
                new PieChartStatisticsEmployeeByDepartmentItem()
                {
                    DepartmentName = "Bộ phận BA",
                    TotalCount = random.Next(min, max)
                },
                new PieChartStatisticsEmployeeByDepartmentItem()
                {
                    DepartmentName = "Bộ phận Lễ Tân",
                    TotalCount = random.Next(min, max)
                },
            };
        }
        #endregion
    }
}
