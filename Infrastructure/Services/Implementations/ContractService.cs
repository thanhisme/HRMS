using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Contract;
using Infrastructure.Models.ResponseModels.Contract;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class ContractService : BaseService, IContractService
    {
        private readonly DbSet<User> _userRepository;

        public ContractService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _userRepository = unitOfWork.Repository<User>();
        }

        #region Get list contracts
        public async Task<(int, List<ContractItem>)> GetListContracts(ContractFilterRequest req)
        {
            Random random = new();
            int min = 0;
            int max = 100;
            await _userRepository.FirstOrDefaultAsync();

            var result = new List<ContractItem> {
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
                new ContractItem()
                {
                    Id = Guid.NewGuid(),
                    Code = "00" + random.Next(1,10),
                    ContractType = "Thử việc",
                    EmployeeName = "Nhân viên " + random.Next(1,10),
                    Position = "Nhân viên",
                    Duration = random.Next(2,10),
                    SignedAt = DateTime.Today.AddMonths(-1 * random.Next(1,20)),
                },
            };

            return (result.Count, result.Skip((req.Page - 1) * req.PageSize).Take(req.PageSize).ToList());
        }
        #endregion

        #region Get contract details
        public async Task<ContractDetail> GetContractDetail(string code)
        {
            await _userRepository.ToListAsync();

            return new ContractDetail()
            {
                ContractId = Guid.NewGuid(),
                ContractCode = code,
                ContractType = "Thử việc",
                EmployeeName = "Nguyễn Hoài Thanh",
                EmployeeCode = "NP001",
                Company = "Công ty trách nhiệm hữu hạn công nghệ số Nam Phương",
                Position = "Nhân viên",
                Status = "Đã duyệt",
                SignedAt = DateTime.Now,
                Duration = 12,
                SignerName = "Phan Thị Ngọc Yến",
                ValidFrom = DateTime.Now,
                Note = "",
                Attachments = new List<string>() { "https://google.com", "https://google.com", "https://google.com" },
                RenewalTimes = 0,
                BaseSalary = 10000000,
                HousingAllowance = 0,
                FuelAllowance = 600000,
                MaternityAllowance = 0,
                HealthInsurance = 20000000,
                SocialInsurance = 20000000,
                UnemploymentInsurance = 0,
            };
        }
        #endregion

        #region Create contract
        public async Task<Guid> CreateContract(Dictionary<string, dynamic> req)
        {
            await _userRepository.ToListAsync();
            return Guid.NewGuid();
        }
        #endregion

        #region Create multiple contracts
        public async Task<bool> CreateMultipleContracts(Dictionary<string, dynamic> req)
        {
            await _userRepository.ToListAsync();
            return true;
        }
        #endregion

        #region Update contract
        public async Task<Guid> UpdateContract(string code, Dictionary<string, dynamic> req)
        {
            await _userRepository.ToListAsync();
            return Guid.NewGuid();
        }
        #endregion

        #region Delete contract
        public async Task<Guid> DeleteContract(string code)
        {
            await _userRepository.ToListAsync();
            return Guid.NewGuid();
        }
        #endregion

        #region Extend contract
        public async Task<Guid> ExtendContract(string code)
        {
            await _userRepository.ToListAsync();
            return Guid.NewGuid();
        }
        #endregion

        #region Extend multiple contracts
        public async Task<bool> ExtendMultipleContracts(List<string> codes)
        {
            await _userRepository.ToListAsync();
            return true;
        }
        #endregion

        #region Cancel contract
        public async Task<Guid> CancelContract(CancelContractRequest req)
        {
            await _userRepository.ToListAsync();
            return Guid.NewGuid();
        }
        #endregion
    }
}
