using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.User;
using Infrastructure.Models.ResponseModels.User;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Utils.HelperFuncs;
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
    }
}
