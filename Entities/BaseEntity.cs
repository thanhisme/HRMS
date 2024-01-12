namespace Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public Guid CreatorId { get; set; }

        public string Creator { get; set; } = string.Empty;

        public Guid ModifierId { get; set; }

        public string Modifier { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}
