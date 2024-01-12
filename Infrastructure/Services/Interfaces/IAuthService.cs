﻿using Entities;
using Infrastructure.Models.RequestModels.Auth;

namespace Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Guid> SignUp(Dictionary<string, dynamic> req);

        public Task<(string, RefreshToken)> SignIn(SignInRequest req, string ipAddress);

        public Task<(string, RefreshToken)?> Refresh2TokenTypes(string? refreshToken, string ipAddress);

        public Task SignOut(string? refreshToken, string accessToken, string ipAddress);

        public Task ChangePassword(Guid currentUserId, ChangePasswordRequest req);

        public bool IsTokenInBlackList(string token);

        public bool IsPasswordChangedAfterTokenIssued(string token);

        public Task RevokeRefreshToken(string token, string ipAddress);

        public Task AddAccessToken2BlackList(string token);
    }
}
