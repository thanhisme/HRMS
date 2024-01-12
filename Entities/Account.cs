using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

[Table("Accounts")]
[Index(nameof(Email), IsUnique = true, Name = "Username")]
public class Account : BaseEntity
{
    public const string FIRST_PASSWORD_PREFIX = "NP";
    public static Dictionary<string, string> ACCOUNT_STATES = new()
    {
        { LOCKED, "Locked" },
        { ACTIVATED, "Activated" }
    };
    public const string LOCKED = "locked";
    public const string ACTIVATED = "activated";

    public virtual User User { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public DateTime PasswordChangedAt { get; set; } = DateTime.UtcNow;

    public string? ResetPasswordToken { get; set; } = null;

    public string State { get; set; } = ACTIVATED;
}