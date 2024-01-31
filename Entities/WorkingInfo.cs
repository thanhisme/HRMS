namespace Entities
{
    public class WorkingInfo : BaseEntity
    {
        public virtual User Employee { get; set; } = new User();

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

        public DateTime ValidFrom { get; set; } = DateTime.UtcNow;
    }
}
