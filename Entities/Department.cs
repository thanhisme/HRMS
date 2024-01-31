namespace Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Branch { get; set; } = string.Empty;

        public virtual List<User> Members { get; set; } = new List<User>();

        public virtual List<Policy> Policies { get; set; } = new List<Policy>();
    }
}
