using AutoMapper;
using Entities;
using Infrastructure.Models.MailModels;
using Infrastructure.Models.RequestModels.Auth;
using Infrastructure.Models.ResponseModels.Auth;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using Utils.Constants.Strings;
using Utils.HelperFuncs;
using Utils.HttpResponseModels;
using Utils.UnitOfWork.Interfaces;

namespace Infrastructure.Services.Implementations
{
    public class AuthService : BaseService, IAuthService
    {
        private const string COMPANY_DOMAIN = "namphuongso.com";

        private readonly DbSet<User> _userDbSet;

        private readonly DbSet<RefreshToken> _refreshTokenDbSet;

        private readonly DbSet<BlackListToken> _blackListTokenDbSet;

        private readonly DbSet<Account> _accountDbSet;

        private readonly string _secretKey;

        private static IMailService _mailService;

        public AuthService(
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IMapper mapper,
            IConfiguration configuration,
            IMailService mailService
        ) : base(unitOfWork, memoryCache, mapper)
        {
            _userDbSet = unitOfWork.Repository<User>();
            _refreshTokenDbSet = unitOfWork.Repository<RefreshToken>();
            _blackListTokenDbSet = unitOfWork.Repository<BlackListToken>();
            _accountDbSet = unitOfWork.Repository<Account>();
            _secretKey = configuration.GetSection("AppSetting:JwtSecretKey").Value ?? "";
            _mailService = mailService;
        }

        #region Sign up
        public async Task<Guid> SignUp(Dictionary<string, dynamic> req)
        {
            var user = new User();

            ObjectMapper.Mapping(req, user);

            var account = new Account
            {
                Email = user.Email,
                // change the default password after the prefix
                Password = MD5Algorithm.HashMd5(Account.FIRST_PASSWORD_PREFIX + "123456"),
                User = user
            };

            _userDbSet.Add(user);
            _accountDbSet.Add(account);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
        #endregion

        #region Sign in
        public async Task<(string, RefreshToken)> SignIn(
            SignInRequest req,
            string ipAddress
        )
        {
            var hashedPassword = MD5Algorithm.HashMd5(req.Password);
            var account = _accountDbSet
                .Include(a => a.User)
                .FirstOrDefault(a =>
                    a.Email == req.Username + "@" + COMPANY_DOMAIN &&
                    !a.IsDeleted &&
                    a.State == Account.ACTIVATED
                );

            if (account == null)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.INVALID_USERNAME_OR_PASSWORD
                );
            }

            if (!MD5Algorithm.VerifyMd5Hash(req.Password, account.Password))
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.INVALID_USERNAME_OR_PASSWORD
                );
            }

            var user = account.User;
            if (user == null)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.INVALID_USERNAME_OR_PASSWORD
                );
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(
                    JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                ),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var accessToken = Jwt.GenerateToken(claims, _secretKey);
            var refreshToken = GenerateRefreshToken(user, ipAddress);

            await _unitOfWork.SaveChangesAsync();

            return (accessToken, refreshToken);
        }
        #endregion

        #region Sign out
        public async Task SignOut(
            string? refreshToken,
            string accessToken,
            string ipAddress
        )
        {
            await RevokeRefreshToken(refreshToken!, ipAddress);
            await AddAccessToken2BlackList(accessToken);
        }
        #endregion

        #region Refresh tokens
        public async Task<(string, RefreshToken)?> Refresh2TokenTypes(
            string? refreshToken,
            string ipAddress
        )
        {
            if (refreshToken == null)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.INVALID_REFRESH_TOKEN
                );
            }

            var token = _refreshTokenDbSet
                .Include(rt => rt.User)
                .FirstOrDefault(
                    rt => rt.Token == refreshToken && rt.RevokedAt == null && rt.Expiry > DateTime.UtcNow
                );

            if (token == null || token.User == null)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.INVALID_REFRESH_TOKEN
                );
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Sid, token.User!.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var newAccessToken = Jwt.GenerateToken(claims, _secretKey);
            var newRefreshToken = GenerateRefreshToken(token.User, ipAddress);

            await _unitOfWork.SaveChangesAsync();

            return (newAccessToken, newRefreshToken);
        }
        #endregion

        #region Change password
        public async Task ChangePassword(
            Guid currentUserId,
            ChangePasswordRequest req
        )
        {
            if (req.NewPassword != req.PasswordConfirm)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.PASSWORD_CONFIRM_IS_NOT_MATCH
                );
            }

            var account = _accountDbSet
                .Include(a => a.User)
                .FirstOrDefault(a => a.User.Id == currentUserId);

            if (account == null)
            {
                throw new AppException(
                    HttpStatusCode.InternalServerError,
                    HttpExceptionMessages.INTERNAL_SERVER_ERROR
                );
            }

            if (!MD5Algorithm.VerifyMd5Hash(req.OldPassword, account.Password))
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.PASSWORD_IS_NOT_CORRECT
                );
            }

            account.Password = MD5Algorithm.HashMd5(req.NewPassword);
            account.PasswordChangedAt = DateTime.UtcNow;
            account.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Reset password

        #region Generate reset password token
        public async Task GenerateResetPasswordToken(GenerateResetPasswordTokenRequest req, string domain)
        {
            var account = _accountDbSet
                .Include(a => a.User)
                .FirstOrDefault(a => a.Email == req.Email);

            if (account == null || account.User == null)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.INVALID_EMAIL
                );
            }

            var resetPasswordToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            account.ResetPasswordToken = resetPasswordToken;
            account.ResetPasswordTokenExpiresAt = DateTime.UtcNow.AddHours(1);

            var mailRequest = new MailRequest
            {
                Receiver = account.Email,
                Subject = "Reset password",
                Body = $"<p>Hi {account.User.FullName},</p>" +
                    $"<p>Please click the link below to reset your password:</p>" +
                    $"<p><a href=\"{domain}/reset-password?token={resetPasswordToken}\">Reset password</a></p>" +
                    $"<p>If you did not request this, please ignore this email.</p>" +
                    $"<p>Thanks,</p>" +
                    $"<p>HRMS Team</p>"
            };

            await _mailService.SendEmailAsync(mailRequest);
            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region Validate reset password token
        public async Task<Account> VerifyResetPasswordToken(string token)
        {
            var account = await _accountDbSet
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ResetPasswordToken == token);

            if (account == null || account.User == null)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.INVALID_RESET_PASSWORD_TOKEN
                );
            }

            if (account.ResetPasswordTokenExpiresAt < DateTime.UtcNow)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.RESET_PASSWORD_TOKEN_EXPIRED
                );
            }

            return account;
        }
        #endregion

        #region Reset password
        public async Task ResetPassword(string resetPasswordToken, ResetPasswordRequest req)
        {
            var account = await VerifyResetPasswordToken(resetPasswordToken);

            if (req.NewPassword != req.PasswordConfirm)
            {
                throw new AppException(
                    HttpStatusCode.BadRequest,
                    HttpExceptionMessages.PASSWORD_CONFIRM_IS_NOT_MATCH
                );
            }

            account.Password = MD5Algorithm.HashMd5(req.NewPassword);
            account.PasswordChangedAt = DateTime.UtcNow;
            account.ResetPasswordToken = null;
            account.ResetPasswordTokenExpiresAt = null;
            account.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #endregion

        #region Helpers
        public bool IsTokenInBlackList(string token)
        {
            var blToken = _blackListTokenDbSet.FirstOrDefault(_blToken => _blToken.Token == token);

            return blToken != null;
        }

        public bool IsPasswordChangedAfterTokenIssued(string token)
        {
            try
            {
                var jwt = new JwtSecurityToken(token);
                var userId = Guid.Parse(jwt.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
                var account = _accountDbSet.FirstOrDefault(a => a.User.Id == userId);

                if (account == null)
                {
                    throw new AppException(
                        HttpStatusCode.InternalServerError,
                        HttpExceptionMessages.INTERNAL_SERVER_ERROR
                    );
                }

                return account.PasswordChangedAt > jwt.IssuedAt;
            }
            catch
            {
                return false;
            }
        }

        public async Task RevokeRefreshToken(string token, string ipAddress)
        {
            var refreshToken = _refreshTokenDbSet.FirstOrDefault(rt => rt.Token == token);

            if (refreshToken == null)
            {
                return;
            }

            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await _unitOfWork.SaveChangesAsync();
        }

        private RefreshToken GenerateRefreshToken(User user, string ipAddress)
        {
            var refreshToken = new RefreshToken()
            {
                User = user,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expiry = DateTime.Now.AddMonths(1),
                CreatedByIp = ipAddress
            };

            _refreshTokenDbSet.Add(refreshToken);

            return refreshToken;
        }

        public async Task AddAccessToken2BlackList(string token)
        {
            var blackListToken = new BlackListToken
            {
                Token = token,
                Expiry = Jwt.GetTokenExpiry(token)
            };

            _blackListTokenDbSet.Add(blackListToken);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<GoogleProfile> GetGoogleProfile(string token)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "https://www.googleapis.com/oauth2/v2/userinfo"
            );
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GoogleProfile>(content);
        }
        #endregion
    }
}
