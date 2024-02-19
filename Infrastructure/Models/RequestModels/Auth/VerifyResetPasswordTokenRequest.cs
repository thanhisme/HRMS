namespace Infrastructure.Models.RequestModels.Auth
{
    public class VerifyResetPasswordTokenRequest
    {
        public string Token { get; set; } = string.Empty;
    }
}
