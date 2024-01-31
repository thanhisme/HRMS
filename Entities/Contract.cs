using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Contract : BaseEntity
    {
        public enum ContractType
        {
            FullTime,
            PartTime,
            Internship,
            Probation,
            Other
        }

        public enum ContractStatus
        {
            Draft,
            Pending,
            Approved,
            Rejected,
            Expired,
            Renewed,
            Terminated
        }

        public Guid PartnerAId { get; set; } = Guid.Empty;

        public Guid PartnerBId { get; set; } = Guid.Empty;

        [ForeignKey("PartnerAId")]
        public virtual User PartnerA { get; set; } = new User();

        [ForeignKey("PartnerBId")]
        public virtual User PartnerB { get; set; } = new User();

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; } = new Department();

        [ForeignKey("PositionId")]
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

        public DateTime ValidFrom { get; set; } = DateTime.UtcNow;

        public DateTime ValidTo { get; set; } = DateTime.UtcNow;

        public string Html { get; set; } = string.Empty;

        public bool CanRenew { get; set; } = false;

        public int RenewTimes { get; set; } = 0;

        public DateTime RenewalNoticeDate => ValidTo.AddDays(-7); // 1 month

        public string DigitalSignature { get; set; } = string.Empty;

        public string SignatoryName { get; set; } = string.Empty;

        public string SignatoryEmail { get; set; } = string.Empty;

        public DateTime? SignedDate { get; set; } = null;

        public ContractType Type { get; set; } = ContractType.FullTime;

        public ContractStatus Status { get; set; } = ContractStatus.Draft;
    }
}
