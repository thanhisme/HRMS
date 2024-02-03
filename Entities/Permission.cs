namespace Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public virtual List<Role> Roles { get; set; } = new List<Role>();
    }
}
