using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Utils.Constants.Strings;
using Utils.HttpResponseModels;

namespace HRMS.Middlewares
{
    public class ProtectedMiddleware
    {
        private readonly RequestDelegate _next;

        public ProtectedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Replace("Bearer ", "");
            var authService = context.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
            var endpoint = context.GetEndpoint();
            var isAllowAnonymous = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
            var isValidToken = true;
            var message = "";

            if (authService != null)
            {
                if (authService.IsTokenInBlackList(token ?? ""))
                {
                    isValidToken = false;
                    message = HttpExceptionMessages.TOKEN_IN_BLACKLIST;
                }
                else if (authService.IsPasswordChangedAfterTokenIssued(token ?? ""))
                {
                    string refreshToken = context.Request.Cookies["refreshToken"] ?? "";

                    await authService.RevokeRefreshToken(refreshToken, context.Connection.RemoteIpAddress!.ToString());
                    await authService.AddAccessToken2BlackList(token ?? "");

                    isValidToken = false;
                    message = HttpExceptionMessages.PASSWORD_HAS_CHANGED;
                }
            }

            if (!isValidToken && !isAllowAnonymous)
            {
                throw new AppException(HttpStatusCode.Unauthorized, message);
            }

            if (!isValidToken)
            {
                context.Request.Headers["Authorization"] = "";
            }

            await _next(context);
        }
    }
}
