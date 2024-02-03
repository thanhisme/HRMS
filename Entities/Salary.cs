namespace Entities
{
    public class Salary : BaseEntity
    {
        public virtual User Employee { get; set; } = new User();

        public double SenioritySalary { get; set; } = 0;

        public double BaseSalary { get; set; } = 0;

        public double OvertimeSalary { get; set; } = 0;

        public double Bonus { get; set; } = 0;

        public double Fine { get; set; } = 0;

        public double AdvancedMoney { get; set; } = 0;

        public double HealthInsurance { get; set; } = 0;

        public double SocialInsurance { get; set; } = 0;

        public double UnemploymentInsurance { get; set; } = 0;

        public double PersonalIncomeTax { get; set; } = 0;

        public double HousingAllowance { get; set; } = 0;

        public double LunchAllowance { get; set; } = 0;

        public double FuelAllowance { get; set; } = 0;

        public double MaternityAllowance { get; set; } = 0;

        public double OtherAllowance { get; set; } = 0;

        public string Note { get; set; } = string.Empty;

        public DateTime Start { get; set; } = DateTime.UtcNow;

        public DateTime End { get; set; } = DateTime.UtcNow;

        public double TotalSalary { get; set; } = 0;

        public double ActualSalary { get; set; } = 0;

        public DateTime? StatusChangedDate { get; set; } = null;

        public string? ReasonForDecision { get; set; } = null;

        public User? DecisionMaker { get; set; } = null;
    }
}
