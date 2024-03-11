namespace Infrastructure.Models.ResponseModels.Contract
{
    public class ContractDetail
    {
        public Guid ContractId { get; set; }

        public string ContractCode { get; set; }

        public string ContractType { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeCode { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public string Status { get; set; }

        public DateTime SignedAt { get; set; }

        public int Duration { get; set; }

        public string SignerName { get; set; }

        public DateTime ValidFrom { get; set; }

        public string Note { get; set; }

        public List<string> Attachments { get; set; } = new();

        public int RenewalTimes { get; set; }

        public int BaseSalary { get; set; }

        public int HousingAllowance { get; set; }

        public int FuelAllowance { get; set; }

        public int MaternityAllowance { get; set; }

        public int HealthInsurance { get; set; }

        public int SocialInsurance { get; set; }

        public int UnemploymentInsurance { get; set; }
    }
}
