namespace Entities
{
    public class WorkingInfo : BaseEntity
    {
        public enum WorkingInfoStatus
        {
            COMFIRMED,
            REJECTED,
            PENDING,
        }

        public virtual User Employee { get; set; } = new User();

        public Guid EmployeeId { get; set; } = Guid.Empty;

        public virtual Department Department { get; set; } = new Department();

        public virtual Position Position { get; set; } = new Position();

        public string Branch { get; set; } = string.Empty;

        public double Seniority { get; set; } = 0;

        public double BaseSalary { get; set; } = 0;

        public double HealthInsurance { get; set; } = 0;

        public double SocialInsurance { get; set; } = 0;

        public double UnemploymentInsurance { get; set; } = 0;

        public double HousingAllowance { get; set; } = 0;

        public double LunchAllowance { get; set; } = 0;

        public double FuelAllowance { get; set; } = 0;

        public double MaternityAllowance { get; set; } = 0;

        public double OtherAllowance { get; set; } = 0;

        public string Note { get; set; } = string.Empty;

        public DateTime? StatusChangedDate { get; set; } = null;

        public string? ReasonForDecision { get; set; } = null;

        public User? DecisionMaker { get; set; } = null;

        public WorkingInfoStatus Status { get; set; } = WorkingInfoStatus.PENDING;

        public DateTime ValidFrom { get; set; } = DateTime.UtcNow;
    }
}
