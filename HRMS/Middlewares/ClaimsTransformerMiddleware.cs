using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Utils.UnitOfWork.Interfaces;

namespace HRMS.Middlewares
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClaimsTransformer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is not ClaimsIdentity claimsIdentity || !claimsIdentity.IsAuthenticated)
            {
                return principal;
            }

            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.Sid);
            if (userIdClaim == null)
            {
                return principal;
            }

            var userId = Guid.Parse(userIdClaim.Value);

            // Get dynamic permissions from the database and add them to the claims
            var permissions = await _unitOfWork.Repository<UserRole>()
                .Where(ur => ur.User.Id == userId)
                .Select(ur => ur.PermissionCodes)
                .ToListAsync();

            foreach (var permission in permissions)
            {
                foreach (var permissionCode in permission.Split(','))
                {
                    var permissionClaimType = $"Permission:{permissionCode}";

                    if (!claimsIdentity.HasClaim(c => c.Type == permissionClaimType && c.Value == "true"))
                    {
                        claimsIdentity.AddClaim(new Claim(permissionClaimType, "true"));
                    }
                }
            }

            return principal;
        }
    }

    public class ClaimsTransformerMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsTransformerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork)
        {
            var transformer = new ClaimsTransformer(unitOfWork);
            var transformedPrincipal = await transformer.TransformAsync(context.User);

            context.User = transformedPrincipal;

            await _next(context);
        }
    }
}
