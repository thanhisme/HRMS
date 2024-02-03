using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class RewardFineHistory : BaseEntity
    {
        public enum RewardFineType
        {
            Reward,
            Fine
        }

        public enum RewardFineStatus
        {
            Pending,
            Approved,
            Rejected
        }

        public enum Level
        {
            Department,
            Position,
            Employee
        }

        public enum Form
        {
            Monetary,
            NonMonetary
        }

        public RewardFineType Type { get; set; } = RewardFineType.Reward;

        [ForeignKey("EmployeeId")]
        public virtual User? Employee { get; set; } = null;

        [ForeignKey("PositionId")]
        public virtual Department? Department { get; set; } = null;

        public string Description { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public List<Policy> Policies { get; set; } = new List<Policy>();

        public DateTime? StatusChangedDate { get; set; } = null;

        public string? ReasonForDecision { get; set; } = null;

        [ForeignKey("DecisionMakerId")]
        public User? DecisionMaker { get; set; } = null;

        public RewardFineStatus Status { get; set; } = RewardFineStatus.Pending;

        public double Amount { get; set; } = 0;

        public Level LevelOfRewardFine { get; set; } = Level.Employee;

        public Form FormOfRewardFine { get; set; } = Form.Monetary;
    }
}
