using AutoMapper;
using Entities;
using Infrastructure.Models.ResponseModels.User;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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

        public User? GetById(Guid id)
        {
            return _userRepository.FirstOrDefault(u => u.Id == id);
        }

        public List<UserResponse> GetMany()
        {
            var users = _userRepository.Where(u => !u.IsDeleted).ToList();

            return users.Select(user => new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
            }).ToList();
        }
    }
}
