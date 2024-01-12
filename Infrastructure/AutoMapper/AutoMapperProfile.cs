using AutoMapper;
using Entities;
using Infrastructure.Models.RequestModels.Assignment;
using Infrastructure.Models.RequestModels.Auth;
using Infrastructure.Models.RequestModels.Permission;
using Infrastructure.Models.RequestModels.PermissionGroup;
using Infrastructure.Models.RequestModels.Position;

namespace Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        private static readonly Func<object, object, object, bool> _skipNullProps =
            (_, __, srcMember) => srcMember != null;

        public AutoMapperProfile()
        {
            CreateMap<SignUpRequest, User>().ForAllMembers(opts => opts.Condition(_skipNullProps));
        }
    }
}
