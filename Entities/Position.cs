namespace Entities
{
    public class Position : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Requirements { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public int? MinSalary { get; set; } = null;

        public int? MaxSalary { get; set; } = null;

        public virtual List<User> Members { get; set; } = new List<User>();

        public virtual List<Policy> Policies { get; set; } = new List<Policy>();
    }
}
