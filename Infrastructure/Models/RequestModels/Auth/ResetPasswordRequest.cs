namespace Infrastructure.Models.RequestModels.Auth
{
    public class ResetPasswordRequest
    {
        public string ResetToken { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;

        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
