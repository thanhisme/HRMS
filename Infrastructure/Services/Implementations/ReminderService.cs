using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Common;
using Infrastructure.Models.ResponseModels.Reminder;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class ReminderService : BaseService, IReminderService
    {
        private readonly DbSet<User> _userRepository;

        public ReminderService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _userRepository = unitOfWork.Repository<User>();
        }

        #region Reminder: The employee's contract is about to expire
        public async Task<(int, List<ContractAboutToExpireItem>)> GetListContractsAboutToExpire(PaginationFilterRequest req)
        {
            Random random = new();
            int min = 0;
            int max = 100;
            await _userRepository.FirstOrDefaultAsync();

            var result = new List<ContractAboutToExpireItem> {
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                   EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
                new ContractAboutToExpireItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "TV" + random.Next( min, max ),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Duration = random.Next( 1, 36 ),
                    Expiry = DateTime.Today.AddHours( random.Next(12, 100) )
                },
            };

            return (result.Count, result.Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToList());
        }
        #endregion

        #region Reminder: List of violating employees
        public async Task<(int, List<ViolatingEmployeeItem>)> GetListViolatingEmployees(PaginationFilterRequest req)
        {
            Random random = new();
            int min = 0;
            int max = 100;
            await _userRepository.FirstOrDefaultAsync();

            var result = new List<ViolatingEmployeeItem> {
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Cảnh cáo"
                },
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Cảnh cáo"
                },
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Cảnh cáo"
                },
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Cảnh cáo"
                },
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Cảnh cáo"
                },
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Cảnh cáo"
                },
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                   EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Phạt tiền"
                },
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Phạt tiền"
                },
                new ViolatingEmployeeItem()
                {
                    Id = Guid.NewGuid(),
                    EmployeeCode =  "NP001",
                    EmployeeName = "Nguyễn Hoài Thanh",
                    Reason = "Đi trễ",
                    NoOfRecidivism = random.Next( 1, 36 ),
                    Time = DateTime.Today.AddHours( random.Next(12, 100) ),
                    HandlingMeasures = "Phạt tiền"
                },
            };

            return (result.Count, result.Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToList());
        }
        #endregion

        #region Reminder: List of employees with birthdate this month
        public async Task<(int, List<EmployeeWithBirthdateThisMonthItem>)> GetListEmployeeWithBirthdateThisMonth(PaginationFilterRequest req)
        {
            Random random = new();
            int min = 0;
            int max = 100;
            await _userRepository.FirstOrDefaultAsync();

            var result = new List<EmployeeWithBirthdateThisMonthItem> {
                new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
                new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
                new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
                new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
                new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên  " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
               new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên  " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
               new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên  " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
                new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên  " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
                new EmployeeWithBirthdateThisMonthItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                    DateOfBirth = DateTime.Today.AddDays( random.Next(12, 100) ),
                },
            };

            return (result.Count, result.Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToList());
        }
        #endregion

        #region Reminder: List of employees have not signed a contract
        public async Task<(int, List<EmployeeHaveNotSignedAContractItem>)> GetListEmployeeHaveNotSignedAContract(PaginationFilterRequest req)
        {
            Random random = new();
            int min = 0;
            int max = 100;
            await _userRepository.FirstOrDefaultAsync();

            var result = new List<EmployeeHaveNotSignedAContractItem> {
                new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
                new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
                new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
                new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
                new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
              new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
               new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
                new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
                new EmployeeHaveNotSignedAContractItem()
                {
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Avatar = "https://lh3.googleusercontent.com/a/ACg8ocKAA8sayFXATU1rOjp8aUhlYRUlv1kSQDja4sE6xuRVPs0=s96-c",
                },
            };

            return (result.Count, result.Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToList());
        }
        #endregion
    }
}
