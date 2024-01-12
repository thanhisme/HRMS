using Entities;
using Infrastructure.Models.RequestModels.Auth;
using Infrastructure.Models.ResponseModels.Auth;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utils.Annotations.Authorization;
using Utils.HelperFuncs;
using Utils.HttpResponseModels;

namespace HRMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [AnonymousOnly]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<HttpResponse<Guid>>> SignUp(Dictionary<string, dynamic> req)
        {
            var id = await _authService.SignUp(req);

            return CreatedResponse(id);
        }

        [HttpPost]
        [AllowAnonymous]
        [AnonymousOnly]
        public async Task<ActionResult<HttpResponse<TokensResponse>>> SignIn(SignInRequest req)
        {
            var (accessToken, refreshToken) = await _authService.SignIn(req, GetIpAddress());
            var userAgent = GetUserAgent();
            var response = HandleResponseBasedOnDevice(userAgent, accessToken, refreshToken);

            return SuccessResponse(response);
        }

        [HttpGet]
        [AllowAnonymous]
        [AnonymousOnly]
        public async Task<ActionResult<HttpResponse<TokensResponse>>> RefreshTokens()
        {
            var refreshTokensResult = await _authService.Refresh2TokenTypes(
                Request.Cookies["refreshToken"],
                GetIpAddress()
            );

            var (accessToken, refreshToken) = refreshTokensResult.Value;
            var userAgent = GetUserAgent();
            var response = HandleResponseBasedOnDevice(userAgent, accessToken, refreshToken);

            return SuccessResponse(response);
        }

        [HttpGet]
        [Authorize]
        public new async Task<ActionResult<HttpResponse<bool>>> SignOut()
        {
            string refreshToken = Request.Cookies["refreshToken"];
            string bearerToken = Request.Headers["Authorization"]!;
            string accessToken = bearerToken["Bearer ".Length..];

            await _authService.SignOut(refreshToken, accessToken, accessToken);

            return SuccessResponse(true);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<HttpResponse<bool>>> ChangePassword(ChangePasswordRequest req)
        {
            await _authService.ChangePassword(Guid.Parse(User.FindFirst(ClaimTypes.Sid).Value), req);

            return SuccessResponse(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [AnonymousOnly]
        public async Task<ActionResult<HttpResponse<bool>>> GenerateResetPasswordToken([FromBody] GenerateResetPasswordTokenRequest req)
        {
            var domain = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            await _authService.GenerateResetPasswordToken(req, domain);

            return SuccessResponse(true);
        }

        [HttpGet]
        [AllowAnonymous]
        [AnonymousOnly]
        public async Task<ActionResult<HttpResponse<bool>>> VerifyResetPasswordToken([FromQuery] string token)
        {
            var account = await _authService.VerifyResetPasswordToken(token);

            return SuccessResponse(account != null);
        }

        [HttpPost]
        [AllowAnonymous]
        [AnonymousOnly]
        public async Task<ActionResult<HttpResponse<bool>>> ResetPassword([FromQuery] string token, ResetPasswordRequest req)
        {
            await _authService.ResetPassword(token, req);

            return SuccessResponse(true);
        }

        #region Helpers
        private string GetIpAddress()
        {
            return HttpContext.Connection.RemoteIpAddress!.ToString();
        }

        private string GetUserAgent()
        {
            return Request.Headers["User-Agent"].ToString();
        }

        private static void SetRefreshToken2Cookie(
                IResponseCookies cookies,
                RefreshToken refreshToken
            )
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expiry,
                Secure = true
            };

            cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }

        private TokensResponse HandleResponseBasedOnDevice(
                string agent,
                string accessToken,
                RefreshToken refreshToken
        )
        {
            var response = new TokensResponse()
            {
                AccessToken = accessToken
            };

            if (DeviceDetection.IsFromMobileDevice(agent) ||
                DeviceDetection.IsFromTabletDevice(agent)
            )
            {
                response.RefreshToken = refreshToken.Token;
            }
            else
            {
                SetRefreshToken2Cookie(Response.Cookies, refreshToken);
            }

            return response;
        }
        #endregion
    }
}
