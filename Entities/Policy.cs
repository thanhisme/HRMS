using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Policy : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Policy? Parent { get; set; } = null;

        public virtual List<Policy> SubPolicies { get; set; } = new List<Policy>();
    }
}
