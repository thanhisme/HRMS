namespace Infrastructure.Models.RequestModels.Auth
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;

        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
