namespace Infrastructure.Models.RequestModels.Auth
{
    public class GenerateResetPasswordTokenRequest
    {
        public string Email { get; set; } = string.Empty;
    }
}
