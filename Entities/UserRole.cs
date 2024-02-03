namespace Entities
{
    public class UserRole
    {
        public int Id { get; set; }

        public virtual User User { get; set; } = new User();

        public virtual Role Role { get; set; } = new Role();

        // This is a string of comma-separated permission codes that are associated with the user
        public string PermissionCodes { get; set; } = string.Empty;
    }
}
