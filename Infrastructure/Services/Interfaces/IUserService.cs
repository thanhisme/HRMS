using Infrastructure.Models.RequestModels.User;
using Infrastructure.Models.ResponseModels.User;

namespace Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        public Task<(int, List<UserResponse>)> GetMany(UserFilterRequest req);
    }
}
